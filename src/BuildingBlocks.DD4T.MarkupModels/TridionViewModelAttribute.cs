using System;
using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;

namespace BuildingBlocks.DD4T.MarkupModels
{
    /// <summary>
    /// Indicates that a model is mapped from Tridion and that it uses a particular Schema
    /// </summary>
    public class TridionViewModelAttribute : Attribute
    {
        public string SchemaTitle { get; set; }

        public ParentLinkType ParentLinkType { get; set; }

        public string ParentLinkFieldName { get; set; }        

    }
}