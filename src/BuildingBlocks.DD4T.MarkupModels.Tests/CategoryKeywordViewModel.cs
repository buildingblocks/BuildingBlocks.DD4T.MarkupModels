namespace BuildingBlocks.DD4T.MarkupModels.Tests
{
    [TridionViewModel]
    public class CategoryKeywordViewModel : IKeywordBase
    {
        public string KeywordId { get; set; }
        public string KeywordValue { get; set; }
        public string KeywordKey { get; set; }

        [TextField("translated")]
        public string Translated { get; set; }
    }
}
