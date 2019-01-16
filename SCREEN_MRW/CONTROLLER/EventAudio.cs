using SCREEN_MRW.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCREEN_MRW.CONTROLLER
{
    public class EventAudio : EventArgs
    {
        Dictionary<string, Counter> dicCounter;
        Dictionary<string, Ticket> dicServing;
        ObjectAudio audio;
        string action;
        public Dictionary<string, Ticket> DicServing
        {
            get { return dicServing; }
            set { dicServing = value; }
        }
        public string Action
        {
            get { return action; }
            set { action = value; }
        }


        public ObjectAudio Audio
        {
            get { return audio; }
            set { audio = value; }
        }
        public Dictionary<string, Counter> DicCounter
        {
            get { return dicCounter; }
            set { dicCounter = value; }
        }

        public EventAudio(string action, Dictionary<string, Counter> dicCounter,  Dictionary<string, Ticket> dicServing, ObjectAudio audio )
        {
            this.Action = action;
            this.DicCounter = dicCounter;
            this.audio = audio;
            this.DicServing = dicServing;
        }
    }
}
