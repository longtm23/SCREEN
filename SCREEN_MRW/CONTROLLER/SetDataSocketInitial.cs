using SCREEN_MRW.DTO;
using SCREEN_MRW.ULTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCREEN_MRW.CONTROLLER
{
    public class SetDataSocketInitial
    {
       private string platform = ActionTicket.PLATFORM;
        public ObjectSend SetDataFromTicketInitial(Ticket ticket, string action)
        {
            var counterID = ticket.counter_id;
            return new ObjectSend(action, ticket.id, ticket.state, counterID, ticket.cnum, platform, true);
        }

        public Dictionary<string, Dictionary<string, ObjectSend>> SetDataToObjectSend(Initial initial)
        {
            string action = "";
            Dictionary<string, Dictionary<string, ObjectSend>> dicObjSend = new Dictionary<string, Dictionary<string, ObjectSend>>();
            Dictionary<string, ObjectSend> lstObjectWaiting = new Dictionary<string, ObjectSend>();
            Dictionary<string, ObjectSend> lstObjectCanceled = new Dictionary<string, ObjectSend>();
            Dictionary<string, ObjectSend> lstObjectServing = new Dictionary<string, ObjectSend>();

            SetDataSocketInitial setData = new SetDataSocketInitial();

            if (initial != null)
            {
                action = ActionTicket.INITIAL;
                foreach (var ticketDic in initial.Serving)
                {
                    var ticket = ticketDic.Value;
                    var state = ticket.state;
                    ObjectSend objSend = setData.SetDataFromTicketInitial(ticket, action);
                    AddList(state, lstObjectWaiting, lstObjectCanceled, lstObjectServing, objSend);
                }
            }

            dicObjSend.Add(ActionTicket.STATE_SERVING, lstObjectServing);
            return dicObjSend;
        }
        public void AddList(string state, Dictionary<string, ObjectSend> lstObjectWaiting, Dictionary<string, ObjectSend> lstObjectCanceled,
            Dictionary<string, ObjectSend> lstObjectServing, ObjectSend objSend)
        {
            switch (state)
            {
                case ActionTicket.STATE_SERVING:
                    lstObjectServing.Add(objSend.ticket_id, objSend);
                    break;
                default:
                    break;
            }
        }
  }
}
