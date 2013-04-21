using System;
using System.Collections.Generic;

using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels
{
    ///<summary>
    /// EmbeddedValuesAttribute is responsible for PURPOSE
    /// Author: Robert Stevenson-Leggett
    /// Date: 2013-04-09
    ///</summary>
    [Obsolete]
    internal class EmbeddedValuesAttribute : BaseNestedTridionViewModelPropertyAttribute
    {
        public EmbeddedValuesAttribute(string schemaFieldName, Type targetType)
            : base(schemaFieldName, targetType)
        {
        }

        public override object GetValue(IComponent component)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<object> GetMultiValue(IComponent component)
        {
            throw new System.NotImplementedException();
        }
    }
}