using SCREEN_MRW.CONTROLLER;
using SCREEN_MRW.DTO;
using SCREEN_MRW.SOCKET;
using SCREEN_MRW.ULTILITY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace SCREEN_MRW
{
    public partial class SCREEN : Form
    {
        #region PARAM
        private string[] fileVideos;
        private Dictionary<string, string> fileAudioVis = new Dictionary<string, string>();
        private Dictionary<string, string> fileAudioEns = new Dictionary<string, string>();
        private Dictionary<string, string> fileAudioSps = new Dictionary<string, string>();
        WMPLib.IWMPPlaylist playlist;
        MRW_Socket socket = null;
        Mutex m;
        WMPLib.IWMPMedia media;
        private Dictionary<string, Counter> dicCounter;
        Queue<ObjectAudio> sendQueueByNum = null;
        //BackgroundWorker comSendWorker;
        WMPLib.WindowsMediaPlayer audioPlayer;
        ConfigMrw config;
        private readonly string READY = "Ready";
        private const string TICKET_LEFT_LABEL = "lblLefTicket";
        private const string COUNTER_LEFT_LABEL = "lblLefCounter";
        private const string PANEL_COUNTER_LEFT_IN = "panelCounterLeftIn";
        private const string PANEL_COUNTER_LEFT = "panelCounterLeft";
        private string status = "Ready";
        private Voice voice;
        private Dictionary<string, bool> lsPanelCounter;
        bool isRunning;
        #endregion

        #region CONTRUCTOR
        public SCREEN(ConfigMrw mrwConfig)
        {
            InitializeComponent();
            config = mrwConfig;
            Init();
        }
        private void ContructorParam()
        {
            m = new Mutex();
            sendQueueByNum = new Queue<ObjectAudio>();
            audioPlayer = new WMPLib.WindowsMediaPlayer();
            audioPlayer.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(audio_PlayStateChange);
            lsPanelCounter = new Dictionary<string, bool>();
        }

        private void evenLoop()
        {
            while (sendQueueByNum.Count() != 0)
            {
                isRunning = true;
                if (status == READY)
                {
                    m.WaitOne();
                    PlayAudio();
                    Thread.Sleep(1000);
                    m.ReleaseMutex();
                }
            }
            isRunning = false;
        }

        private void Init()
        {

            loadUI();
            ContructorParam();
            loadVideo(@config.folder_video);
            loadAudio(@config.folder_audio);
            InitSocket(config.branch, config.ip, config.screen_code, config.port_host);
            SetTimer();


        }
        private void loadUI()
        {
            drawUI();
            hideFooter(false);
            wmpVideo.settings.setMode("loop", true);
        }
        int heightPanelCounter = 0;
        int heightCou = 0;
        int heightCoup = 0;
        int widthLeft = 0;

        private void drawLeft(Dictionary<string, Counter> dicCou)
        {

            var panelFCouterAll = (float)(dicCou.Count / 4);
            var panelICouterAll = (int)panelFCouterAll;
            if (panelFCouterAll > panelICouterAll)
            {
                panelICouterAll += 1;
            }
            var count = dicCou.Values.Count();
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate()
                   {
                       for (int i = 0; i < panelICouterAll; i++)
                       {
                           var panelCounterAllNext = new System.Windows.Forms.Panel();
                           this.panelLeft.Controls.Add(panelCounterAllNext);

                           //panelCounterLeft
                           string name = PANEL_COUNTER_LEFT + i;
                           panelCounterAllNext.Dock = System.Windows.Forms.DockStyle.Fill;
                           panelCounterAllNext.Location = new System.Drawing.Point(0, 100);
                           panelCounterAllNext.Margin = new System.Windows.Forms.Padding(0);
                           panelCounterAllNext.Name = name;
                           panelCounterAllNext.Size = new System.Drawing.Size(widthLeft, heightPanelCounter);
                           panelCounterAllNext.TabIndex = 0;
                           panelCounterAllNext.BackColor = Color.FromArgb(52, 69, 139);
                           var panelAllNew2 = new System.Windows.Forms.Panel();
                           panelCounterAllNext.Controls.Add(panelAllNew2);

                           if (i == 0)
                           {
                               lsPanelCounter.Add(name, true);
                               panelCounterAllNext.Visible = true;
                           }
                           else
                           {
                               lsPanelCounter.Add(name, false);
                               panelCounterAllNext.Visible = false;
                           }
                           var k = 0;
                           for (int t = 0; t < count; t++)
                           {

                               if (k - 4 == 0)
                               {
                                   k = 0;
                                   break;
                               }
                               else
                               {
                                   if (t >= i * 4)
                                   {
                                       var cou = dicCou.Values.ToList()[t];
                                       var panelAllNew = new System.Windows.Forms.Panel();
                                       panelCounterAllNext.Controls.Add(panelAllNew);
                                       var panelCouNew = new System.Windows.Forms.Panel();
                                       panelAllNew.Controls.Add(panelCouNew);
                                       var panelTicketNew = new System.Windows.Forms.Panel();
                                       panelAllNew.Controls.Add(panelTicketNew);
                                       var lblTicketNew = new System.Windows.Forms.Label();
                                       panelTicketNew.Controls.Add(lblTicketNew);
                                       var lblCounterNew = new System.Windows.Forms.Label();
                                       panelCouNew.Controls.Add(lblCounterNew);

                                       lblCounterNew.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Left)
                                           | System.Windows.Forms.AnchorStyles.Right))));
                                       lblCounterNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 30.1F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
                                       lblCounterNew.Location = new System.Drawing.Point(0, 100);
                                       lblCounterNew.Dock = System.Windows.Forms.DockStyle.Fill;
                                       lblCounterNew.Name = "cou" + cou.EId;
                                       lblCounterNew.ForeColor = Color.White;
                                       lblCounterNew.TabIndex = 3;
                                       lblCounterNew.Text = "CỬA SỐ " + cou.Data.Cnum;
                                       lblCounterNew.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                                       // 
                                       // lblTicket3
                                       // 
                                       lblTicketNew.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                        | System.Windows.Forms.AnchorStyles.Left)
                                        | System.Windows.Forms.AnchorStyles.Right)));
                                       lblTicketNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 90F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
                                       lblTicketNew.Dock = System.Windows.Forms.DockStyle.Fill;
                                       lblTicketNew.Location = new System.Drawing.Point(0, 0);
                                       lblTicketNew.ForeColor = Color.White;
                                       lblTicketNew.Name = "tk" + cou.EId;
                                       lblTicketNew.TabIndex = 3;
                                       lblTicketNew.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                                       // panelTicketNew

                                       panelTicketNew.Dock = System.Windows.Forms.DockStyle.Fill;
                                       panelTicketNew.Location = new System.Drawing.Point(0, 0);
                                       panelTicketNew.Name = "panelTicketLeft" + cou.EId;
                                       panelTicketNew.TabIndex = 2;
                                       panelTicketNew.BackColor = Color.White;
                                       panelTicketNew.BackColor = Color.FromArgb(52, 69, 139);

                                       // 
                                       // panel3Cou3
                                       // 
                                       panelCouNew.Dock = System.Windows.Forms.DockStyle.Bottom;
                                       panelCouNew.Location = new System.Drawing.Point(0, 70);
                                       panelCouNew.Name = "panelCountNew" + cou.EId;
                                       panelCouNew.TabIndex = 2;
                                       panelCouNew.ClientSize = new System.Drawing.Size(widthLeft, heightCoup);
                                       panelCouNew.BackColor = Color.White;
                                       panelCouNew.BackColor = Color.FromArgb(3, 23, 109);

                                       panelAllNew.Dock = System.Windows.Forms.DockStyle.Bottom;
                                       panelAllNew.Location = new System.Drawing.Point(0, 0);
                                       panelAllNew.Margin = new System.Windows.Forms.Padding(0);
                                       panelAllNew.Name = PANEL_COUNTER_LEFT_IN + cou.EId;
                                       panelAllNew.ClientSize = new System.Drawing.Size(widthLeft, heightCou);
                                       //panelAllNew.Size = new System.Drawing.Size(0, heightCou);
                                       panelAllNew.TabIndex = 1;
                                       k++;
                                   }
                               }
                           }
                       }
                   });
            }

        }

        private void updateTicketLable(Dictionary<string, Ticket> dicServing)
        {
            foreach (var tk in dicServing.Values.ToList())
            {
                loadLableTiket(tk.cnum, tk.counter_id);
            }
        }
        private void drawUI()
        {
            int weightAll = Screen.PrimaryScreen.Bounds.Width;
            int heightAll = Screen.PrimaryScreen.Bounds.Height;
            widthLeft = weightAll / 4;
            var widthFooter = weightAll - widthLeft;
            //draw left
            panelLeft.ClientSize = new System.Drawing.Size(widthLeft, heightAll);
            var heightLogo = heightAll / 6;
            panelLogo.ClientSize = new System.Drawing.Size(widthLeft, heightLogo);
            heightPanelCounter = heightAll - heightLogo;
            heightCou = heightPanelCounter / 4;
            heightCoup = heightCou / 4;
            //draw right
            panelFooter.ClientSize = new System.Drawing.Size(widthFooter, heightCou);
            panelFooterVideo.ClientSize = new System.Drawing.Size(widthFooter, heightCoup);
            var widthInFooter = widthFooter / 4;

            panelBegin.ClientSize = new System.Drawing.Size(widthInFooter, heightCou);
            panelTicket.ClientSize = new System.Drawing.Size(widthInFooter, heightCou);
            panelAfter.ClientSize = new System.Drawing.Size(widthInFooter, heightCou);
            panelCounter.ClientSize = new System.Drawing.Size(widthInFooter, heightCou);
            var heightTicket = heightCou - heightCoup;
        }

        #endregion

        #region VIDEO
        private void loadVideo(string urlFolder)
        {
            if (Directory.Exists(urlFolder))
            {
                fileVideos = Directory.GetFiles(urlFolder, "*.mp4");
                playlist = wmpVideo.playlistCollection.newPlaylist("a");
                foreach (string nameFile in fileVideos)
                {
                    media = wmpVideo.newMedia(nameFile);
                    playlist.appendItem(media);
                }
                wmpVideo.currentPlaylist = playlist;
                wmpVideo.Ctlcontrols.play();
            }
            else
            {
                MessageBox.Show("thư mục video quảng cáo không tồn tại!");
            }
        }
        private void wmpVideo_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (wmpVideo.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                wmpVideo.Ctlcontrols.play();
            }
        }
        #endregion

        #region AUDIO
        private Dictionary<string, string> loadFileAudio(string urlFolder, string lang)
        {
            var dic = new Dictionary<string, string>();
            var url = urlFolder + "\\" + lang;
            if (Directory.Exists(url))
            {
                var arrAudio = Directory.GetFiles(url, "*.mp3");
                char[] reg = { '.', '\\' };
                foreach (var iNum in arrAudio)
                {
                    var key = getFileName(iNum, reg);
                    dic.Add(key, iNum);
                }
            }
            else
            {
                MessageBox.Show("thư mục audio không tồn tại!");
            }
            return dic;
        }

        private void loadAudio(string urlFolder)
        {
            fileAudioVis = loadFileAudio(urlFolder, ActionTicket.LANG_VI);
            fileAudioEns = loadFileAudio(urlFolder, ActionTicket.LANG_EN);
            fileAudioSps = loadFileAudio(urlFolder, ActionTicket.LANG_SP);
        }
        private string getFileName(string url, char[] reg)
        {
            var str = url.Split(reg);
            return str[str.Length - 2];
        }
        #endregion

        #region SOCKET
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
                    drawLeft(dicCounter);
                    Thread.Sleep(1000);
                    updateTicketLable(eventData.DicServing);
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
                    loadLableTiket(eventData.Ticket.cnum, counterID);
                    if (!isRunning)
                    {
                        evenLoop();
                    }
             
                    break;
                case ActionTicket.ACTION_RESTORE:
                    break;
                case ActionTicket.ACTION_MOVE:
                case ActionTicket.ACTION_CANCEL:
                case ActionTicket.ACTION_FINISH:
                    loadLableTiket("", eventData.Ticket.counter_id);
                    break;
                default:
                    //bug
                    break;
            }
        }
        private void loadLableTiket(string num, string couID)
        {
            var name = "tk" + couID;
            Label tk = this.Controls.Find(name, true).FirstOrDefault() as Label;
            if (tk != null)
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)delegate()
                    {
                        tk.Text = num;
                    });
                }
                else
                {
                    tk.Text = num;
                }
            }
        }
        #endregion SOCKET

        #region PLAY AUDIO
        private void PlayAudio()
        {
            try
            {
                audioPlayer.currentPlaylist.clear();
                var objAudio = this.sendQueueByNum.Dequeue();
                addMedia(objAudio);
                audioPlayer.controls.play();
                UpdateUI(objAudio.Cnum, objAudio.CounterNum);
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
        private void audio_PlayStateChange(int newState)
        {
            status = audioPlayer.status;

            if (status == READY || status == "")
            {
                status = READY;
                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)delegate()
                    {
                        hideFooter(false);
                    });
                }
                else
                {
                    hideFooter(false);
                }
            }
        }
        #endregion PLAY AUDIO

        #region UPDATE FOOTER
        private void UpdateUI(string cNum, string counterNum)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate()
                {
                    hideFooter(true);
                    this.lblTicket.Text = cNum;
                    this.lblCounter.Text = counterNum;
                });
            }
            else
            {
                hideFooter(true);
                this.lblTicket.Text = cNum;
                this.labelBf.Text = counterNum;
            }

        }

        private void hideFooter(bool isHide)
        {
            this.labelBf.Visible = isHide;
            this.lblAf.Visible = isHide;
            this.lblTicket.Visible = isHide;
            this.lblCounter.Visible = isHide;
        }
        #endregion UPDATE FOOTER

        #region UPDATE LEFT
        private Dictionary<string, Counter> sortCounter(IDictionary<string, Counter> dicTicket)
        {
            var list = dicTicket.ToList();
            list.Sort((pair1, pair2) => pair1.Value.Data.Cnum.CompareTo(pair2.Value.Data.Cnum));
            return list.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        #endregion UPDATE LEFT

        #region TIMER
        private void SetTimer()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 1000;
            aTimer.Enabled = true;

        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (lsPanelCounter.Count() > 1)
            {
                loadPanel();
            }
        }

        private void loadPanel()
        {
            string name = "";
            bool isUsed = true;
            foreach (var p in lsPanelCounter.ToList())
            {
                if (!p.Value)
                {
                    isUsed = false;
                    name = p.Key;
                    break;
                }
                else
                {
                    panelUpdate(false, p.Key);
                }
            }
            if (isUsed)
            {
                int i = 0;
                foreach (var p in lsPanelCounter.ToList())
                {
                    if (i == 0)
                    {
                        name = p.Key;
                    }
                    lsPanelCounter[p.Key] = false;
                    i++;
                }
            }
            lsPanelCounter[name] = true;
            panelUpdate(true, name);

        }

        private void panelUpdate(bool isHide, string name)
        {
            Panel p = this.Controls.Find(name, true).FirstOrDefault() as Panel;
            if (p != null)
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)delegate()
                    {
                        p.Visible = isHide;
                    });
                }
                else
                {
                    p.Visible = isHide;
                }
            }
        }
        bool isFull = true;
        private void picLogo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (isFull)
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                isFull = false;
            }
            else
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                isFull = true;
            }

        }
        #endregion TIMER
    }
}
