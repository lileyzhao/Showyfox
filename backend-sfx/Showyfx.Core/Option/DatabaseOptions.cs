namespace Showyfx.Core;

/// <summary>
/// 数据库配置选项
/// </summary>
public sealed class DatabaseOptions : IConfigurableOptions<DatabaseOptions>
{
    /// <summary>
    /// 启用SQL打印
    /// </summary>
    public bool EnablePrintSql { get; set; } = true;

    /// <summary>
    /// 数据库集合
    /// </summary>
    public List<DbConnConfig>? DbConnConfigs { get; set; }

    /// <summary>
    /// 选项后期配置
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    public void PostConfigure(DatabaseOptions options, IConfiguration configuration)
    {
        if (options.DbConnConfigs == null || options.DbConnConfigs.Count == 0)
            options.DbConnConfigs = new List<DbConnConfig>
            {
                new() { DbType = DbType.Sqlite, ConnectionString = "DataSource=./Showyfx.db" }
            };

        foreach (var dbConfig in options.DbConnConfigs)
        {
            if (dbConfig.ConfigId != null && !dbConfig.ConfigId.ToString().IsNullOrWhiteSpace()) continue;
            dbConfig.ConfigId = DbConst.SfxConfigId;
            break;
        }
    }
}

/// <summary>
/// 数据库连接配置
/// </summary>
/// <remarks>基于<see cref="SqlSugar.ConnectionConfig">SqlSugar.ConnectionConfig</see>扩展</remarks>
public sealed class DbConnConfig : ConnectionConfig
{
    private string _tablePrefix = string.Empty;

    /// <summary>
    /// 是否初始化数据库，默认为 true
    /// </summary>
    /// <remarks>值为true且数据库或表不存在时，则初始化数据库和表</remarks>
    public bool IsInitDb { get; set; } = true;

    /// <summary>
    /// 是否更新数据库，默认为 false
    /// </summary>
    /// <remarks>值为true时，则每次启动更新表，否则只创建不修改不删除表和列</remarks>
    public bool IsUpdateDb { get; set; }

    /// <summary>
    /// 是否初始化种子数据，默认为 true
    /// </summary>
    /// <remarks>值为true且主键判断不存在时，则插入种子数据</remarks>
    public bool IsInitSeed { get; set; } = true;

    /// <summary>
    /// 是否更新种子数据，默认为 false
    /// </summary>
    /// <remarks>值为true，则每次启动更新种子数据</remarks>
    public bool IsUpdateSeed { get; set; }

    /// <summary>
    /// 启用蛇形命名规则(下划线命名规则)，对表名及列名同时生效，默认为 true
    /// </summary>
    public bool EnableSnakeCase { get; set; } = true;

    /// <summary>
    /// 启用差异日志，默认为 false
    /// </summary>
    public bool EnableDiffLog { get; set; }

    /// <summary>
    /// 业务表名前缀，启用 <see cref="EnableSnakeCase"/> 时，如果非下划线 _ 结尾，则自动添加下划线
    /// </summary>
    public string TablePrefix
    {
        get => _tablePrefix;
        set => _tablePrefix = EnableSnakeCase && !value.EndsWith('_') ? value.ToLower() + "_" : value;
    }

    /// <summary>
    /// 默认字符串类型长度
    /// </summary>
    public int DefaultStringLength { get; set; } = 256;
}