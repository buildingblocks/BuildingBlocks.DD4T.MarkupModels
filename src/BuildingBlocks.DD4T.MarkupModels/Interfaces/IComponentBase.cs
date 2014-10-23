namespace BuildingBlocks.DD4T.MarkupModels
{
    ///<summary>
    /// IComponentLinkViewModelPropertyAttribute is responsible for marking a property as using a Component Link
    ///
    /// Author: John Askew
    ///</summary>
    public interface IComponentBase
    {
        [TextField("id")]
        string ComponentId { get; set; }

        string Schema { get; set; }
    }
}
