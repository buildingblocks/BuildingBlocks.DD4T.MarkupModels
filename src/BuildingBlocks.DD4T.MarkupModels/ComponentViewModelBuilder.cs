using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels
{
    public static class ComponentViewModelBuilder
    {
        public static T Build<T>(IComponent source) where T : new()
        {
            var destinationModel = new T();
            return (T)Build(source, destinationModel);
        }

        public static object Build(IComponent source, Type targetType)
        {
            var destinationModel = Activator.CreateInstance(targetType);
            return Build(source, destinationModel);
        }

        private static object Build(IComponent source, object destinationModel)
        {
            var type = destinationModel.GetType();
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(true);
                foreach (var attr in attributes)
                {
                    var tridionAttribute = attr as ITridionViewModelPropertyAttribute;
                    if (tridionAttribute == null)
                    {
                        continue;
                    }

                    if(tridionAttribute.IsMultiValue)
                    {
                        TrySetMultiValueProperty<bool>(property, destinationModel, source, tridionAttribute);
                        TrySetMultiValueProperty<DateTime>(property, destinationModel, source, tridionAttribute);
                        TrySetMultiValueProperty<string>(property, destinationModel, source, tridionAttribute);
                        TrySetMultiValueProperty<object>(property, destinationModel, source, tridionAttribute);
                    }
                    else
                    {
                        TrySetProperty<bool>(property, destinationModel, source, tridionAttribute);
                        TrySetProperty<DateTime>(property, destinationModel, source, tridionAttribute);
                        TrySetProperty<string>(property, destinationModel, source, tridionAttribute);
                        TrySetProperty<object>(property, destinationModel, source, tridionAttribute);
                    }
                }
            }

            return destinationModel;
        }

        private static void TrySetProperty<T>(PropertyInfo property, object destinationModel, IComponent source, ITridionViewModelPropertyAttribute tridionAttribute)
        {
            T value;
            if (TryResolveViewModelProperty(source, tridionAttribute, out value))
            {
                property.SetValue(destinationModel, value, null);
            }
        }

        private static void TrySetMultiValueProperty<T>(PropertyInfo property, object destinationModel, IComponent source, ITridionViewModelPropertyAttribute tridionAttribute)
        {
            IEnumerable<T> value;
            if (TryResolveMultiValueViewModelProperty(source, tridionAttribute, out value))
            {
                if (tridionAttribute is BaseNestedTridionViewModelPropertyAttribute)
                {
                    var nestedAttr = (BaseNestedTridionViewModelPropertyAttribute)tridionAttribute;
                    var caster = typeof(System.Linq.Enumerable)
                        .GetMethod("Cast", new[] { typeof(IEnumerable) })
                        .MakeGenericMethod(nestedAttr.TargetType);
                    var castedValue = caster.Invoke(null, new object[] { value });
                    property.SetValue(destinationModel, castedValue, null);
                }
                else
                {
                    property.SetValue(destinationModel, value, null);    
                }
            }
        }

        private static bool TryResolveViewModelProperty<T>(IComponent source, ITridionViewModelPropertyAttribute attribute, out T value)
        {
            value = default(T);
            var objAttribute = attribute as ITridionViewModelPropertyAttribute<T>;
            if (objAttribute != null)
            {
                 value = objAttribute.GetValue(source);
            }
            return objAttribute != null;
        }

        private static bool TryResolveMultiValueViewModelProperty<T>(IComponent source, ITridionViewModelPropertyAttribute attribute, out IEnumerable<T> values)
        {
            values = null;
            var objAttribute = attribute as ITridionViewModelPropertyAttribute<T>;
            if (objAttribute != null)
            {
                values = objAttribute.GetMultiValue(source);
            }
            return objAttribute != null;
        }
    }
}