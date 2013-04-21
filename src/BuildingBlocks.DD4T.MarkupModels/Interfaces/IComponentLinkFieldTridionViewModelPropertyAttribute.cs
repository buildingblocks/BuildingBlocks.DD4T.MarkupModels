namespace BuildingBlocks.DD4T.MarkupModels
{
    ///<summary>
    /// IComponentLinkViewModelPropertyAttribute is responsible for marking a property as using a Component Link
    ///
    /// Author: Robert Stevenson-Leggett
    ///</summary>
    public interface IComponentLinkFieldTridionViewModelPropertyAttribute
    {
        string ComponentFieldName { get; set; }

        bool IsLinkedFieldMetadata { get; set; }
    }
}