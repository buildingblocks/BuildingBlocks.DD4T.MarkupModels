using System;
using System.Collections.Generic;

using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels
{
    ///<summary>
    /// BaseDateTimeTridionViewModelPropertyAttribute is a base class for schema fields based on booleans
    /// Date: 2013-04-09
    ///</summary>
    public abstract class BaseBooleanTridionViewModelPropertyAttribute : Attribute, ITridionViewModelPropertyAttribute<bool>
    {
        public string SchemaFieldName { get; set; }

        public bool IsMetadata { get; set; }

        public bool InlineEditable { get; set; }

        public bool IsMultiValue { get; set; }

        protected BaseBooleanTridionViewModelPropertyAttribute(string schemaFieldName)
        {
            SchemaFieldName = schemaFieldName;
        }

        public abstract bool GetValue(IFieldSet fields);

        public abstract bool GetValue(IComponent source);

        public abstract IEnumerable<bool> GetMultiValue(IFieldSet source);

        protected bool ParseString(string value)
        {
            bool output;
            if (!bool.TryParse(value, out output))
            {
                return value.ToLower() == "yes";
            }
            return output;
        }
    }
}