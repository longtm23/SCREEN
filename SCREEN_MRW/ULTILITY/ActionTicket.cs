using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCREEN_MRW.ULTILITY
{
    public class ActionTicket
    {
        public const string TICKET_ONCE = "/ticketsuper?once=";
        public const string ERROR_SERVER = "error";
        public const string INITIAL = "initial";
        public const string ASSETS = "assets";
        public const string RELOAD = "reload";
        public const string TICKET_ACTION = "ticket_action";

        public const string ACTION_CALL = "call";
        public const string ACTION_CALL_AUDIO = "call_au";
        public const string ACTION_RECALL = "recall";
        public const string ACTION_MOVE = "move";
        public const string ACTION_CANCEL = "cancel";
        public const string ACTION_FINISH = "finish";
        public const string ACTION_FEEDBACK = "feedback";

        public const string ACTION_UPDATE_LABLE = "up_label";
        public const string ACTION_DEBUG = "debug";

        public const string ACTION_CREATE = "create";
        public const string ACTION_RESTORE = "restore";
        public const string ACTION_ALL_RESTORE = "all_restore";
        public const string ACTION_MOVE_SERVICE = "move_service";
        public const string ACTION_MOVE_COUNTER = "move_counter";
        public const string ACTION_RESET_COUNTER = "reload";
        public const string CALL_LIST_WATTING = "call_list_watting";
        public const string CALL_PRIORITY = "call_priority";
        public const string STATE_WATING = "waiting";
        public const string STATE_CANCELLED = "cancelled";
        public const string STATE_SERVING = "serving";

        public const int LENGH_RANDOM = 5;
        public const string PLATFORM = "keyboard";
        public const int DEVICE_ID = 1;


        public const string LANG_VI = "vi";
        public const string LANG_SP = "sp";
        public const string LANG_EN = "en";

        public const string TYPE_URL = "url";
        public const string TYPE_DATA = "data";
        public const string TICKET_NUMBER = "ticket_number";
        public const string COUNTER_NUMBER = "counter_number";
    }
}
