using System;
using System.Collections.Generic;

namespace RealState.Common.Entities
{
    public class Property: BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string CodeInternal { get; set; }
        public double Price { get; set; }
        public int Year { get; set; }
        public Guid OwnerId { get; set; }
        public Owner Owner { get; set; }
        public ICollection<PropertyImage> PropertyImages { get; set; }
        public ICollection<PropertyTrace> PropertyTraces { get; set; }
    }
}
