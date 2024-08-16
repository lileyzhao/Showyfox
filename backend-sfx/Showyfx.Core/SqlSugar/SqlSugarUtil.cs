using System.Collections;

namespace Showyfx.Core;

/// <summary>
/// SqlSugar工具类
/// </summary>
public static class SqlSugarUtil
{
    /// <summary>
    /// 初始化数据库
    /// </summary>
    /// <param name="db"></param>
    /// <param name="config"></param>
    internal static void InitDatabase(SqlSugarScope db, DbConnConfig config)
    {
        var dbProvider = db.GetConnectionScope(config.ConfigId);

        if (!config.IsInitDb) return;

        // Oracle和个别国产库需不支持该方法，需要手动建库
        if (config.DbType != DbType.Oracle) dbProvider.DbMaintenance.CreateDatabase();

        var effTypes = App.EffectiveTypes
            .Where(t => t is { IsInterface: false, IsAbstract: false, IsClass: true } && t.GetInterface(typeof(IEntity).FullName!) != null)
            .ToList();

        var entityTypes = effTypes.Where(et => !et.IsDefined(typeof(SfxTableAttribute)) && !et.IsDefined(typeof(LogTableAttribute))).ToList();

        // 添加系统库
        if (config.ConfigId.ToString() == DbConst.SfxConfigId)
            entityTypes.AddRange(effTypes.Where(et => et.IsDefined(typeof(SfxTableAttribute))).ToList());

        // 添加日志库
        if (config.ConfigId.ToString() == DbConst.LogConfigId)
            entityTypes.AddRange(effTypes.Where(et => et.IsDefined(typeof(LogTableAttribute))).ToList());

        // 获取所有表，不走缓存
        var tablesInfo = db.DbMaintenance.GetTableInfoList(false);

        foreach (var entityType in entityTypes)
        {
            if (config.IsUpdateDb == false &&
                entityType.IsDefined(typeof(ForceUpdateAttribute)) == false &&
                tablesInfo.Any(t => t.Name.Equals(db.EntityMaintenance.GetTableName(entityType))))
                continue;

            if (entityType.GetCustomAttribute<SplitTableAttribute>() == null)
                dbProvider.CodeFirst.SetStringDefaultLength(config.DefaultStringLength).InitTables(entityType);
            else
                dbProvider.CodeFirst.SetStringDefaultLength(config.DefaultStringLength).SplitTables().InitTables(entityType);
        }

        // 初始化种子数据
        if (!config.IsInitSeed) return;

        // 重新获取所有表，不走缓存
        tablesInfo = db.DbMaintenance.GetTableInfoList(false);

        var seedDataTypes = App.EffectiveTypes
            .Where(u => u is { IsInterface: false, IsAbstract: false, IsClass: true } &&
                        u.GetInterfaces().Any(ifc => ifc is { IsGenericType: true, FullName: not null } && ifc.FullName.StartsWith(typeof(IEntitySeedData).FullName!)))
            .ToList();

        foreach (var seedType in seedDataTypes)
        {
            var entityType = seedType.GetInterfaces().First().GetGenericArguments().First();
            if (!tablesInfo.Any(t => t.Name.Equals(db.EntityMaintenance.GetTableName(entityType))))
                continue;

            if (config.IsUpdateSeed == false &&
                seedType.IsDefined(typeof(ForceUpdateAttribute)) == false &&
                db.Queryable<SfxUser>().AS(db.EntityMaintenance.GetTableName(entityType)).ClearFilter().Count() > 0)
                continue;

            var instance = Activator.CreateInstance(seedType);
            var hasDataMethod = seedType.GetMethod(nameof(IEntitySeedData<SfxUser>.HasData));
            var seedData = ((IEnumerable?)hasDataMethod?.Invoke(instance, null))?.Cast<object?>();
            if (seedData == null) continue;

            var entityInfo = dbProvider.EntityMaintenance.GetEntityInfo(entityType);
            if (entityInfo.Columns.Any(u => u.IsPrimarykey))
            {
                // 按主键进行批量增加和更新
                var storage = dbProvider.StorageableByObject(seedData.ToList()).ToStorage();
                storage.AsInsertable.ExecuteCommand();
                storage.AsUpdateable.ExecuteCommand();
            }
            else
            {
                // 无主键则只进行插入
                if (!dbProvider.Queryable(entityInfo.DbTableName, entityInfo.DbTableName).Any())
                    dbProvider.InsertableByObject(seedData.ToList()).ExecuteCommand();
            }
        }
    }

    /// <summary>
    /// 初始化租户业务数据库
    /// </summary>
    /// <param name="iTenant"></param>
    /// <param name="config"></param>
    public static void InitTenantDatabase(ITenant iTenant, DbConnConfig config)
    {
        // SetDbConfig(config);
        //
        // iTenant.AddConnection(config);
        // var db = iTenant.GetConnectionScope(config.ConfigId);
        // db.DbMaintenance.CreateDatabase();
        //
        // // 获取所有业务表-初始化租户库表结构（排除系统表、日志表、特定库表）
        // var entityTypes = App.EffectiveTypes.Where(u =>
        //     !u.IsInterface && !u.IsAbstract && u.IsClass && u.IsDefined(typeof(SugarTable), false) &&
        //     !u.IsDefined(typeof(SysTableAttribute), false) && !u.IsDefined(typeof(LogTableAttribute), false) &&
        //     !u.IsDefined(typeof(TenantAttribute), false)).ToList();
        // if (!entityTypes.Any()) return;
        //
        // foreach (var entityType in entityTypes)
        // {
        //     var splitTable = entityType.GetCustomAttribute<SplitTableAttribute>();
        //     if (splitTable == null)
        //         db.CodeFirst.InitTables(entityType);
        //     else
        //         db.CodeFirst.SplitTables().InitTables(entityType);
        // }
    }
}