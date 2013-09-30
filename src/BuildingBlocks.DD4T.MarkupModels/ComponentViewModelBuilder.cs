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
            var obj = (T)Build(source.Fields, source.MetadataFields, destinationModel, source);
            if (obj is IComponentBase)
            {
                obj.GetType().GetProperty("ComponentId").SetValue(obj, source.Id, null);
            }
            return obj;
        }        

        public static object Build(IComponent source, Type targetType)
        {
            var destinationModel = Activator.CreateInstance(targetType);
            var obj = Build(source.Fields, source.MetadataFields, destinationModel, source);
            if (obj is IComponentBase)
            {
                obj.GetType().GetProperty("ComponentId").SetValue(obj, source.Id, null);
            }
            return obj;
        }

        internal static T Build<T>(IFieldSet source) where T : new()
        {
            var destinationModel = new T();
            return (T)Build(source, null, destinationModel, null);
        }

        internal static object Build(IFieldSet source, Type targetType)
        {
            var destinationModel = Activator.CreateInstance(targetType);
            return Build(source, null, destinationModel, null);
        }

        private static object Build(IFieldSet sourceFields, IFieldSet sourceMetadataFields, object destinationModel, IComponent source)
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

                    var fields = tridionAttribute.IsMetadata ? sourceMetadataFields : sourceFields;

                    if (tridionAttribute.SchemaFieldName == String.Empty && source != null)
                    {
                        //Multimedia object
                        TrySetProperty<object>(property, destinationModel, source, tridionAttribute);
                    }
                    else if (tridionAttribute.IsMultiValue)
                    {
                        TrySetMultiValueProperty<bool>(property, destinationModel, fields, tridionAttribute);
                        TrySetMultiValueProperty<DateTime>(property, destinationModel, fields, tridionAttribute);
                        TrySetMultiValueProperty<string>(property, destinationModel, fields, tridionAttribute);
                        TrySetMultiValueProperty<object>(property, destinationModel, fields, tridionAttribute);                            
                    }
                    else
                    {
                        TrySetProperty<bool>(property, destinationModel, fields, tridionAttribute);
                        TrySetProperty<DateTime>(property, destinationModel, fields, tridionAttribute);
                        TrySetProperty<string>(property, destinationModel, fields, tridionAttribute);
                        TrySetProperty<object>(property, destinationModel, fields, tridionAttribute);
                    }
                }
            }

            return destinationModel;
        }

        private static void TrySetProperty<T>(PropertyInfo property, object destinationModel, IFieldSet source, ITridionViewModelPropertyAttribute tridionAttribute)
        {
            T value;
            if (source!=null && TryResolveViewModelProperty(source, tridionAttribute, out value))
            {
                property.SetValue(destinationModel, value, null);
            }
        }

        private static void TrySetProperty<T>(PropertyInfo property, object destinationModel, IComponent source, ITridionViewModelPropertyAttribute tridionAttribute)
        {
            T value;
            if (source != null && TryResolveViewModelProperty(source, tridionAttribute, out value))
            {
                property.SetValue(destinationModel, value, null);
            }
        }

        private static void TrySetMultiValueProperty<T>(PropertyInfo property, object destinationModel, IFieldSet source, ITridionViewModelPropertyAttribute tridionAttribute)
        {
            IEnumerable<T> value;
            if (source != null && TryResolveMultiValueViewModelProperty(source, tridionAttribute, out value))
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

        private static bool TryResolveViewModelProperty<T>(IFieldSet source, ITridionViewModelPropertyAttribute attribute, out T value)
        {
            value = default(T);
            var objAttribute = attribute as ITridionViewModelPropertyAttribute<T>;
            if (objAttribute != null)
            {
                 value = objAttribute.GetValue(source);
            }
            return objAttribute != null;
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

        private static bool TryResolveMultiValueViewModelProperty<T>(IFieldSet source, ITridionViewModelPropertyAttribute attribute, out IEnumerable<T> values)
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