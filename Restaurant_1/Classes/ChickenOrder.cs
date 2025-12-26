namespace Restaurant_1.Classes
{
    internal class ChickenOrder
    {
        private int _chickenQuantity;

        public ChickenOrder(int chickenQuantity)
        {
            _chickenQuantity = chickenQuantity;
        }

        public int GetChickenQuantity()
        {
            return _chickenQuantity;
        }

        // Cuts the chicken into portions
        // Should be called once per chicken unit
        public void CutUp()
        {
            // cut up chicken
        }

        // Cooks the chicken
        // Should be called only once
        public void Cook()
        {
            // cook chicken
        }
    }
}
