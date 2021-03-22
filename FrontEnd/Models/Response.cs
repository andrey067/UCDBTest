using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Models
{
    public class Response
    {


        public string message { get; set; }
        public bool success { get; set; }

        public List<Produto> data { get; set; }
        public Response(string message, bool success)
        {
            this.message = message;
            this.success = success;
        }
        public Response() { }



    }
}
