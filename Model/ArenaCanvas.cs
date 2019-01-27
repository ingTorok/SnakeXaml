using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SnakeXaml.Model
{
    

    class ArenaCanvas : Arena
    {
        private Canvas CanvasArena = new Canvas();

        private List<CanvasPosition> ArenaBag = new List<CanvasPosition>();   

        public ArenaCanvas(MainWindow view, int size) :
                      base(           view,     size)
        {
            
            CanvasArena.Background = Brushes.Orange;
        }

        protected override void CreateAreana()
        {
            View.ArenaGrid.Children.Add(CanvasArena);
        }

        protected override void ShowPoint(ArenaPoints point, int rowPosition, int columnPosition)
        {

            if (point == ArenaPoints.Empty)
            {
                CanvasPosition item = new CanvasPosition(null,0,0);
                while (item!=null)
                {
                    var paintTodDelete = ArenaBag.FirstOrDefault(x => x.RowPosition == rowPosition & x.ColumnPosition == columnPosition);
                    if (paintTodDelete == null)
                    {
                        return;
                    }
                    item = paintTodDelete;
                    ArenaBag.Remove(paintTodDelete);
                    CanvasArena.Children.Remove(paintTodDelete.Paint);
                };

                return;
            }
            var paint = new Rectangle
            {
                Height = CanvasArena.ActualHeight / RowCounnt,
                Width = CanvasArena.ActualWidth / ColumnCount
            };

            Canvas.SetTop(paint, rowPosition * paint.Height);
            Canvas.SetLeft(paint, columnPosition * paint.Width);

            switch (point)
            {
                case ArenaPoints.Head:
                    paint.Fill = Brushes.Black;
                    break;
                case ArenaPoints.Body:
                    paint.Fill = Brushes.Gray;
                    break;
                case ArenaPoints.Food:
                    paint.Fill = Brushes.Red;
                    break;
                case ArenaPoints.Empty:
                    break;
                default:
                    break;
            }

            ArenaBag.Add(new CanvasPosition(paint, rowPosition, columnPosition));

            CanvasArena.Children.Add(paint);

            Console.WriteLine(ArenaBag.Count + "   " + CanvasArena.Children.Count + "   " + Snake.Length);
        }
    }
}
