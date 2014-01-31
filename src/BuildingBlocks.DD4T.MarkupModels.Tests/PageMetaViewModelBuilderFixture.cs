using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;
using DD4T.ContentModel;
using DD4T.ContentModel.Contracts.Providers;
using DD4T.ContentModel.Factories;
using Moq;
using NUnit.Framework;

namespace BuildingBlocks.DD4T.MarkupModels.Tests
{

    [TestFixture]
    public class PageMetaViewModelBuilderFixture
    {
        // TODO: Can we mock this object as dynamic at runtime?
        [TridionViewModel]
        public class PageMetaViewModel
        {
            [TextField("title", IsMetadata = true)]
            public string Title { get; set; }

            [InternalOrExternalLink("link", IsMetadata = true)]
            public string Link { get; set; }
        }

        [SetUp]
        public void Setup()
        {
            RichTextHelper.LinkFactory = new Mock<ILinkFactory>().Object;
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PageBuilder_WhenPassedNullPage_ThrowsArgumentNullException()
        {
            Page p = null;
            PageMetaViewModelBuilder.Build<PageMetaViewModel>(p);
        }

        [Test]
        public void PageBuilder_CanMapTextFieldToString()
        {
            Page p = new Page();
            p.MetadataFields = new FieldSet();
            p.MetadataFields.Add("title", new Field {Values = new List<string> {"foo"}});

            var model = PageMetaViewModelBuilder.Build<PageMetaViewModel>(p);
            Assert.That(model.Title == "foo");
        }

        [Test]
        public void PageBuilder_CanMapInternalLinkToString()
        {
            Page p = new Page();
            p.MetadataFields = new FieldSet();
            p.MetadataFields.Add("link",
                                 new Field()
                                     {
                                         LinkedComponentValues = new List<Component>{new Component {Id = "tcm:0-000-16"}}
                                     });

            // Mock link provider to resolve link
            var linkProvider = new Mock<ILinkProvider>();
            linkProvider.Setup(o => o.ResolveLink("tcm:0-000-16")).Returns("http://foo.com");

            // Mock dependency resolver to return link provider
            var dependencyResolver = new Mock<IDependencyResolver>();
            dependencyResolver.Setup(o => o.GetService(typeof(ILinkProvider))).Returns(linkProvider.Object);
            DependencyResolver.SetResolver(dependencyResolver.Object);

            var result = PageMetaViewModelBuilder.Build<PageMetaViewModel>(p);
            Assert.That(result.Link == "http://foo.com");
        }

        [Test]
        public void PageBuilder_CanMapExternalLinkToString()
        {
            Page p = new Page();
            p.MetadataFields = new FieldSet();
            p.MetadataFields.Add("link",
                                 new Field()
                                 {
                                     Values = new List<string> { "http://foo.com" }
                                 });

            var result = PageMetaViewModelBuilder.Build<PageMetaViewModel>(p);
            Assert.That(result.Link == "http://foo.com");
        }
    }
}
