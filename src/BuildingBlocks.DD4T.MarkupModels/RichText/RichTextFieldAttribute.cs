using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;

using DD4T.ContentModel;
using DD4T.Mvc.Html;

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

        public override string GetValue(IComponent component)
        {
            var result = new StringBuilder();
            if (component.Fields.ContainsKey(SchemaFieldName))
            {
                string value = component.Fields[SchemaFieldName].Value;
                result.Append(value.ResolveRichText());
            }
            return result.ToString();
        }

        public override IEnumerable<string> GetMultiValue(IComponent component)
        {
            var result = new List<MvcHtmlString>();
            if (component.Fields.ContainsKey(SchemaFieldName))
            {
                foreach(var item in component.Fields[SchemaFieldName].Values)
                {
                    result.Add(item.ResolveRichText());
                }
            }
            return result.Select(x=>x.ToString());
        }
    }
}