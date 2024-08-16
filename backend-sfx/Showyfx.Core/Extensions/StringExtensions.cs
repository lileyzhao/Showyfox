using System.Diagnostics.CodeAnalysis;

namespace Showyfx.Core;

/// <summary>
/// 字符串扩展
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// 是否非Null且非空字符串
    /// </summary>
    public static bool NotNullOrEmpty([NotNullWhen(true)] this string? value)
    {
        return !value.IsNullOrEmpty();
    }

    /// <summary>
    /// 是否非Null、非空字符串且非纯空白字符
    /// </summary>
    public static bool NotNullOrWhiteSpace([NotNullWhen(true)] this string? value)
    {
        return !value.IsNullOrWhiteSpace();
    }
}