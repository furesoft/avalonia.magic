using Avalonia.Data;
using magic.node;
using magic.node.extensions;
using magic.signals.contracts;

namespace Avalonia.Magic.Slots;

[Slot(Name = "binding")]
public class BindingSlot : ISlot
{
    public void Signal(ISignaler signaler, Node input)
    {
         signaler.Signal("eval", input);

        var path = input.GetEx<string>();

        input.Parent.Value = new Binding(path);
    }
}