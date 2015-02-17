using System;
using System.Collections.Generic;
using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;
using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels
{
    /// <summary>
    /// Indicates that a text field from an IComponent should be mapped to this field. Also contains the knowledge of how
    /// the mapping should work.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TextFieldAttribute : BaseStringTridionViewModelPropertyAttribute
    {
        public TextFieldAttribute(string schemaFieldName) : base(schemaFieldName)
        {
        }

        public override string GetValue(IFieldSet fields, IPage page = null)
        {
            return fields.GetValue(SchemaFieldName);
        }

        public override string GetValue(IComponent fields, IPage page = null)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetMultiValue(IFieldSet fields, IPage page = null)
        {
            return fields.GetMultiValue(SchemaFieldName);
        }
    }
}