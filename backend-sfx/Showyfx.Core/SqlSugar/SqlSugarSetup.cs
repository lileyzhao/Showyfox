using Spectre.Console;
using System.Reflection;

namespace Showyfx.Core;

/// <summary>
/// 数据库访问注册(SqlSugar库) 
/// </summary>
public static class SqlSugarSetup
{
    /// <inheritdoc cref="SqlSugarSetup"/>
    public static void AddSqlSugar(this IServiceCollection services)
    {
        // 配置雪花Id生成器，并设置为SqlSugar的自定义雪花Id生成器
        var snowIdOptions = SfxApp.GetOrCreateConfig<SnowIdOptions>(true);
        YitIdHelper.SetIdGenerator(snowIdOptions);

        // 设置为SqlSugar的自定义雪花Id生成器
        SnowFlakeSingle.WorkId = snowIdOptions.WorkerId;
        StaticConfig.CustomSnowFlakeFunc = YitIdHelper.NextId;

        // 读取配置文件中的数据库配置
        var dbOptions = SfxApp.GetOrCreateConfig<DatabaseOptions>(true);
        if (dbOptions.DbConnConfigs!.Count == 0) throw new ArgumentException("缺少数据库配置，最少需要配置一个数据库", nameof(DatabaseOptions));

        dbOptions.DbConnConfigs.ForEach(SetEntityAop);

        var sqlSugarScope = new SqlSugarScope(dbOptions.DbConnConfigs.Adapt<List<ConnectionConfig>>(), db =>
        {
            dbOptions.DbConnConfigs.ForEach(config =>
            {
                var dbProvider = db.GetConnectionScope(config.ConfigId);
                SetDbAop(dbProvider, dbOptions);
                // SetDbDiffLog(dbProvider, config);
            });
        });

        services.AddSingleton(sqlSugarScope); // 单例注册
        // services.AddScoped(typeof(SqlSugarRepository<>)); // 仓储注册
        services.AddUnitOfWork<SqlSugarUnitOfWork>(); // 事务与工作单元注册

        // 初始化数据库表结构及种子数据
        dbOptions.DbConnConfigs.ForEach(config =>
        {
            if (SfxConst.Environment == EnvironmentEnum.Debug) SqlSugarUtil.InitDatabase(sqlSugarScope, config);
        });
    }

