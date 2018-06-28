using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCREEN_MRW.DTO
{
    public class Counter
    {
        public string EId { get; set; }
        public string bId { get; set; }
        public string Name { get; set; }
        public Data Data { get; set; }
        public bool IsUse { get; set; }
    }
    public class Initial
    {
        public Dictionary<string, Counter> Counters { get; set; }
        public Dictionary<string, Ticket> Serving { get; set; }
        public Initial()
        {
            Counters = new Dictionary<string, Counter>();
            Serving = new Dictionary<string, Ticket>();
        }
    }

    public class Data
    {
        public string Cnum { get; set; }
    }
}
