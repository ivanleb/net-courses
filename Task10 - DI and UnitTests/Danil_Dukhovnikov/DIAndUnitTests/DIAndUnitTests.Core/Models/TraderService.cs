namespace DIAndUnitTests.Core.Models
{
    public static class TraderService
    {
        public static Zone GetZone(Trader trader)
        {
            if (trader.Balance > 0)
            {
                return Zone.Green;
            }

            if (trader.Balance == 0)
            {
                return Zone.Orange;
            }

            return Zone.Black;
        }
    }
}