    /// <summary>
    /// 配置连接属性(AOP)
    /// </summary>
    /// <param name="config">数据库连接配置(SqlSugar)</param>
    private static void SetEntityAop(DbConnConfig config)
    {
        config.InitKeyType = InitKeyType.Attribute;
        config.IsAutoCloseConnection = true;
        config.MoreSettings = new ConnMoreSettings
        {
            IsAutoRemoveDataCache = true,
            IsAutoDeleteQueryFilter = true, // 启用删除查询过滤器
            IsAutoUpdateQueryFilter = true, // 启用更新查询过滤器
            EnableCodeFirstUpdatePrecision = true, // 启用精度修改，有约束的情况下可能失败
            SqlServerCodeFirstNvarchar = true // SqlServer库默认使用Nvarchar
        };
        config.ConfigureExternalServices = new ConfigureExternalServices
        {
            // 处理表
            EntityNameService = (type, entity) =>
            {
                entity.IsCreateTableFiledSort = true; // 启用列排序
                entity.IsDisabledDelete = true; // 禁止删除列

                // TODO:排除DTO

                // 使用 SugarTable 特性设置 TableName 优先级最高，不应用 蛇形命名规则(下划线命名规则)
                if (type.GetCustomAttributes<SugarTable>().FirstOrDefault() is { } sugarTableAttr && sugarTableAttr.TableName.NotNullOrWhiteSpace()) return;

                // 使用 TableAttribute 特性设置 Name 优先级高于 实体类名，并且继续应用 蛇形命名规则(下划线命名规则)
                if (type.GetCustomAttributes<TableAttribute>().FirstOrDefault() is { } tableAttr && tableAttr.Name.NotNullOrWhiteSpace())
                    entity.DbTableName = tableAttr.Name;

                // 蛇形命名规则(下划线命名规则)，CodeFirst实体必须要继承 IEntity 接口或其实现类
                if (config.EnableSnakeCase && type.GetInterface(typeof(IEntity).FullName!) != null)
                    entity.DbTableName = config.TablePrefix + UtilMethods.ToUnderLine(entity.DbTableName);

                // 有表描述，跳过
                if (entity.TableDescription.NotNullOrWhiteSpace()) return;

                // 兼容 Description特性、Display特性、DisplayName特性，优先级从高至低 Description > Display > DisplayName
                if (type.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault() is { } descAttr && descAttr.Description.NotNullOrWhiteSpace())
                    entity.TableDescription = descAttr.Description;
                else if (type.GetCustomAttributes<DisplayAttribute>().FirstOrDefault() is { } displayAttr && displayAttr.Description.NotNullOrWhiteSpace())
                    entity.TableDescription = displayAttr.Description ?? displayAttr.Name;
                else if (type.GetCustomAttributes<DisplayNameAttribute>().FirstOrDefault() is { } disNameAttr && disNameAttr.DisplayName.NotNullOrEmpty())
                    entity.TableDescription = disNameAttr.DisplayName;
            },
            // 处理列
            EntityService = (prop, column) =>
            {
                column.IsNullable = true; // 默认所有列均为可空，后续根据其他配置转为 非空列

                // TODO:排除DTO

                // 如果未通过 SugarColumn.ColumnName 设置列名，则应用蛇形命名规则(下划线命名规则)，否则跳过。（SugarColumn.ColumnName优先级最高）
                if (prop.GetCustomAttributes<SugarColumn>().FirstOrDefault() is { } sugarColumnAttr && sugarColumnAttr.ColumnName.IsNullOrWhiteSpace())
                {
                    // 如果通过 ColumnAttribute 设置列名，则应用 ColumnAttribute.Name 设置的列名，否则应用蛇形命名规则(下划线命名规则)。（优先级次高）
                    if (prop.GetCustomAttributes<ColumnAttribute>().FirstOrDefault() is { } colAttr && colAttr.Name.NotNullOrWhiteSpace())
                        column.DbColumnName = colAttr.Name; // 优先使用特性定义
                    // 应用蛇形命名规则(下划线命名规则)，CodeFirst实体必须要继承 IEntity 接口、继承IEntity的其他接口，或其实现类
                    else if (config.EnableSnakeCase && prop.DeclaringType?.GetInterface(typeof(IEntity).FullName!) != null && !column.IsIgnore)
                        column.DbColumnName = UtilMethods.ToUnderLine(column.DbColumnName); // 驼峰转蛇形命名规则(下划线命名规则)
                }

                // 主键配置兼容 KeyAttribute
                if (prop.GetCustomAttributes<KeyAttribute>().FirstOrDefault() is not null)
                {
                    column.IsPrimarykey = true;
                    column.IsNullable = false;
                }

                // 可空配置兼容 RequireAttribute
                if (prop.GetCustomAttributes<RequiredAttribute>().FirstOrDefault() is not null) column.IsNullable = false;

                // 长度参数兼容 StringLengthAttribute，优先级高于 MaxLengthAttribute
                if (prop.GetCustomAttributes<SugarColumn>().FirstOrDefault() is { Length: <= 0 }
                    && prop.GetCustomAttributes<StringLengthAttribute>().FirstOrDefault() is { MaximumLength: > 0 } stringLengthAttr)
                    column.Length = stringLengthAttr.MaximumLength;
                // 长度参数兼容 MaxLengthAttribute
                else if (prop.GetCustomAttributes<SugarColumn>().FirstOrDefault() is { Length: <= 0 }
                         && prop.GetCustomAttributes<MaxLengthAttribute>().FirstOrDefault() is { Length: > 0 } maxLengthAttr)
                    column.Length = maxLengthAttr.Length;

                // 默认值参数兼容 DefaultValueAttribute，DefaultValueAttribute 设置需遵守 SugarColumn规则
                if (prop.GetCustomAttributes<SugarColumn>().FirstOrDefault() is { DefaultValue: null }
                    && prop.GetCustomAttributes<DefaultValueAttribute>().FirstOrDefault() is { Value: not null } defaultValueAttr)
                    column.DefaultValue = defaultValueAttr.Value.ToString();

                // 有列描述，跳过
                if (column.ColumnDescription.NotNullOrWhiteSpace()) return;

                // 兼容 Description特性、Display特性、DisplayName特性，优先级从高至低 Description > Display > DisplayName
                if (prop.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault() is { } descAttr && descAttr.Description.NotNullOrWhiteSpace())
                    column.ColumnDescription = descAttr.Description;
                else if (prop.GetCustomAttributes<DisplayAttribute>().FirstOrDefault() is { } displayAttr && displayAttr.Description.NotNullOrWhiteSpace())
                    column.ColumnDescription = displayAttr.Description ?? displayAttr.Name;
                else if (prop.GetCustomAttributes<DisplayNameAttribute>().FirstOrDefault() is { } disNameAttr && disNameAttr.DisplayName.NotNullOrEmpty())
                    column.ColumnDescription = disNameAttr.DisplayName;
            },
            // 查询缓存
            DataInfoCacheService = new SqlSugarCache()
        };
    }

