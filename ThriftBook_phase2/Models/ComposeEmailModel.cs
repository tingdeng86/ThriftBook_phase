using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThriftBook_phase2.Models
{
    public class ComposeEmailModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }

    }
}
