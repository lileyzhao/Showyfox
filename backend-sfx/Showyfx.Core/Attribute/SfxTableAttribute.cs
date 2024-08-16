namespace Showyfx.Core;

/// <summary>
/// SFX框架核心表特性，不包含此特性则为业务表
/// </summary>
[SuppressSniffer]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class SfxTableAttribute : Attribute;