using System;
using System.Web.Mvc;

namespace Simple.Mvc.ModelBinders
{
    public interface IFilteredModelBinder : IModelBinder
    {
        bool IsMatch(Type modelType);
    }
}