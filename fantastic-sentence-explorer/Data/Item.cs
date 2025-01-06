using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fantastic_sentence_explorer.Data
{
    internal class Item
    {
        public string EnglishName;
        public string TranslationName { get; set; }
        public string OriginalName { get; set; }
        public string BangumiUrl { get; set; }
        public List<Sentence> Sentences { get;set; }
        public Item(string englishName = "Default Name",string translationName = "Default Name",string originalName = "Default Name",string bangumiUrl = "Default Url",List<Sentence> sentences = null)
        {
            EnglishName = englishName;
            TranslationName = translationName;
            OriginalName = originalName;
            BangumiUrl = bangumiUrl;
            Sentences = sentences ?? new List<Sentence>();
        }
        public override string ToString() {
            return EnglishName;
        }
    }
}
