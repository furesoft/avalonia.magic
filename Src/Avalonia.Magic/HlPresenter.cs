using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Avalonia.Magic;

public class HlPresenter : ContentControl
{
    public static readonly StyledProperty<string> SourceProperty = AvaloniaProperty.Register<HlPresenter, string>(nameof(Source));

    public HlPresenter()
    {
        SourceProperty.Changed.Subscribe(SourceChanged);
    }

    private void SourceChanged(AvaloniaPropertyChangedEventArgs<string> obj)
    {
        Evaluate();
    }

    public string Source
    {
        get { return GetValue(SourceProperty); }
        set { SetValue(SourceProperty, value); Evaluate(); }
    }

    private void Evaluate()
    {
        var evaluatedTree = Common.Evaluate(Source);

        Content = evaluatedTree.Children.First().Value;
    }
}