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
        public ArenaPosition HeadPosition { get; set; }

        public SnakeHeadDirectionEnum HeadDirection { get; set; }

        public List<ArenaPosition> Tail { get; set; }

        public int MyProperty { get; set; }

        public int Length { get; set; }

        public Snake(int rowPosition, int columnPosition)
        {
            HeadPosition = new ArenaPosition(rowPosition, columnPosition);
            HeadDirection = SnakeHeadDirectionEnum.StartingPosition;
            Length = 6;
            Tail = new List<ArenaPosition>();
        }
    }
}
