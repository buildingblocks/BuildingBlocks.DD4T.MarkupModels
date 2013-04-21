using System.Collections.Generic;

using DD4T.ContentModel;

namespace BuildingBlocks.DD4T.MarkupModels
{
    /// <summary>
    /// Strongly typed interface indicating that the View Model Property should be converted to the type T when extracted from the Component
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITridionViewModelPropertyAttribute<out T> : ITridionViewModelPropertyAttribute
    {
        T GetValue(IComponent component);

        IEnumerable<T> GetMultiValue(IComponent component);
    }

    ///<summary>
    /// ITridionViewModelPropertyAttribute is an interface to define the common fields between the different attributes
    ///
    /// Author: Robert Stevenson-Leggett
    ///</summary>
    public interface ITridionViewModelPropertyAttribute
    {
        string SchemaFieldName { get; set; }

        bool IsMetadata { get; set; }

        bool InlineEditable { get; set; }

        bool IsMultiValue { get; set; }
    }
}