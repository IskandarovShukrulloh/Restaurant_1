using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_1.Classes
{
    internal class ChickenOrder
    {
        private int _chickenQty;
        public ChickenOrder(int chickenQty) {
            this._chickenQty = chickenQty;
        }
        public int GetChickenQty() => _chickenQty;

        public void CutUp()
        {
            // cut up chicken
            // Should be called the number of times requested in quantity
        }

        public void Cook()
        {
            // cook chicken
            // Should be called once only
        }

    }
}
