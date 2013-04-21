using System;
using System.Collections.Generic;
using System.Linq;
using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;
using DD4T.ContentModel;
using DD4T.ContentModel.Factories;
using Moq;
using NUnit.Framework;

namespace BuildingBlocks.DD4T.MarkupModels.Tests
{
    [TestFixture]
    public class ViewModelBuilderTests
    {
        [SetUp]
        public void SetUp()
        {
            RichTextHelper.LinkFactory = new Mock<ILinkFactory>().Object;
        }

        [Test]
        public void Builder_Can_Map_A_TextField_Has_Had_The_TextFieldAttribute_Applied()
        {
            var component = new Component();
            component.Fields.Add("title_line_1", new Field() { Values = { "test title" } });

            var headingModel = ComponentViewModelBuilder.Build<HeadingViewModel>(component);

            Assert.That(headingModel.TitleLine1, Is.EqualTo("test title"));
        }

        [Test]
        public void Builder_Can_Build_A_ComponentLinkField_That_Has_Had_The_ComponentLinkFieldAttributeApplied()
        {
            var linkedComponent = new Component();
            linkedComponent.Fields.Add("link_url", new Field() { Values = { "http://www.google.co.uk" } });
            linkedComponent.Fields.Add("link_text", new Field() { Values = { "Find it on google" } });

            var component = new Component();
            component.Fields.Add("link", new Field() { LinkedComponentValues = new List<Component>() { linkedComponent } });

            var headingModel = ComponentViewModelBuilder.Build<HeadingViewModel>(component);

            Assert.That(headingModel.RelatedLinkUrl, Is.EqualTo("http://www.google.co.uk"));
            Assert.That(headingModel.RelatedLinkText, Is.EqualTo("Find it on google"));
        }

        [Test]
        public void Builder_Can_Build_A_ImageUrlField_That_Has_Had_The_MultimediaUrlAttributeApplied()
        {
            var linkedComponent = new Component();
            linkedComponent.Multimedia = new Multimedia() { Url = "_images/test.png" };
            linkedComponent.MetadataFields.Add("AltText", new Field() { Values = { "Some alt text" } });

            var component = new Component();
            component.Fields.Add("image", new Field() { LinkedComponentValues = new List<Component>() { linkedComponent } });

            var headingModel = ComponentViewModelBuilder.Build<HeadingViewModel>(component);

            Assert.That(headingModel.ImageUrl, Is.EqualTo("_images/test.png"));
            Assert.That(headingModel.ImageAltText, Is.EqualTo("Some alt text"));
        }

        [Test]
        public void Builder_Can_Build_A_Multivalue_Image_Field_Magically()
        {
            var linkedComponent = new Component();
            linkedComponent.Multimedia = new Multimedia() { Url = "_images/test.png" };
            linkedComponent.MetadataFields.Add("AltText", new Field() { Values = { "Some alt text" } });

            var linkedComponent2 = new Component();
            linkedComponent2.Multimedia = new Multimedia() { Url = "_images/test.png" };
            linkedComponent2.MetadataFields.Add("AltText", new Field() { Values = { "Some alt text" } });

            var linkedComponent3 = new Component();
            linkedComponent3.Multimedia = new Multimedia() { Url = "_images/test.png" };
            linkedComponent3.MetadataFields.Add("AltText", new Field() { Values = { "Some alt text" } });

            var linkedComponen4 = new Component();
            linkedComponen4.Multimedia = new Multimedia() { Url = "_images/test.png" };
            linkedComponen4.MetadataFields.Add("AltText", new Field() { Values = { "Some alt text" } });

            var component = new Component();
            component.Fields.Add("thumbnail_images", new Field() { LinkedComponentValues = new List<Component>() { linkedComponent, linkedComponent2, linkedComponent3, linkedComponen4 } });

            var headingModel = ComponentViewModelBuilder.Build<HeadingViewModel>(component);

            Assert.That(headingModel.ThumbnailImages, Has.Count.EqualTo(4));
        }

