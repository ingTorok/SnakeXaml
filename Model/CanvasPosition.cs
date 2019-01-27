using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace SnakeXaml.Model
{
    class CanvasPosition : ArenaPosition
    {
        public UIElement Paint;

        public CanvasPosition(UIElement paint, int rowPosition, int columnPosition) :
                         base(                     rowPosition,     columnPosition)
        {
            Paint = paint;
        }
    }
}
