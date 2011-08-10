using System;
using System.Web.Mvc;
using EF.CodeFirst.Common.Domain;
using EF.CodeFirst.Common.Repository;
using Microsoft.Practices.ServiceLocation;

namespace Simple.Mvc.ModelBinders
{
    public class EntityModelBinder : IFilteredModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (value == null || string.IsNullOrEmpty(value.AttemptedValue)) return null;

            int entityId;

            if (!int.TryParse(value.AttemptedValue, out entityId)) return null;

            Type repositoryType = typeof(IRepository<>).MakeGenericType(bindingContext.ModelType);

            var repository = (IReadOnlyRepository) ServiceLocator.Current.GetInstance(repositoryType);

            return repository.GetById(entityId);
        }

        public bool IsMatch(Type modelType)
        {
            return typeof(IEntity).IsAssignableFrom(modelType);
        }
    }
}