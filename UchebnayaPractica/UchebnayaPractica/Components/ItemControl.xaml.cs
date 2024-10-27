using System.Windows.Controls;
using UchebnayaPractica.Database;
using System.Windows.Input;
using System.Windows;

namespace UchebnayaPractica.Components
{
    /// <summary>
    /// Логика взаимодействия для ItemControl.xaml
    /// </summary>
    public partial class ItemControl : UserControl
    {
        private ItemLocation itemLocation;
        public ItemControl(WorkshopItem item)
        {
            InitializeComponent();
            DataContext = item;
        }

        private void MainImage_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                itemLocation = new ItemLocation()
                {
                    item = DataContext as WorkshopItem
                };
                DragDrop.DoDragDrop(MainImage, new DataObject(DataFormats.Serializable,
                    itemLocation), DragDropEffects.Move);
            }
        }
    }
}
