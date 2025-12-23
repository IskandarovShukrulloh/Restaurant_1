using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_1.Classes
{
    internal class EggOrder
    {
        private int _eggQty;
        private int _eggQuality;
        private static Random rand = new Random();

        public EggOrder(int eggQty)
        {
            this._eggQty = eggQty;
        }

        public int GetEggQuantity() => _eggQty;

        public int? GetEggQuality()
        {
            // Calculated once per instance of the class
            // To simulate the employee forgetting 1/2 of the time,
                // the method should return a null value on the
                // 2nd, 4th, 6th, etc., instances of the class.
            // Generate and display a random integer between 0 and 100.

            this._eggQuality = rand.Next(101);

            return this._eggQuality;
        }

        public void Crack()
        {
            // should throw an exception if the egg quality is less than 25
            // Should be called the number of times requested in quantity

            if (this._eggQuality < 25)
            {
                throw new Exception("Egg quality is too low to crack.");
            }
        }
        public void DiscardShell()
        {
            // discard eggs
            // Should be called the number of times requested in quantity
        }
        public void Cook()
        {
            // cook eggs
            // Should be called once only
            int qty = this._eggQty;
            for (int i = 1; i <= qty; i++)
            {
                this.DiscardShell(); // discard shell for each egg
            }

        }
    }
}
