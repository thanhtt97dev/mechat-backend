using System.Reflection;

namespace MeChat.Infrastucture.DistributedCache;

public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
