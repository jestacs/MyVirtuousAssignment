using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sync
{
    public class ContactQueryRequest
    {
        public ContactQueryRequest()
        {
            Groups = new List<Conditions>();
            SortBy = "Id";
            Descending = false;
        }

        public List<Conditions> Groups { get; set; }

        public string SortBy { get; set; }

        public bool Descending { get; set; }
    }

    public class Conditions
    {
        public string Parameter { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
        public string SecondaryValue { get; set; }
        public List<string> Values { get; set; }
    }
}
