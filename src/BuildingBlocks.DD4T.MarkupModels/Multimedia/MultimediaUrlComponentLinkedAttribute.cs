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

        public override string GetValue(IFieldSet fields, IPage page = null)
        {
            if (fields.ContainsKey(SchemaFieldName))
            {
                if (fields[SchemaFieldName].LinkedComponentValues.Count > 0)
                {
                    var linkedComponent = fields[SchemaFieldName].LinkedComponentValues[0];
                    if(linkedComponent.Fields.ContainsKey(ComponentFieldName))
                    {
                        return linkedComponent.Fields[ComponentFieldName].LinkedComponentValues[0].Multimedia.Url;
                    }
                }
            }
            return string.Empty;
        }

        public override string GetValue(IComponent fields, IPage page = null)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetMultiValue(IFieldSet fields, IPage page = null)
        {
            List<string> values = new List<string>();
            if(fields.ContainsKey(SchemaFieldName))
            {
                var linkedComponent = fields[SchemaFieldName].LinkedComponentValues[0];
                
                if (linkedComponent.Fields.ContainsKey(ComponentFieldName))
                {
                    return linkedComponent.Fields[ComponentFieldName].LinkedComponentValues.Select(x=>x.Multimedia.Url);
                }
                
            }
            return values;
        }
    }
}