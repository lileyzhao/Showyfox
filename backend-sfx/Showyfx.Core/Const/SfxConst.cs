using Microsoft.Extensions.Hosting;

namespace Showyfx.Core;

/// <summary>
/// 系统常量
/// </summary>
public class SfxConst
{
#if DEBUG
    /// <summary>
    /// 程序运行环境
    /// </summary>
    public readonly static EnvironmentEnum Environment = App.WebHostEnvironment.IsDevelopment() ? EnvironmentEnum.Debug : EnvironmentEnum.Release;
#else
    /// <summary>
    /// 程序运行环境
    /// </summary>
    public readonly static EnvironmentEnum Environment = EnvironmentEnum.Release;
#endif

    /// <summary>
    /// 是否单元测试
    /// </summary>
    public const bool IsUnitTest = false;

    /// <summary>
    /// 框架启动时间，用于雪花Id默认基础时间及实体种子默认时间
    /// </summary>
    public static readonly DateTime SfxTime = new(2021, 11, 17, 1, 3, 1, 4, DateTimeKind.Utc);
}