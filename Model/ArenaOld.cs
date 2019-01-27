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
    /// <summary>
    /// The gameprozess
    /// </summary>
    class ArenaOld
    {
        private MainWindow View;
        private Snake snake;
        private DispatcherTimer pendulum;
        private bool isStarted;
        private int RowCounnt;
        private int ColumnCount;
        private Random Random;
        private Foods foods;
        private int foodsHaveEatenCount;

        /// <summary>
        /// Contructor for Arena Class
        /// </summary>
        /// <param name="view">The window where we will play the game</param>
        public ArenaOld(MainWindow view)
        {
            RowCounnt = 20;
            ColumnCount = 20;

            View = view;
            CreateGrid();

            snake = new Snake(10, 10);

            StartPendulum();

            isStarted = false;

            Random = new Random();

            foods = new Foods();

            foodsHaveEatenCount = 0;

        }

        private void StartPendulum()
        {
            if (pendulum != null && pendulum.IsEnabled)
            {
                pendulum.Stop();
            }

            var interval = 1000 / snake.Length/2;

            pendulum = new DispatcherTimer(TimeSpan.FromMilliseconds(interval), DispatcherPriority.Normal, ItsTimeForDisplay, Application.Current.Dispatcher);
        }

        private void ItsTimeForDisplay(object sender, EventArgs e)
        {
            if (!isStarted)
            {
                return;
            }

            var neck = new ArenaPosition(snake.HeadPosition.RowPosition, snake.HeadPosition.ColumnPosition);

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
                    return;
                default:
                    break;
            }

            if (snake.HeadPosition.RowPosition<0 || snake.HeadPosition.RowPosition>RowCounnt-1 ||
                snake.HeadPosition.ColumnPosition<0 || snake.HeadPosition.ColumnPosition> ColumnCount-1)
            {//Collosion with border
                EndOfGame();
                return;
            }

            if (snake.Tail.Any(x=>x.RowPosition == snake.HeadPosition.RowPosition 
                                && x.ColumnPosition == snake.HeadPosition.ColumnPosition))
            {//Collosion with snake
                EndOfGame();
                return;
            }


            //todo bescomagolni a Foods classbe es ott vizsgalni meg hogy etelt ettunke ha igen trueval jon vissza ha nem akkor falseal
            if (foods.FoodPositions.Any(x=>x.RowPosition==snake.HeadPosition.RowPosition 
                                        && x.ColumnPosition==snake.HeadPosition.ColumnPosition))
            {
                foods.Remove(snake.HeadPosition.RowPosition, snake.HeadPosition.ColumnPosition);

                foodsHaveEatenCount = foodsHaveEatenCount + 1;

                View.NumberOfMealsTextBlock.Text = foodsHaveEatenCount.ToString();

                snake.Length = snake.Length + 1;

                GetNewFood();
            }

            ShowSnakeHead(snake.HeadPosition.RowPosition, snake.HeadPosition.ColumnPosition);

            ShowSnakeNeck(neck.RowPosition, neck.ColumnPosition);

            snake.Tail.Add(new ArenaPosition(neck.RowPosition, neck.ColumnPosition));


            if (snake.Tail.Count < snake.Length)
            {

            }
            else
            {
                var end = snake.Tail[0];

                ShowEmptyArenaPosition(end.RowPosition, end.ColumnPosition);

                snake.Tail.RemoveAt(0);
            }

            //cell = View.ArenaGrid.Children[neck.RowPosition * 20 + neck.ColumnPosition];
            //image = (ImageAwesome)cell;
            //image.Icon = FontAwesomeIcon.SquareOutline;
        }

        private void EndOfGame()
        {
            pendulum.Stop();
        }

        //todo vondd össze a négy függvényt!
        //todo az etelt tudja a fejere is tenni!!!

        private UIElement ShowNewFood(int rowPosition, int columnPosition)
        {
            var image = GetImage(rowPosition, columnPosition);

            image.Icon = FontAwesomeIcon.Apple;
            image.Foreground = Brushes.Red;
            //var paint = PaintOnCanvas(rowPosition, columnPosition);
            return null;
        }

        //private UIElement PaintOnCanvas(int rowPosition, int columnPosition)
        //{
            //var paint = new Ellipse();

            //paint.Height = View.ArenaCanvas.ActualHeight / RowCounnt;
            //paint.Width = View.ArenaCanvas.ActualHeight / ColumnCount;

            //paint.Fill = Brushes.Red;

            //Canvas.SetTop(paint, rowPosition * paint.Height);
            //Canvas.SetLeft(paint, columnPosition * paint.Width);

            //View.ArenaCanvas.Children.Add(paint);

            //return paint;
        //}

        private void EraseFromCanvas(UIElement paint)
        {
            //View.ArenaCanvas.Children.Remove(paint);
        }

        private void ShowEmptyArenaPosition(int rowPosition, int columnPosition)
        {
            var image = GetImage(rowPosition, columnPosition);

            image.Icon = FontAwesomeIcon.SquareOutline;
            image.Foreground = Brushes.Black;
        }

        private void ShowSnakeNeck(int rowPosition, int columnPosition)
        {
            var image = GetImage(rowPosition, columnPosition);

            image.Icon = FontAwesomeIcon.Square;
            image.Foreground = Brushes.Gray;
        }

        private void ShowSnakeHead(int RowPosition, int ColumnPosition)
        {
            var image = GetImage(RowPosition, ColumnPosition);

            image.Icon = FontAwesomeIcon.Square;
        }

        private ImageAwesome GetImage(int RowPosition, int ColumnPosition)
        {
            var cell = View.ArenaGrid.Children[RowPosition * 20 + ColumnPosition];

            var image = (ImageAwesome)cell;
            return image;
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
            GetNewFood();
        }

        private void GetNewFood()
        {
            var row = Random.Next(0, RowCounnt - 1);
            var column = Random.Next(0, ColumnCount - 1);

            //todo szebben megoldani


            while (snake.HeadPosition.RowPosition == row && snake.HeadPosition.ColumnPosition == column
                || snake.Tail.Any(x => x.RowPosition == row && x.ColumnPosition == column))
            {
                row = Random.Next(0, RowCounnt - 1);
                column = Random.Next(0, ColumnCount - 1);
            }

            foods.Add(row, column);

           var paint = ShowNewFood(row, column);
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
    }
}
