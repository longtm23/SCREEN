using SCREEN_MRW.CONTROLLER;
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
using SCREEN_MRW.AUDIO;
using SCREEN_MRW.DTO;


namespace SCREEN_MRW
{
    public partial class SCREEN : Form
    {
        #region PARAM
        private Dictionary<string, bool> fileVideos;

        private const string TICKET_LEFT_LABEL = "lblLefTicket";
        private const string COUNTER_LEFT_LABEL = "lblLefCounter";
        private const string PANEL_COUNTER_LEFT_IN = "panelCounterLeftIn";
        private const string PANEL_COUNTER_LEFT = "panelCounterLeft";
        private const string PANEL_COU_ADD = "panCAdd";
        private const string PANEL_TK_ADD = "panTAdd";
        private Dictionary<string, bool> lsPanelCounter;
        private PlayAudio playAudio;
        private ConfigMrw configMrw = null;

        public delegate void ReceivedEventHandler(EventScreen eScreen);
        public event ReceivedEventHandler DataReceived;

        private int widthScreen = 1920;
        private int heightScreen = 1080;
        #endregion

        #region CONTRUCTOR
        public SCREEN(ConfigMrw mrwConfig)
        {
            configMrw = mrwConfig;
            if (mrwConfig.height_screen > 0 && mrwConfig.width_screen > 0)
            {
                widthScreen = mrwConfig.width_screen;
                heightScreen = mrwConfig.height_screen;
            }

            InitializeComponent(mrwConfig.text_run, mrwConfig.width_screen, mrwConfig.height_screen, configMrw.size_num, configMrw.size_xmdqs);
            this.wmpVideo.stretchToFit = true;
            this.wmpVideo.uiMode = "none";
            //InitializeComponent();
            this.CenterToScreen();
            drawUI();
            SetFORM(false);
            ContructorParam();
            loadVideo(mrwConfig.folder_video);
           
            Init();
            playAudio = new PlayAudio();
            playAudio.Init(mrwConfig);
            playAudio.DataReceived += reciveAudio;
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void ContructorParam()
        {
            lsPanelCounter = new Dictionary<string, bool>();
            fileVideos = new Dictionary<string, bool>();
        }

        private void Init()
        {
            loadUI();
            SetTimerTextRun();
            SetTimerScreen();
        }
        private void loadUI()
        {
            hideFooter(false);
        }
        int heightPanelCounter = 0;
        int heightCou = 0;
        int heightCoup = 0;
        int widthLeft = 0;

        private void reciveAudio(EventAudio eAudio)
        {
            try
            {
                switch (eAudio.Action)
                {
                    case ActionTicket.INITIAL:
                        DrawUiLeft();
                        drawLeft(sortCounter(eAudio.DicCounter));
                        Thread.Sleep(1000);
                        updateTicketLable(eAudio.DicServing);
                        break;
                    case ActionTicket.ACTION_CALL:
                        string counterID = eAudio.Audio.CounterID;
                        UpdateCounterLeft(eAudio.Action, eAudio.Audio.Cnum, eAudio.Audio.CounterNum, counterID);
                        Thread.Sleep(500);
                        break;
                    case ActionTicket.ACTION_CALL_AUDIO:
                        string couID = eAudio.Audio.CounterID;
                        UpdateUI(eAudio.Action, eAudio.Audio.Cnum, eAudio.Audio.CounterNum, couID);
                        Thread.Sleep(500);
                        break;
                    case ActionTicket.ACTION_FINISH:
                        loadLableTiket("", eAudio.Audio.CounterID);
                        break;
                    case ActionTicket.ACTION_CANCEL:
                        loadLableTiket("", eAudio.Audio.CounterID);
                        break;
                    case ActionTicket.ACTION_MOVE:
                        loadLableTiket("", eAudio.Audio.CounterID);
                        break;
                    //case ActionTicket.ACTION_UPDATE_LABLE:
                    //    hideFooter(false);
                    //    loadLableTiket("", eAudio.Audio.CounterID);
                    case ActionTicket.RELOAD:
                        EventScreen eventScreen = new EventScreen(true);
                        DataReceived(eventScreen);
                        break;
                    default: break;
                }
            }
            catch
            {

            }
        }
        private void drawLeft(Dictionary<string, Counter> dicCou)
        {
            var countCounter = dicCou.Count;
            var fFive = configMrw.row_counter;
            var panelFCouterAll = (float)countCounter / (float)(fFive * 2);
            int panelICouterAll = (int)panelFCouterAll;

            int intPanelAdd = 0;
            if (panelFCouterAll > panelICouterAll)
            {
                panelICouterAll += 1;
                intPanelAdd = panelICouterAll * 2 * fFive - countCounter;
            }

            try
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)delegate()
                    {
                        drawCounterTicket(dicCou, panelICouterAll, countCounter, fFive, intPanelAdd);
                    });
                }
                else
                {
                    drawCounterTicket(dicCou, panelICouterAll, countCounter, fFive, intPanelAdd);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Err" + ex.Message);
            }


        }
        private void drawCounterTicket(Dictionary<string, Counter> dicCou, int panelICouterAll, int countCounter, int fFive, int intPanelAdd)
        {
            try
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
                    panelCounterAllNext.BackColor = System.Drawing.Color.FromArgb(171, 28, 63);


                    var panelAllNew2 = new System.Windows.Forms.Panel();
                    panelCounterAllNext.Controls.Add(panelAllNew2);

                    var panelAllNew3 = new System.Windows.Forms.Panel();
                    panelCounterAllNext.Controls.Add(panelAllNew3);

                    panelAllNew2.Dock = System.Windows.Forms.DockStyle.Left;
                    panelAllNew2.Location = new System.Drawing.Point(0, 100);
                    panelAllNew2.Margin = new System.Windows.Forms.Padding(0);
                    panelAllNew2.Name = "panelAllNewLeft_" + i;
                    panelAllNew2.Size = new System.Drawing.Size((widthLeft / 2) - 1, heightPanelCounter);
                    panelAllNew2.TabIndex = 0;
                    panelAllNew2.BackColor = Color.White;
                    panelAllNew2.Margin = new System.Windows.Forms.Padding(0, 0, 2, 0);



                    panelAllNew3.Dock = System.Windows.Forms.DockStyle.Right;
                    panelAllNew3.Location = new System.Drawing.Point(100, 100);
                    panelAllNew3.Margin = new System.Windows.Forms.Padding(0);
                    panelAllNew3.Name = "panelAllNewRight_" + i;
                    panelAllNew3.Size = new System.Drawing.Size((widthLeft / 2) - 1, heightPanelCounter);
                    panelAllNew3.TabIndex = 0;
                    panelAllNew3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
                    panelAllNew3.BackColor = System.Drawing.Color.FromArgb(171, 28, 63);

                    if (i == 0)
                    {
                        lsPanelCounter.Add(name, true);
                        panelCounterAllNext.Visible = true;
                    }
                    else
                    {
                        lsPanelCounter.Add(name, false);
                        panelCounterAllNext.Visible = true;
                    }
                    int iadd2 = 0;
                    int iadd3 = 0;

                    for (int t = 0; t < countCounter; t++)
                    {
                        var cou = dicCou.Values.ToList()[t];

                        if ((i == 0) || (i > 0 && (t + 1) > 2 * i * fFive))
                        {
                            if (iadd2 >= 0 && iadd2 < fFive)
                            {
                                addPanelNew(panelAllNew2, cou.EId, cou.Data.Cnum + "");
                                iadd2++;
                            }
                            else if (iadd3 >= 0 && iadd3 < fFive)
                            {
                                addPanelNew(panelAllNew3, cou.EId, cou.Data.Cnum + "");
                                iadd3++;
                            }
                            else
                            {
                                break;
                            }

                        }
                    }

                    if (i == (panelICouterAll - 1) && intPanelAdd > 0)
                    {
                        var iAddPan = intPanelAdd - fFive;
                        var iAddPan2 = 0;
                        var iAddPan3 = 0;

                        if (iAddPan < fFive && iAddPan > 0)
                        {
                            iAddPan3 = fFive;
                            iAddPan2 = iAddPan;
                        }
                        else
                        {
                            iAddPan3 = intPanelAdd;
                        }

                        for (int iAdd = 0; iAdd < iAddPan2; iAdd++)
                        {
                            addPanelNew(panelAllNew2, PANEL_COU_ADD + iAdd, "");
                        }
                        for (int iAdd = 0; iAdd < iAddPan3; iAdd++)
                        {
                            addPanelNew(panelAllNew3, PANEL_COU_ADD + iAdd, "");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void addPanelNew(Panel panelCounterAllNext, string couID, string counterNum)
        {
            try
            {
                var panelAllNew = new System.Windows.Forms.Panel();
                panelCounterAllNext.Controls.Add(panelAllNew);
                var panelCouNew = new System.Windows.Forms.Panel();

                var panelTicketNew = new System.Windows.Forms.Panel();

                var lblTicketNew = new System.Windows.Forms.Label();

                var lblCounterNew = new System.Windows.Forms.Label();

                panelAllNew.Controls.Add(panelCouNew);
                panelAllNew.Controls.Add(panelTicketNew);
                panelAllNew.Dock = System.Windows.Forms.DockStyle.Bottom;
                panelAllNew.Location = new System.Drawing.Point(0, 0);
                panelAllNew.Margin = new System.Windows.Forms.Padding(0);
                panelAllNew.Name = PANEL_COUNTER_LEFT_IN + couID;
                panelAllNew.ClientSize = new System.Drawing.Size(widthLeft / 2 - 2, heightCou);
                panelAllNew.Size = new System.Drawing.Size(0, heightCou);
                panelAllNew.TabIndex = 1;

                // 
                // panel3Cou3
                // 
                panelCouNew.Controls.Add(lblCounterNew);
                panelCouNew.Dock = System.Windows.Forms.DockStyle.Bottom;
                panelCouNew.Location = new System.Drawing.Point(0, 70);
                panelCouNew.Name = "panCou" + couID;
                panelCouNew.TabIndex = 2;
                panelCouNew.ClientSize = new System.Drawing.Size(widthLeft / 2 - 2, heightCoup);
                panelCouNew.BackColor = Color.FromArgb(171, 28, 63);

                lblCounterNew.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right))));
                lblCounterNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 38F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
                lblCounterNew.Location = new System.Drawing.Point(0, 0);
                lblCounterNew.ClientSize = new System.Drawing.Size(widthLeft / 2 - 2, heightCoup);
                lblCounterNew.Dock = System.Windows.Forms.DockStyle.Fill;
                lblCounterNew.Name = "cou" + couID;
                lblCounterNew.ForeColor = Color.White;
                lblCounterNew.BackColor = Color.FromArgb(171, 28, 63);
                lblCounterNew.TabIndex = 3;
                if (counterNum.Length > 0)
                {
                    lblCounterNew.Text = "Quầy " + counterNum;
                }
                lblCounterNew.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                // panelTicketNew
                panelTicketNew.Controls.Add(lblTicketNew);
                panelTicketNew.Dock = System.Windows.Forms.DockStyle.Fill;
                panelTicketNew.Location = new System.Drawing.Point(0, 100);
                panelTicketNew.Name = "panelTicketLeft" + couID;
                panelTicketNew.TabIndex = 2;
                panelTicketNew.BackColor = Color.FromArgb(171, 28, 63);

                // 
                // lblTicket3
                // 

                lblTicketNew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
                lblTicketNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
                lblTicketNew.ForeColor = Color.FromArgb(171, 28, 63);
                lblTicketNew.BackColor = Color.White;
                lblTicketNew.Location = new System.Drawing.Point(0, 0);
                lblTicketNew.Size = new System.Drawing.Size(widthLeft / 2 - 2, heightCou - heightCoup);
                lblTicketNew.Name = "tk" + couID;
                lblTicketNew.TabIndex = 3;
                lblTicketNew.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void updateTicketLable(Dictionary<string, Ticket> dicServing)
        {
            foreach (var tk in dicServing.Values.ToList())
            {
                loadLableTiket(tk.cnum, tk.counter_id);
            }
        }
        private void DrawUiLeft()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)delegate()
                    {
                        drawUI();
                    });
                }
                else
                {
                    drawUI();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void drawUI()
        {
            try
            {
                int fFive = configMrw.row_counter;
                var fLeft = 0.39;
                //if (countCounter % 5 == 0)
                //{
                //    fFive = 5;
                //}

                //int weightAll = Screen.PrimaryScreen.Bounds.Width;
                //int heightAll = Screen.PrimaryScreen.Bounds.Height;

                widthLeft = (int)(widthScreen * fLeft);
                var widthFooter = widthScreen - widthLeft;
                //draw left
                panelLeft.ClientSize = new System.Drawing.Size(widthLeft, heightScreen);
                var heightLogo = heightScreen / 8;
                panelLogo.ClientSize = new System.Drawing.Size(widthLeft, heightLogo);
                heightPanelCounter = heightScreen - heightLogo;
                heightCou = heightPanelCounter / fFive;
                heightCoup = heightCou / 3;
                //draw right
                panelFooter.ClientSize = new System.Drawing.Size(widthFooter, heightCou);
                panelFooterVideo.ClientSize = new System.Drawing.Size(widthFooter, heightCoup);
                var widthInFooter = widthFooter / 6;

                panelBegin.ClientSize = new System.Drawing.Size(widthInFooter, heightCou);
                panelTicket.ClientSize = new System.Drawing.Size(widthFooter / 3, heightCou);
                panelAfter.ClientSize = new System.Drawing.Size(widthFooter / 3, heightCou);
                panelCounter.ClientSize = new System.Drawing.Size(widthInFooter, heightCou);
                panelBootomFooter.ClientSize = new System.Drawing.Size(widthFooter, heightCoup);

                panel2.ClientSize = new System.Drawing.Size(widthFooter, heightCou);
                this.wmpVideo.ClientSize = new Size(widthFooter, heightScreen - heightCou - heightCoup);
                this.lblHello.Padding = new System.Windows.Forms.Padding(0, (this.panelFooterVideo.Height - lblHello.Height) / 2, 0, 0);
                //var heightTicket = heightCou - heightCoup;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion CONTRUCTOR

        #region VIDEO
        private void loadVideo(string urlFolder)
        {
            if (Directory.Exists(urlFolder))
            {
                wmpVideo.currentPlaylist.clear();

                wmpVideo.currentPlaylist = wmpVideo.newPlaylist("a", "");
                var files = Directory.GetFiles(urlFolder, "*.mp4");
                var plCollection = wmpVideo.playlistCollection.getAll();
                if (plCollection.count > 0)
                {
                    for (var i = 0; i < plCollection.count; i++)
                    {
                        try
                        {
                            var pl = plCollection.Item(i);
                            wmpVideo.playlistCollection.remove(pl);
                        }
                        catch { }

                    }
                }


                    if (files.Count() > 0)
                    {
                        foreach (var vi in files)
                        {
                            fileVideos[vi] = false;
                        }

                        runVideo();
                    }
                }
                else
                {
                    MessageBox.Show("thư mục video quảng cáo không tồn tại!");
                }

        }
        private bool isRunning = false;
        private void wmpVideo_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {

            if (wmpVideo.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                isRunning = true;
            }

            if (wmpVideo.playState == WMPLib.WMPPlayState.wmppsStopped ||( wmpVideo.playState == WMPLib.WMPPlayState.wmppsReady && isRunning))
            {
                isRunning = false;
                this.BeginInvoke(new Action(() =>
                {
                    wmpVideo.close();
                    GC.Collect(); // Start .NET CLR Garbage Collection
                    GC.WaitForPendingFinalizers(); // Wait for Garbage Collection to finish
                    var plCollection = wmpVideo.playlistCollection.getAll();
                    if (plCollection.count > 0)
                    {
                        for (var i = 0; i < plCollection.count; i++ )
                        {
                            try
                            {
                                var pl = plCollection.Item(i);
                                wmpVideo.playlistCollection.remove(pl);
                            }
                            catch { }
                        }
                        
                    }
                    wmpVideo.currentPlaylist = wmpVideo.newPlaylist("a", "");
                    var url = getUrlVideo();
                    var mediaItem = wmpVideo.newMedia(url);
                    //this.wmpVideo.URL = @url;
                    wmpVideo.currentPlaylist.appendItem(mediaItem);
                    this.wmpVideo.Ctlcontrols.play();
                }));
            }
        }

        private void wmpVideo_MediaError(object sender, AxWMPLib._WMPOCXEvents_MediaErrorEvent e)
        {
            wmpVideo.close();
            loadVideo(configMrw.folder_video);
        }

        #endregion

        #region UPDATE FOOTER
        private void UpdateUI(string action, string cNum, string counterNum, string counterID)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate()
                {
                    this.lblTicket.Text = cNum;
                    this.lblCounter.Text = counterNum;
                    hideFooter(true);
                });
            }
            else
            {
                this.lblTicket.Text = cNum;
                this.labelBf.Text = counterNum;
                hideFooter(true);
            }
        }

        private void UpdateCounterLeft(string action, string cNum, string counterNum, string counterID)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate()
                {
                    loadLableTiket(cNum, counterID);
                });
            }
            else
            {
                loadLableTiket(cNum, counterID);
            }
        }
        private void hideFooter(bool isHide)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate()
                {
                    if (this.labelBf.Visible != isHide)
                    {
                        this.labelBf.Visible = isHide;
                        this.lblAf.Visible = isHide;
                        this.lblTicket.Visible = isHide;
                        this.lblCounter.Visible = isHide;
                    }
                   
                });
            }
            else
            {
                if (this.labelBf.Visible != isHide)
                {
                    this.labelBf.Visible = isHide;
                    this.lblAf.Visible = isHide;
                    this.lblTicket.Visible = isHide;
                    this.lblCounter.Visible = isHide;
                }
            }
        }
        #endregion UPDATE FOOTER

        #region UPDATE LEFT
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

        void loadPanel(string cNum, string couID, Color colorPan)
        {
            var name = "panelTicketLeft" + couID;
            Panel tk = this.Controls.Find(name, true).FirstOrDefault() as Panel;
            if (tk != null)
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)delegate()
                    {
                        tk.BackColor = colorPan;
                    });
                }
                else
                {
                    tk.BackColor = colorPan;
                }
            }
        }
        private Dictionary<string, Counter> sortCounter(IDictionary<string, Counter> dicTicket)
        {
            var list = dicTicket.ToList();
            list.Sort((pair1, pair2) => pair1.Value.Data.Cnum.CompareTo(pair2.Value.Data.Cnum));
            return list.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        #endregion UPDATE LEFT

        #region TIMER ANIMATION PANEL
        private int xpos = -2;
        private void SetTimerTextRun()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 15;
            aTimer.Enabled = true;

        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate()
                {
                    runtText();
                });
            }
            else
            {
                runtText();
            }
        }
        bool isRunText = false;
        void runtText()
        {
            if (!isRunText)
            {
                xpos = this.panelFooterVideo.Width;
                isRunText = true;
            }

            if (xpos + this.panelFooterVideo.Width <= 0)
            {
                xpos = this.panelFooterVideo.Width;
            }
            else
            {
                xpos -= 2;
            }
            this.lblHello.Location = new System.Drawing.Point(xpos, 0);
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
        #endregion TIMER ANIMATION PANEL

        #region TIMER SCREEN
        System.Timers.Timer aTimersCreen;

        private void SetTimerScreen()
        {
            // Create a timer with a two second interval.
            aTimersCreen = new System.Timers.Timer(configMrw.time_next * 1000);
            // Hook up the Elapsed event for the timer. 
            aTimersCreen.Elapsed += OnTimedScreenEvent;
            aTimersCreen.AutoReset = true;
            aTimersCreen.Enabled = true;
        }
        int timeRestart = 0;
        private void restartAPP()
        {
            timeRestart += configMrw.time_next;
            if (timeRestart > 3600)
            {
                if (DateTime.Now.Hour == 23)
                {
                    EventScreen eventScreen = new EventScreen(true);
                    DataReceived(eventScreen);
                }
                timeRestart = 0;
            }
           
        }

        private void OnTimedScreenEvent(Object source, ElapsedEventArgs e)
        {
            try
            {
                restartAPP();
                if (lsPanelCounter.Count() > 1)
                {
                    loadPanel();
                }
                if (configMrw.screen_display == 2)
                {
                    if (aTimersCreen == null)
                    {
                        SetTimerScreen();
                    }
                    SetFORM(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void wmpVideo_PlayStateChange_1(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            try
            {
                if (e.newState == 1)
                {
                    if (this.InvokeRequired)
                    {
                        this.BeginInvoke((MethodInvoker)delegate()
                        {
                            runVideo();
                        });
                    }
                    else
                    {
                        runVideo();
                    }

                }
            }
            catch { }
        }
        private void SetFORM(bool isReload)
        {
            try
            {
                Screen[] screens = Screen.AllScreens;
                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)delegate()
                    {
                        if (this.Visible && screens.Length == 1 && configMrw.screen_display == 2)
                        {
                            this.Hide();
                        }
                    });
                }
                else
                {
                    if (this.Visible && screens.Length == 1 && configMrw.screen_display == 2)
                    {
                        this.Hide();
                    }
                }

                if (screens.Length > 1)
                {
                    Screen sc = screens.FirstOrDefault(m => m.Primary == false);

                    if (this.InvokeRequired)
                    {
                        this.BeginInvoke((MethodInvoker)delegate()
                        {
                            try
                            {
                                if (!this.Visible)
                                {
                                    if (isReload)
                                    {
                                        EventScreen eventScreen = new EventScreen(true);
                                        DataReceived(eventScreen);
                                    }
                                    else
                                    {
                                        play2Screen(sc);
                                        if (this.Width != sc.WorkingArea.Width || this.Height != sc.WorkingArea.Height)
                                        {
                                            EventScreen eventScreen = new EventScreen(true);
                                            DataReceived(eventScreen);
                                        }
                                    }
                                }
                                else if (this.Location != sc.WorkingArea.Location)
                                {
                                    EventScreen eventScreen = new EventScreen(true);
                                    DataReceived(eventScreen);
                                }
                            }
                            catch { }

                        });
                    }
                    else
                    {
                        try
                        {
                            if (!this.Visible)
                            {
                                if (isReload)
                                {
                                    EventScreen eventScreen = new EventScreen(true);
                                    DataReceived(eventScreen);
                                }
                                else
                                {
                                    play2Screen(sc);
                                    if (this.Width != sc.WorkingArea.Width)
                                    {
                                        EventScreen eventScreen = new EventScreen(true);
                                        DataReceived(eventScreen);
                                    }
                                }
                            }
                            else if (this.Location != sc.WorkingArea.Location)
                            {
                                EventScreen eventScreen = new EventScreen(true);
                                DataReceived(eventScreen);
                            }
                        }
                        catch { }
                    }
                }
                else if (screens.Length == 1)
                {
                    if (this.InvokeRequired)
                    {
                        this.BeginInvoke((MethodInvoker)delegate()
                        {
                            play1Screen();
                        });
                    }
                    else
                    {
                        play1Screen();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        void play2Screen(Screen sc)
        {
            try
            {
                this.StartPosition = FormStartPosition.Manual;

                var widSc = sc.WorkingArea.Width;
                var heiSc = sc.WorkingArea.Height;
                this.Top = sc.Bounds.Top;
                this.Left = sc.Bounds.Left;
                this.Location = new Point(sc.Bounds.Left, sc.Bounds.Top);
                this.UpdateBounds(sc.WorkingArea.X, sc.WorkingArea.Y, widSc, heiSc, widSc, heiSc);
                
                this.picLogo.Focus();
                this.Show();
                this.Refresh();
            }
            catch { }
        }
        private void play1Screen()
        {
            try
            {
                this.Hide();
                if (configMrw.screen_display == 2)
                {
                    this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                    playVideo();
                }
                else if (configMrw.screen_display == 1)
                {
                    this.Show();
                    playVideo();
                }
                this.Invalidate();
                this.Refresh();
            }
            catch { }


        }
        private void runVideo()
        {
            try
            {
                try
                {
                    this.wmpVideo.Update();
                    this.wmpVideo.stretchToFit = true;
                    this.wmpVideo.settings.volume = 0;
                    this.wmpVideo.settings.balance = 0;
                    this.wmpVideo.settings.mute = true;
                    if (!this.wmpVideo.settings.autoStart)
                    {
                        this.wmpVideo.settings.autoStart = true;
                    }

                }
                catch { }
                wmpVideo.currentPlaylist = wmpVideo.newPlaylist("a", "");
                var url = getUrlVideo();
                var mediaItem = wmpVideo.newMedia(url);
                //this.wmpVideo.URL = @url;
                wmpVideo.currentPlaylist.appendItem(mediaItem);
                this.wmpVideo.Ctlcontrols.play();
                //this.wmpVideo.URL = urlVideo;
                //this.wmpVideo.Ctlcontrols.play();
            }
            catch(Exception ex) {
                Console.Write(ex.Message);
            }
        }
        private void playVideo()
        {
            try
            {
                this.wmpVideo.Ctlcontrols.stop();
               runVideo();
            }
            catch { }

        }
        private string getUrlVideo()
        {
            foreach (var vi in fileVideos.ToList())
            {
                if (!vi.Value)
                {
                    fileVideos[vi.Key] = true;
                    return vi.Key;
                }
            }
            foreach (var vi in fileVideos.ToList())
            {
                fileVideos[vi.Key] = false;
            }
            fileVideos[fileVideos.First().Key] = true;
            return fileVideos.First().Key;
        }
        #endregion TIMER SCREEN
    }
}
