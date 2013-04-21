using System;
using System.Collections.Generic;

namespace BuildingBlocks.DD4T.MarkupModels.Tests
{
    [TridionViewModel(SchemaTitle = "General")]
    public class HeadingViewModel
    {
        [TextField("title_line_1", InlineEditable = false)]
        public string TitleLine1 { get; set; }

        [TextField("title_line_2", InlineEditable = false)]
        public string TitleLine2 { get; set; }

        [TextField("sub_title", InlineEditable = false)]
        public string SubTitle { get; set; }

        [RichTextField("summary", InlineEditable = false)]
        public string Summary { get; set; }

        [MultimediaUrl("image")]
        public string ImageUrl { get; set; }

        [TextComponentLinkField("image", "AltText", IsMetadata = true)]
        public string ImageAltText { get; set; }

        [TextComponentLinkField("link","link_url")]
        public string RelatedLinkUrl { get; set; }

        [TextComponentLinkField("link","link_text")]
        public string RelatedLinkText { get; set; }

        [BooleanField("show_social_links", IsMetadata = true)]
        public bool ShowSocialLinks { get; set; }

        [MultimediaUrl("thumbnail_images", IsMultiValue = true)]
        public IEnumerable<string> ThumbnailImages { get; set; }

        [TextField("bullet_points", InlineEditable = false, IsMetadata = true, IsMultiValue = true)]
        public IEnumerable<string> SomeMultiValueText { get; set; }

        [BooleanComponentLinkField("featured", "show", IsMultiValue = true)]
        public IEnumerable<bool> CheckBoxes { get; set; }

        [DateTimeField("publish_date")]
        public DateTime PublishDate { get; set; }

        [DateTimeComponentLinkField("date_component", "dates", IsMultiValue = true)]
        public IEnumerable<DateTime> Dates { get; set; }

        [RichTextComponentLinkField("linked_rich_text", "body")]
        public string RichTextComponentLinkField { get; set; }
    }
}