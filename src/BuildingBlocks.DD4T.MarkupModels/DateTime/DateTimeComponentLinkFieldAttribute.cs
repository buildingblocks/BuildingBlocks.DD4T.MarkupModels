using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;

using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels
{
    /// <summary>
    /// Indicates that this property should have the DateTime values from the from the Linked Component Field specifed by the Component
    /// FieldName property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DateTimeComponentLinkFieldAttribute : BaseDateTimeTridionViewModelPropertyAttribute, IComponentLinkFieldTridionViewModelPropertyAttribute
    {
        public string ComponentFieldName { get; set; }

        public bool IsLinkedFieldMetadata { get; set; }

        public DateTimeComponentLinkFieldAttribute(string schemaFieldName, string componentExpression)
            : base(schemaFieldName)
        {
            ComponentFieldName = componentExpression;
        }

        public override DateTime GetValue(IComponent component)
        {
            var linkedComponent = component.GetLinkedComponent(SchemaFieldName, IsMetadata);
            if (linkedComponent != null && linkedComponent.Fields.ContainsKey(ComponentFieldName))
            {
                return linkedComponent.Fields[ComponentFieldName].DateTimeValues[0];
            }
            return DateTime.MinValue;
        }

        public override IEnumerable<DateTime> GetMultiValue(IComponent component)
        {
            var linkedComponent = component.GetLinkedComponent(SchemaFieldName, IsMetadata);
            if (linkedComponent != null && linkedComponent.Fields.ContainsKey(ComponentFieldName))
            {
                return linkedComponent.Fields[ComponentFieldName].DateTimeValues;
            }
            return new List<DateTime>();
        }
    }
}