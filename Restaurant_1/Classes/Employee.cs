using System;

namespace Restaurant_1.Classes
{
    internal class Employee
    {
        private object? _lastRequest;
        private int _requestCount;
        private bool _isPrepared;

        public Employee()
        {
        }

        // Creates a new request (ChickenOrder or EggOrder)
        public object NewRequest(int quantity, string menuItem)
        {
            _isPrepared = false;

            if (quantity <= 0)
            {
                throw new Exception("Please make an order!");
            }

            _requestCount++;

            bool isWrongOrder = _requestCount % 3 == 0;
            object request;

            if (menuItem == "chicken")
            {
                request = isWrongOrder
                    ? new EggOrder(quantity)
                    : new ChickenOrder(quantity);
            }
            else if (menuItem == "egg")
            {
                request = isWrongOrder
                    ? new ChickenOrder(quantity)
                    : new EggOrder(quantity);
            }
            else
            {
                throw new ArgumentException(
                    $"Unknown menu item: '{menuItem}'",
                    nameof(menuItem)
                );
            }

            _lastRequest = request;
            return request;
        }

        // Creates a copy of the last request
        public object CopyRequest()
        {
            if (_lastRequest == null)
            {
                throw new InvalidOperationException(
                    "I'm upset. There is no previous request!"
                );
            }

            object copy;

            if (_lastRequest is ChickenOrder chicken)
            {
                copy = new ChickenOrder(chicken.GetChickenQuantity());
            }
            else
            {
                var egg = (EggOrder)_lastRequest;
                copy = new EggOrder(egg.GetEggQuantity());
            }

            _lastRequest = copy;
            _isPrepared = false;

            return copy;
        }

        // Inspects the given order and returns a result message
        public string Inspect(object order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if (order is ChickenOrder)
            {
                return "Chicken order does not require inspection.";
            }

            if (order is EggOrder egg)
            {
                int total = egg.GetEggQuantity();
                string result = string.Empty;

                for (int i = 1; i <= total; i++)
                {
                    int? quality = egg.GetEggQuality();

                    if (quality.HasValue)
                    {
                        try
                        {
                            egg.Crack();
                            result += $"\nEgg No. {i} has quality: {quality}\n";
                        }
                        catch
                        {
                            result += $"\nEgg No. {i} is rotten\n";
                        }
                        finally
                        {
                            egg.DiscardShell();
                        }
                    }
                    else
                    {
                        result += $"\nEgg No. {i} quality is null\n";
                    }
                }

                return result;
            }

            throw new ArgumentException(
                "Unsupported order type for inspection.",
                nameof(order)
            );
        }

        // Prepares the given order
        public string PrepareFood(object order)
        {
            if (_isPrepared)
            {
                throw new InvalidOperationException(
                    "Food has already been prepared for this order."
                );
            }

            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            string result;

            if (order is ChickenOrder chicken)
            {
                int quantity = chicken.GetChickenQuantity();

                for (int i = 1; i <= quantity; i++)
                {
                    chicken.CutUp();
                }

                chicken.Cook();
                result = "\nPrepared chicken: all cut up and cooked once.";
            }
            else if (order is EggOrder egg)
            {
                egg.Cook();
                result = "\nPrepared eggs: all shells discarded and cooked once.";
            }
            else
            {
                throw new ArgumentException(
                    "Unsupported order type for preparation.",
                    nameof(order)
                );
            }

            _isPrepared = true;
            return result;
        }
    }
}
