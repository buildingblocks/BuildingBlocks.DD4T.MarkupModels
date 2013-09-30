using System;
using System.Collections.Generic;
using System.Text;

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

        public override string GetValue(IFieldSet fields)
        {
            return fields.GetComponentLinkedValue(SchemaFieldName, IsLinkedFieldMetadata, ComponentFieldName);
        }

        public override string GetValue(IComponent fields)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetMultiValue(IFieldSet fields)
        {
            return fields.GetComponentLinkedMultiValue(SchemaFieldName, IsLinkedFieldMetadata, ComponentFieldName);
        }
    }
}