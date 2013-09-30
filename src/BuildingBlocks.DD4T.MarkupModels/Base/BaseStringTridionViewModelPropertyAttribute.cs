using System;
using System.Collections.Generic;

using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels
{
    /// <summary>
    /// Abstract class for MarkupModel attributes that contains an abstract method for the subclasses to define their mapping
    /// </summary>
    public abstract class BaseStringTridionViewModelPropertyAttribute : Attribute, ITridionViewModelPropertyAttribute<string>
    {
        public string SchemaFieldName { get; set; }

        public bool IsMetadata { get; set; }

        public bool InlineEditable { get; set; }

        public bool IsMultiValue { get; set; }

        protected BaseStringTridionViewModelPropertyAttribute(string schemaFieldName)
        {
            SchemaFieldName = schemaFieldName;
        }

        public abstract string GetValue(IFieldSet fields);

        public abstract string GetValue(IComponent fields);

        public abstract IEnumerable<string> GetMultiValue(IFieldSet source);
    }
}