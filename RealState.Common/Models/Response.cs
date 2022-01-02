using RealState.Common.Interfaces;
using System.Collections.Generic;

namespace RealState.Common.Models
{
    public class Response : IResponse
    {
        public bool Status { get; set; }
        public dynamic Content { get; set; }
        public IList<string> Errors { get; set; }

        public Response()
        {
            Errors = new List<string>();
            Status = false;
            Content = null;
        }
    }
}
