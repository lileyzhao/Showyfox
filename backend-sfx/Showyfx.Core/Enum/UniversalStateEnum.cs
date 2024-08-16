namespace Showyfx.Core;

/// <summary>
/// 通用状态枚举
/// </summary>
public enum UniversalStateEnum
{
    /// <summary>
    /// 未知状态
    /// </summary>
    [Description("身份证")] Unknown,

    /// <summary>
    /// 活动状态
    /// </summary>
    [Description("活动状态")] Active,

    /// <summary>
    /// 已完成状态
    /// </summary>
    [Description("已完成状态")] Completed,

    /// <summary>
    /// 已取消状态
    /// </summary>
    [Description("已取消状态")] Canceled,

    /// <summary>
    /// 暂停状态
    /// </summary>
    [Description("暂停状态")] Paused,

    /// <summary>
    /// 错误状态
    /// </summary>
    [Description("错误状态")] Error,

    /// <summary>
    /// 禁用状态
    /// </summary>
    [Description("禁用状态")] Disabled,

    /// <summary>
    /// 运行中状态
    /// </summary>
    [Description("运行中状态")] Running,

    /// <summary>
    /// 等待状态
    /// </summary>
    [Description("等待状态")] Waiting,

    /// <summary>
    /// 审核中状态
    /// </summary>
    [Description("审核中状态")] UnderReview,

    /// <summary>
    /// 已过期状态
    /// </summary>
    [Description("Expired")] Expired,

    /// <summary>
    /// 挂起状态
    /// </summary>
    [Description("挂起状态")] Suspended,

    /// <summary>
    /// 草稿状态
    /// </summary>
    [Description("草稿状态")] Draft,

    /// <summary>
    /// 完全状态
    /// </summary>
    [Description("完全状态")] Full
}