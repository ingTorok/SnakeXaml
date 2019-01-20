using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeXaml.Model
{
    class ArenaPosition
    {
        public ArenaPosition(int rowPosition, int columnPosition)
        {
            RowPosition = rowPosition;
            ColumnPosition = columnPosition;
        }

        public int ColumnPosition { get; set; }
        public int RowPosition { get; set; }
    }
}
