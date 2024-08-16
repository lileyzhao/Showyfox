using System.ComponentModel.DataAnnotations;

namespace Showyfx.XConsole;

/// <summary>
/// 
/// </summary>
public sealed class UserEntity
{
    /// <summary>
    /// 账号
    /// </summary>
    [Required, MaxLength(32)]
    public string? Account { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [MaxLength(32)]
    public required string UserName { get; set; }
}