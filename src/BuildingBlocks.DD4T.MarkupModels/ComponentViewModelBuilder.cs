using System;
using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels
{
    public static class ComponentViewModelBuilder
    {
        public static T Build<T>(IComponent source) where T : new()
        {
            var destinationModel = new T();
            var obj = (T) FieldSetMapper.Build(source.Fields, source.MetadataFields, destinationModel, source);
            if (obj is IComponentBase)
            {
                obj.GetType().GetProperty("ComponentId").SetValue(obj, source.Id, null);

            }
            return obj;
        }

        public static object Build(IComponent source, Type targetType)
        {
            var destinationModel = Activator.CreateInstance(targetType);
            var obj = FieldSetMapper.Build(source.Fields, source.MetadataFields, destinationModel, source);
            if (obj is IComponentBase)
            {
                obj.GetType().GetProperty("ComponentId").SetValue(obj, source.Id, null);
            }
            return obj;
        }

        internal static T Build<T>(IFieldSet source) where T : new()
        {
            var destinationModel = new T();
            return (T) FieldSetMapper.Build(source, null, destinationModel, null);
        }

        internal static object Build(IFieldSet source, Type targetType)
        {
            var destinationModel = Activator.CreateInstance(targetType);
            return FieldSetMapper.Build(source, null, destinationModel, null);
        }
    }
}