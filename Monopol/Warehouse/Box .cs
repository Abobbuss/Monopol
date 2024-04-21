using System;

namespace Monopol.Warehouse
{
    public  class Box : WarehouseItem
    {
        private int _expirationPeriodInDays = 100;

        public DateTime ProductionDate { get; set; }
        public DateTime ExpiryDate
        {
            get
            {
                return ProductionDate.AddDays(_expirationPeriodInDays).Date;
            }
        }
        public string ProductionDateShortString => ProductionDate.ToShortDateString();
        public string ExpiryDateShortString => ExpiryDate.ToShortDateString();

        public override double Volume
        {
            get
            {
                return Width * Height * Depth;
            }
        }
    }
}
