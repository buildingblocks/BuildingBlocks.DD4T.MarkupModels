using System.Web.Mvc;

namespace BuildingBlocks.DD4T.MarkupModels.ExtensionMethods
{
    ///<summary>
    /// StringExtensionMethods is responsible for PURPOSE
    /// Author: Robert Stevenson-Leggett
    /// Date: 2013-04-07
    ///</summary>
    public static class StringExtensionMethods
    {
        /// <summary>
        /// xhtml namespace uri
        /// </summary>
        private const string XhtmlNamespaceUri = "http://www.w3.org/1999/xhtml";
        /// <summary>
        /// xlink namespace uri
        /// </summary>
        private const string XlinkNamespaceUri = "http://www.w3.org/1999/xlink";

        /// <summary>
        /// removes unwanted namespace references (like xhtml and xlink) from the html
        /// </summary>
        /// <param name="html">html as a string</param>
        /// <returns>html as a string without namespace references</returns>
        public static string RemoveNamespaceReferences(this string html)
        {
            if (!string.IsNullOrEmpty(html))
            {
                html = html.Replace("xmlns=\"\"", "");
                html = html.Replace(string.Format("xmlns=\"{0}\"", XhtmlNamespaceUri), "");
                html = html.Replace(string.Format("xmlns:xhtml=\"{0}\"", XhtmlNamespaceUri), "");
                html = html.Replace(string.Format("xmlns:xlink=\"{0}\"", XlinkNamespaceUri), "");
            }

            return html;
        }

        /// <summary>
        /// Removes the namespaces and wraps in 'p' tag if not already wrapped.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string RemoveNamespacesAndWrapInParagraph(this string text)
        {
            string unparsedText = text;
            string htmlText = RemoveNamespaceReferences(unparsedText);
            if (!htmlText.StartsWith("<p"))
            {
                var tag = new TagBuilder("p");
                tag.InnerHtml = htmlText;
                htmlText = tag.ToString();
            }
            return htmlText;
        }
    }
}