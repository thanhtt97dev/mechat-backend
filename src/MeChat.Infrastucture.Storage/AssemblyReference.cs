using System.Reflection;

namespace MeChat.Infrastucture.Storage;
public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
