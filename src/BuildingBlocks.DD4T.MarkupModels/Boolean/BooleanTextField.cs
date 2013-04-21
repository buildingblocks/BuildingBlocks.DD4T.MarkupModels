using System;
using System.Collections.Generic;
using System.Linq;

using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;

using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels
{
    ///<summary>
    /// BooleanTextField is responsible for PURPOSE
    /// Author: Robert Stevenson-Leggett
    /// Date: 2013-04-09
    ///</summary>
    public class BooleanFieldAttribute : BaseBooleanTridionViewModelPropertyAttribute
    {
        public BooleanFieldAttribute(string schemaFieldName)
            : base(schemaFieldName)
        {
        }

        public override bool GetValue(IComponent component)
        {
            string value = component.GetValue(SchemaFieldName, IsMetadata);
            return ParseString(value);
        }

        public override IEnumerable<bool> GetMultiValue(IComponent component)
        {
            var values = component.GetMultiValue(SchemaFieldName, IsMetadata);
            return values.Select(ParseString).ToList();
        }
    }
}