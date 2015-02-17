using DD4T.ContentModel;
using System;
using System.Collections.Generic;

namespace BuildingBlocks.DD4T.MarkupModels
{
    /// <summary>
    /// Abstract class for MarkupModel attributes that contains an abstract method for the subclasses to define their mapping
    /// </summary>
    public abstract class BaseNumberTridionViewModelPropertyAttribute : Attribute, ITridionViewModelPropertyAttribute<double>
    {
        public string SchemaFieldName { get; set; }

        public bool IsMetadata { get; set; }

        public bool InlineEditable { get; set; }

        public bool IsMultiValue { get; set; }

        protected BaseNumberTridionViewModelPropertyAttribute(string schemaFieldName)
        {
            SchemaFieldName = schemaFieldName;
        }

        public abstract double GetValue(IFieldSet fields, IPage page = null);

        public abstract double GetValue(IComponent fields, IPage page = null);

        public abstract IEnumerable<double> GetMultiValue(IFieldSet source, IPage page = null);
    }
}
