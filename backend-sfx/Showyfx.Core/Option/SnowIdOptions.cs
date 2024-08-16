// ReSharper disable HeuristicUnreachableCode

#pragma warning disable CS0162 // 检测到无法访问的代码

namespace Showyfx.Core;

/// <summary>
/// 雪花Id配置选项
/// </summary>
public class SnowIdOptions : IdGeneratorOptions, IConfigurableOptions<SnowIdOptions>
{
    private ushort _workerId;

    /// <summary>
    /// 基点时间
    /// </summary>
    /// <remarks>
    /// 基点时间，修改该值会影响生成的ID，不建议有生产数据后修改。该值的用生成ID时的系统时间与基点时间的差值（毫秒数）作为生成ID的时间戳。
    /// </remarks>
    public override DateTime BaseTime { get; set; }

    /// <summary>
    /// 工作节点Id，分布式架构内唯一
    /// </summary>
    public override ushort WorkerId
    {
        get => _workerId;
        // Debug环境下工作节点Id为 0,以防止与线上Id冲突
        set => _workerId = SfxConst.Environment == EnvironmentEnum.Debug ? (ushort)0 : value;
    }

    /// <summary>
    /// 选项后期配置
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    public void PostConfigure(SnowIdOptions options, IConfiguration configuration)
    {
        if (options.BaseTime <= new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc))
            options.BaseTime = SfxConst.SfxTime;
        options.BaseTime = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
    }
}