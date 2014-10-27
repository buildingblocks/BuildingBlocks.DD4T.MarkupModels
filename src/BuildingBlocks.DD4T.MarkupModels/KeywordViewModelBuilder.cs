using DD4T.ContentModel;
using System;

namespace BuildingBlocks.DD4T.MarkupModels
{
    public static class KeywordViewModelBuilder
    {
        public static T Build<T>(IKeyword source) where T : new()
        {
            var destinationModel = new T();
            var obj = (T) FieldSetMapper.Build(source.MetadataFields, destinationModel, source);
            if (obj is IKeywordBase)
            {
                obj.GetType().GetProperty("KeywordId").SetValue(obj, source.Id, null);
                obj.GetType().GetProperty("KeywordValue").SetValue(obj, source.Title, null);
                obj.GetType().GetProperty("KeywordKey").SetValue(obj, source.Key, null);
            }
            return obj;
        }

        public static object Build(IKeyword source, Type targetType)
        {
            var destinationModel = Activator.CreateInstance(targetType);
            var obj = FieldSetMapper.Build(source.MetadataFields, destinationModel, source);
            if (obj is IKeywordBase)
            {
                obj.GetType().GetProperty("KeywordId").SetValue(obj, source.Id, null);
                obj.GetType().GetProperty("KeywordValue").SetValue(obj, source.Title, null);
                obj.GetType().GetProperty("KeywordKey").SetValue(obj, source.Key, null);
            }
            return obj;
        }
    }
}
