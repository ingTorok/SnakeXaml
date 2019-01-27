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
    class Arena
    {
        protected MainWindow View;
        protected Snake Snake;
        protected DispatcherTimer Pendulum;
        protected bool IsStarted;
        protected int ArenaSize;
        protected int RowCounnt;
        protected int ColumnCount;
        protected Random RandomNr;
        protected Foods FoodBag;
        protected int FoodsHaveEatenCount;

        /// <summary>
        /// Contructor for Arena Class
        /// </summary>
        /// <param name="view">The window where we will play the game</param>
        public Arena(MainWindow view, int arenaSize)
        {
            //Setting the size of the Arena
            ArenaSize = arenaSize;
            RowCounnt = arenaSize;
            ColumnCount = arenaSize;

            //taking over the Mainwindow
            View = view;

            // Creating the graphical user interface for the Arena
            CreateAreana();
            
            //The startposition will be at the middle of the Arena
            int startPosition = Convert.ToInt32(Math.Round(Convert.ToDouble(ArenaSize/2)));

            //Construct the Snake at the startPosition
            Snake = new Snake(startPosition, startPosition);

            IsStarted = false;

            RandomNr = new Random();

            FoodBag = new Foods();

            FoodsHaveEatenCount = 0;

        }

        /// <summary>
        /// Creating the graphical user interface for the Arena
        /// Itt will defined in the childclass, depend on Grid or Canvas version
        /// </summary>
        protected virtual void CreateAreana()
        {

        }
        
        /// <summary>
        /// This event will handle the moving of the Snake
        /// It will call the ItsTimeForDisplay event periodically
        /// The interval depends on the size of our Snake
        /// How our Snake is growing so will the interval decrease -> faster mooving
        /// </summary>
        private void StartPendulum()
        {
            if (Pendulum != null && Pendulum.IsEnabled)
            {
                Pendulum.Stop();
            }

            var interval = 100000 / Snake.Length / 2;

            Pendulum = new DispatcherTimer(TimeSpan.FromMilliseconds(interval), DispatcherPriority.Normal, ItsTimeForDisplay, Application.Current.Dispatcher);
        }

        /// <summary>
        /// This function will manege the Snakes body 
        /// and will send the position of the Snakes to the graphical interface
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItsTimeForDisplay(object sender, EventArgs e)
        {
            if (!IsStarted)
            {//if the game is'nt started then exit
                return;
            }

            //The old poisition of the Head
            var neck = new ArenaPosition(Snake.HeadPosition.RowPosition, Snake.HeadPosition.ColumnPosition);

            switch (Snake.HeadDirection)
            {//We will check the direction of the Snake, and get the new position of the Snakes Head
                case SnakeHeadDirectionEnum.Up:
                    Snake.HeadPosition.RowPosition = Snake.HeadPosition.RowPosition - 1;
                    break;
                case SnakeHeadDirectionEnum.Down:
                    Snake.HeadPosition.RowPosition = Snake.HeadPosition.RowPosition + 1;
                    break;
                case SnakeHeadDirectionEnum.Left:
                    Snake.HeadPosition.ColumnPosition = Snake.HeadPosition.ColumnPosition - 1;
                    break;
                case SnakeHeadDirectionEnum.Right:
                    Snake.HeadPosition.ColumnPosition = Snake.HeadPosition.ColumnPosition + 1;
                    break;
                case SnakeHeadDirectionEnum.StartingPosition: //if the Snake is in the "cave" then we will do nothing
                    return;
                default:
                    break;
            }

            //The Snake is growing
            Snake.Tail.Add(new ArenaPosition(neck.RowPosition, neck.ColumnPosition));

            //Checking the new position
            if (CheckNewHeadPosition())
            {//if collosion with border or Snake then return
                EndOfGame();
                return;
            }

            //Show new Head position
            ShowPoint(ArenaPoints.Head, Snake.HeadPosition.RowPosition, Snake.HeadPosition.ColumnPosition);

            //Show new Neck position
            ShowPoint(ArenaPoints.Body, neck.RowPosition, neck.ColumnPosition);

            if (!(Snake.Tail.Count < Snake.Length))
            {//Check if the Snake is bigger then the actually lenght, if yes then we will romove the last added Snakepart
                var end = Snake.Tail[0];

                ShowPoint(ArenaPoints.Empty, end.RowPosition, end.ColumnPosition);

                Snake.Tail.RemoveAt(0);
            }
            
        }

        protected virtual void ShowPoint(ArenaPoints head, int rowPosition, int columnPosition)
        {
           
        }

        /// <summary>
        /// It will proove if the Snakes new position is border/snake/food. 
        /// If collosion with snake or border then true.
        /// </summary>
        /// <returns>If food or nothing then return fals</returns>
        private bool CheckNewHeadPosition()
        {
            if (Snake.HeadPosition.RowPosition < 0 || Snake.HeadPosition.RowPosition > RowCounnt - 1 ||
                            Snake.HeadPosition.ColumnPosition < 0 || Snake.HeadPosition.ColumnPosition > ColumnCount - 1)
            {//Collosion with border
                return true;
            }

            if (Snake.Tail.Any(x => x.RowPosition == Snake.HeadPosition.RowPosition
                                && x.ColumnPosition == Snake.HeadPosition.ColumnPosition))
            {//Collosion with Snake
                return true;
            }

            if (FoodBag.FoodPositions.Any(x=> x.RowPosition == Snake.HeadPosition.RowPosition
                                          &&   x.ColumnPosition == Snake.HeadPosition.ColumnPosition))
            {//The Snake ate a food

                //remove Food from foodbag
                FoodBag.Remove(Snake.HeadPosition.RowPosition, Snake.HeadPosition.ColumnPosition);

                //Count the number of foods eaten
                FoodsHaveEatenCount = FoodsHaveEatenCount + 1;

                //Show the number of foods aten
                View.NumberOfMealsTextBlock.Text = FoodsHaveEatenCount.ToString();

                //Add a nother food to Arena
                GetNewFood();

                //The Snake is growing
                Snake.Length++;

                return false;
            }

            //no collosion, no food
            return false;
        }

        private void EndOfGame()
        {
            Pendulum.Stop();
        }

        /// <summary>
        /// This function get the Key events from the Mainwindow.
        /// Here will be handled the direction of the Snake.
        /// </summary>
        /// <param name="e"></param>
        internal void KeyDown(KeyEventArgs e)
        {
            if (!IsStarted)
            {
                StartNewGame();
                IsStarted = true;
            }
            else
            {
                switch (e.Key)
                {//Check the diraction of our Snake. Ee cannot go to the opposit diraction
                    case Key.Left:
                        if (Snake.HeadDirection == SnakeHeadDirectionEnum.Right) { break; } //we cannot go to the opposit diraction
                        Snake.HeadDirection = SnakeHeadDirectionEnum.Left;
                        break;
                    case Key.Up:
                        if (Snake.HeadDirection == SnakeHeadDirectionEnum.Down) { break; } //we cannot go to the opposit diraction
                        Snake.HeadDirection = SnakeHeadDirectionEnum.Up;
                        break;
                    case Key.Right:
                        if (Snake.HeadDirection == SnakeHeadDirectionEnum.Left) { break; } //we cannot go to the opposit diraction
                        Snake.HeadDirection = SnakeHeadDirectionEnum.Right;
                        break;
                    case Key.Down:
                        if (Snake.HeadDirection == SnakeHeadDirectionEnum.Up) { break; } //we cannot go to the opposit diraction
                        Snake.HeadDirection = SnakeHeadDirectionEnum.Down;
                        break;
                }
            }

        }

        /// <summary>
        /// At the beginning of the game we will 
        /// </summary>
        private void StartNewGame()
        {
            StartPendulum();
            GetNewFood();
            IsStarted = true;          
        }

        /// <summary>
        /// Add new food to Arena
        /// </summary>
        private void GetNewFood()
        {
            //Getting a random position for the food
            var row = RandomNr.Next(0, RowCounnt - 1);
            var column = RandomNr.Next(0, ColumnCount - 1);

            //Check if the food position is on the Snake, until it will be on a free position
            while (Snake.HeadPosition.RowPosition == row && Snake.HeadPosition.ColumnPosition == column
                || Snake.Tail.Any(x => x.RowPosition == row && x.ColumnPosition == column))
            {//if yes the we will get a new food position
                row = RandomNr.Next(0, RowCounnt - 1);
                column = RandomNr.Next(0, ColumnCount - 1);
            }

            //Add the new food in the Foodbag
            FoodBag.Add(row, column);

            //Show the new food on the Arena
            ShowPoint(ArenaPoints.Food, row, column);
        }
    }
}
