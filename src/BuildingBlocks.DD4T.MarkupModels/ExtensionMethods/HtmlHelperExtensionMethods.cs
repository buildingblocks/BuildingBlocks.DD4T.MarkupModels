using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

using DD4T.ContentModel;
using DD4T.Mvc.Html;
using DD4T.Mvc.SiteEdit;

namespace BuildingBlocks.DD4T.MarkupModels.ExtensionMethods
{
    ///<summary>
    /// HtmlHelperExtensionMethods is responsible for adding Experience Manager JSON
    /// Author: Robert Stevenson-Leggett
    /// Date: 2013-04-13
    ///</summary>
    public static class HtmlHelperExtensionMethods
    {
        public static MvcHtmlString MarkComponentPresentationInlineEditable(this HtmlHelper helper)
        {
            if (!SiteEditService.SiteEditSettings.Enabled)
                return new MvcHtmlString(string.Empty);

            var componentPresentation = GetComponentPresentation(helper);
                
            if(componentPresentation != null)
            {
                return helper.SiteEditComponentPresentation(componentPresentation);
            }

            return new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString MarkComponentPresentationInlineEditable<T>(this HtmlHelper helper, T model, int index)
        {
            if(!SiteEditService.SiteEditSettings.Enabled)
                return new MvcHtmlString(string.Empty);

            var attribute = (TridionViewModelAttribute)model.GetType().GetCustomAttributes(typeof(TridionViewModelAttribute), true).FirstOrDefault();
            if (attribute != null && !String.IsNullOrEmpty(attribute.ParentLinkFieldName) &&
                attribute.ParentLinkType != ParentLinkType.Multimedia)
            {
                var parentComponentPresentation = GetComponentPresentation(helper);

                foreach (string parentLinkFieldName in attribute.ParentLinkFieldName.Split(','))
                {
                    IComponent linkedComponent = null;
                    if (parentComponentPresentation.Component.Fields.ContainsKey(parentLinkFieldName))
                    {
                        linkedComponent =
                            parentComponentPresentation.Component.Fields[parentLinkFieldName].LinkedComponentValues[
                                index];
                    }
                    if (linkedComponent != null)
                    {
                        return helper.SiteEditComponentPresentation(linkedComponent,
                                                                    parentComponentPresentation.ComponentTemplate.Id,
                                                                    false,
                                                                    string.Empty);
                        break;
                    }
                }
            }
            else
            {
                return MarkComponentPresentationInlineEditable(helper);
            }
            return new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString InlineEditable<T,TP>(this HtmlHelper<T> helper, Expression<Func<T,TP>> fieldSelector)
        {
            T model = helper.ViewData.Model;
            return InlineEditableInternal(helper, model, fieldSelector, 0);
        }

        public static MvcHtmlString InlineEditable<T,TP>(this HtmlHelper helper, T model, Expression<Func<T,TP>> fieldSelector)
        {
            return InlineEditableInternal(helper, model, fieldSelector, 0);
        }
        
        public static MvcHtmlString InlineEditable<T, TP>(this HtmlHelper helper, T model, Expression<Func<T, TP>> fieldSelector, int index)
        {
            return InlineEditableInternal(helper, model, fieldSelector, index);
        }

        private static MvcHtmlString InlineEditableInternal<T,TP>(this HtmlHelper helper, T model, Expression<Func<T, TP>> fieldSelector, int index)
        {
            if (!SiteEditService.SiteEditSettings.Enabled)
                return new MvcHtmlString(string.Empty);

            Func<T, TP> compiledFieldSelector = fieldSelector.Compile();
            TP value = compiledFieldSelector(model);
            var sb = new StringBuilder();
            sb.Append(GetInlineEditableMarkupInternal(helper, fieldSelector, index));
            sb.Append(value);
            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString GetInlineEditableFieldMarkup<T,TP>(this HtmlHelper helper, T model, Expression<Func<T,TP>> fieldSelector, int index)
        {
            return GetInlineEditableMarkupInternal(helper, fieldSelector, index);
        }        
        
        public static MvcHtmlString GetInlineEditableFieldMarkup<T,TP>(this HtmlHelper<T> helper, Expression<Func<T,TP>> fieldSelector)
        {
            return GetInlineEditableMarkupInternal(helper, fieldSelector, 0);
        }

        public static MvcHtmlString GetInlineEditableFieldMarkup<T,TP>(this HtmlHelper helper, Expression<Func<T,TP>> fieldSelector)
        {
            return GetInlineEditableMarkupInternal(helper, fieldSelector, 0);
        }

        public static MvcHtmlString GetInlineEditableFieldMarkup<T, TP>(this HtmlHelper helper, Expression<Func<T, TP>> fieldSelector, int index)
        {
            return GetInlineEditableMarkupInternal(helper, fieldSelector, index);
        }

        private static MvcHtmlString GetInlineEditableMarkupInternal<T, TP>(HtmlHelper helper, Expression<Func<T, TP>> fieldSelector, int index)
        {
            if (!SiteEditService.SiteEditSettings.Enabled)
                return new MvcHtmlString(string.Empty);
            
            var componentPresentation = GetComponentPresentation(helper);

            if (componentPresentation != null)
            {
                var member = (MemberExpression)fieldSelector.Body;
                var attributes = member.Member.GetCustomAttributes(typeof(ITridionViewModelPropertyAttribute), true);
                if (attributes.Any())
                {
                    var attribute = (ITridionViewModelPropertyAttribute)attributes.First();
                    if (attribute.InlineEditable)
                    {
                        var viewModelAttribute = (TridionViewModelAttribute)member.Member.DeclaringType.GetCustomAttributes(typeof(TridionViewModelAttribute), true).FirstOrDefault();
                        IFieldSet fields;
                        var component = componentPresentation.Component;
                        if (viewModelAttribute != null && viewModelAttribute.ParentLinkType!=ParentLinkType.None)
                        {
                            fields = attribute.IsMetadata ? componentPresentation.Component.MetadataFields : componentPresentation.Component.Fields;    
                            foreach (string parentLinkFieldName in viewModelAttribute.ParentLinkFieldName.Split(','))
                            {
                                if (
                                    componentPresentation.Component.Fields.ContainsKey(
                                        parentLinkFieldName) &&
                                    viewModelAttribute.ParentLinkType != ParentLinkType.Multimedia)
                                {
                                    if (viewModelAttribute.ParentLinkType == ParentLinkType.Nested)
                                    {
                                        var linkedComponent =
                                            componentPresentation.Component.Fields[parentLinkFieldName].LinkedComponentValues[index
                                                ];
                                        fields = linkedComponent.Fields;
                                    }
                                    else
                                    {
                                        fields =
                                            componentPresentation.Component.Fields[parentLinkFieldName].EmbeddedValues[index];
                                    }
                                    break;
                                }
                                else
                                {
                                    fields = attribute.IsMetadata
                                                 ? componentPresentation.Component.MetadataFields
                                                 : componentPresentation.Component.Fields;
                                }
                            }
                        }
                        else
                        {
                            fields = attribute.IsMetadata
                                         ? componentPresentation.Component.MetadataFields
                                         : componentPresentation.Component.Fields;    
                        }

                        string schemaFieldName = String.Empty;
                        if (viewModelAttribute.ParentLinkType == ParentLinkType.Multimedia)
                        {
                            schemaFieldName = viewModelAttribute.ParentLinkFieldName;
                        }
                        else
                        {
                            schemaFieldName = attribute.SchemaFieldName;
                        }

                        if(fields.ContainsKey(schemaFieldName))
                        {
                            var field = fields[schemaFieldName];

                            if (attribute is IComponentLinkFieldTridionViewModelPropertyAttribute)
                            {
                                var clAttribute = (IComponentLinkFieldTridionViewModelPropertyAttribute)attribute;
                                var clFields = clAttribute.IsLinkedFieldMetadata
                                                   ? field.LinkedComponentValues[0].MetadataFields
                                                   : field.LinkedComponentValues[0].Fields;
                                field = clFields[clAttribute.ComponentFieldName];
                                return helper.SiteEditField(component, field);
                            }

                            return helper.SiteEditField(component, field);   
                        }
                    }
                }
            }

            return new MvcHtmlString(string.Empty);  
        }

        private static IComponentPresentation GetComponentPresentation(HtmlHelper helper)
        {
            return helper.ViewContext.RouteData.Values["componentPresentation"] as IComponentPresentation;
        }
    }
}