using System;

namespace ORM.Core.Entities
{

    public enum StockType
    {
        Magnit,
        Gazprom,
        GMKNorNik,
        LUKOIL,
        Rosneft,
        Novatek,
        Sberbank,
        Severstal
    };

    public class Stock
    {
        public int Id { get; set; }

        public StockType Type { get; set; }

        public decimal Cost { get { return GetStockCost(Type); } }

        public decimal GetStockCost(StockType type)
        {
            if (type == StockType.Magnit)
                return 5278M;
            else if (type == StockType.Gazprom)
                return 147.27M;
            else if (type == StockType.GMKNorNik)
                return 10889;
            else if (type == StockType.LUKOIL)
                return 4254.5M;
            else if (type == StockType.Rosneft)
                return 378.45M;
            else if (type == StockType.Novatek)
                return 756.5M;
            else if (type == StockType.Sberbank)
                return 222.20M;
            else if (type == StockType.Severstal)
                return 992.2M;
            else return 0;

        }
    }
}
