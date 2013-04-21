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

        public override string GetValue(IComponent component)
        {
            return component.GetComponentLinkedValue(SchemaFieldName, IsMetadata, ComponentFieldName);
        }

        public override IEnumerable<string> GetMultiValue(IComponent component)
        {
            return component.GetComponentLinkedMultiValue(SchemaFieldName, IsMetadata, ComponentFieldName);
        }
    }
}