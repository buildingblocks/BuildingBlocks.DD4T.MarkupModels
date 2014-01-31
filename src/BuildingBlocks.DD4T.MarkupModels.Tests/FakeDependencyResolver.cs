using System;
using System.Collections.Generic;
using System.Web.Mvc;

using DD4T.ContentModel.Contracts.Providers;

using Moq;

namespace BuildingBlocks.DD4T.MarkupModels.Tests
{
    public class FakeDependencyResolver : IDependencyResolver
    {
        public Mock<ILinkProvider> LinkProviderMock { get; set; }

        public FakeDependencyResolver()
        {
            LinkProviderMock = new Mock<ILinkProvider>();
        }

        public object GetService(Type serviceType)
        {
            if(serviceType == typeof(ILinkProvider))
            {
                return LinkProviderMock.Object;
            }
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}