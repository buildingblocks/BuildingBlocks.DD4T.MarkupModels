using DD4T.ContentModel;
using System;

namespace BuildingBlocks.DD4T.MarkupModels
{
    public static class ComponentViewModelBuilder
    {
        public static T Build<T>(IComponent source, IPage page = null) where T : new()
        {
            var destinationModel = new T();
            var obj = (T) FieldSetMapper.Build(source.Fields, source.MetadataFields, destinationModel, source, page);
            if (obj is IComponentBase)
            {
                obj.GetType().GetProperty("ComponentId").SetValue(obj, source.Id, null);
                obj.GetType().GetProperty("Schema").SetValue(obj, source.Schema.Title, null);
            }
            return obj;
        }

        public static object Build(IComponent source, Type targetType, IPage page = null)
        {
            var destinationModel = Activator.CreateInstance(targetType);
            var obj = FieldSetMapper.Build(source.Fields, source.MetadataFields, destinationModel, source, page);
            if (obj is IComponentBase)
            {
                obj.GetType().GetProperty("ComponentId").SetValue(obj, source.Id, null);
                obj.GetType().GetProperty("Schema").SetValue(obj, source.Schema.Title, null);
            }
            return obj;
        }

        internal static object Build(IFieldSet source, Type targetType, IPage page = null)
        {
            var destinationModel = Activator.CreateInstance(targetType);
            return FieldSetMapper.Build(source, null, destinationModel, null, page);
        }
    }
}
