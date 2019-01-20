using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    break;
            }
        }
    }
}
