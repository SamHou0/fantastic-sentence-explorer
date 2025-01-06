using fantastic_sentence_explorer.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace fantastic_sentence_explorer
{
    internal static class ItemParser
    {
        public static List<Item> Parse(string path)
        {
            List<Item> items = new List<Item>();
            string[] directories = Directory.GetDirectories(path);
            foreach (string dir in directories) {
                string[] content = File.ReadAllLines(dir + "\\zh.md");
                List<Sentence> sentences = new List<Sentence>();
                for (int i = 14; i < content.Length; i++) {
                    string[] sentenceContent = content[i].Split('|');
                    sentences.Add(new Sentence(sentenceContent[1],
                        sentenceContent[2], sentenceContent[3], sentenceContent[4]));
                }
                string urlContent = content[6].Split('|')[2];
                int urlIndex = urlContent.IndexOf('(') + 1;
                urlContent = urlContent.Substring(urlIndex);
                items.Add(new(content[0].Substring(2), content[8].Split('|')[2], content[7].Split('|')[2],                    urlContent.Remove(urlContent.Length - 1), sentences));
            }
            return items;
        }
        public async static Task SaveAsync(List<Item> items, string path)
        {
            await Task.Run(() => Save(items, path));
        }

        public static void Save(List<Item> items, string path)
        {

            // Clear the sub directories
            Directory.GetDirectories(path);
            foreach (string dir in Directory.GetDirectories(path))
            {
                Directory.Delete(dir, true);
            }
            foreach (Item item in items)
            {
                // Replace space with dash
                string dir = path + "\\" + item.EnglishName.Replace(" ", "-");
                Directory.CreateDirectory(dir);
                // Create the markdown file
                string[] content = new string[item.Sentences.Count + 14];
                content[0] = "# " + item.EnglishName;
                content[1] = "";
                content[2] = "## 信息";
                content[3] = "";
                content[4] = "|项|详细信息|";
                content[5] = "|-|-|";
                content[6] = "|链接|[Bangumi](" + item.BangumiUrl + ")|";
                content[7] = "|原名|" + item.OriginalName + "|";
                content[8] = "|中文译名|" + item.TranslationName + "|";
                content[9] = "";
                content[10] = "## 金句";
                content[11] = "";
                content[12] = "|原文|译文|时间点|解析|";
                content[13] = "|-|-|-|-|";
                for (int i = 0; i < item.Sentences.Count; i++)
                {
                    content[i + 14] = "|" + item.Sentences[i].Original + "|" + item.Sentences[i].Translation + "|" +
                        item.Sentences[i].Time + "|" + item.Sentences[i].Explanation + "|";
                }
                File.WriteAllLines(dir + "\\zh.md", content);
            }
        }
    }
}
