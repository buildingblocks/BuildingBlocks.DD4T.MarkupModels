using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;
using DD4T.ContentModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuildingBlocks.DD4T.MarkupModels
{
    /// <summary>
    /// Indicates that a keyword uri from an IComponent should be mapped to this field.
    /// </summary>
    public class NestedKeywordFieldAttribute : BaseNestedTridionViewModelPropertyAttribute
    {
        public NestedKeywordFieldAttribute(string schemaFieldName, Type type) : base(schemaFieldName, type)
        {
        }

        public override object GetValue(IFieldSet fields)
        {
            var linkedKeyword = fields.GetLinkedKeyword(SchemaFieldName);
            if (linkedKeyword != null)
            {
                return KeywordViewModelBuilder.Build(linkedKeyword, TargetType);
            }
            return null;
        }

        public override object GetValue(IComponent fields)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> GetMultiValue(IFieldSet fields)
        {
            var linkedKeywords = fields.GetLinkedKeywordMultiValue(SchemaFieldName);

            return linkedKeywords.Select(keyword => KeywordViewModelBuilder.Build(keyword, TargetType));
        }
    }
}
