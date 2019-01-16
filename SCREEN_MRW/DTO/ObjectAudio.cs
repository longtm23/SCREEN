using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCREEN_MRW.DTO
{
    public class ObjectAudio
    {
        public string TicketID { get; set; }
        public string CounterID { get; set; }
        public string Lang { get; set; }
        public string Cnum {get;set;}
        public string CounterNum { get; set; }
        public char[] ArrNumT { get; set; }
        public char[] ArrNumC { get; set; }
        public ObjectAudio(char[] num, string counterNum)
        {
            this.ArrNumT = num;
            this.CounterNum = counterNum;

        }
        public ObjectAudio(string num, string counterNum, string ticketID, string lang, string counterID)
        {
            this.ArrNumT = split(num);
            this.ArrNumC = split(counterNum);
            this.TicketID = ticketID;
            this.Cnum = num;
            this.CounterNum = counterNum;
            this.Lang = lang;
            this.CounterID = counterID;
        }

        char[] split(string num)
        {
            return num.ToCharArray();
        }
    }
}
