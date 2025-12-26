namespace Restaurant_1.Classes
{
    internal class EggOrder
    {
        private static readonly Random RandomGenerator = new Random();

        private int _eggQuantity;
        private int _eggQuality;
        private int _qualityCheckCount;

        public EggOrder(int eggQuantity)
        {
            _eggQuantity = eggQuantity;
        }

        public int GetEggQuantity()
        {
            return _eggQuantity;
        }

        // Generates egg quality
        // Returns null on every second call
        public int? GetEggQuality()
        {
            _qualityCheckCount++;

            if (_qualityCheckCount % 2 == 0)
            {
                return null;
            }

            _eggQuality = RandomGenerator.Next(0, 101);
            return _eggQuality;
        }

        // Cracks the egg
        // Throws exception if quality is too low
        public void Crack()
        {
            if (_eggQuality < 25)
            {
                throw new Exception("Egg quality is too low to crack.");
            }
        }

        // Discards the eggshell
        // Should be called once per egg
        public void DiscardShell()
        {
            // discard shell
        }

        // Cooks the eggs
        // Should be called only once
        public void Cook()
        {
            for (int i = 1; i <= _eggQuantity; i++)
            {
                DiscardShell();
            }
        }
    }
}
