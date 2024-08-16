namespace Showyfx.Core;

/// <summary>
/// 证件类型枚举
/// </summary>
public enum CardTypeEnum
{
    /// <summary>
    /// 身份证
    /// </summary>
    [Description("身份证")] IdCard,

    /// <summary>
    /// 护照
    /// </summary>
    [Description("护照")] Passport,

    /// <summary>
    /// 港澳通行证
    /// </summary>
    [Description("港澳通行证")] HkMacauTravelPermit,

    /// <summary>
    /// 台湾通行证
    /// </summary>
    [Description("台湾通行证")] TaiwanTravelPermit,

    /// <summary>
    /// 军官证
    /// </summary>
    [Description("军官证")] OfficerCard,

    /// <summary>
    /// 出生证
    /// </summary>
    [Description("出生证")] BirthCertificate,

    /// <summary>
    /// 外国人永久居留身份证
    /// </summary>
    [Description("外国人永久居留身份证")] PrCertificate,

    /// <summary>
    /// 其他证件
    /// </summary>
    [Description("其他证件")] OtherDocument
}