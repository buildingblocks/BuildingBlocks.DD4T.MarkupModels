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

        public override DateTime GetValue(IFieldSet fields, IPage page = null)
        {
            var linkedComponent = fields.GetLinkedComponent(SchemaFieldName);
            if (linkedComponent != null && linkedComponent.Fields.ContainsKey(ComponentFieldName))
            {
                return linkedComponent.Fields[ComponentFieldName].DateTimeValues[0];
            }
            return DateTime.MinValue;
        }

        public override DateTime GetValue(IComponent source, IPage page = null)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<DateTime> GetMultiValue(IFieldSet source, IPage page = null)
        {
            var linkedComponent = source.GetLinkedComponent(SchemaFieldName);
            if (linkedComponent != null && linkedComponent.Fields.ContainsKey(ComponentFieldName))
            {
                return linkedComponent.Fields[ComponentFieldName].DateTimeValues;
            }
            return new List<DateTime>();
        }
    }
}