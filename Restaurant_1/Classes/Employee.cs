using System;
using System.Text;

namespace Restaurant_1.Classes
{
    internal class Employee
    {
        private object? _lastRequest;
        private int _requestCount = 0;
        private bool _isPrepared = false;

        public Employee()
        {
        }

        // NewRequest: (ChickenOrder or EggOrder).
        public object NewRequest(int quantity, string menuItem)
        {
            _isPrepared = false;
            if (quantity <= 0)
                throw new Exception("Please make an order!");

            _requestCount++;

            bool wrongOrder = (_requestCount % 3 == 0);

            object request;

            // new object based on menuItem
            if (menuItem == "chicken")
            {
                request = wrongOrder ? new EggOrder(quantity) : new ChickenOrder(quantity);
            }
            else if (menuItem == "egg")
            {
                request = wrongOrder ? new ChickenOrder(quantity) : new EggOrder(quantity);
            }
            else
            {
                throw new ArgumentException($"Unknown menu item: '{menuItem}'", nameof(menuItem));
            }

            _lastRequest = request;
            return request;
        }

        // CopyRequest: new object with the same parameters as the last request.
        // returns ChickenOrder or EggOrder.
        public object CopyRequest()
        {

            if (_lastRequest == null)
                throw new InvalidOperationException("I'm upset. There is no previous request!");

            object copy;

            if (_lastRequest is ChickenOrder chicken)
            {
                copy = new ChickenOrder(chicken.GetChickenQty());
            }
            else{
                var egg = (EggOrder)_lastRequest;
                copy = new EggOrder(egg.GetEggQuantity());
            }

            _lastRequest = copy;
            _isPrepared = false;
            return copy;
        }

        // Inspect: takes ChickenOrder or EggOrder and returns result string.
        public string Inspect(object order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            else if (order is ChickenOrder)
            {
                return "Chicken order does not require inspection.";
            }

            else if (order is EggOrder egg)
            {
                int total = egg.GetEggQuantity();
                string res = "";

                for (int i = 1; i <= total; i++)
                {
                    int? quality = egg.GetEggQuality(); // get quality

                    if (quality.HasValue)
                    {
                        try
                        {
                            egg.Crack(); // try to crack
                            res += $"\nEgg No. {i} has quality: {quality}\n";
                        }
                        catch
                        {
                            res += $"\nEgg No. {i} is rotten\n";
                        }
                        finally
                        {
                            egg.DiscardShell();
                        }
                    }
                    else
                    {
                        res += $"\nEgg No. {i} quality is null\n";
                    }
                }

                return res;
            }
            else
            {
                throw new ArgumentException("Unsupported order type for inspection.", nameof(order));
            }
        }

        public string PrepareFood(object order)
        {

            if (_isPrepared)
                throw new InvalidOperationException("Food has already been prepared for this order.");

            string res = "";

            if (order == null)
                throw new ArgumentNullException(nameof(order));

            else if (order is ChickenOrder chicken)
            {
                int qty = chicken.GetChickenQty();

                for (int i = 1; i <= qty; i++)
                {
                    chicken.CutUp();
                }
                
                chicken.Cook();
                res += "\nPrepared chicken: all cut up and cooked once.";
            }

            else if (order is EggOrder egg)
            {
                egg.Cook(); // cook eggs once
                res = "\nPrepared eggs: all shells discarded and cooked once.";
            }
            else
                throw new ArgumentException("Unsupported order type for preparation.", nameof(order));

            _isPrepared = true;
            return res;
        }
    }
}
