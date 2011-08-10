using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EF.CodeFirst.Common.Domain;
using Simple.Extensions;

namespace Simple.Mvc.Extensions
{
    public static class SelectListItemExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectItems<T>(this IEnumerable<T> items, int selectedItem = -1)
            where T : BaseReferenceEntity
        {
            if (items.IsNullOrEmpty()) return Enumerable.Empty<SelectListItem>();
            
            return items.Select(i => new SelectListItem
                                        {
                                            Text = i.Name,
                                            Value = i.Id.ToString(),
                                            Selected = i.Id.Equals(selectedItem)
                                        });
        }

        public static IEnumerable<SelectListItem> ToSelectItems<T>(this IEnumerable<T> items, Func<T, string> textFunc, Func<T, string> valueFunc)
        {
            if (items.IsNullOrEmpty()) return Enumerable.Empty<SelectListItem>();

            return items.Select(i => new SelectListItem { Text = textFunc(i), Value = valueFunc(i) });
        }
    }
}