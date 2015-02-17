using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;
using DD4T.ContentModel;
using System;
using System.Collections.Generic;

namespace BuildingBlocks.DD4T.MarkupModels
{
    /// <summary>
    /// Indicates that this property shouldn have the value of the ComponentPathExpression from the Linked Component Field
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NumberComponentLinkFieldAttribute : BaseNumberTridionViewModelPropertyAttribute,
                                                     IComponentLinkFieldTridionViewModelPropertyAttribute
    {
        public string ComponentFieldName { get; set; }

        public bool IsLinkedFieldMetadata { get; set; }

        public NumberComponentLinkFieldAttribute(string schemaFieldName, string componentExpression)  : base(schemaFieldName)
        {
            ComponentFieldName = componentExpression;
        }

        public override double GetValue(IFieldSet fields, IPage page = null)
        {
            return fields.GetComponentLinkedNumberValue(SchemaFieldName, IsLinkedFieldMetadata, ComponentFieldName);
        }

        public override double GetValue(IComponent source, IPage page = null)
        {
            return source.Fields.GetComponentLinkedNumberValue(SchemaFieldName, IsLinkedFieldMetadata, ComponentFieldName);
        }

        public override IEnumerable<double> GetMultiValue(IFieldSet source, IPage page = null)
        {
            return source.GetComponentLinkedNumberMultiValue(SchemaFieldName, IsLinkedFieldMetadata, ComponentFieldName);
        }
    }
}
