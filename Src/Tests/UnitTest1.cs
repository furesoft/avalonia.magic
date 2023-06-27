using Avalonia.Magic;
using magic.node.extensions;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void EndsWith_001()
    {
        var lambda = Common.Evaluate(@"
widget:
   element: Button
   content: " + "\"hello world\"");
        
        Assert.True(lambda.Children.Skip(1).First().Get<bool>());
    }

}