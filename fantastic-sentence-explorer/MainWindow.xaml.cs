using fantastic_sentence_explorer.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Microsoft.VisualBasic.Interaction;

namespace fantastic_sentence_explorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Whether the data of text boxes is changing
        /// </summary>
        bool isChanging = false;
        public static string FolderPath { get; set; } = "";
        List<Item> items = new List<Item>();
        public MainWindow()
        {
            InitializeComponent();
            Title = App.DisplayName;
            FolderPath = InputBox("请输入数据文件夹位置", App.DisplayName);
            if (FolderPath == "")
            {
                Close();
                return;
            }
            // Button "Cancel" clicked
            while (!Directory.Exists(FolderPath))
            {
                FolderPath = InputBox("未找到对应文件夹，请重新输入", App.DisplayName);
            }
            pathTextBox.Text = FolderPath;
            loadItems(FolderPath);
        }

        private void loadItems(string path, int flag = 0)
        {
            try
            {
                items = ItemParser.Parse(path);
                fileList.ItemsSource = items;
                FolderPath = path;
                pathTextBox.Text = path;
                fileList.Focus();
            } catch
            {
                switch (flag)
                {
                    case 0:
                        MessageBox.Show("读取失败，请检查文件夹内容是否正确", App.DisplayName, MessageBoxButton.OK, MessageBoxImage.Error);
                        pathTextBox.Text = FolderPath;
                        pathTextBox.Focus();
                        pathTextBox.SelectAll();
                        break;
                    case 1:
                        pathTextBox.Text = FolderPath;
                        break;
                    default:
                        break;
                }
            }
        }
        #region FileList
        private void FileList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fileList.SelectedIndex >= 0)
            {
                isChanging = true;
                englishNameBox.Text = items[fileList.SelectedIndex].EnglishName;
                zhTranslationBox.Text = items[fileList.SelectedIndex].TranslationName;
                nameBox.Text = items[fileList.SelectedIndex].OriginalName;
                bangumiUrlBox.Text = items[fileList.SelectedIndex].BangumiUrl;
                sentenceGrid.ItemsSource = items[fileList.SelectedIndex].Sentences;
                isChanging = false;
            }
        }
        /// <summary>
        /// Context menu for file list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteFileMenu_Click(object sender, RoutedEventArgs e)
        {
            if (fileList.SelectedIndex >= 0)
            {
                items.RemoveAt(fileList.SelectedIndex);
            }
            fileList.Items.Refresh();
        }
        #endregion
        #region Buttons
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            saveButton.IsEnabled = false;
            saveButton.DataContext = "正在保存";
            await ItemParser.SaveAsync(items, FolderPath);
            saveButton.IsEnabled = true;
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (fileList.SelectedIndex >= 0)
            {
                items[fileList.SelectedIndex].Sentences.Add(new Sentence("", "", "", ""));
            }
            sentenceGrid.Items.Refresh();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            items.Add(new Item("新的文件", "", "", "", new List<Sentence>()));
            fileList.Items.Refresh();
            fileList.SelectedIndex = items.Count - 1;
        }
        #endregion
        /// <summary>
        /// Save the data when closing the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsLoaded)
            {
                MessageBoxResult result = MessageBox.Show("要保存更改吗？", App.DisplayName, MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    ItemParser.Save(items, FolderPath);
                } else if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (fileList.SelectedIndex >= 0 && isChanging == false)
            {
                items[fileList.SelectedIndex].EnglishName = englishNameBox.Text;
                items[fileList.SelectedIndex].TranslationName = zhTranslationBox.Text;
                items[fileList.SelectedIndex].OriginalName = nameBox.Text;
                items[fileList.SelectedIndex].BangumiUrl = bangumiUrlBox.Text;
            }
            if (sender == englishNameBox)
            {
                fileList.Items.Refresh();
            }
        }

        private void PathTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            loadItems(pathTextBox.Text);
        }

        private void PathTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                loadItems(pathTextBox.Text);
            } else if (e.Key == Key.Escape)
            {
                pathTextBox.Text = FolderPath;
            }
        }
    }
}