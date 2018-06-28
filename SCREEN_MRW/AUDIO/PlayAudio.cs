using SCREEN_MRW.CONTROLLER;
using SCREEN_MRW.DTO;
using SCREEN_MRW.SOCKET;
using SCREEN_MRW.ULTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SCREEN_MRW.AUDIO
{
    public class PlayAudio
    {
        private MRW_Socket socket;
        private Dictionary<string, Counter> dicCounter;
        Queue<ObjectAudio> sendQueueByNum = null;
        private Voice voice;
        WMPLib.WindowsMediaPlayer audioPlayer;
        private Dictionary<string, string> fileAudioVis = new Dictionary<string, string>();
        private Dictionary<string, string> fileAudioEns = new Dictionary<string, string>();
        private Dictionary<string, string> fileAudioSps = new Dictionary<string, string>();
        private void Init(ConfigMrw config)
        {
            loadAudio(config.folder_audio);
            audioPlayer = new WMPLib.WindowsMediaPlayer();
            audioPlayer.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(audio_PlayStateChange);
            InitSocket(config.branch, config.ip, config.screen_code, config.port_host);
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
            Console.WriteLine("Open Socket");
            socket.OpendSocket();
            Console.WriteLine("Done socket");
            socket.DataReceived += ReciveDataSocket;
        }

        private void ReciveDataSocket(EventSocketSendProgram eventData)
        {
            switch (eventData.Action)
            {
                case ActionTicket.INITIAL:
                    dicCounter = sortCounter(eventData.DicAllCounter);
                    return;
                case ActionTicket.ASSETS:
                    voice = eventData.Voice;
                    return;
                case ActionTicket.ACTION_CREATE:// không làm gì
                    break;
                case ActionTicket.ACTION_CALL:
                case ActionTicket.ACTION_RECALL:
                    var ticketID = eventData.Ticket.id;
                    var counterID = eventData.Ticket.counter_id;
                    var counterNum = dicCounter[counterID].Data.Cnum;
                    var objAudio = new ObjectAudio(eventData.Ticket.cnum, counterNum, ticketID, eventData.Ticket.lang);
                    this.sendQueueByNum.Enqueue(objAudio);
                    break;
                case ActionTicket.ACTION_RESTORE:
                    break;
                case ActionTicket.ACTION_MOVE:
                case ActionTicket.ACTION_CANCEL:
                case ActionTicket.ACTION_FINISH:
                    break;
                default:
                    //bug
                    break;
            }
        }
        private Dictionary<string, Counter> sortCounter(IDictionary<string, Counter> dicTicket)
        {
            var list = dicTicket.ToList();
            list.Sort((pair1, pair2) => pair1.Value.Data.Cnum.CompareTo(pair2.Value.Data.Cnum));
            return list.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        #region PLAY AUDIO
        private void PlayAudio()
        {
            try
            {
                audioPlayer.currentPlaylist.clear();
                var objAudio = this.sendQueueByNum.Dequeue();
                addMedia(objAudio);
                audioPlayer.controls.play();
                return;
            }
            catch
            {
            }
        }

        private void addMedia(ObjectAudio objAudio)
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
                        addMediaItem(item.value, dic); break;
                    case ActionTicket.TYPE_DATA:
                        if (item.value == ActionTicket.TICKET_NUMBER)
                        {
                            foreach (char num in objAudio.Num)
                            {
                                string numAudio = num.ToString();
                                addMediaItem(numAudio, dic);
                            }
                        }
                        else
                        {
                            foreach (char num in objAudio.CounterNum)
                            {
                                string numAudio = num.ToString();
                                addMediaItem(numAudio, dic);
                            }
                        }
                        break;
                    default: break;
                }
            }
        }

        private void addMediaItem(string value, Dictionary<string, string> dic)
        {
            if (dic.ContainsKey(value))
            {
                WMPLib.IWMPMedia add = audioPlayer.newMedia(dic[value]);
                audioPlayer.currentPlaylist.appendItem(add);
            }
        }
        private readonly string READY = "Ready";
        string status = "Ready";
        private void audio_PlayStateChange(int newState)
        {
            status = audioPlayer.status;

            if (status == READY || status == "")
            {
                status = READY;
                // ban su kien sang form
            }
        }
        #endregion PLAY AUDIO
    }
       

}
