using System.Collections.Generic;

namespace BuildingBlocks.DD4T.MarkupModels.Tests
{
    [TridionViewModel(SchemaTitle = "Carousel Item", Nested = true, ParentComponentLinkFieldName = "list")]
    public class CarouselItemViewModel
    {
        [TextField("heading")]
        public string Heading { get; set; }

        [TextField("summary")]
        public string Summary { get; set; }

        [MultimediaUrl("image")]
        public string ImageUrl { get; set; }
    }
}