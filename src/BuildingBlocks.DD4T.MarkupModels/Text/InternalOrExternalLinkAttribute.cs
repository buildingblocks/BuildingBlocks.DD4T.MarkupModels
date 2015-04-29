using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BuildingBlocks.DD4T.MarkupModels.ExtensionMethods;

using DD4T.ContentModel;
using DD4T.ContentModel.Contracts.Providers;

namespace BuildingBlocks.DD4T.MarkupModels
{
    /// <summary>
    /// Resolves a Component Link using the LinkProvider if it starts with tcm: or just returns the value of the field.
    /// </summary>
    public class InternalOrExternalLinkAttribute : BaseStringTridionViewModelPropertyAttribute
    {
        public string ComponentFieldName { get; set; }

        public bool IsLinkedFieldMetadata { get; set; }

        public InternalOrExternalLinkAttribute(string schemaFieldName)
            : base(schemaFieldName)
        {
        }

        public override string GetValue(IFieldSet fields, IPage page = null)
        {
            var internalLink = fields.GetLinkedComponent(SchemaFieldName);
            
            string link = string.Empty;
            if(internalLink != null)
            {
                var linkProvider = DependencyResolver.Current.GetService<ILinkProvider>();

                if (linkProvider != null) link = page != null ? linkProvider.ResolveLink(page.Id, internalLink.Id, "") : linkProvider.ResolveLink(internalLink.Id);
            }
            else
            {
                //External Link
                return fields.GetValue(SchemaFieldName);
            }

            return link;
        }

        public override string GetValue(IComponent fields, IPage page = null)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<string> GetMultiValue(IFieldSet source, IPage page = null)
        {
            var internalLinks = source.GetLinkedComponentMultiValue(SchemaFieldName);

            if (internalLinks.Any())
            {
                var linkProvider = DependencyResolver.Current.GetService<ILinkProvider>();
                return internalLinks.Select(internalLink => page != null ? linkProvider.ResolveLink(page.Id, internalLink.Id, "") : linkProvider.ResolveLink(internalLink.Id));
            }

            return source.GetMultiValue(SchemaFieldName);
        }
    }
}
