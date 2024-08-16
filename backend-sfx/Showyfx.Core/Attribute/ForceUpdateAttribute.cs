namespace Showyfx.Core;

/// <summary>
/// 强制更新标记特性，可用于标记实体、种子强制更新，即使数据库配置了 IsUpdateDb=false
/// </summary>
[SuppressSniffer]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ForceUpdateAttribute : Attribute;