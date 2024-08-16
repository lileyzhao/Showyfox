namespace Showyfx.Core;

/// <summary>
/// SFX框架日志表特性
/// </summary>
[SuppressSniffer]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class LogTableAttribute : Attribute;