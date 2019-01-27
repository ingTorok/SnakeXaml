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
    class ArenaGrid : Arena
    {
        public ArenaGrid(MainWindow view, int size) :
                    base(           view,     size)
        {

        }

        /// <summary>
        /// Creating the graphical user interface for the Arena
        /// Here we will create the Grids and fill up with fontawsemo Icons
        /// </summary>
        protected override void CreateAreana()
        {
            //variables to add colums and rows
            RowDefinition row;
            ColumnDefinition column;

            //variable to add fontawesome image to grid
            ImageAwesome image;

            for (int i = 0; i < ArenaSize; i++)
            {//we will add 20 columns and 20 rows
             //Define colums and rows
             //the Width and Height are automatically set to "*"
                row = new RowDefinition();
                column = new ColumnDefinition();

                //add columns add rows to ArenaGrid
                View.ArenaGrid.RowDefinitions.Add(row);
                View.ArenaGrid.ColumnDefinitions.Add(column);

            }

            for (int i = 0; i < RowCounnt; i++)
            {//here we will fill the grid with image, so the gamearen will be visible
                for (int j = 0; j < ColumnCount; j++)
                {
                    //create new image
                    image = new ImageAwesome();
                    image.Icon = FontAwesomeIcon.SquareOutline;

                    //set the grid colums and rows for the image object
                    Grid.SetRow(image, i);
                    Grid.SetColumn(image, j);

                    //add the image object to ArenaGrid
                    //todo: insert at lehetseges feltötltés
                    View.ArenaGrid.Children.Add(image);

                }
            }
        }

        /// <summary>
        /// The method to show the points on the Grid
        /// </summary>
        /// <param name="point"></param>
        /// <param name="rowPosition"></param>
        /// <param name="columnPosition"></param>
        protected override void ShowPoint(ArenaPoints point, int rowPosition, int columnPosition)
        {
            var image = GetImage(rowPosition, columnPosition);

            switch (point)
            {
                case ArenaPoints.Head:
                    image.Icon = FontAwesomeIcon.Square;
                    image.Foreground = Brushes.Black;
                    break;
                case ArenaPoints.Body:
                    image.Icon = FontAwesomeIcon.Square;
                    image.Foreground = Brushes.Gray;
                    break;
                case ArenaPoints.Food:
                    image.Icon = FontAwesomeIcon.Square;
                    image.Foreground = Brushes.Red;
                    break;
                case ArenaPoints.Empty:
                    image.Icon = FontAwesomeIcon.SquareOutline;
                    image.Foreground = Brushes.Black;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Get the ImageAwseome object corresponding Row and Column
        /// </summary>
        /// <param name="RowPosition"></param>
        /// <param name="ColumnPosition"></param>
        /// <returns></returns>
        private ImageAwesome GetImage(int RowPosition, int ColumnPosition)
        {
            var cell = View.ArenaGrid.Children[RowPosition * ArenaSize + ColumnPosition];

            var image = (ImageAwesome)cell;
            return image;
        }
    }
}

