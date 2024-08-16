namespace Showyfx.Core;

/// <summary>
/// 全局应用类
/// </summary>
public static class SfxApp
{
    /// <summary>
    /// 获取配置
    /// </summary>
    /// <typeparam name="TOptions">强类型选项类</typeparam>
    /// <param name="path">配置中对应的Key</param>
    /// <param name="loadPostConfigure"></param>
    /// <returns>TOptions</returns>
    public static TOptions GetOrCreateConfig<TOptions>(string path, bool loadPostConfigure = false) where TOptions : new()
    {
        return App.GetConfig<TOptions>(path, loadPostConfigure) ?? new TOptions();
    }

    /// <summary>
    /// 获取配置
    /// </summary>
    /// <typeparam name="TOptions">强类型选项类</typeparam>
    /// <param name="loadPostConfigure"></param>
    /// <returns>TOptions</returns>
    public static TOptions GetOrCreateConfig<TOptions>(bool loadPostConfigure = false) where TOptions : new()
    {
        return App.GetConfig<TOptions>(typeof(TOptions).Name.Replace("Options", ""), loadPostConfigure) ?? new TOptions();
    }
}