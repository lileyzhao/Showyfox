namespace Showyfx.Core;

/// <summary>
/// 字典类型表
/// </summary>
[SfxTable]
public class SfxDictType : EntityBaseMore
{
    /// <summary>
    /// 字典名称
    /// </summary>
    [Required, StringLength(64)]
    public string? Name { get; set; }

    /// <summary>
    /// 字典编码
    /// </summary>
    [Required, StringLength(64)]
    public string? Code { get; set; }

    /// <summary>
    /// 是否枚举，枚举字典是由系统自动生成，不允许编辑
    /// </summary>
    [Required, MaxLength(64)]
    public bool IsEnum { get; set; }

    /// <summary>
    /// 启停状态
    /// </summary>
    [SugarColumn(ColumnDescription = "启停状态")]
    public EnabledStatusEnum Status { get; set; } = EnabledStatusEnum.Enabled;

    /// <summary>
    /// 字典值集合
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(SfxDictData.TypeId))]
    public List<SfxDictData>? RefDictData { get; set; }
}