        [Test]
        public void Builder_Can_Build_A_Multivalue_Metadata_text_field()
        {
            var component = new Component();
            component.MetadataFields.Add("bullet_points", new Field() { Values = {"test1","test2","test3"} });

            var headingModel = ComponentViewModelBuilder.Build<HeadingViewModel>(component);

            Assert.That(headingModel.SomeMultiValueText, Has.Count.EqualTo(3));
        }

        [Test] 
        public void Builder_Can_Convert_To_Boolean_On_A_Metadata_Field()
        {
            var component = new Component();
            component.MetadataFields.Add("show_social_links", new Field() { Values = {"yes"} });

            var headingModel = ComponentViewModelBuilder.Build<HeadingViewModel>(component);

            Assert.That(headingModel.ShowSocialLinks, Is.EqualTo(true));
        }

        [Test]
        public void Builder_Can_Convert_A_List_Of_ComponentLinked_Booleans()
        {
            var linkedComponent = new Component();
            linkedComponent.Fields.Add("show", new Field() {Values = {"yes"}});

            var linkedComponent2 = new Component();
            linkedComponent2.Fields.Add("show", new Field() {Values = {"no"}});

            var linkedComponent3 = new Component();
            linkedComponent3.Fields.Add("show", new Field() {Values = {"yes"}});

            var component = new Component();
            component.Fields.Add("featured", new Field() { LinkedComponentValues = { linkedComponent, linkedComponent2, linkedComponent3 } });

            var headingModel = ComponentViewModelBuilder.Build<HeadingViewModel>(component);

            Assert.That(headingModel.CheckBoxes.First(),Is.EqualTo(true));
        }

        [Test]
        public void Builder_Can_Build_A_Carousel_Using_The_Nested_Component_Attribute()
        {
            var slide1 = new Component();
            slide1.Fields.Add("heading", new Field() { Values = {"Slide 1"}});
            slide1.Fields.Add("summary", new Field() { Values = {"Slide 1 Summary"}});
            slide1.Fields.Add("image", new Field() { LinkedComponentValues = { new Component() { Multimedia = new Multimedia() { Url = "_images/asd.png" }}}});

            var slide2 = new Component();
            slide2.Fields.Add("heading", new Field() { Values = { "Slide 2" } });
            slide2.Fields.Add("summary", new Field() { Values = { "Slide 2 Summary" } });
            slide2.Fields.Add("image", new Field() { LinkedComponentValues = { new Component() { Multimedia = new Multimedia() { Url = "_images/asdc.png" } } } });

            var carousel = new Component();
            carousel.Fields.Add("title", new Field() { Values = { "My Carousel" }});
            carousel.Fields.Add("list", new Field() { LinkedComponentValues = {slide1,slide2}});

            var carouselModel = ComponentViewModelBuilder.Build<CarouselViewModel>(carousel);

            Assert.That(carouselModel.CarouselItemViewModels.Count(), Is.EqualTo(2));
            Assert.That(carouselModel.CarouselItemViewModels.First().Heading, Is.EqualTo("Slide 1"));
            Assert.That(carouselModel.CarouselItemViewModels.Skip(1).First().Heading, Is.EqualTo("Slide 2"));
        }

        [Test]
        public void Builder_Can_Build_MultiValue_ComponentLinked_DateField()
        {
            //Arrange
            var linkedComponent = new Component();
            linkedComponent.Fields.Add("dates", new Field() { DateTimeValues = { DateTime.Now, DateTime.Now.AddDays(1) }});

            var component = new Component(); 
            component.Fields.Add("date_component",new Field() { LinkedComponentValues = { linkedComponent }});
                
            //Act
            var model = ComponentViewModelBuilder.Build<HeadingViewModel>(component); 

            //Assert
            Assert.That(model.Dates, Has.Count.EqualTo(2));
        }
    }
}