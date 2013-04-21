using System;

namespace BuildingBlocks.DD4T.MarkupModels
{
    /// <summary>
    /// Indicates that a model is mapped from Tridion and that it uses a particular Schema
    /// </summary>
    public class TridionViewModelAttribute : Attribute
    {
        public string SchemaTitle { get; set; }

        public bool Nested { get; set; }

        public string ParentComponentLinkFieldName { get; set; }
    }
}