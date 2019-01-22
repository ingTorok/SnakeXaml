using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeXaml.Model
{
    /// <summary>
    /// 
    /// </summary>
    class ArenaPosition
    {
        public int ColumnPosition { get; set; }
        public int RowPosition { get; set; }


        public ArenaPosition(int rowPosition, int columnPosition)
        {
            RowPosition = rowPosition;
            ColumnPosition = columnPosition;
        }
    }
}
