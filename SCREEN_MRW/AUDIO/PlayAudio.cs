using SCREEN_MRW.CONTROLLER;
using SCREEN_MRW.DTO;
using SCREEN_MRW.SOCKET;
using SCREEN_MRW.ULTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WMPLib;

namespace SCREEN_MRW.AUDIO
{
    public class PlayAudio
    {

        public const uint ES_CONTINUOUS = 0x80000000;
        public const uint ES_SYSTEM_REQUIRED = 0x00000001;
        public const uint ES_DISPLAY_REQUIRED = 0x00000002;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint SetThreadExecutionState([In] uint esFlags);
        /// <summary>
        private MRW_Socket socket;
        private Dictionary<string, Counter> dicCounter;
        public delegate void ReceivedEventHandler(EventAudio eAudio);
        public event ReceivedEventHandler DataReceived;
        Queue<ObjectAudio> sendQueueByNum = null;
        private Voice voice;
        string urlFolderAudio;

        private Dictionary<string, string> fileAudioVis = new Dictionary<string, string>();
        private Dictionary<string, string> fileAudioEns = new Dictionary<string, string>();
        private Dictionary<string, string> fileAudioSps = new Dictionary<string, string>();

        private readonly Mutex m = new Mutex();
        private readonly Mutex mQueue = new Mutex();
        private List<string> fileNums = null;
        Thread tPlayAudio;
        public void Init(ConfigMrw config)
        {
            urlFolderAudio = config.folder_audio;
            sendQueueByNum = new Queue<ObjectAudio>();
            tPlayAudio = new Thread(loopAudio);
            tPlayAudio.Name = "loop";
            tPlayAudio.Start();
            Thread.Sleep(1000);
            //initAudio();
            InitPlayAudio(null);
            InitSocket(config.branch, config.ip, config.screen_code, config.port_host);
            SetThreadExecutionState(ES_CONTINUOUS | ES_DISPLAY_REQUIRED);
        }

        private void loadAudio(string urlFolder)
        {
            AudioController audio = new AudioController();
            fileAudioVis = audio.loadFileAudio(urlFolder, ActionTicket.LANG_VI);
            fileAudioEns = audio.loadFileAudio(urlFolder, ActionTicket.LANG_EN);
            fileAudioSps = audio.loadFileAudio(urlFolder, ActionTicket.LANG_SP);
        }
        private void InitSocket(string branchCode1, string ip, string screenCode, string portHost)
        {
            socket = new MRW_Socket(branchCode1, ip, screenCode, portHost);
            socket.DataReceived += ReciveDataSocket;
        }

        private void ReciveDataSocket(EventSocketSendProgram eventData)
        {
            try
            {
                switch (eventData.Action)
                {
                    case ActionTicket.INITIAL:
                        dicCounter = sortCounter(eventData.DicAllCounter);
                        EventAudio eventSend = new EventAudio(ActionTicket.INITIAL, eventData.DicAllCounter, eventData.DicServing, null);
                        Thread.Sleep(2000);
                        DataReceived(eventSend);
                        return;
                    case ActionTicket.ASSETS:
                        voice = eventData.Voice;
                        loadAudio(urlFolderAudio);
                        Thread.Sleep(200);
                        return;
                    case ActionTicket.ACTION_CALL: call(eventData); return;
                    case ActionTicket.ACTION_RECALL: call(eventData); return;
                    case ActionTicket.ACTION_CANCEL: finish(eventData); return;
                    case ActionTicket.ACTION_MOVE: finish(eventData); return;
                    case ActionTicket.ACTION_FINISH: finish(eventData); return;
                    case ActionTicket.RELOAD:
                        SetThreadExecutionState(ES_CONTINUOUS);
                        tPlayAudio.Abort();
                        EventAudio eventSendRe = new EventAudio(ActionTicket.RELOAD, null, null, null);
                        DataReceived(eventSendRe);
                        return;
                    default:
                        return;
                }
            }
            catch
            {

            }

        }

        private void finish(EventSocketSendProgram eventData)
        {
            var ticket = eventData.Ticket;
            string counterID = "";
            counterID = ticket.counter_id;
            var objAudioFinish = new ObjectAudio("", "", "", "", counterID);
            EventAudio eventSendFinish = new EventAudio(eventData.Action, null, null, objAudioFinish);
            DataReceived(eventSendFinish);
        }
        void call(EventSocketSendProgram eventData)
        {

            if (eventData != null && eventData.Ticket != null && !string.IsNullOrWhiteSpace(eventData.Ticket.id))
            {
                var ticketID = eventData.Ticket.id;
                if (!checkExistNum(ticketID))
                {
                    var counterID = eventData.Ticket.counter_id;
                    var counterNum = dicCounter[counterID].Data.Cnum;
                    var objAudio = new ObjectAudio(eventData.Ticket.cnum, counterNum + "", ticketID, eventData.Ticket.lang, counterID);
                    mQueue.WaitOne();
                    this.sendQueueByNum.Enqueue(objAudio);
                    mQueue.ReleaseMutex();
                    EventAudio eventSendFinish = new EventAudio(eventData.Action, null, null, objAudio);
                    DataReceived(eventSendFinish);
                }
            }

        }

