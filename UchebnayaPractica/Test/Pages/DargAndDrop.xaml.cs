using System;
using System.Collections.Generic;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using Test.Components;
using System.IO;

namespace Test.Pages
{
    /// <summary>
    /// Логика взаимодействия для DargAndDrop.xaml
    /// </summary>
    public partial class DargAndDrop : Page
    {
        Item sword;
        double width = 0f;
        double height = 0f;
        public DargAndDrop()
        {
            InitializeComponent();
            sword = new Item()
            {
                Name = "Main Sword",
                ItRectangle = rectangle
            };
            if (File.Exists(@"rectanglePosition.txt"))
            {
                string[] data = File.ReadAllText(@"rectanglePosition.txt").Split(';');
                Move(new Point() { X = float.Parse(data[0]), Y = float.Parse(data[1]) });
            }
            width = canvas.Width;
            height = canvas.Height;
        }

        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            Move(e.GetPosition(canvas));
            object data = e.Data.GetData(DataFormats.Serializable);

            //if(data is Item element)
            //    TextTb.Text = element.Name + $" X = {e.GetPosition(canvas).X}   Y = {e.GetPosition(canvas).Y}";

            File.WriteAllText(@"rectanglePosition.txt", $"{e.GetPosition(canvas).X};{e.GetPosition(canvas).Y}");
            canMove = true;
        }

        private void rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(rectangle, new DataObject(DataFormats.Serializable, sword), DragDropEffects.Move);
                canMove = false;
            }
        }

        private void Move(Point point)
        {
            Canvas.SetLeft(rectangle, point.X);
            Canvas.SetTop(rectangle, point.Y);

            TextTb.Text = $" X = {point.X.ToString("N1")}   Y = {point.Y.ToString("N1")}";
        }



        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double zoom = e.Delta > 0 ? 1.1 : 0.9;

            // Определите координаты точки под курсором мыши
            Point mousePosition = e.GetPosition(canvas);

            // Сохраните текущие трансформации
            double currentScaleX = scaleTransform.ScaleX;
            double currentScaleY = scaleTransform.ScaleY;
            double currentTranslateX = translateTransform.X;
            double currentTranslateY = translateTransform.Y;

            Point position = e.GetPosition(Origin);
            TextTb.Text = $"X = {position.X}    Y = {position.Y}";

            // Рассчитайте новую позицию для компенсации сдвига
            double newTranslateX = currentTranslateX - ((15 * (zoom == 0.9 ? -1 : 1)) * (position.X < 0 ? -1 : 3) * currentScaleX);
            double newTranslateY = currentTranslateY - ((15 * (zoom == 0.9 ? -1 : 1)) * (position.Y < 0 ? -1 : 3) * currentScaleY);


            // Примените новую трансформацию масштаба и позицию
            scaleTransform.ScaleX *= zoom;
            scaleTransform.ScaleY *= zoom;
            translateTransform.X = newTranslateX;
            translateTransform.Y = newTranslateY;
        }

        private Point _mouseOffset;
        private bool _isDragging = false;
        private bool canMove = true;
        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && canMove)
            {
                _mouseOffset = e.GetPosition(rectangle);
                _isDragging = true;
                rectangle.CaptureMouse();
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && canMove)
            {
                Point currentPosition = e.GetPosition(canvas);
                double newX = currentPosition.X - _mouseOffset.X;
                double newY = currentPosition.Y - _mouseOffset.Y;

                Canvas.SetLeft(rectangle, newX);
                Canvas.SetTop(rectangle, newY);


            }
        }

        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_isDragging && canMove)
            {
                _isDragging = false;
                rectangle.ReleaseMouseCapture();
            }
        }

        private void rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            canMove = false;
        }
    }
}
