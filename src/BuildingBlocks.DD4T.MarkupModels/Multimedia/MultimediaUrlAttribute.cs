using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels
{
    /// <summary>
    /// Indicates the property should map contain the Multimedia.Url property from the Linked Component specified by the SchemaFieldName
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MultimediaUrlAttribute : BaseStringTridionViewModelPropertyAttribute
    {
        public MultimediaUrlAttribute() : base(String.Empty)
        {
        }

        public MultimediaUrlAttribute(string schemaFieldName) : base(schemaFieldName)
        {
        }

        public override string GetValue(IFieldSet fields, IPage page = null)
        {
            var stringBuilder = new StringBuilder();
            if (fields.ContainsKey(SchemaFieldName))
            {
                if (fields[SchemaFieldName].LinkedComponentValues.Count > 0)
                {
                    var linkedComponent = fields[SchemaFieldName].LinkedComponentValues[0];
                    stringBuilder.Append(linkedComponent.Multimedia.Url);
                }
            }
            return stringBuilder.ToString();
        }

        public override string GetValue(IComponent source, IPage page = null)
        {
            var stringBuilder = new StringBuilder();
            if(SchemaFieldName == String.Empty && source != null)
            {
                if (source.Multimedia != null) stringBuilder.Append(source.Multimedia.Url);
            }
            return stringBuilder.ToString();
        }

        public override IEnumerable<string> GetMultiValue(IFieldSet source, IPage page = null)
        {
            var values = new List<string>();
            if(source.ContainsKey(SchemaFieldName))
            {
                foreach(var linkedComponent in source[SchemaFieldName].LinkedComponentValues)
                {
                    values.Add(linkedComponent.Multimedia.Url);
                }
            }
            else if(SchemaFieldName == "" && source.Count > 0)
            {
                foreach (var linkedComponent in source.First().Value.LinkedComponentValues)
                {
                    values.Add(linkedComponent.Multimedia.Url);
                }
            }
            return values;
        }
    }
}