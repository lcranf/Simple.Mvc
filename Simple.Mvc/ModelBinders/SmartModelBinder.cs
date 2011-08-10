using System.Collections.Generic;
using System.Web.Mvc;

namespace Simple.Mvc.ModelBinders
{
    public class SmartModelBinder : DefaultModelBinder
    {
        private readonly IEnumerable<IFilteredModelBinder> _filteredModelBinders;

        public SmartModelBinder(IEnumerable<IFilteredModelBinder> filteredModelBinders)
        {
            _filteredModelBinders = filteredModelBinders;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            foreach (var modelBinder in _filteredModelBinders)
            {
                if (modelBinder.IsMatch(bindingContext.ModelType))
                {
                    object model = modelBinder.BindModel(controllerContext, bindingContext);

                    //if model binder fails to bind, give base model binder one more chance
                    return model ?? base.BindModel(controllerContext, bindingContext);
                }
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}
