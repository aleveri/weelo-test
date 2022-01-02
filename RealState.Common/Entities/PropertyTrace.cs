using System;

namespace RealState.Common.Entities
{
    public class PropertyTrace: BaseEntity
    {
        public string Name { get; set; }
        public DateTime DateSale { get; set; }
        public double Value { get; set; }
        public double Tax { get; set; }
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
