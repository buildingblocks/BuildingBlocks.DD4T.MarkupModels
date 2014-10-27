Markup Models - User Guide
==========================

This is a draft user guide for the BuildingBlocks.DD4T.MarkupModels project. It will explain how to use the library and the different options available.

Author: Robert Stevenson-Leggett  
Date: 15/04/2013  
Version: 0.18

The library can be installed from NuGet with `Install-Package BuildingBlocks.DD4T.MarkupModels -Pre`

Concepts
========
The aims of MarkupModels are: 1) to remove any logic for mapping IComponent to a POCO ViewModel from the application and 2) simplify the addition of Inline Editable markup (for Experience Manager or Site Edit).

The main concept is the addition of attributes to the model in order to define the mapping at compile time. The actual mapping happens at run time.

Disadvantages of this approach are that you need to know the types of the schema fields you are mapping - but this would be the same when using our Html helper library, builder or traditional mapping code.

A typical model might look like:

    [TridionViewModel(SchemaTitle = "General")]
    public class HeadingViewModel
    {
        [TextField("title_line_1", InlineEditable = true)]
        public string TitleLine1 { get; set; }

        [TextField("title_line_2", InlineEditable = true)]
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

A nested scenario might look like:

    [TridionViewModel(SchemaTitle = "Component List")]
    public class CarouselViewModel
    {
        [TextField("title")]
        public string Title { get; set; }

        [NestedComponent("list", typeof(CarouselItemViewModel), IsMultiValue = true)]
        public IEnumerable<CarouselItemViewModel> CarouselItemViewModels { get; set; }
    }


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


Attributes available
====================

The following class attributes are available:
 
 * TridionViewModel
  * Gives the SchemaTitle, whether this is a Nested model and if so the ComponentFieldName on it�s parent

The following property attributes are available:

 * TextField
  * Simple text field on a Component. Will look in the .Values collection for the value.
 * TextComponentLinkField
  * A text field on a linked Component. Allows flattening a linked Componentinto the current model
 * RichTextField
  * A text field that will be resolved as rich text. Namespaces will be removed and Component Links resolved when the item is Built by the builder.
 * RichTextComponentLinkField
  * A text field on a linked Component that will be mapped from that linked Component.
 * MultimediaUrl
   * Gets a Multimedia Links�s Url
 * MultimediaUrlComponentLink
   * Gets a Multimedia Url from a Linked component
 * DateTimeField
   * Looks for the value in the DateTimeValues collection on IComponent
 * DateTimeComponentLinkField
   * Looks for the value in the DateTimeValues collection on a Linked IComponent
 * BooleanField
   * Converts the value into a boolean (handles the case for a field's string value of "Yes" = true. This can be configured using the StringValue property) keywords
 * BooleanComponentLinkField
   * Converts the value into a boolean from a component linked value
 * NumberField
   * Gets the double value from a number field
 * NumberComponentLinkField
   * Gets the double value from a number field of a linked component
 * NestedComponent
   * This is used to indicate that the property type is a submodel. For example a collection of carousel slides.
 * NestedKeyword
   * This is used to indicate that the property type is a submodel for keyword metadata
   * This field requires a tweak to the DD4T source before it will work (see following pull request: https://github.com/dd4t/dynamic-delivery-4-tridion/pull/5)

Options available on all attributes:

 * SchemaFieldName (string)
  * This is mandatory and is the first argument in the attribute�s constructor. It indicates which field to look for on the IComponent�s fields (or metadata fields) collection
 * IsMetadata (bool)
  * Indicates the code should look in the MetadataFields collection rather than the Fields collection
 * InlineEditable (bool)
  * Marks this field as available for Inline Editing. This will not output the markup by default. See further in the document for details of how this flag is used. (This may be removed as not needed)
 * IsMultiValue (bool)
  * This property is used to indicate that the Schema field may contain multiple values for this item and they should be mapped as an IEnumerable. The type of the property this is applied to should be IEnumerable (a future version may remove this and apply automatically if the type of property is IEnumerable).

Options available on Component Link attributes

 * ComponentFieldName (mandatory)
  * The field name on the Linked Component to search in. E.g. if the Component used in our Component Presentation contained a Component Link field called �link� to an Internal Link Component with a property caled �link_url� The value of SchemaFieldName would be �link� and the value of ComponentFieldName would be �link_url�

Options available on Nested Component attributes

 * TargetType (mandatory)
  * The type of the sub ViewModel. This should be set like typeof(CaroselItemViewModel). This is needed to convert the type to the correct type (only really used when IsMultivalue is set to true.

How to make a Component Presentation InlineEditable
===================================================
The Library adds a couple of HtmlHelper methods to make inline editting easier when using ViewModels.

To Mark the ComponentPresentation as inline editable
====================================================
Do the following at the top of the View file within the first containing element.

    @model Project.Web.Models.Components.StepCarouselViewModel
    @using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;
    <!-- Start: stepped carousel module -->
    <div class="header-component stepped-carousel">
        @Html.MarkComponentPresentationInlineEditable()
        <!-- More code -->
    </div>

To output the inline editable markup for a field on the root ViewModel use:

    <h1>@Html.InlineEditable(x => x.Title)</h1>

Note that this also needs a containing element. If one does not exist. Try adding a div or span. This can be used inplace of the outputting of the value so you don�t need to repeat yourself.

For submodels use:
Submodels are a little more complicated. This is because we need to pass the item and if we are looping over a list, the index of the linked component in the IComponentPresentation

    <div class=�items�>
    @{ int count = 0;} 
    @foreach(var item in Model.SubModels)
    {
        <h2>@Html.InlineEditable(item, x => x.HeadingLine2, count)</h2>
        <p>@Html.InlineEditable(item, x=>x.Summary, count)</p>
        @{ count  = count +1; }
    }
    </div>

Note that for this to work. The Submodel class must have the [TridionViewModel] attribute applied with the Nested property set to true with the correct ComponentLinkFieldName set. See above CarouselItemViewModel class for an example.

Also notice for multivalue nested components you need to pass an index for this to work. This is so that the correct component can be located on the LinkedComponentValues collection of the model.
To just get the JSON markup without the value (e.g. for Image tags)

    <div class=�imagemasksmall�>
        @Html.GetInlineEditableFieldMarkup(x=>x.Image)
        <img src=�@Model.Image� alt=�@Model.ImageAltText� />
    </div>

For Multivalue submodels the pass the index

    <div class=�items�>
    @{ int count = 0;} 
    @foreach(var item in Model.SubModels)
    {
        <div class=�imagemasksmall�>
             @Html.GetInlineEditableFieldMarkup(x=>x.Image, count)
             <img src=�@Model.Image� alt=�@Model.ImageAltText� />
        </div>
   	    @{ count  = count +1; }
    }
    </div>
Note that there is no need to pass the model in this case.
