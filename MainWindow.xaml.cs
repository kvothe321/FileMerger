using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FileMerger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> Files { get; set; } = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            MainGrid.DataContext = this;
        }

        private async void OpenFileDialogClick(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                RootPathTextBox.Text = dialog.FileName;
            }
        }

        private async void MergeFilesButton_Click(object sender, RoutedEventArgs e)
        {
            isMerginFileProgressBar.Visibility = Visibility.Visible;
            FileContentTextBlock.Visibility = Visibility.Hidden;

            await MergeIntoDestinationFile();

            isMerginFileProgressBar.Visibility = Visibility.Hidden;
            FileContentTextBlock.Visibility = Visibility.Visible;
        }

        private async Task MergeIntoDestinationFile()
        {
            await Task.Delay(1000);

            foreach (string file in Files)
            {
                try
                {
                    // Open the text file using a stream reader.
                    using (var sr = new StreamReader(file))
                    {
                        // Read the stream as a string, and write the string to the console.
                        //FileContentTextBlock.Document.Blocks.Add(new Paragraph(new Run(sr.ReadToEnd())));
                        mergedFileContent.Text += sr.ReadToEnd() + "\n";
                    }
                }
                catch (IOException error)
                {
                    MessageBox.Show("The file could not be opened:\n" + error.Message);
                }
            }
            await WriteToFile();

            await Task.Delay(1000);
        }

        private async Task WriteToFile()
        {
            var destinationPath = DestinationPathTextBox.Text;
            var filestContent = new List<string>();
            filestContent.Add("");
            filestContent.Add(mergedFileContent.Text);
            try
            {
                await File.WriteAllLinesAsync(destinationPath + "\\merged.c", filestContent);
            }
            catch (Exception error)
            {
                MessageBox.Show("The file could not be opened:\n" + error.Message);
            }
        }

        private void FindFilesButton_Click(object sender, RoutedEventArgs e)
        {
            var rootPath = RootPathTextBox.Text;

            DirectoryInfo rootDirectory = new DirectoryInfo(RootPathTextBox.Text);
            treeView.ItemsSource = rootDirectory.GetDirectories();


            // TODO: try catch here
            var filePaths = Directory.GetFiles(
                path: rootPath,
                searchPattern: "*.*",
                searchOption: SearchOption.AllDirectories
            ).Where(s => s.EndsWith(".c") || s.EndsWith(".cpp")).ToList();

            foreach (string filePath in filePaths)
            {
                FileNamesTextBlock.Text += filePath + '\n';
                Files.Add(filePath);
            }

        }

        private void SetDestinationPathButton_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                DestinationPathTextBox.Text = dialog.FileName;
            }
        }
    }
}
