using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeXaml.Model
{
    class Foods
    {
        public Foods()
        {
            FoodPositions = new List<ArenaPosition>();
        }
        public List<ArenaPosition> FoodPositions { get; set; }

        internal void Add(int row, int column)
        {
            FoodPositions.Add(new ArenaPosition(row, column));
        }

        internal void Remove(int rowPosition, int columnPosition)
        {
            var foodToDelete = FoodPositions.Single(x => x.RowPosition == rowPosition 
                                                      && x.ColumnPosition == columnPosition);

            FoodPositions.Remove(foodToDelete);
        }
    }

}
