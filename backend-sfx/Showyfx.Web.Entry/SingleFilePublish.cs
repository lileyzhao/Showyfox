using System.Reflection;
using Furion;

namespace Showyfx.Web.Entry;

public class SingleFilePublish : ISingleFilePublish
{
    public Assembly[] IncludeAssemblies()
    {
        return Array.Empty<Assembly>();
    }

    public string[] IncludeAssemblyNames()
    {
        return new[]
        {
            "Showyfx.Application",
            "Showyfx.Core",
            "Showyfx.Web.Core"
        };
    }
}