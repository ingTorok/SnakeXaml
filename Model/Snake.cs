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
    class Snake
    {
        public Snake(int rowPosition, int columnPosition)
        {
            HeadPosition = new ArenaPosition(rowPosition, columnPosition);
            HeadDirection = SnakeHeadDirectionEnum.StartingPosition;
        }

        public ArenaPosition HeadPosition { get; set; }

        public SnakeHeadDirectionEnum HeadDirection { get; set; }
    }
}
