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
    public class MultimediaUrlComponentLinkedAttribute : BaseStringTridionViewModelPropertyAttribute, IComponentLinkFieldTridionViewModelPropertyAttribute
    {
        public string ComponentFieldName { get; set; }

        public bool IsLinkedFieldMetadata { get; set; }

        public MultimediaUrlComponentLinkedAttribute(string schemaFieldName)
            : base(schemaFieldName)
        {
        }

        public override string GetValue(IComponent component)
        {
            if (component.Fields.ContainsKey(SchemaFieldName))
            {
                if (component.Fields[SchemaFieldName].LinkedComponentValues.Count > 0)
                {
                    var linkedComponent = component.Fields[SchemaFieldName].LinkedComponentValues[0];
                    if(linkedComponent.Fields.ContainsKey(ComponentFieldName))
                    {
                        return linkedComponent.Fields[ComponentFieldName].LinkedComponentValues[0].Multimedia.Url;
                    }
                }
            }
            return string.Empty;
        }

        public override IEnumerable<string> GetMultiValue(IComponent component)
        {
            List<string> values = new List<string>();
            if(component.Fields.ContainsKey(SchemaFieldName))
            {
                var linkedComponent = component.Fields[SchemaFieldName].LinkedComponentValues[0];
                
                if (linkedComponent.Fields.ContainsKey(ComponentFieldName))
                {
                    return linkedComponent.Fields[ComponentFieldName].LinkedComponentValues.Select(x=>x.Multimedia.Url);
                }
                
            }
            return values;
        }
    }
}