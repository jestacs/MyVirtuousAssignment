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
            Groups = new List<Group>();
            SortBy = "Id";
            Descending = false;
        }

        public List<Group> Groups { get; set; }
        public string SortBy { get; set; }
        public bool Descending { get; set; }
    }

    public class Condition
    {
        public string Parameter { get; set; }
        public string Value { get; set; }
        public string SecondaryValue { get; set; }
        public List<string> Values { get; set; }
    }

    public class Group
    {
        public List<Condition> Conditions { get; set; }
    }
}
