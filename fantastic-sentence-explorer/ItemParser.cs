using fantastic_sentence_explorer.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fantastic_sentence_explorer
{
    internal static class ItemParser
    {
        public static List<Item> Parse(string path)
        {
            List<Item> items = new List<Item>();
            string[] directories = Directory.GetDirectories(path);
            foreach (string dir in directories)
            {
                string[] content = File.ReadAllLines(dir + "\\zh.md");
                List<Sentence> sentences = new List<Sentence>();
                for (int i = 14; i < content.Length; i++)
                {
                    string[] sentenceContent = content[i].Split('|');
                    sentences.Add(new Sentence(sentenceContent[3], 
                        sentenceContent[2], sentenceContent[1]));

                }
                string urlContent = content[6].Split('|')[2];
                int urlIndex = urlContent.IndexOf('(')+1;
                urlContent = urlContent.Substring(urlIndex);
                items.Add(new(content[0].Substring(2), content[8].Split('|')[2], content[7].Split('|')[2],
                    urlContent.Remove(urlContent.Length-1), sentences));
            }
            return items;

        }
    }
}
