using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;
using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels
{
    public class KeywordMetadataFieldAttribute : BaseStringTridionViewModelPropertyAttribute
    {
        public string MetadataFeildName;

        public KeywordMetadataFieldAttribute(string schemaFieldName , string metadataFeildName): base(schemaFieldName)
        {
            MetadataFeildName = metadataFeildName;
        }

        public override string GetValue(IFieldSet fields)
        {
            return fields.GetKeywordMetadataUriValue(SchemaFieldName, MetadataFeildName);
        }

        public override string GetValue(IComponent fields)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetMultiValue(IFieldSet fields)
        {
            return fields.GetKeywordMetadataUriMultiValue(SchemaFieldName, MetadataFeildName);
        }
    }
}
