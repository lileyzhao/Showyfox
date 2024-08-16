namespace Showyfx.Core;

/// <summary>
/// 用户表
/// </summary>
[SfxTable]
public class SfxUser : EntityBaseMore
{
    /// <summary>
    /// 登录账号
    /// </summary>
    [Required, StringLength(32)]
    public string? Account { get; set; }

    /// <summary>
    /// 登录密码
    /// </summary>
    [Required, StringLength(512), JsonIgnore]
    public string? Password { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    [StringLength(16)]
    public string? Phone { get; set; }

    /// <summary>
    /// 电子邮件
    /// </summary>
    [StringLength(128)]
    public string? Email { get; set; }

    /// <summary>
    /// 账号类型
    /// </summary>
    public AccountTypeEnum AccountType { get; set; } = AccountTypeEnum.User;

    /// <summary>
    /// 真实姓名
    /// </summary>
    [StringLength(32)]
    public string? RealName { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    [StringLength(32)]
    public string? NickName { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    [StringLength(512)]
    public string? Avatar { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public GenderEnum Sex { get; set; } = GenderEnum.Male;

    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// 民族
    /// </summary>
    [StringLength(32)]
    public string? Ethnic { get; set; }

    /// <summary>
    /// 体重，用户的体重。
    /// </summary>
    public double Weight { get; set; }

    /// <summary>
    /// 身高，用户的身高。
    /// </summary>
    public double Height { get; set; }

    /// <summary>
    /// 血型，用户的血型。
    /// </summary>
    public string? BloodType { get; set; }

    /// <summary>
    /// 证件类型
    /// </summary>
    public CardTypeEnum CardType { get; set; }

    /// <summary>
    /// 证件号码
    /// </summary>
    [StringLength(32)]
    public string? CardNo { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    [StringLength(256)]
    public string? Address { get; set; }

    /// <summary>
    /// 邮政编码，用户所在地区的邮政编码。
    /// </summary>
    [StringLength(16)]
    public string? PostalCode { get; set; }

    /// <summary>
    /// 文化程度
    /// </summary>
    public CultureLevelEnum CultureLevel { get; set; }

    /// <summary>
    /// 婚姻状态，用户的婚姻状况。
    /// </summary>
    public string? MaritalStatus { get; set; }

    /// <summary>
    /// 政治面貌
    /// </summary>
    [StringLength(16)]
    public string? PoliticalAffiliation { get; set; }

    /// <summary>
    /// 毕业院校
    /// </summary>
    [StringLength(128)]
    public string? College { get; set; }

    /// <summary>
    /// 专业，用户的专业领域。
    /// </summary>
    public string? Major { get; set; }

    /// <summary>
    /// 个人简介
    /// </summary>
    [StringLength(512)]
    public string? Introduction { get; set; }

    /// <summary>
    /// 紧急联系人
    /// </summary>
    [StringLength(32)]
    public string? EmergencyContact { get; set; }

    /// <summary>
    /// 紧急联系人电话
    /// </summary>
    [StringLength(16)]
    public string? EmergencyPhone { get; set; }

    /// <summary>
    /// 紧急联系人地址
    /// </summary>
    [StringLength(256)]
    public string? EmergencyAddress { get; set; }

    /// <summary>
    /// 直属机构Id
    /// </summary>
    public long OrgId { get; set; }

    ///// <summary>
    ///// 直属机构
    ///// </summary>
    //[Navigate(NavigateType.OneToOne, nameof(OrgId))]
    //public SfxOrg SfxOrg { get; set; }

    /// <summary>
    /// 直属主管Id
    /// </summary>
    public long? ManagerUserId { get; set; }

    ///// <summary>
    ///// 直属主管
    ///// </summary>
    //[Navigate(NavigateType.OneToOne, nameof(ManagerUserId))]
    //public SfxUser ManagerUser { get; set; }

    /// <summary>
    /// 公司名称
    /// </summary>
    public string? CompanyName { get; set; }

    /// <summary>
    /// 职位Id
    /// </summary>
    public long PosId { get; set; }

    ///// <summary>
    ///// 职位
    ///// </summary>
    //[Navigate(NavigateType.OneToOne, nameof(PosId))]
    //public SfxPos SfxPos { get; set; }

    /// <summary>
    /// 工作年限，用户的工作经验年限。
    /// </summary>
    public int YearsOfExperience { get; set; }

    /// <summary>
    /// 收入，用户的年收入水平。
    /// </summary>
    public decimal Income { get; set; }

    /// <summary>
    /// 工号
    /// </summary>
    [StringLength(32)]
    public string? EmployeeId { get; set; }

    /// <summary>
    /// 职级
    /// </summary>
    [StringLength(32)]
    public string? PosLevel { get; set; }

    /// <summary>
    /// 职称
    /// </summary>
    [StringLength(32)]
    public string? PosTitle { get; set; }

    /// <summary>
    /// 入职日期
    /// </summary>
    public DateTime? JoinDate { get; set; }

    /// <summary>
    /// 擅长领域
    /// </summary>
    [StringLength(64)]
    public string? Expertise { get; set; }

    /// <summary>
    /// 最新登录Ip
    /// </summary>
    [StringLength(256)]
    public string? LastLoginIp { get; set; }

    /// <summary>
    /// 最新登录地点
    /// </summary>
    [StringLength(128)]
    public string? LastLoginAddress { get; set; }

    /// <summary>
    /// 最新登录时间
    /// </summary>
    public DateTime? LastLoginTime { get; set; }

    /// <summary>
    /// 最新登录设备
    /// </summary>
    [StringLength(128)]
    public string? LastLoginDevice { get; set; }

    /// <summary>
    /// 电子签名
    /// </summary>
    [StringLength(512)]
    public string? Signature { get; set; }

    /// <summary>
    /// 启停状态
    /// </summary>
    public EnabledStatusEnum Status { get; set; } = EnabledStatusEnum.Enabled;
}