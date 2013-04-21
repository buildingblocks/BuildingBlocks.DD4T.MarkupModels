using System.Collections.Generic;
using BuildingBlocks.DD4T.MarkupModels.Nested;

namespace BuildingBlocks.DD4T.MarkupModels.Tests
{
    ///<summary>
    /// CarouselViewModel
    /// Author: Robert Stevenson-Leggett
    /// Date: 2013-04-10
    ///</summary>
    [TridionViewModel(SchemaTitle = "Component List")]
    public class CarouselViewModel
    {
        [TextField("title")]
        public string Title { get; set; }

        [NestedComponent("list", typeof(CarouselItemViewModel), IsMultiValue = true)]
        public IEnumerable<CarouselItemViewModel> CarouselItemViewModels { get; set; }
    }
}