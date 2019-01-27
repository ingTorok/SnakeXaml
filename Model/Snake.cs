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
        /// <summary>
        /// Actually Head position
        /// </summary>
        public ArenaPosition HeadPosition { get; set; }

        /// <summary>
        /// The diraction in which the Snake is moving
        /// </summary>
        public SnakeHeadDirectionEnum HeadDirection { get; set; }

        /// <summary>
        /// Here are stored the body positions of the Snake
        /// </summary>
        public List<ArenaPosition> Tail { get; set; }

        /// <summary>
        /// The actually Length of the Snake
        /// Startlength is 6
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// To build up the Snake at first we need the starting position of the Head
        /// </summary>
        /// <param name="rowPosition">Row position of the Head at Start</param>
        /// <param name="columnPosition">Column position of the Head at Start</param>
        public Snake(int rowPosition, int columnPosition)
        {
            HeadPosition = new ArenaPosition(rowPosition, columnPosition);
            HeadDirection = SnakeHeadDirectionEnum.StartingPosition;
            Length = 100;
            Tail = new List<ArenaPosition>();
        }
    }
}
