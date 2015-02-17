using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;
using DD4T.ContentModel;
using System;
using System.Collections.Generic;

namespace BuildingBlocks.DD4T.MarkupModels
{
    /// <summary>
    /// Indicates that a text field from an IComponent should be mapped to this field. Also contains the knowledge of how
    /// the mapping should work.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NumberFieldAttribute : BaseNumberTridionViewModelPropertyAttribute
    {
        public NumberFieldAttribute(string schemaFieldName) : base(schemaFieldName)
        {
        }

        public override double GetValue(IFieldSet fields, IPage page = null)
        {
            return fields.GetNumberValue(SchemaFieldName);
        }

        public override double GetValue(IComponent fields, IPage page = null)
        {
            return fields.Fields.GetNumberValue(SchemaFieldName);
        }

        public override IEnumerable<double> GetMultiValue(IFieldSet fields, IPage page = null)
        {
            return fields.GetNumberMultiValue(SchemaFieldName);
        }
    }
}
