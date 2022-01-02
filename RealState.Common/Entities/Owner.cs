using System;
using System.Collections.Generic;

namespace RealState.Common.Entities
{
    public class Owner: BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public byte[] Photo { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Property> Properties { get; set; }
    }
}
