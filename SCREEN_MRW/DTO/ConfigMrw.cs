using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCREEN_MRW.DTO
{
    public class ConfigMrw
    {
        public string port_host { get; set; }
        public string ip { get; set; }
        public string lang { get; set; }
        public string branch { get; set; }
        public string screen_code { get; set; }
        public string folder_audio { get; set; }
        public string folder_video { get; set; }
        public string text_run { get; set; }
        public int width_screen { get; set; }
        public int height_screen { get; set; }
        public int screen_display { get; set; }
        public int time_next { get; set; }
        public int size_xmdqs { get; set; }
        public int size_num { get; set; }
        public int row_counter { get; set; }
    }
    public class Priority
    {
        public int priority_step { get; set; }
        public int internal_vip_card { get; set; }
        public int customer_vip_card { get; set; }
        public int privileged_customer { get; set; }
        public int moved_ticket { get; set; }
        public int booked_ticket { get; set; }
        public int restore_ticket { get; set; }
        public int min_priority_restricted { get; set; }
        public int min_priority_unordered_call { get; set; }
        public int priority_service { get; set; }
    }
    public class Config
    {
        public Priority priority { get; set; }
    }
}
