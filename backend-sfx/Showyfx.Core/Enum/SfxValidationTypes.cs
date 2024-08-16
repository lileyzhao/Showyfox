namespace Showyfx.Core;

/// <summary>
/// 自定义验证
/// </summary>
[ValidationType]
public enum SfxValidationTypes
{
    /// <summary>
    /// 强密码类型
    /// </summary>
    [ValidationItemMetadata(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,10}$", "必须须包含大小写字母和数字的组合，不能使用特殊字符，长度在8-10之间")]
    StrongPassword,

    /// <summary>
    /// 以 Sfx 字符串开头，忽略大小写
    /// </summary>
    [ValidationItemMetadata(@"^(Sfx).*", "默认提示：必须以Sfx字符串开头，忽略大小写", RegexOptions.IgnoreCase)]
    StartWithFurString,

    /// <summary>
    /// 缓存类型，Memory、Redis
    /// </summary>
    [ValidationItemMetadata(@"^(Memory|Redis)$", "缓存类型必须为 Memory、Redis", RegexOptions.IgnoreCase)]
    CacheType
}