using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SimpleInjector;

namespace MarsRover.API.Helpers
{
    class SimpleInjectorModelValidatorProvider : IModelValidatorProvider
    {
        private readonly Container _container;

        public SimpleInjectorModelValidatorProvider(Container container) =>
            _container = container;

        public void CreateValidators(ModelValidatorProviderContext ctx)
        {
            var validatorType = typeof(ModelValidator<>).MakeGenericType(ctx.ModelMetadata.ModelType);
            var validator = (IModelValidator)_container.GetInstance(validatorType);
            ctx.Results.Add(new ValidatorItem { Validator = validator });
            
        }
    }

    // Adapter that translates calls from IModelValidator into the IValidator<T>
    // application abstraction.
    class ModelValidator<TModel> : IModelValidator
    {
        private readonly IEnumerable<IValidator<TModel>> validators;

        public ModelValidator(IEnumerable<IValidator<TModel>> validators) =>
            this.validators = validators;

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext ctx) =>
            this.Validate((TModel)ctx.Model);

        private IEnumerable<ModelValidationResult> Validate(TModel model) =>
            from validator in this.validators
            from errorMessage in validator.Validate(model)
            select new ModelValidationResult(string.Empty, errorMessage);

        IEnumerable<ModelValidationResult> IModelValidator.Validate(ModelValidationContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}