using System;
using System.Collections.Generic;

using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;

using DD4T.ContentModel;

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

        public override string GetValue(IFieldSet fields, IPage page = null)
        {
            var linkedComponent = fields.GetLinkedComponent(SchemaFieldName);
            if(linkedComponent != null)
            {
                return linkedComponent.Fields.GetValue(ComponentFieldName).RemoveNamespacesAndWrapInParagraph().ResolveRichText(page).ToString();
            }
            return string.Empty;
        }

        public override string GetValue(IComponent fields, IPage page = null)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetMultiValue(IFieldSet fields, IPage page = null)
        {
            var result = new List<string>();
            if (fields.ContainsKey(SchemaFieldName))
            {
                foreach(var linkedComponent in fields[SchemaFieldName].LinkedComponentValues)
                {
                    string value = linkedComponent.Fields.GetValue(ComponentFieldName);
                    result.Add(value.RemoveNamespacesAndWrapInParagraph().ResolveRichText(page).ToString());
                }
            }
            return result;
        }
    }
}