using System;
using System.Collections.Generic;
using System.Linq;
using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;
using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels.Nested
{
    ///<summary>
    /// EmbeddedValuesAttribute is responsible for PURPOSE
    /// Author: Robert Stevenson-Leggett
    /// Date: 2013-04-09
    ///</summary>
    public class EmbeddedValuesAttribute : BaseNestedTridionViewModelPropertyAttribute
    {
        public EmbeddedValuesAttribute(string schemaFieldName, Type targetType)
            : base(schemaFieldName, targetType)
        {
        }

        public override object GetValue(IFieldSet fields, IPage page = null)
        {
            var embeddedFieldSet = fields.GetEmbeddedFieldSet(SchemaFieldName);
            if (embeddedFieldSet != null)
            {
                return ComponentViewModelBuilder.Build(embeddedFieldSet, TargetType);
            }
            return null;
        }

        public override object GetValue(IComponent source, IPage page = null)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> GetMultiValue(IFieldSet fields, IPage page = null)
        {
            var embeddedFieldSets = fields.GetEmbeddedFieldSetMultiValue(SchemaFieldName);

            return embeddedFieldSets.Select(fieldSet => ComponentViewModelBuilder.Build(fieldSet, TargetType));
        }
    }
}