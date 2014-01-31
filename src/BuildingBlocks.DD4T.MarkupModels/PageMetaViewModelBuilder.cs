using System;
using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels
{
    public static class PageMetaViewModelBuilder
    {
        public static T Build<T>(IPage source) where T : new()
        {
            if (source == null) throw new ArgumentNullException();

            var dest = new T();
            return (T) FieldSetMapper.Build(null, source.MetadataFields, dest, null);
        }
    }
}