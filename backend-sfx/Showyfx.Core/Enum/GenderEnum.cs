namespace Showyfx.Core;

/// <summary>
/// 性别枚举
/// </summary>
public enum GenderEnum
{
    /// <summary>
    /// 未知
    /// </summary>
    [Description("未知")] Unknown = 0,

    /// <summary>
    /// 男性
    /// </summary>
    [Description("男性")] Male = 1,

    /// <summary>
    /// 女性
    /// </summary>
    [Description("女性")] Female = 2,

    /// <summary>
    /// 其他
    /// </summary>
    [Description("其他")] Other = 3
}