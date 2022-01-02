using System.Collections.Generic;

namespace RealState.Common.Interfaces
{
    public interface IResponse
    {
        public bool Status { get; set; }
        public dynamic Content { get; set; }
        public IList<string> Errors { get; set; }
    }
}
