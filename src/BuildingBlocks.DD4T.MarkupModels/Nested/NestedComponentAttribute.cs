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
    public class NestedComponentAttribute : BaseNestedTridionViewModelPropertyAttribute
    {
        public NestedComponentAttribute(string schemaFieldName, Type type)
            : base(schemaFieldName, type)
        {
        }

        public override object GetValue(IComponent component)
        {
            var linkedComponent = component.GetLinkedComponent(SchemaFieldName, IsMetadata);
            return ComponentViewModelBuilder.Build(linkedComponent, TargetType);
        }

        public override IEnumerable<object> GetMultiValue(IComponent component)
        {
            var linkedComponents = component.GetLinkedComponentMultiValue(SchemaFieldName, IsMetadata);

            return linkedComponents.Select(comp => ComponentViewModelBuilder.Build(comp, TargetType));
        }
    }
}