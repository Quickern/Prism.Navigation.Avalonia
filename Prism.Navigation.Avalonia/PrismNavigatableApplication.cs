using Avalonia;
using DryIoc;

namespace Prism;

public abstract class PrismNavigatableApplication : PrismNavigatableApplicationBase
{
    protected virtual Rules CreateContainerRules() => DryIocContainerExtension.DefaultRules;

    protected override IContainerExtension CreateContainerExtension()
    {
        return new DryIocContainerExtension(CreateContainerRules());
    }

    protected override void RegisterFrameworkExceptionTypes()
    {
        ExceptionExtensions.RegisterFrameworkExceptionType(typeof (ContainerException));
    }
}