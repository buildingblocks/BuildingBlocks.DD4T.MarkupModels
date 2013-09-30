using System;
using System.Collections.Generic;
using System.Text;

using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels.ExtensionMethods
{
    ///<summary>
    /// IComponentExtensionMethods is responsible for PURPOSE
    /// Author: Robert Stevenson-Leggett
    /// Date: 2013-04-09
    ///</summary>
    public static class IFieldSetExtensionMethods
    {
        public static string GetValue(this IFieldSet fields, string schemaFieldName)
        {
            var result = new StringBuilder();
            if (fields.ContainsKey(schemaFieldName))
            {
                result.Append(fields[schemaFieldName].Value);
            }
            return result.ToString();
        }

        public static IEnumerable<string> GetMultiValue(this IFieldSet fields, string schemaFieldName)
        {
            var result = new List<string>();
            if (fields.ContainsKey(schemaFieldName))
            {
                return fields[schemaFieldName].Values;
            }
            return result;
        }

        public static string GetComponentLinkedValue(this IFieldSet fields, string schemaFieldName, bool isLinkedFieldMetadata, string componentFieldName)
        {
            if (fields.ContainsKey(schemaFieldName))
            {
                if (fields[schemaFieldName].LinkedComponentValues.Count > 0)
                {
                    var linkedComponent = fields[schemaFieldName].LinkedComponentValues[0];
                    if (isLinkedFieldMetadata)
                    {
                        if (linkedComponent.MetadataFields.ContainsKey(componentFieldName))
                        {
                            return linkedComponent.MetadataFields.GetValue(componentFieldName);
                        }
                    }
                    else
                    {
                        if (linkedComponent.Fields.ContainsKey(componentFieldName))
                        {
                            return linkedComponent.Fields.GetValue(componentFieldName);
                        }
                    }
                }
            }
            return string.Empty;
        }

        public static IEnumerable<string> GetComponentLinkedMultiValue(this IFieldSet fields, string schemaFieldName, bool isLinkedFieldMetadata, string componentFieldName)
        {
            var result = new List<string>();

            if (fields.ContainsKey(schemaFieldName))
            {
                foreach (var linkedComponent in fields[schemaFieldName].LinkedComponentValues)
                {
                    if (isLinkedFieldMetadata)
                    {
                        result.Add(linkedComponent.MetadataFields.GetValue(componentFieldName));
                    }
                    else
                    {
                        result.Add(linkedComponent.Fields.GetValue(componentFieldName));
                    }
                }
            }

            return result;
        }

        public static IComponent GetLinkedComponent(this IFieldSet fields, string schemaFieldName)
        {
            if (fields.ContainsKey(schemaFieldName))
            {
                if(fields[schemaFieldName].LinkedComponentValues.Count > 0)
                {
                    return fields[schemaFieldName].LinkedComponentValues[0];
                }
            }
            return null;
        }

        public static IEnumerable<IComponent> GetLinkedComponentMultiValue(this IFieldSet fields, string schemaFieldName)
        {
            if (fields.ContainsKey(schemaFieldName))
            {
                if (fields[schemaFieldName].LinkedComponentValues.Count > 0)
                {
                    return fields[schemaFieldName].LinkedComponentValues;
                }
            }
            return new List<IComponent>();
        }

        public static IFieldSet GetEmbeddedFieldSet(this IFieldSet fields, string schemaFieldName)
        {
            if (fields.ContainsKey(schemaFieldName))
            {
                if (fields[schemaFieldName].EmbeddedValues.Count > 0)
                {
                    return fields[schemaFieldName].EmbeddedValues[0];
                }
            }
            return null;
        }

        public static IEnumerable<IFieldSet> GetEmbeddedFieldSetMultiValue(this IFieldSet fields, string schemaFieldName)
        {
            if (fields.ContainsKey(schemaFieldName))
            {
                if (fields[schemaFieldName].EmbeddedValues.Count > 0)
                {
                    return fields[schemaFieldName].EmbeddedValues;
                }
            }
            return new List<IFieldSet>();
        }

        public static DateTime GetDateTimeValue(this IFieldSet fields, string schemaFieldName)
        {
            if (fields.ContainsKey(schemaFieldName))
            {
                if (fields[schemaFieldName].LinkedComponentValues.Count > 0)
                {
                    return fields[schemaFieldName].DateTimeValues[0];
                }
            }
            return DateTime.MinValue;
        }

        public static IEnumerable<DateTime> GetDateTimeMultiValue(this IFieldSet fields, string schemaFieldName)
        {
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