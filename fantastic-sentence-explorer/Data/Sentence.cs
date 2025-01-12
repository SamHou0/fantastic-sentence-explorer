namespace fantastic_sentence_explorer.Data
{
    internal class Sentence
    {
        public string Time { get; set; }
        public string Translation { get; set; }
        public string Original { get; set; }
        public string Explanation { get; set; }
        public Sentence(string original, string translation, string time, string explanation)
        {
            Original = original;
            Translation = translation;
            Time = time;
            Explanation = explanation;
        }
    }
}
