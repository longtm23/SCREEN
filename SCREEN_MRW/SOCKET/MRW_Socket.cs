using Newtonsoft.Json;
using SCREEN_MRW.CONTROLLER;
using SCREEN_MRW.DTO;
using SCREEN_MRW.ULTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using WebSocket4Net;

namespace SCREEN_MRW.SOCKET
{
    public class MRW_Socket
    {
        //private string LangCd = null;
        private string IP = null;
        private string Port = null;
        private string UserId = null;
        private string BranchCode = null;
        private WebSocket webSocket;
        private SetDataSocketInitial setData = null;

        public delegate void ReceivedEventHandler(EventSocketSendProgram objectData);
        public event ReceivedEventHandler DataReceived;
        bool isReload = false;
        bool isInitial = true;
        public const uint ES_CONTINUOUS = 0x80000000;
        public const uint ES_SYSTEM_REQUIRED = 0x00000001;
        public const uint ES_DISPLAY_REQUIRED = 0x00000002;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint SetThreadExecutionState([In] uint esFlags);

        public MRW_Socket(string branchCode, string ip, string screenCode, string portHost)
        {
            Init_WebSocket(branchCode, ip, screenCode, portHost);
            SetThreadExecutionState(ES_CONTINUOUS | ES_DISPLAY_REQUIRED);
            setData = new SetDataSocketInitial();
        }
        private void Init_WebSocket(string branchCode, string ip, string userID, string portHost)
        {
            //this.LangCd = langCd;
            this.IP = ip;
            this.Port = portHost;
            this.BranchCode = branchCode;
            this.UserId = userID;
            string url = "ws://" + ip + ":" + portHost + "/room/actor/join?branch_code=" + branchCode + "&actor_type=screen&screen_code=" + userID + "&reconnect_count=0";
            webSocket = new WebSocket(url);
            webSocket.Opened += websocket_Opened;
            webSocket.Error += websocket_Error;
            webSocket.Closed += websocket_Closed;
            webSocket.MessageReceived += websocket_MessageReceived;
            webSocket.EnableAutoSendPing = true;
            try
            {
                webSocket.Open();
            }
            catch
            {
                OpendSocket();
            }
        }
        private static System.Timers.Timer aTimer;
        private void SetTimer()
        {
            // Create a timer with a two second interval.
            if (aTimer == null)
            {
                aTimer = new System.Timers.Timer(5000);
                // Hook up the Elapsed event for the timer. 
                aTimer.Elapsed += OnTimedEvent;
                aTimer.AutoReset = true;
                aTimer.Enabled = true;
            }
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            ScreenCtrl sCtrl = new ScreenCtrl();
            if (sCtrl.GetConfig("http://" + IP + ":" + Port))
            {
                Init_WebSocket(BranchCode, IP, UserId, Port);
                aTimer.Enabled = false;
                aTimer = null;
            }
        }
        public void OpendSocket()
        {
            try
            {
                if (webSocket.State != WebSocketState.Connecting && webSocket.State != WebSocketState.Open)
                {

                    while (webSocket.State != WebSocketState.Open)
                    {
                        webSocket.Open();
                    }

                }
            }
            catch
            {
                this.webSocket = null;
                SetTimer();
                return;
            }
            finally
            {
                Thread.Sleep(200);
            }
        }

        public void CloseSocket()
        {
            while (webSocket != null && webSocket.State != WebSocketState.Closed)
            {
                webSocket.Close();
                while (true)
                {
                    if (webSocket.State == WebSocketState.Closed)
                    {
                        return;
                    }
                    Thread.Sleep(200);
                }
            }
        }
        public void websocket_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Opened Socket");
        }
        public void websocket_Error(object sender, EventArgs e)
        {
            Console.WriteLine("Error Socket");

        }
        public void websocket_Closed(object sender, EventArgs e)
        {
            Console.WriteLine("Close Socket");

            while (webSocket != null && webSocket.State == WebSocketState.Closed && !isReload)
            {
                OpendSocket();
                Thread.Sleep(200);
            }
        }
        public void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Received(e.Message.TrimEnd('\0'));
        }
        public void Received(string str)
        {
            var handle = str.Split(' ')[0].Trim('/');
            var data = str.Remove(0, str.IndexOf(' '));

            Console.WriteLine("handle " + handle);
            try
            {
                switch (handle)
                {
                    case ActionTicket.INITIAL:
                        if (isInitial)
                        {
                            Initial dataUser = JsonConvert.DeserializeObject<Initial>(data);
                            EventSocketSendProgram eventSend = new EventSocketSendProgram(ActionTicket.INITIAL, null, dataUser.Counters, dataUser.Serving, null);
                            Thread.Sleep(2000);
                            DataReceived(eventSend);
                        }
                        break;
                    case ActionTicket.ASSETS:
                        if (isInitial)
                        {
                            isInitial = false;
                            var voice = JsonConvert.DeserializeObject<List<Asset>>(data);
                            if (voice.Count() > 0)
                            {
                                EventSocketSendProgram eventSend1 = new EventSocketSendProgram(ActionTicket.ASSETS, null, null, null, voice[0].voice);
                                DataReceived(eventSend1);
                            }
                        }
                        break;
                    case ActionTicket.TICKET_ACTION:
                        var tkAc = JsonConvert.DeserializeObject<TicketActionRecive>(data);
                        string action = tkAc.action;
                        switch (action)
                        {
                            case ActionTicket.ACTION_FEEDBACK: break;
                            case ActionTicket.ACTION_MOVE:
                                Ticket tk = tkAc.ticket;
                                tk.counter_id = tkAc.counter_id;
                                EventSocketSendProgram eventSendMove = new EventSocketSendProgram(action, tk, null, null, null);
                                DataReceived(eventSendMove); break;
                            case ActionTicket.ACTION_FINISH:
                            case ActionTicket.ACTION_CANCEL:
                            case ActionTicket.ACTION_RECALL:
                            case ActionTicket.ACTION_CALL:
                                string counterID = tkAc.ticket.counter_id;
                                string cnum = tkAc.ticket.cnum;
                                string ticketID = tkAc.ticket.id;
                                EventSocketSendProgram eventSendTK = new EventSocketSendProgram(action, tkAc.ticket, null, null, null);
                                DataReceived(eventSendTK);
                                break;
                            default:
                                break;
                        }
                        break;
                    case ActionTicket.RELOAD:
                        isReload = true;
                        SetThreadExecutionState(ES_CONTINUOUS);
                        Thread.Sleep(500);
                        EventSocketSendProgram eventSendReload = new EventSocketSendProgram(ActionTicket.RELOAD, null, null, null, null);
                        DataReceived(eventSendReload);
                        CloseSocket();// khi close sẽ tự mở
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi send data" + ex.Message);
            }
        }
    }
}
