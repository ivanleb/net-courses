namespace SESimulator.Model
{
    public class Stock
    {
        public decimal Cost
        {
            get
            {
                return this.Type.Cost;
            }
        }

        //public string Name
        //{
        //    get
        //    {
        //        return this.Type.Name;
        //    }
        //}

        public int Id { get; set; }

        public StockType Type { get; set; }
    }
}
