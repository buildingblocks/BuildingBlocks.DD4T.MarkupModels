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

        public override bool GetValue(IFieldSet fields, IPage page = null)
        {
            string value = fields.GetValue(SchemaFieldName);
            return ParseString(value);
        }

        public override bool GetValue(IComponent source, IPage page = null)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<bool> GetMultiValue(IFieldSet source, IPage page = null)
        {
            var values = source.GetMultiValue(SchemaFieldName);
            return values.Select(ParseString).ToList();
        }
    }
}