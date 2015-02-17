using System;
using System.Collections.Generic;

using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;

using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels
{
    /// <summary>
    /// Indicates that this property shouldn have the value of the ComponentPathExpression from the Linked Component Field
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TextComponentLinkFieldAttribute : BaseStringTridionViewModelPropertyAttribute, IComponentLinkFieldTridionViewModelPropertyAttribute
    {
        public string ComponentFieldName { get; set; }

        public bool IsLinkedFieldMetadata { get; set; }

        public TextComponentLinkFieldAttribute(string schemaFieldName, string componentFieldName) : base(schemaFieldName)
        {
            ComponentFieldName = componentFieldName;
        }

        public override string GetValue(IFieldSet fields, IPage page = null)
        {
            return fields.GetComponentLinkedValue(SchemaFieldName, IsLinkedFieldMetadata, ComponentFieldName);
        }

        public override string GetValue(IComponent fields, IPage page = null)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetMultiValue(IFieldSet fields, IPage page = null)
        {
            return fields.GetComponentLinkedMultiValue(SchemaFieldName, IsLinkedFieldMetadata, ComponentFieldName);
        }
    }
}