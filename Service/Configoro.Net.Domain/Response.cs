using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configoro.Net.Domain
{
    public class Response<T>
    {
        public Response()
        {
            Errors = new List<string>();
        }
        public T Entity { get; set; }
        public List<string> Errors { get; set; }
        public List<string> Message { get; set; }
        public bool IsErrored
        {
            get
            {
                return Errors.Count > 0;
            }
        }
    }
}
