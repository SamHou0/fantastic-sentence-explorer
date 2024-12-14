using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fantastic_sentence_explorer.Data
{
    internal class Sentence
    {
        public string Time { get; set; }
        public string Translation { get; set; }
        public string Original { get; set; }
        public Sentence(string time,string translation,string original)
        {
            Time = time;
            Translation = translation;
            Original = original;
        }
    }
}
