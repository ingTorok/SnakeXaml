using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace SnakeXaml.Model
{
    /// <summary>
    /// The gameprozess
    /// </summary>
    class Arena
    {
        private MainWindow View;
        private Snake snake;
        private DispatcherTimer pendulum;
        private bool isStarted;

        /// <summary>
        /// Contructor for Arena Class
        /// </summary>
        /// <param name="view">The window where we will play the game</param>
        public Arena(MainWindow view)
        {
            View = view;
            CreateGrid();

            snake = new Snake(10,10);

            pendulum = new DispatcherTimer(TimeSpan.FromMilliseconds(100),DispatcherPriority.Normal, ItsTimeForDisplay, Application.Current.Dispatcher);

            isStarted = false;
        }

        private void ItsTimeForDisplay(object sender, EventArgs e)
        {
            var currentPosition = new ArenaPosition(snake.HeadPosition.RowPosition, snake.HeadPosition.ColumnPosition);

            switch (snake.HeadDirection)
            {
                case SnakeHeadDirectionEnum.Up:
                    snake.HeadPosition.RowPosition = snake.HeadPosition.RowPosition - 1;
                    break;
                case SnakeHeadDirectionEnum.Down:
                    snake.HeadPosition.RowPosition = snake.HeadPosition.RowPosition + 1;
                    break;
                case SnakeHeadDirectionEnum.Left:
                    snake.HeadPosition.ColumnPosition = snake.HeadPosition.ColumnPosition - 1;
                    break;
                case SnakeHeadDirectionEnum.Right:
                    snake.HeadPosition.ColumnPosition = snake.HeadPosition.ColumnPosition + 1;
                    break;
                case SnakeHeadDirectionEnum.StartingPosition:
                    break;
                default:
                    break;
            }

            var cell = View.ArenaGrid.Children[snake.HeadPosition.RowPosition * 20 + snake.HeadPosition.ColumnPosition];

            var image = (ImageAwesome)cell;
            Console.WriteLine(image.Name);

            image.Icon = FontAwesomeIcon.Circle;

            cell = View.ArenaGrid.Children[currentPosition.RowPosition * 20 + currentPosition.ColumnPosition];
            image = (ImageAwesome)cell;
            image.Icon = FontAwesomeIcon.SquareOutline;
        }

        internal void KeyDown(KeyEventArgs e)
        {
            if (!isStarted)
            {
                StartNewGame();
                isStarted = true;
            }
            else
            {
                switch (e.Key)
                {
                    case Key.Left:
                        snake.HeadDirection = SnakeHeadDirectionEnum.Left;
                        break;
                    case Key.Up:
                        snake.HeadDirection = SnakeHeadDirectionEnum.Up;
                        break;
                    case Key.Right:
                        snake.HeadDirection = SnakeHeadDirectionEnum.Right;
                        break;
                    case Key.Down:
                        snake.HeadDirection = SnakeHeadDirectionEnum.Down;
                        break;
                }
            }
            
        }

        private void StartNewGame()
        {
            View.NumberOfMealsTextBlock.Visibility = Visibility.Visible;
            isStarted = true;
        }

        /// <summary>
        /// Create the grid for Arena and fill with images
        /// </summary>
        private void CreateGrid()
        {
            //variables to add colums and rows
            RowDefinition row;
            ColumnDefinition column;

            //variable to add fontawesome image to grid
            ImageAwesome image;
        
            for (int i = 0; i < 20; i++)
            {//we will add 20 columns and 20 rows
             //Define colums and rows
             //the Width and Height are automatically set to "*"
                row = new RowDefinition();
                column = new ColumnDefinition();

                //add columns add rows to ArenaGrid
                View.ArenaGrid.RowDefinitions.Add(row);
                View.ArenaGrid.ColumnDefinitions.Add(column);

            }

            for (int i = 0; i < 20; i++)
            {//here we will fill the grid with image, so the gamearen will be visible
                for (int j = 0; j < 20; j++)
                {
                    //create new image
                    image = new ImageAwesome();
                    image.Icon = FontAwesomeIcon.SquareOutline;

                    //set the grid colums and rows for the image object
                    Grid.SetRow(image, i);
                    Grid.SetColumn(image, j);

                    //add the image object to ArenaGrid
                    View.ArenaGrid.Children.Add(image);

                }
            }

            
        }
    }
}
