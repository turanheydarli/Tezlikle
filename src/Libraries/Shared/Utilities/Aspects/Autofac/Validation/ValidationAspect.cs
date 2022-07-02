using Castle.DynamicProxy;
using FluentValidation;
using Shared.Common.Messages;
using Shared.CrossCuttingConcerns.Validation;
using Shared.Utilities.Interceptors;

namespace Shared.Utilities.Aspects.Autofac.Validation;

public class ValidationAspect : MethodInterception
{
    private Type _validatorType;
    public ValidationAspect(Type validatorType)
    {
        if (!typeof(IValidator).IsAssignableFrom(validatorType))
        {
            throw new Exception(Messages.WrongValidationType);
        }

        _validatorType = validatorType;
    }
    protected override void OnBefore(IInvocation invocation)
    {
        var validator = (IValidator)Activator.CreateInstance(_validatorType);
        if (_validatorType.BaseType != null)
        {
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(type => type.GetType() == entityType);

            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}