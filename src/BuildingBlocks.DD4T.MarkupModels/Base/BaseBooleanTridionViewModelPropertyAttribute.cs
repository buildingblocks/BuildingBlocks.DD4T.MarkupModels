using DD4T.ContentModel;
using System;
using System.Collections.Generic;

namespace BuildingBlocks.DD4T.MarkupModels
{
    ///<summary>
    /// BaseDateTimeTridionViewModelPropertyAttribute is a base class for schema fields based on booleans
    /// Date: 2013-04-09
    ///</summary>
    public abstract class BaseBooleanTridionViewModelPropertyAttribute : Attribute, ITridionViewModelPropertyAttribute<bool>
    {
        private string _stringValue = "Yes";

        public string SchemaFieldName { get; set; }

        public bool IsMetadata { get; set; }

        public bool InlineEditable { get; set; }

        public bool IsMultiValue { get; set; }

        public string StringValue
        {
            get { return _stringValue; }
            set { _stringValue = value; }
        }

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
            return !bool.TryParse(value, out output)
                       ? value.Equals(StringValue, StringComparison.OrdinalIgnoreCase)
                       : output;
        }
    }
}
