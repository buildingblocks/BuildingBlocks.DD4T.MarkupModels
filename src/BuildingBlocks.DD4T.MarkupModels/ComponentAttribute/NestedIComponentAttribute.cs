using System;
using System.Collections.Generic;
using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels.ComponentAttribute
{
    ///<summary>
    /// Is designed for the sub model
    ///</summary>
    public class ComponentAttribute : BaseNestedTridionViewModelPropertyAttribute
    {
        public ComponentAttribute()
            : base(String.Empty)
        {
        }

        public override object GetValue(IFieldSet fields)
        {
            throw new NotImplementedException();
        }

        public override object GetValue(IComponent source)
        {
            return source;
        }

        public override IEnumerable<object> GetMultiValue(IFieldSet fields)
        {
            throw new NotImplementedException();
        }
    }
}