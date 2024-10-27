using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using UchebnayaPractica.Components;
using UchebnayaPractica.Database;

namespace UchebnayaPractica.Windows
{
    /// <summary>
    /// Логика взаимодействия для DocumentsWindow.xaml
    /// </summary>
    public partial class DocumentsWindow : Window
    {
        public List<Document> documents;
        bool canEdit;
        public DocumentsWindow(List<Document> documents, bool canEdit)
        {
            InitializeComponent();
            this.documents = documents;
            this.canEdit = canEdit;
            AddBtn.Visibility = canEdit ? Visibility.Visible : Visibility.Collapsed;
            Refresh();
        }
        public void RemoveDocument(Document document)
        {
            try
            {
                documents.Remove(document);
                if (document.Id != 0)
                    App.db.Document.Remove(document);
                App.db.SaveChanges();
                Methods.TakeInformation("Успешно удалено!");
            }
            catch(Exception ex) { Methods.TakeWarning("Ошибка при удалении документа!\n" +
                ex.Message); }
        }

        public void Refresh()
        {
            MyPanel.Children.Clear();
            foreach (Document document in documents)
            {
                MyPanel.Children.Add(new DocumentControl(document, this, canEdit));
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                documents.Add(new Document()
                {
                    Bytes = File.ReadAllBytes(openFileDialog.FileName),
                    Name = Path.GetFileNameWithoutExtension(openFileDialog.FileName),
                    Format = Path.GetExtension(openFileDialog.FileName),
                });
                Refresh();
                Methods.TakeInformation("Документ успешно добавлен!");
            }
        }
    }
}
