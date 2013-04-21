using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

using DD4T.ContentModel;
using DD4T.Mvc.Html;

namespace BuildingBlocks.DD4T.MarkupModels.ExtensionMethods
{
    ///<summary>
    /// IComponentExtensionMethods is responsible for PURPOSE
    /// Author: Robert Stevenson-Leggett
    /// Date: 2013-04-09
    ///</summary>
    public static class ComponentExtensionMethods
    {
        public static string GetValue(this IComponent component, string schemaFieldName, bool isMetadata)
        {
            var result = new StringBuilder();
            var fields = component.Fields;
            if (isMetadata)
            {
                fields = component.MetadataFields;
            }
            if (fields.ContainsKey(schemaFieldName))
            {
                result.Append(fields[schemaFieldName].Value);
            }
            return result.ToString();
        }

        public static IEnumerable<string> GetMultiValue(this IComponent component, string schemaFieldName, bool isMetadata)
        {
            var result = new List<string>();
            var fields = component.Fields;
            if (isMetadata)
            {
                fields = component.MetadataFields;
            }
            if (fields.ContainsKey(schemaFieldName))
            {
                foreach (var value in fields[schemaFieldName].Values)
                {
                    result.Add(value);
                }
            }
            return result;
        }

        public static string GetComponentLinkedValue(this IComponent component, string schemaFieldName, bool isMetadata, string componentFieldName)
        {
            if (component.Fields.ContainsKey(schemaFieldName))
            {
                if (component.Fields[schemaFieldName].LinkedComponentValues.Count > 0)
                {
                    var linkedComponent = component.Fields[schemaFieldName].LinkedComponentValues[0];
                    if (isMetadata)
                    {
                        if (linkedComponent.MetadataFields.ContainsKey(componentFieldName))
                        {
                            return linkedComponent.MetadataFields[componentFieldName].Value;
                        }
                    }
                    else
                    {
                        if (linkedComponent.Fields.ContainsKey(componentFieldName))
                        {
                            return linkedComponent.Fields[componentFieldName].Value;
                        }
                    }
                }
            }
            return string.Empty;
        }

        public static IEnumerable<string> GetComponentLinkedMultiValue(this IComponent component, string schemaFieldName, bool isMetadata, string componentFieldName)
        {
            var result = new List<string>();

            if (component.Fields.ContainsKey(schemaFieldName))
            {
                foreach (var linkedComponent in component.Fields[schemaFieldName].LinkedComponentValues)
                {
                   result.Add(linkedComponent.GetValue(componentFieldName, false));
                }
            }

            return result;
        }

        public static IComponent GetLinkedComponent(this IComponent component, string schemaFieldName, bool isMetadata)
        {
            var fields = component.Fields;
            if(isMetadata)
            {
                fields = component.MetadataFields;
            }
            if (fields.ContainsKey(schemaFieldName))
            {
                if(fields[schemaFieldName].LinkedComponentValues.Count > 0)
                {
                    return fields[schemaFieldName].LinkedComponentValues[0];
                }
            }
            return null;
        }

        public static IEnumerable<IComponent> GetLinkedComponentMultiValue(this IComponent component, string schemaFieldName, bool isMetadata)
        {
            var fields = component.Fields;
            if (isMetadata)
            {
                fields = component.MetadataFields;
            }
            if (fields.ContainsKey(schemaFieldName))
            {
                if (fields[schemaFieldName].LinkedComponentValues.Count > 0)
                {
                    return fields[schemaFieldName].LinkedComponentValues;
                }
            }
            return new List<IComponent>();
        }

        public static DateTime GetDateTimeValue(this IComponent component, string schemaFieldName, bool isMetadata)
        {
            var fields = component.Fields;
            if (isMetadata)
            {
                fields = component.MetadataFields;
            }
            if (fields.ContainsKey(schemaFieldName))
            {
                if (fields[schemaFieldName].LinkedComponentValues.Count > 0)
                {
                    return fields[schemaFieldName].DateTimeValues[0];
                }
            }
            return DateTime.MinValue;
        }

        public static IEnumerable<DateTime> GetDateTimeMultiValue(this IComponent component, string schemaFieldName, bool isMetadata)
        {
            var fields = component.Fields;
            if (isMetadata)
            {
                fields = component.MetadataFields;
            }
            if (fields.ContainsKey(schemaFieldName))
            {
                if (fields[schemaFieldName].LinkedComponentValues.Count > 0)
                {
                    return fields[schemaFieldName].DateTimeValues;
                }
            }
            return new List<DateTime>();
        }
    }
}