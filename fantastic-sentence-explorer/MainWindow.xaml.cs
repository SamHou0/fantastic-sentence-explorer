using fantastic_sentence_explorer.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using static Microsoft.VisualBasic.Interaction;

namespace fantastic_sentence_explorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string FolderPath { get; set; } = "";
        List<Item> items = new List<Item>();
        public MainWindow()
        {
            InitializeComponent();
            System.Windows.Forms.Application.EnableVisualStyles();  // Get rid of 20th 3D ui
            FolderPath = InputBox("请输入数据文件夹位置", "Fantastic Sentence Explorer");
            if (FolderPath == "")
            {
                Close();
                return;
            }
            // Button "Cancel" clicked
            while (!Directory.Exists(FolderPath))
            {
                FolderPath = InputBox("未找到对应文件夹，请重新输入", "Fantastic Sentence Explorer");
            }
            items = ItemParser.Parse(FolderPath);
            foreach (Item item in items)
            {
                fileList.Items.Add(item);
            }

        }

        private void FileList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            englishNameBox.Text = items[fileList.SelectedIndex].EnglishName;
            zhTranslationBox.Text = items[fileList.SelectedIndex].TranslationName;
            nameBox.Text = items[fileList.SelectedIndex].OriginalName;
            bangumiUrlBox.Text = items[fileList.SelectedIndex].BangumiUrl;
            sentenceGrid.ItemsSource = items[fileList.SelectedIndex].Sentences;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ItemParser.Save(items, FolderPath);
        }
    }
}