using fantastic_sentence_explorer.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using static Microsoft.VisualBasic.Interaction;

namespace fantastic_sentence_explorer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string FolderPath = "";
        List<Item> items;
        public MainWindow()
        {
            InitializeComponent();
            System.Windows.Forms.Application.EnableVisualStyles();  // Get rid of 20th 3D ui
            FolderPath = InputBox("请输入数据文件夹位置", "Fantansic Sentence Explorer");
            if (FolderPath == "") { Environment.Exit(0); }  // Button "Cancal" clicked
            while (!Directory.Exists(FolderPath)) 
            {
                FolderPath = InputBox("未找到对应文件夹，请重新输入", "Fantansic Sentence Explorer");
            }
            items = ItemParser.Parse(FolderPath);
            foreach (Item item in items)
            {
                FileList.Items.Add(item);
            }
            
        }

        private void FileList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnglishNameBox.Text = items[FileList.SelectedIndex].EnglishName;
            ZHTranslationBox.Text = items[FileList.SelectedIndex].TranslationName;
            NameBox.Text = items[FileList.SelectedIndex].OriginalName;
            BangumiUrlBox.Text=items[FileList.SelectedIndex].BangumiUrl;
            SentenceGrid.ItemsSource = items[FileList.SelectedIndex].Sentences;
        }
    }
}