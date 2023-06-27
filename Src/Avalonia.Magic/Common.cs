using Avalonia.Magic.Slots;
using magic.lambda.eval;
using magic.node;
using magic.node.extensions.hyperlambda;
using magic.signals.contracts;
using magic.signals.services;
using Microsoft.Extensions.DependencyInjection;

namespace Avalonia.Magic;

public static class Common
{
    public static Node Evaluate(string hl)
    {
        new Widget();
        
        var services = Initialize();
        var lambda = HyperlambdaParser.Parse(hl);
        var signaler = services.GetService(typeof(ISignaler)) as ISignaler;

        new Eval().Signal(signaler, lambda);
        
        return lambda;
    }

    public static async Task<Node> EvaluateAsync(string hl)
    {
        var services = Initialize();
        var lambda = HyperlambdaParser.Parse(hl);
        var signaler = services.GetService(typeof(ISignaler)) as ISignaler;
        await signaler.SignalAsync("eval", lambda);
        
        return lambda;
    }

    #region [ -- Private helper methods -- ]

    static IServiceProvider Initialize()
    {
        var services = new ServiceCollection();
        services.AddTransient<ISignaler, Signaler>();
        var types = new SignalsProvider(InstantiateAllTypes<ISlot>(services));
        services.AddTransient<ISignalsProvider>((svc) => types);

        var provider = services.BuildServiceProvider();
        return provider;
    }

    static IEnumerable<Type> InstantiateAllTypes<T>(ServiceCollection services) where T : class
    {
        var type = typeof(T);
        var result = AppDomain.CurrentDomain.GetAssemblies()
            .Where(x => !x.IsDynamic && !x.FullName.StartsWith("Microsoft", StringComparison.InvariantCulture))
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract).ToList();
        
        foreach (var idx in result)
        {
            services.AddTransient(idx);
        }


        return result;
    }

    #endregion
}