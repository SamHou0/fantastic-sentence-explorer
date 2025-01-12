namespace fantastic_sentence_explorer.Data
{
    internal class Item
    {
        public string EnglishName;
        public string TranslationName { get; set; }
        public string OriginalName { get; set; }
        public string BangumiUrl { get; set; }
        public List<Sentence> Sentences { get; set; }
        public Item(string englishName, string translationName, string originalName, string bangumiUrl, List<Sentence> sentences)
        {
            EnglishName = englishName;
            TranslationName = translationName;
            OriginalName = originalName;
            BangumiUrl = bangumiUrl;
            Sentences = sentences ?? new List<Sentence>();
        }
        public override string ToString()
        {
            return EnglishName;
        }
    }
}
