using System;
using System.Collections.Generic;

using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;

using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels
{
    /// <summary>
    /// Marks this property as mapped from the DateTimeValues collection to build it's list.
    /// </summary>
    public class DateTimeFieldAttribute : BaseDateTimeTridionViewModelPropertyAttribute
    {
        public DateTimeFieldAttribute(string schemaFieldName)
            : base(schemaFieldName)
        {
        }

        public override DateTime GetValue(IComponent component)
        {
            return component.GetDateTimeValue(SchemaFieldName,IsMetadata);
        }

        public override IEnumerable<DateTime> GetMultiValue(IComponent component)
        {
            return component.GetDateTimeMultiValue(SchemaFieldName, IsMetadata);
        }
    }
}