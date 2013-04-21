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

        public override string GetValue(IComponent component)
        {
            var linkedComponent = component.GetLinkedComponent(SchemaFieldName, IsMetadata);
            if(linkedComponent != null)
            {
                return linkedComponent.GetValue(ComponentFieldName, IsLinkedFieldMetadata).RemoveNamespacesAndWrapInParagraph().ResolveRichText().ToString();
            }
            return string.Empty;
        }

        public override IEnumerable<string> GetMultiValue(IComponent component)
        {
            var result = new List<string>();
            if (component.Fields.ContainsKey(SchemaFieldName))
            {
                foreach(var linkedComponent in component.Fields[SchemaFieldName].LinkedComponentValues)
                {
                    string value = linkedComponent.GetValue(ComponentFieldName, IsLinkedFieldMetadata);
                    result.Add(value.RemoveNamespacesAndWrapInParagraph().ResolveRichText().ToString());
                }
            }
            return result;
        }
    }
}