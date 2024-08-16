namespace Showyfx.Core;

/// <summary>
/// 账号类型枚举
/// </summary>
[Description("账号类型枚举")]
public enum AccountTypeEnum
{
    /// <summary>
    /// 超级管理员，系统所有权限
    /// </summary>
    [Description("超级管理员")] SuperAdmin = 999,

    /// <summary>
    /// 系统管理员，所属租户内的所有权限
    /// </summary>
    [Description("系统管理员")] SystemAdmin = 888,

    /// <summary>
    /// 普通用户
    /// </summary>
    [Description("普通用户")] User = 777,

    /// <summary>
    /// 会员用户
    /// </summary>
    [Description("会员用户")] Member = 666
}