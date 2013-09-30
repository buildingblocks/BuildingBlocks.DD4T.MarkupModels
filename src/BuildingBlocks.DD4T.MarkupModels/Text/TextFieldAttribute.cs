using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;

using DD4T.ContentModel;
using DD4T.Mvc.Html;

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

        public override string GetValue(IFieldSet fields)
        {
            return fields.GetValue(SchemaFieldName);
        }

        public override string GetValue(IComponent fields)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetMultiValue(IFieldSet fields)
        {
            return fields.GetMultiValue(SchemaFieldName);
        }
    }
}