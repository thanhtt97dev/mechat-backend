using System.Reflection;

namespace MeChat.Infrastucture.Dapper;

public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
