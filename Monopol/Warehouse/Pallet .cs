using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopol.Warehouse
{
    public class Pallet : WarehouseItem
    {
        private List<Box> _boxes = new List<Box>();
        private int _weightPallet = 30;

        public List<Box> Boxes
        {
            get { return _boxes; }
            set { _boxes = value; }
        }
        public DateTime ExpiryDate
        {
            get
            {
                DateTime minExpiry = DateTime.MaxValue;

                foreach (var box in _boxes)
                {
                    if (box.ExpiryDate < minExpiry)
                        minExpiry = box.ExpiryDate;
                }

                return minExpiry;
            }
        }
        public string ExpiryDateShortString => ExpiryDate.ToShortDateString();

        public new double Weight
        {
            get
            {
                double totalWeight = _boxes.Sum(box => box.Weight);
                return totalWeight + _weightPallet;
            }
        }

        public override double Volume
        {
            get
            {
                double totalVolume = Width * Height * Depth;

                foreach (var box in _boxes)
                {
                    totalVolume += box.Volume;
                }

                return totalVolume;
            }
        }
    }
}
