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

        public override DateTime GetValue(IFieldSet fields, IPage page = null)
        {
            return fields.GetDateTimeValue(SchemaFieldName);
        }

        public override DateTime GetValue(IComponent source, IPage page = null)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<DateTime> GetMultiValue(IFieldSet source, IPage page = null)
        {
            return source.GetDateTimeMultiValue(SchemaFieldName);
        }
    }
}