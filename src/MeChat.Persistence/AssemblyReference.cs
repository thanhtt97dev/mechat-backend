using System.Reflection;

namespace MeChat.Persistence;

public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
