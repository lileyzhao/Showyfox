namespace Showyfx.Core;

/// <summary>
/// 系统字典值表种子数据
/// </summary>
public class SfxDictDataSeedData : IEntitySeedData<SfxDictData>
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public IEnumerable<SfxDictData> HasData()
    {
        // @formatter:wrap_lines false
        return new[]
        {
            new SfxDictData { Id = 188_000_000_001, TypeId = 188_000_000_001, Value = "输入框", Code = "Input", OrderNo = 100, Remark = "输入框", Status = EnabledStatusEnum.Enabled, CreateTime = SfxConst.SfxTime },
            new SfxDictData { Id = 188_000_000_002, TypeId = 188_000_000_001, Value = "外键", Code = "fk", OrderNo = 100, Remark = "外键", Status = EnabledStatusEnum.Enabled, CreateTime = SfxConst.SfxTime },
            new SfxDictData { Id = 188_000_000_003, TypeId = 188_000_000_001, Value = "时间选择", Code = "DatePicker", OrderNo = 100, Remark = "时间选择", Status = EnabledStatusEnum.Enabled, CreateTime = SfxConst.SfxTime },
            new SfxDictData { Id = 188_000_000_004, TypeId = 188_000_000_001, Value = "选择器", Code = "Select", OrderNo = 100, Remark = "选择器", Status = EnabledStatusEnum.Enabled, CreateTime = SfxConst.SfxTime },
            new SfxDictData { Id = 188_000_000_005, TypeId = 188_000_000_001, Value = "数字输入框", Code = "InputNumber", OrderNo = 100, Remark = "数字输入框", Status = EnabledStatusEnum.Enabled, CreateTime = SfxConst.SfxTime },
            new SfxDictData { Id = 188_000_000_006, TypeId = 188_000_000_001, Value = "文本域", Code = "InputTextArea", OrderNo = 100, Remark = "文本域", Status = EnabledStatusEnum.Enabled, CreateTime = SfxConst.SfxTime },
            new SfxDictData { Id = 188_000_000_007, TypeId = 188_000_000_001, Value = "上传", Code = "Upload", OrderNo = 100, Remark = "上传", Status = EnabledStatusEnum.Enabled, CreateTime = SfxConst.SfxTime },
            new SfxDictData { Id = 188_000_000_008, TypeId = 188_000_000_001, Value = "树选择", Code = "ApiTreeSelect", OrderNo = 100, Remark = "树选择", Status = EnabledStatusEnum.Enabled, CreateTime = SfxConst.SfxTime },
            new SfxDictData { Id = 188_000_000_009, TypeId = 188_000_000_001, Value = "开关", Code = "Switch", OrderNo = 100, Remark = "开关", Status = EnabledStatusEnum.Enabled, CreateTime = SfxConst.SfxTime },
            new SfxDictData { Id = 188_000_000_010, TypeId = 188_000_000_001, Value = "常量选择器", Code = "ConstSelector", OrderNo = 100, Remark = "常量选择器", Status = EnabledStatusEnum.Enabled, CreateTime = SfxConst.SfxTime },
            new SfxDictData { Id = 188_000_000_011, TypeId = 188_000_000_001, Value = "枚举选择器", Code = "EnumSelector", OrderNo = 100, Remark = "枚举选择器", Status = EnabledStatusEnum.Enabled, CreateTime = SfxConst.SfxTime }
        };
    }
}