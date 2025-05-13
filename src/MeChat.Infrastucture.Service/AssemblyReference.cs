using System.Reflection;

namespace MeChat.Infrastucture.Service;
public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
