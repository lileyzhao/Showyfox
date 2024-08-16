namespace Showyfx.Core;

/// <summary>
/// 缓存配置选项
/// </summary>
public sealed class CacheOptions : IConfigurableOptions<CacheOptions>
{
    /// <summary>
    /// 缓存类型
    /// </summary>
    [DataValidation(SfxValidationTypes.StrongPassword)]
    public string CacheType { get; set; } = "Memory";

    /// <summary>
    /// 缓存前缀
    /// </summary>
    public string? Prefix { get; set; }

    /// <summary>
    /// Redis缓存
    /// </summary>
    public RedisConfigs? RedisConfig { get; set; }

    /// <summary>
    /// 选项后期配置
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    public void PostConfigure(CacheOptions options, IConfiguration configuration)
    {
        options.Prefix = options.Prefix.IsNullOrWhiteSpace() ? string.Empty : options.Prefix.Trim();
    }

    /// <summary>
    /// Redis配置
    /// </summary>
    public sealed class RedisConfigs : RedisOptions
    {
        /// <summary>
        /// 最大消息大小，默认值 1048576 = 1024 * 1024
        /// </summary>
        public int MaxMessageSize { get; set; } = 1024 * 1024;
    }
}

/// <summary>
/// 集群配置选项
/// </summary>
public sealed class ClusterOptions : IConfigurableOptions
{
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// 服务器标识
    /// </summary>
    public string? ServerId { get; set; }

    /// <summary>
    /// 服务器IP
    /// </summary>
    public string? ServerIp { get; set; }

    /// <summary>
    /// SignalR配置
    /// </summary>
    public ClusterSignalR? SignalR { get; set; }

    /// <summary>
    /// 数据保护key
    /// </summary>
    public string? DataProtectKey { get; set; }

    /// <summary>
    /// 是否哨兵模式
    /// </summary>
    public bool IsSentinel { get; set; }

    /// <summary>
    /// 哨兵配置
    /// </summary>
    public StackExchangeSentinelConfig? SentinelConfig { get; set; }

    /// <summary>
    /// 集群SignalR配置
    /// </summary>
    public sealed class ClusterSignalR
    {
        /// <summary>
        /// Redis连接字符串
        /// </summary>
        public string? RedisConfiguration { get; set; }

        /// <summary>
        /// 缓存前缀
        /// </summary>
        public string? ChannelPrefix { get; set; }
    }


    /// <summary>
    /// 哨兵配置
    /// </summary>
    public sealed class StackExchangeSentinelConfig
    {
        /// <summary>
        /// master名称
        /// </summary>
        public string? ServiceName { get; set; }

        /// <summary>
        /// master访问密码
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// 哨兵访问密码
        /// </summary>
        public string? SentinelPassword { get; set; }

        /// <summary>
        /// 哨兵端口
        /// </summary>
        public List<string>? EndPoints { get; set; }

        /// <summary>
        /// 默认库
        /// </summary>
        public int DefaultDb { get; set; }

        /// <summary>
        /// 主前缀
        /// </summary>
        public string? MainPrefix { get; set; }

        /// <summary>
        /// SignalR前缀
        /// </summary>
        public string? SignalRChannelPrefix { get; set; }
    }
}