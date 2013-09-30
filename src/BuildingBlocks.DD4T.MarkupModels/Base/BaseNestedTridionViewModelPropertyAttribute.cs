using System;
using System.Collections.Generic;

using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels
{
    ///<summary>
    /// BaseNestedTridionViewModelPropertyAttribute is a Base class for the case of SubModels
    /// Author: Robert Stevenson-Leggett
    /// Date: 2013-04-10
    ///</summary>
    public abstract class BaseNestedTridionViewModelPropertyAttribute : Attribute, ITridionViewModelPropertyAttribute<object>
    {
        public Type TargetType { get; set; }

        public string SchemaFieldName { get; set; }

        public bool IsMetadata { get; set; }

        public bool InlineEditable { get; set; }

        public bool IsMultiValue { get; set; }

        protected BaseNestedTridionViewModelPropertyAttribute(string schemaFieldName, Type targetType)
        {
            SchemaFieldName = schemaFieldName;
            TargetType = targetType;
        }

        public abstract object GetValue(IFieldSet fields);

        public abstract object GetValue(IComponent source);

        public abstract IEnumerable<object> GetMultiValue(IFieldSet field);
    }
}