using System;
using System.Collections.Generic;
using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;
using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels
{
    /// <summary>
    /// Indicates that a keyword uri from an IComponent should be mapped to this field. 
    /// </summary>
    public class KeywordFieldAttribute : BaseStringTridionViewModelPropertyAttribute
    {
        public KeywordFieldAttribute(string schemaFieldName): base(schemaFieldName)
        {
        }

        public override string GetValue(IFieldSet fields, IPage page = null)
        {
            return fields.GetKeywordUriValue(SchemaFieldName);
        }

        public override string GetValue(IComponent fields, IPage page = null)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetMultiValue(IFieldSet fields, IPage page = null)
        {
            return fields.GetKeywordUriMultiValue(SchemaFieldName);
        }
    }
}