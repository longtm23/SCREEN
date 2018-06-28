using SCREEN_MRW.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCREEN_MRW.CONTROLLER
{
    public class EventSocketSendProgram : EventArgs
    {
        private string action;
        
        private Dictionary<string, Counter> dicAllCounter;

        private Dictionary<string, Ticket> dicServing;

        private Ticket ticket;

        private Voice voice;

      

        public EventSocketSendProgram(string action, Ticket ticket, Dictionary<string, Counter> dicAllCounter, Dictionary<string, Ticket> dicServing, Voice voice)
        {
            this.Action = action;
            this.DicAllCounter = dicAllCounter;
            this.DicServing = dicServing;
            this.Ticket = ticket;
            this.Voice = voice;
        }
         public Voice Voice
        {
            get { return voice; }
            set { voice = value; }
        }
        public string Action
        {
            get { return action; }
            set { action = value; }
        }

        public Dictionary<string, Counter> DicAllCounter
        {
            get { return dicAllCounter; }
            set { dicAllCounter = value; }
        }
         public Ticket Ticket
        {
            get { return ticket; }
            set { ticket = value; }
        }

        public Dictionary<string, Ticket> DicServing
        {
            get { return dicServing; }
            set { dicServing = value; }
        }
    }

}
