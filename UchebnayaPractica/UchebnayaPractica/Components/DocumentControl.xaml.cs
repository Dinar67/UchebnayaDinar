using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using UchebnayaPractica.Database;
using UchebnayaPractica.Windows;
using System.Windows;

namespace UchebnayaPractica.Components
{
    /// <summary>
    /// Логика взаимодействия для DocumentControl.xaml
    /// </summary>
    public partial class DocumentControl : UserControl
    {
        private Document document;
        private DocumentsWindow window;
        public DocumentControl(Document document, DocumentsWindow window, bool canEdit)
        {
            InitializeComponent();
            this.document = document;
            this.window = window;
            DataContext = document;
            Trash.Visibility = canEdit? Visibility.Visible : Visibility.Collapsed;
        }
        private void Trash_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Methods.TakeChoice("Вы точно хотите удалить документ?"))
            {
                window.RemoveDocument(document);
                window.Refresh();
            }
        }

        private void Save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                FileName = document.Name,
                DefaultExt = document.Format,
            };
            if (dialog.ShowDialog() == true)
            {
                File.WriteAllBytes(dialog.FileName, document.Bytes);
                Methods.TakeInformation("Файл успешно сохранен на компьютере!");
            }
        }
    }
}
