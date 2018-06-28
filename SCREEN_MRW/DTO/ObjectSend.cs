using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCREEN_MRW.DTO
{
    public class ObjectSend
    {
        public string action { get; set; }
        public string ticket_id { get; set; }
        public string state { get; set; }
        public string cnum { get; set; }
        public string counter_id { get; set; }
        public string counter_Old { get; set; }
        public int mtime { get; set; }
        public string service_id { get; set; }
        public ExtraSend extra { get; set; }
        public ObjectSend(string action, string ticket_id, string state, string counterID, string cnum, string platform, bool record_transaction)
        {
            this.action = action;
            this.state = state;
            this.cnum = cnum;

            this.ticket_id = ticket_id;
            ExtraSend extra = new ExtraSend(platform, record_transaction);
            this.extra = extra;
            this.counter_id = counterID;
            this.counter_Old = counterID;
        }
        public ObjectSend() { }
    }
    public class ExtraSend
    {
        public string[] services;
        public string[] counters;
        public bool record_transaction { get; set; }
        public string platform { get; set; }
        public ExtraSend(string platform, bool record_transaction)
        {
            this.platform = platform;
            this.record_transaction = record_transaction;
        }
    }
}
