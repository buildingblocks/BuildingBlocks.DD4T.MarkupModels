using System;
using System.Collections.Generic;
using System.Linq;

using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;

using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels.Nested
{
    ///<summary>
    /// Is designed for the sub model
    ///</summary>
    public class NestedIComponentAttribute : BaseNestedTridionViewModelPropertyAttribute
    {
        public NestedIComponentAttribute(string schemaFieldName, Type type)
            : base(schemaFieldName, type)
        {
        }

        public override object GetValue(IFieldSet fields, IPage page = null)
        {
            var linkedComponent = fields.GetLinkedComponent(SchemaFieldName);
            if (linkedComponent != null)
            {
                return linkedComponent;
            }
            return null;
        }

        public override object GetValue(IComponent source, IPage page = null)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> GetMultiValue(IFieldSet fields, IPage page = null)
        {
            var linkedComponents = fields.GetLinkedComponentMultiValue(SchemaFieldName);

            if (linkedComponents != null)
            {
                return linkedComponents;
            }
            return null;
        }
    }
}