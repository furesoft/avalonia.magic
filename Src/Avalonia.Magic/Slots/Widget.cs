using Avalonia.Controls;
using magic.node;
using magic.signals.contracts;

namespace Avalonia.Magic.Slots;

[Slot(Name = "widget")]
public class Widget : ISlot
{
    public void Signal(ISignaler signaler, Node input)
    {
        var element = input.Get<string>("path");

        Control control = null;

        if (element == "Button")
        {
            control = new Button();
        }
        else if (element == "Input")
        {
            control = new TextBox();
        }

        foreach (var child in input.Children)
        {
            if (child.Name == "element")
            {
                continue;
            }

            signaler.Signal("eval", child);
            
            control.GetType().GetProperty(child.Name)?.SetValue(control, child.Value);
        }

        input.Value = control;
    }
}