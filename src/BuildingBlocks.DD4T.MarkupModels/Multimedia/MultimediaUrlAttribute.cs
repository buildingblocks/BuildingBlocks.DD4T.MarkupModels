using System;
using System.Collections.Generic;
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
        public MultimediaUrlAttribute(string schemaFieldName) : base(schemaFieldName)
        {
        }

        public override string GetValue(IComponent component)
        {
            var stringBuilder = new StringBuilder();
            if (component.Fields.ContainsKey(SchemaFieldName))
            {
                if (component.Fields[SchemaFieldName].LinkedComponentValues.Count > 0)
                {
                    var linkedComponent = component.Fields[SchemaFieldName].LinkedComponentValues[0];
                    stringBuilder.Append(linkedComponent.Multimedia.Url);
                }
            }
            return stringBuilder.ToString();
        }

        public override IEnumerable<string> GetMultiValue(IComponent component)
        {
            List<string> values = new List<string>();
            if(component.Fields.ContainsKey(SchemaFieldName))
            {
                foreach(var linkedComponent in component.Fields[SchemaFieldName].LinkedComponentValues)
                {
                    values.Add(linkedComponent.Multimedia.Url);
                }
            }
            return values;
        }
    }
}