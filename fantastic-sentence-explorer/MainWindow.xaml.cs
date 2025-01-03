using fantastic_sentence_explorer.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace fantastic_sentence_explorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string FolderPath;
        List<Item> items;
        public MainWindow()
        {
            InitializeComponent();
            FolderChooseWindow folderChooseWindow = new FolderChooseWindow();
            folderChooseWindow.ShowDialog();
            if (DialogResult == false)
            {
                Close();
                return;
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