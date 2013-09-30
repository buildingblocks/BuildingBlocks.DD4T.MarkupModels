using System;
using System.Collections.Generic;
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
    public class RichTextComponentLinkFieldAttribute : BaseStringTridionViewModelPropertyAttribute, IComponentLinkFieldTridionViewModelPropertyAttribute
    {
        public string ComponentFieldName { get; set; }

        public bool IsLinkedFieldMetadata { get; set; }

        public RichTextComponentLinkFieldAttribute(string schemaFieldName, string linkedComponentField)
            : base(schemaFieldName)
        {
            ComponentFieldName = linkedComponentField;
        }

        public override string GetValue(IFieldSet fields)
        {
            var linkedComponent = fields.GetLinkedComponent(SchemaFieldName);
            if(linkedComponent != null)
            {
                return linkedComponent.Fields.GetValue(ComponentFieldName).RemoveNamespacesAndWrapInParagraph().ResolveRichText().ToString();
            }
            return string.Empty;
        }

        public override string GetValue(IComponent fields)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetMultiValue(IFieldSet fields)
        {
            var result = new List<string>();
            if (fields.ContainsKey(SchemaFieldName))
            {
                foreach(var linkedComponent in fields[SchemaFieldName].LinkedComponentValues)
                {
                    string value = linkedComponent.Fields.GetValue(ComponentFieldName);
                    result.Add(value.RemoveNamespacesAndWrapInParagraph().ResolveRichText().ToString());
                }
            }
            return result;
        }
    }
}