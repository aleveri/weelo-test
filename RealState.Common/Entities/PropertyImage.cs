using System;

namespace RealState.Common.Entities
{
    public class PropertyImage: BaseEntity
    {
        public byte[] File { get; set; }
        public bool Enabled { get; set; }
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
