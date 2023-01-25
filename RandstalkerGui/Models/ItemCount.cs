namespace RandstalkerGui.Models
{
    public class ItemCount
    {
        public string Name { get; }

        public int Count { get; set; }

        public ItemCount(string name, int count)
        {
            Name = name;
            Count = count;
        }
    }
}
