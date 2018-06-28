using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCREEN_MRW.DTO
{
    public class TicketActionRecive
    {
        public string action { get; set; }
        public Ticket ticket { get; set; }
        public Extra extra { get; set; }
    }
    public class Ticket
    {
        public string id { get; set; }
        public string branch_id { get; set; }
        public string counter_id { get; set; }
        public string cnum { get; set; }
        public long mtime { get; set; }
        public string state { get; set; }
        public string lang { get; set; }
        public TicketPriority ticket_priority { get; set; }

    }
    public class TicketPriority
    {
        public int restore_ticket { get; set; }
        public int moved_ticket { get; set; }
        public TicketPriority(int restore, int moved)
        {
            this.restore_ticket = restore;
            this.moved_ticket = moved;
        }
    }
    public class Extra
    {
        public string kiosk_id { get; set; }
        public string lang { get; set; }
        public string[] services { get; set; }
        public string[] counters { get; set; }
    }
}
