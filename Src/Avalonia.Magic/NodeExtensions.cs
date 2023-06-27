using magic.node;
using magic.node.extensions;

namespace Avalonia.Magic;

public static class NodeExtensions
{
    public static T Get<T>(this Node node, string name)
    {
        return node.Children.FirstOrDefault(_ => _.Name == name).GetEx<T>();
    }
    
    public static T Get<T>(this Node node, int index)
    {
        return node.Children.Skip(index).FirstOrDefault().GetEx<T>();
    }
}