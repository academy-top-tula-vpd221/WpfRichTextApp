using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfRichTextApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CommandBinding binding;
        public MainWindow()
        {
            InitializeComponent();

            binding = new CommandBinding();
            binding.Command = ApplicationCommands.Save;
            binding.Executed += Save_Executed;
            binding.CanExecute += Save_CanExecute;
            
            menuSave.CommandBindings.Add(binding);
            menuSave.Command = ApplicationCommands.Save;
            toolSave.CommandBindings.Add(binding);
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //e.CanExecute = richEditor.Document != null;
            var start = richEditor.Document.ContentStart;
            var end = richEditor.Document.ContentEnd;
            e.CanExecute = (start.GetOffsetToPosition(end) != 0);
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|RichText Files (*.rtf)|*.rtf|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                TextRange range = new(richEditor.Document.ContentStart, richEditor.Document.ContentEnd);
                using (FileStream stream = File.Create(saveFileDialog.FileName))
                {
                    if (System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower() == ".rtf")
                        range.Save(stream, DataFormats.Rtf);
                    else if (System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower() == "*.txt")
                        range.Save(stream, DataFormats.Text);
                    else
                        range.Save(stream, DataFormats.Xaml);

                }
            }
        }

        

        


        //private void Save_Click(object sender, RoutedEventArgs e)
        //{
        //    SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    saveFileDialog.Filter = "Text Files (*.txt)|*.txt|RichText Files (*.rtf)|*.rtf|All Files (*.*)|*.*";
        //    if(saveFileDialog.ShowDialog() == true)
        //    {
        //        TextRange range = new(richText.Document.ContentStart, richText.Document.ContentEnd);
        //        using(FileStream stream = File.Create(saveFileDialog.FileName))
        //        {
        //            if (System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower() == ".rtf")
        //                range.Save(stream, DataFormats.Rtf);
        //            else if (System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower() == "*.txt")
        //                range.Save(stream, DataFormats.Text);
        //            else
        //                range.Save(stream, DataFormats.Xaml);

        //        }
        //    }
        //}

        //private void Open_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    openFileDialog.Filter = "Text Files (*.txt)|*.txt|RichText Files (*.rtf)|*.rtf|All Files (*.*)|*.*";

        //    if(openFileDialog.ShowDialog() == true)
        //    {
        //        TextRange range = new(richText.Document.ContentStart, richText.Document.ContentEnd);
        //        using(FileStream stream = File.Open(openFileDialog.FileName, FileMode.Open))
        //        {
        //            if (System.IO.Path.GetExtension(openFileDialog.FileName).ToLower() == ".rtf")
        //                range.Load(stream, DataFormats.Rtf);
        //            else if (System.IO.Path.GetExtension(openFileDialog.FileName).ToLower() == "*.txt")
        //                range.Load(stream, DataFormats.Text);
        //            else
        //                range.Load(stream, DataFormats.Xaml);
        //        }
        //    }
        //}

        //private void Bold_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