        bool checkExistNum(string tickID)
        {
            bool isCheck = false;
            mQueue.WaitOne();
            if (sendQueueByNum != null && sendQueueByNum.Count() > 0)
            {
                isCheck = sendQueueByNum.Any(m => m.TicketID == tickID);
            }

            mQueue.ReleaseMutex();
            return isCheck;
        }
        private Dictionary<string, Counter> sortCounter(IDictionary<string, Counter> dicTicket)
        {
            var list = dicTicket.ToList();
            list.Sort((pair1, pair2) => pair1.Value.Data.Cnum.CompareTo(pair2.Value.Data.Cnum));
            return list.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        #region PLAY AUDIO
        private void loopAudio()
        {
            mQueue.WaitOne();
            if (sendQueueByNum == null)
            {
                sendQueueByNum = new Queue<ObjectAudio>();
            }
            mQueue.ReleaseMutex();
            while (true)
            {
                m.WaitOne();
                try
                {
                    if ((status == READY || status == "Stopped") && objAudioCache == null && isCheckQueue())
                    {
                        RunAudio();
                    }
                    //else if (objAudioCache != null && status == READY)
                    //{
                    //    //EventAudio evenAuUp = new EventAudio(ActionTicket.ACTION_FINISH, null, null, objAudioCache);
                    //    //DataReceived(evenAuUp);
                    //    objAudioCache = null;
                    //}
                }
                catch
                {
                }
                m.ReleaseMutex();
                Thread.Sleep(1000);
            }


        }
        private bool isCheckQueue()
        {
            bool isCheck = false;
            mQueue.WaitOne();
            if (sendQueueByNum != null && sendQueueByNum.Count() > 0)
            {
                isCheck = true;
            }
            mQueue.ReleaseMutex();
            return isCheck;
        }
        private void RunAudio()
        {
            try
            {
                if (objAudioCache != null)
                {
                    EventAudio evenAuUp = new EventAudio(ActionTicket.ACTION_UPDATE_LABLE, null, null, objAudioCache);
                    DataReceived(evenAuUp);
                    objAudioCache = null;
                }
                mQueue.WaitOne();
                var objAudio = this.sendQueueByNum.Dequeue();
                mQueue.ReleaseMutex();
                objAudioCache = objAudio;
                addMedia(objAudio);
                InitPlayAudio(fileNums.First());
                EventAudio evenAu = new EventAudio(ActionTicket.ACTION_CALL_AUDIO, null, null, objAudioCache);
                DataReceived(evenAu);
                return;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void addMedia(ObjectAudio objAudio)
        {
            try
            {
                string lang = objAudio.Lang;
                var items = new List<Item>();
                Dictionary<string, string> dic = null;
                switch (lang)
                {
                    case ActionTicket.LANG_EN:
                        items = voice.i18n.en.items;
                        dic = fileAudioEns;
                        break;
                    case ActionTicket.LANG_SP:
                        items = voice.i18n.sp.items;
                        dic = fileAudioSps;
                        break;
                    default:
                        items = voice.i18n.vi.items;
                        dic = fileAudioVis;
                        break;
                }

                foreach (var item in items)
                {
                    switch (item.type)
                    {
                        case ActionTicket.TYPE_URL:
                            addItemList(item.value, dic);
                            break;
                        case ActionTicket.TYPE_DATA:
                            if (item.value == ActionTicket.TICKET_NUMBER)
                            {
                                foreach (char num in objAudio.Cnum)
                                {
                                    string numAudio = num.ToString();
                                    addItemList(numAudio, dic);
                                }
                            }
                            else
                            {
                                var num = objAudio.ArrNumC[0].ToString();
                                if (num.Equals("0"))
                                {
                                    foreach (char numC in objAudio.ArrNumC)
                                    {
                                        string numAudio = numC.ToString();
                                        addItemList(numAudio, dic);
                                    }
                                }
                                else
                                {
                                    string numC = objAudio.CounterNum;
                                    addItemList(numC, dic);
                                }
                            }
                            break;
                        default: break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        private void addItemList(string value, Dictionary<string, string> dic)
        {
            if (fileNums == null)
            {
                fileNums = new List<string>();
            }
            if (dic.ContainsKey(value))
            {
                fileNums.Add(dic[value]);
            }
        }
        private readonly string READY = "Ready";
        private string status = "Ready";
        private ObjectAudio objAudioCache;
        bool isPlayed = false;
        private void audio_PlayStateChange1(int newState)
        {
            status = wplayer.status;
            if (newState == 3)
            {
                isPlayed = true;
            }

            if (isPlayed && newState == 1)
            {
                isPlayed = false;
                if (fileNums != null && fileNums.Count > 0)
                {
                    fileNums.RemoveAt(0);
                }
                if (fileNums == null || fileNums.ToList().Count() == 0)
                {
                    status = READY;
                    objAudioCache = null;
                }
                else if (fileNums != null && fileNums.Count > 0)
                {

                    InitPlayAudio(fileNums.First());
                }
            }
        }
        WMPLib.WindowsMediaPlayer wplayer = null;
        private void MediaError1(object pMediaObject)
        {
            InitPlayAudio(fileNums.First());
        }
        private void InitPlayAudio(string pathFile)
        {
            wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(audio_PlayStateChange1);
            wplayer.MediaError += new WMPLib._WMPOCXEvents_MediaErrorEventHandler(MediaError1);
            if (pathFile != null)
            {
                wplayer.URL = pathFile;
                wplayer.controls.play();
            }

        }
        #endregion PLAY AUDIO
    }


}
