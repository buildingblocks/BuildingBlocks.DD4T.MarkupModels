using System;
using System.Collections.Generic;

using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels
{
    ///<summary>
    /// BaseDateTimeTridionViewModelPropertyAttribute is responsible for PURPOSE
    /// Author: Robert Stevenson-Leggett
    /// Date: 2013-04-09
    ///</summary>
    public abstract class BaseDateTimeTridionViewModelPropertyAttribute : Attribute, ITridionViewModelPropertyAttribute<DateTime>
    {
        public string SchemaFieldName { get; set; }

        public bool IsMetadata { get; set; }

        public bool InlineEditable { get; set; }

        public bool IsMultiValue { get; set; }

        protected BaseDateTimeTridionViewModelPropertyAttribute(string schemaFieldName)
        {
            SchemaFieldName = schemaFieldName;
        }

        public abstract DateTime GetValue(IFieldSet fields);

        public abstract DateTime GetValue(IComponent source);

        public abstract IEnumerable<DateTime> GetMultiValue(IFieldSet source);
    }
}