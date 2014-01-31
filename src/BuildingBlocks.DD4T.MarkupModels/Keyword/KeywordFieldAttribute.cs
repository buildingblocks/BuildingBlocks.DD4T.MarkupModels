using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;

using DD4T.ContentModel;
using DD4T.Mvc.Html;

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

        public override string GetValue(IFieldSet fields)
        {
            return fields.GetKeywordUriValue(SchemaFieldName);
        }

        public override string GetValue(IComponent fields)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetMultiValue(IFieldSet fields)
        {
            return fields.GetKeywordUriMultiValue(SchemaFieldName);
        }
    }
}