    /// <summary>
    /// 配置数据库功能(AOP)
    /// </summary>
    /// <param name="db"></param>
    /// <param name="dbOptions"></param>
    private static void SetDbAop(SqlSugarScopeProvider db, DatabaseOptions dbOptions)
    {
        var config = db.CurrentConnectionConfig;

        // 设置Sql超时时间
        db.Ado.CommandTimeOut = 30;

        if (dbOptions.EnablePrintSql)
        {
            // Sql执行开始(打印SQL语句)
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                var outColor = Color.Default;
                if (sql.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                    outColor = Color.Green3_1;
                else if (sql.StartsWith("UPDATE", StringComparison.OrdinalIgnoreCase) ||
                         sql.StartsWith("INSERT", StringComparison.OrdinalIgnoreCase))
                    outColor = Color.Yellow3_1;
                else if (sql.StartsWith("DELETE", StringComparison.OrdinalIgnoreCase))
                    outColor = Color.OrangeRed1;
                else if (sql.StartsWith("CREATE", StringComparison.OrdinalIgnoreCase) ||
                         sql.StartsWith("ALTER", StringComparison.OrdinalIgnoreCase))
                    outColor = Color.DodgerBlue3;

                AnsiConsole.Write(new Panel($"[{outColor.ToMarkup()}]↓↓↓ 执行SQL {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}[/]")
                {
                    BorderStyle = new Style(outColor)
                });
                AnsiConsole.MarkupLine($"[{outColor.ToMarkup()}] {UtilMethods.GetSqlString(config.DbType, sql, pars)} [/]");

                App.PrintToMiniProfiler("SqlSugar", "Info",
                    sql + "\r\n" +
                    db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
            };
            // Sql执行失败
            db.Aop.OnError = ex =>
            {
                if (ex.Parametres == null) return;
                var pars = db.Utilities.SerializeObject(((SugarParameter[])ex.Parametres).ToDictionary(it => it.ParameterName, it => it.Value));

                var outColor = Color.Red3_1;
                AnsiConsole.Write(new Panel($"[{outColor.ToMarkup()}]↓↓↓ 异常SQL {DateTime.Now:yyyy-MM-dd HH:mm:ss}[/]")
                {
                    BorderStyle = new Style(outColor)
                });
                AnsiConsole.MarkupLine($"[{outColor.ToMarkup()}]{UtilMethods.GetSqlString(config.DbType, ex.Sql, (SugarParameter[])ex.Parametres)}[/]");

                App.PrintToMiniProfiler("SqlSugar", "Error", $"{ex.Message}{Environment.NewLine}{ex.Sql}{pars}{Environment.NewLine}");
            };
            // Sql执行完成
            db.Aop.OnLogExecuted = (sql, pars) =>
            {
                var tableWriter = new Table { ShowRowSeparators = false };
                tableWriter.AddColumns("执行耗时",
                    $"[green]{db.Ado.SqlExecutionTime.TotalMicroseconds}微妙 | {db.Ado.SqlExecutionTime.TotalMilliseconds}毫秒 | {db.Ado.SqlExecutionTime.TotalSeconds}秒 | {db.Ado.SqlExecutionTime} [/]"); // 执行时间
                tableWriter.AddRow("方法名", $"[green]{db.Ado.SqlStackTrace.FirstMethodName}[/]"); // 方法名
                tableWriter.AddRow("SQL语句", $"[green]{UtilMethods.GetSqlString(config.DbType, sql, pars)}[/]");
                tableWriter.AddRow("所在文件名", $"[green underline]{db.Ado.SqlStackTrace.FirstFileName}【行：{db.Ado.SqlStackTrace.FirstLine}】[/]"); // 文件名
                //tableWriter.AddRow("代码行数", $"[green]{db.Ado.SqlStackTrace.FirstLine}[/]"); // 行号
                tableWriter.BorderColor(Color.Yellow3_1);
                AnsiConsole.Write(tableWriter);

                //if (db.Ado.SqlExecutionTime.TotalSeconds > 5) return; // 执行时间超过5秒，执行xxx
            };
        }

        // 数据审计(数据预处理)
        db.Aop.DataExecuting = (oldValue, entityInfo) =>
        {
            if (entityInfo.OperationType == DataFilterType.InsertByObject)
            {
                // 主键(long)没有值时---赋值雪花Id
                if (entityInfo.EntityColumnInfo.IsPrimarykey && entityInfo.EntityColumnInfo.PropertyInfo.PropertyType == typeof(long))
                {
                    var id = entityInfo.EntityColumnInfo.PropertyInfo.GetValue(entityInfo.EntityValue);
                    if (id == null || (long)id == 0) entityInfo.SetValue(YitIdHelper.NextId());
                }

                // 创建时间为空时，赋值当前时间
                if (entityInfo.PropertyName == nameof(EntityBase.CreateTime))
                {
                    var createTime = ((dynamic)entityInfo.EntityValue).CreateTime;
                    if (createTime == null) entityInfo.SetValue(DateTime.Now);
                }

                if (App.User != null)
                    switch (entityInfo.PropertyName)
                    {
                        case nameof(IEntityFieldTenant.TenantId):
                            var tenantId = ((dynamic)entityInfo.EntityValue).TenantId;
                            if (tenantId == null || tenantId == 0) entityInfo.SetValue(App.User.FindFirst(ClaimConst.TenantId)?.Value);
                            break;
                        case nameof(IEntityBase.CreateUserId):
                            var createUserId = ((dynamic)entityInfo.EntityValue).CreateUserId;
                            if (createUserId == null || createUserId == 0) entityInfo.SetValue(App.User.FindFirst(ClaimConst.UserId)?.Value);
                            break;
                        case nameof(IEntityBase.CreateUserName):
                            var createUserName = ((dynamic)entityInfo.EntityValue).CreateUserName;
                            if (string.IsNullOrEmpty(createUserName)) entityInfo.SetValue(App.User.FindFirst(ClaimConst.RealName)?.Value);
                            break;
                        case nameof(IEntityFieldScope.CreateOrgId):
                            var createOrgId = ((dynamic)entityInfo.EntityValue).CreateOrgId;
                            if (createOrgId == null || createOrgId == 0) entityInfo.SetValue(App.User.FindFirst(ClaimConst.OrgId)?.Value);
                            break;
                        case nameof(IEntityFieldScope.CreateOrgName):
                            var createOrgName = ((dynamic)entityInfo.EntityValue).CreateOrgName;
                            if (string.IsNullOrEmpty(createOrgName)) entityInfo.SetValue(App.User.FindFirst(ClaimConst.OrgName)?.Value);
                            break;
                    }
            }

            if (entityInfo.OperationType == DataFilterType.UpdateByObject)
                switch (entityInfo.PropertyName)
                {
                    case nameof(EntityBase.UpdateTime):
                        var updateTime = ((dynamic)entityInfo.EntityValue).UpdateTime;
                        if (updateTime == null) entityInfo.SetValue(DateTime.Now);
                        break;
                    case nameof(EntityBase.UpdateUserId):
                        var updateUserId = ((dynamic)entityInfo.EntityValue).UpdateUserId;
                        if (updateUserId == null || updateUserId == 0) entityInfo.SetValue(App.User?.FindFirst(ClaimConst.UserId)?.Value);
                        break;
                    case nameof(EntityBase.UpdateUserName):
                        var updateUserName = ((dynamic)entityInfo.EntityValue).UpdateUserName;
                        if (string.IsNullOrEmpty(updateUserName)) entityInfo.SetValue(App.User?.FindFirst(ClaimConst.RealName)?.Value);
                        break;
                }
        };

        // 超管时排除各种过滤器
        if (App.User?.FindFirst(ClaimConst.AccountType)?.Value == ((int)AccountTypeEnum.SuperAdmin).ToString())
            return;

        // 配置实体假删除过滤器
        db.QueryFilter.AddTableFilter<IDeletedFilter>(u => u.IsDeleted == false);

        // 配置租户过滤器
        var tenantId = App.User?.FindFirst(ClaimConst.TenantId)?.Value;
        if (!string.IsNullOrWhiteSpace(tenantId))
            db.QueryFilter.AddTableFilter<ITenantFilter>(u => u.TenantId == long.Parse(tenantId));

        // 配置用户机构（数据范围）过滤器
        //SqlSugarFilter.SetOrgEntityFilter(db);

        // 配置自定义过滤器
        //SqlSugarFilter.SetCustomEntityFilter(db);
    }
}