using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SnakeXaml.Model
{
    /// <summary>
    /// The gameprozess
    /// </summary>
    class Arena
    {
        private MainWindow View;

        /// <summary>
        /// Contructor for Arena Class
        /// </summary>
        /// <param name="view">The window where we will play the game</param>
        public Arena(MainWindow view)
        {
            View = view;
        }

        internal void KeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                case Key.Up:
                case Key.Right:
                case Key.Down:
                    View.NumberOfMealsTextBlock.Visibility = System.Windows.Visibility.Visible;
                    CreateGrid();
                    var cell = View.ArenaGrid.Children[10 * 20 + 10];
                    var image = (ImageAwesome)cell;

                    image.Icon = FontAwesomeIcon.Circle;

                    break;
            }
        }

        /// <summary>
        /// Create the grid for Arena and fill with images
        /// </summary>
        private void CreateGrid()
        {
            //variables to add colums and rows
            ColumnDefinition column;
            RowDefinition row;

            //variable to add fontawesome image to grid
            ImageAwesome image;
        
            for (int i = 0; i < 20; i++)
            {//we will add 20 columns and 20 rows
                //Define colums and rows
                //the Width and Height are automatically set to "*"
                column = new ColumnDefinition();
                row = new RowDefinition();

                //add columns add rows to ArenaGrid
                View.ArenaGrid.ColumnDefinitions.Add(column);
                View.ArenaGrid.RowDefinitions.Add(row);

            }

            for (int i = 0; i < 20; i++)
            {//here we will fill the grid with image, so the gamearen will be visible
                for (int j = 0; j < 20; j++)
                {
                    //create new image
                    image = new ImageAwesome();
                    image.Icon = FontAwesomeIcon.SquareOutline;

                    //set the grid colums and rows for the image object
                    Grid.SetColumn(image, i);
                    Grid.SetRow(image, j);

                    //add the image object to ArenaGrid
                    View.ArenaGrid.Children.Add(image);

                }
            }

            
        }
    }
}
