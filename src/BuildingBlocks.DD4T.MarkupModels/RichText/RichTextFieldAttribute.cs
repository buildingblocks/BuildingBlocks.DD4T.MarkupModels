using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;

using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels
{
    ///<summary>
    /// 
    ///</summary>
    public class RichTextFieldAttribute : BaseStringTridionViewModelPropertyAttribute
    {
        public RichTextFieldAttribute(string schemaFieldName)
            : base(schemaFieldName)
        {
        }

        public override string GetValue(IFieldSet fields, IPage page = null)
        {
            var result = new StringBuilder();
            if (fields.ContainsKey(SchemaFieldName))
            {
                string value = fields[SchemaFieldName].Value;
                result.Append(value.ResolveRichText(page));
            }
            return result.ToString();
        }

        public override string GetValue(IComponent fields, IPage page = null)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetMultiValue(IFieldSet fields, IPage page = null)
        {
            var result = new List<MvcHtmlString>();
            if (fields.ContainsKey(SchemaFieldName))
            {
                result.AddRange(fields[SchemaFieldName].Values.Select(item => item.ResolveRichText(page)));
            }
            return result.Select(x=>x.ToString());
        }
    }
}