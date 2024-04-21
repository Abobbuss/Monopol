namespace Monopol.Warehouse
{
    public abstract class WarehouseItem
    {
        public int ID { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }
        public double Weight { get; set; }
        public abstract double Volume { get; }
    }
}