using DD4T.ContentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace BuildingBlocks.DD4T.MarkupModels
{
    internal static class FieldSetMapper
    {
        internal static object Build(IFieldSet fields, object destinationModel, IKeyword source, IPage page = null)
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

                    TrySetAllValues(fields, destinationModel, tridionAttribute, property, page);
                }
            }

            return destinationModel;
        }

        private static void TrySetAllValues(IFieldSet fields, object destinationModel, ITridionViewModelPropertyAttribute tridionAttribute, PropertyInfo property, IPage page = null)
        {
            if (tridionAttribute.IsMultiValue)
            {
                TrySetMultiValueProperty<double>(property, destinationModel, fields, tridionAttribute, page);
                TrySetMultiValueProperty<bool>(property, destinationModel, fields, tridionAttribute, page);
                TrySetMultiValueProperty<DateTime>(property, destinationModel, fields, tridionAttribute, page);
                TrySetMultiValueProperty<string>(property, destinationModel, fields, tridionAttribute, page);
                TrySetMultiValueProperty<object>(property, destinationModel, fields, tridionAttribute, page);
            }
            else
            {
                TrySetProperty<double>(property, destinationModel, fields, tridionAttribute, page);
                TrySetProperty<bool>(property, destinationModel, fields, tridionAttribute, page);
                TrySetProperty<DateTime>(property, destinationModel, fields, tridionAttribute, page);
                TrySetProperty<string>(property, destinationModel, fields, tridionAttribute, page);
                TrySetProperty<object>(property, destinationModel, fields, tridionAttribute, page);
            }
        }

        internal static object Build(IFieldSet sourceFields, IFieldSet sourceMetadataFields, object destinationModel, IComponent source, IPage page = null)
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
                        var tridionSchemaNameAttribute = attr as SchemaNameAttribute;
                        if (tridionSchemaNameAttribute != null)
                        {
                            if (source != null) property.SetValue(destinationModel, source.Schema.Title, null);
                        }
                        continue;
                    }

                    var fields = tridionAttribute.IsMetadata ? sourceMetadataFields : sourceFields;

                    if (tridionAttribute.SchemaFieldName == String.Empty && source != null)
                    {
                        //Multimedia object or IComponent attribute
                        TrySetProperty<object>(property, destinationModel, source, tridionAttribute, page);
                    }
                    else
                    {
                        TrySetAllValues(fields, destinationModel, tridionAttribute, property, page);
                    }
                }
            }

            return destinationModel;
        }

        private static void TrySetProperty<T>(PropertyInfo property, object destinationModel, IFieldSet source,
                                              ITridionViewModelPropertyAttribute tridionAttribute, IPage page = null)
        {
            T value;
            if (source != null && TryResolveViewModelProperty(source, tridionAttribute, out value, page))
            {
                property.SetValue(destinationModel, value, null);
            }
        }

        private static void TrySetProperty<T>(PropertyInfo property, object destinationModel, IComponent source, ITridionViewModelPropertyAttribute tridionAttribute, IPage page = null)
        {
            T value;
            if (source != null && TryResolveViewModelProperty(source, tridionAttribute, out value, page))
            {
                property.SetValue(destinationModel, value, null);
            }
        }

        private static void TrySetMultiValueProperty<T>(PropertyInfo property, object destinationModel, IFieldSet source,
                                                        ITridionViewModelPropertyAttribute tridionAttribute, IPage page = null)
        {
            IEnumerable<T> value;
            if (source != null && TryResolveMultiValueViewModelProperty(source, tridionAttribute, out value, page))
            {
                if (tridionAttribute is BaseNestedTridionViewModelPropertyAttribute)
                {
                    var nestedAttr = (BaseNestedTridionViewModelPropertyAttribute) tridionAttribute;
                    var caster = typeof (System.Linq.Enumerable)
                        .GetMethod("Cast", new[] { typeof (IEnumerable) })
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

        private static bool TryResolveViewModelProperty<T>(IFieldSet source, ITridionViewModelPropertyAttribute attribute, out T value, IPage page = null)
        {
            value = default(T);
            var objAttribute = attribute as ITridionViewModelPropertyAttribute<T>;
            if (objAttribute != null)
            {
                value = objAttribute.GetValue(source, page);
            }
            return objAttribute != null;
        }

        private static bool TryResolveViewModelProperty<T>(IComponent source, ITridionViewModelPropertyAttribute attribute, out T value, IPage page = null)
        {
            value = default(T);
            var objAttribute = attribute as ITridionViewModelPropertyAttribute<T>;
            if (objAttribute != null)
            {
                value = objAttribute.GetValue(source, page);
            }
            return objAttribute != null;
        }

        private static bool TryResolveMultiValueViewModelProperty<T>(IFieldSet source, ITridionViewModelPropertyAttribute attribute, out IEnumerable<T> values, IPage page = null)
        {
            values = null;
            var objAttribute = attribute as ITridionViewModelPropertyAttribute<T>;
            if (objAttribute != null)
            {
                values = objAttribute.GetMultiValue(source, page);
            }
            return objAttribute != null;
        }
    }
}
