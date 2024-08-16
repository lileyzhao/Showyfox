namespace Showyfx.Core;

/// <summary>
/// 类型对象扩展
/// </summary>
public static class TypeExtensions
{
    // /// <summary>
    // /// 判断类型是否实现某个泛型
    // /// </summary>
    // /// <param name="type">类型</param>
    // /// <param name="generic">泛型类型</param>
    // /// <returns>bool</returns>
    // public static bool HasImplementedRawGeneric(this Type type, Type generic)
    // {
    //     // 检查接口类型
    //     var isTheRawGenericType = type.GetInterfaces().Any(IsTheRawGenericType);
    //     if (isTheRawGenericType) return true;
    //
    //     // 检查类型
    //     while (type != null && type != typeof(object))
    //     {
    //         isTheRawGenericType = IsTheRawGenericType(type);
    //         if (isTheRawGenericType) return true;
    //         type = type.BaseType!;
    //     }
    //
    //     return false;
    //
    //     // 判断逻辑
    //     bool IsTheRawGenericType(Type type) => generic == (type.IsGenericType ? type.GetGenericTypeDefinition() : type);
    // }
}