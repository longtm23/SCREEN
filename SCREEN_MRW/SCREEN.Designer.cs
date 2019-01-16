using System.Drawing;
namespace SCREEN_MRW
{
    partial class SCREEN
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /// 
        private void InitializeComponent(string txtRun, int height_screen, int width_screen, int sizeNum, int sizeXMS)
        {
            if (txtRun == null)
            {
                txtRun = "";
            }
         //private void InitializeComponent(){
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SCREEN));
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.panelAll = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panelVideo = new System.Windows.Forms.Panel();
            this.wmpVideo = new AxWMPLib.AxWindowsMediaPlayer();
            this.panelFooterVideo = new System.Windows.Forms.Panel();
            this.lblHello = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelCounter = new System.Windows.Forms.Panel();
            this.lblCounter = new System.Windows.Forms.Label();
            this.panelAfter = new System.Windows.Forms.Panel();
            this.lblAf = new System.Windows.Forms.Label();
            this.panelTicket = new System.Windows.Forms.Panel();
            this.lblTicket = new System.Windows.Forms.Label();
            this.panelBegin = new System.Windows.Forms.Panel();
            this.labelBf = new System.Windows.Forms.Label();
            this.panelBootomFooter = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelLeft.SuspendLayout();
            this.panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.panelAll.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelVideo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wmpVideo)).BeginInit();
            this.panelFooterVideo.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelCounter.SuspendLayout();
            this.panelAfter.SuspendLayout();
            this.panelTicket.SuspendLayout();
            this.panelBegin.SuspendLayout();
            this.panelBootomFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(28)))), ((int)(((byte)(63)))));
            this.panelLeft.Controls.Add(this.panelLogo);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Margin = new System.Windows.Forms.Padding(0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(358, width_screen);
            this.panelLeft.TabIndex = 0;
            // 
            // panelLogo
            // 
            this.panelLogo.Controls.Add(this.picLogo);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Margin = new System.Windows.Forms.Padding(0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(358, 100);
            this.panelLogo.TabIndex = 0;
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(28)))), ((int)(((byte)(63)))));
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picLogo.Image = global::SCREEN_MRW.Properties.Resources.agribank_8_quay_01;
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Margin = new System.Windows.Forms.Padding(0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(358, 100);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 5;
            this.picLogo.TabStop = false;
            // 
            // panelAll
            // 
            this.panelAll.Controls.Add(this.panelRight);
            this.panelAll.Controls.Add(this.panelLeft);
            this.panelAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAll.Location = new System.Drawing.Point(0, 0);
            this.panelAll.Margin = new System.Windows.Forms.Padding(0);
            this.panelAll.Name = "panelAll";
            this.panelAll.Size = new System.Drawing.Size(height_screen, width_screen);
            this.panelAll.TabIndex = 14;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.panelVideo);
            this.panelRight.Controls.Add(this.panelFooter);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(358, 0);
            this.panelRight.Margin = new System.Windows.Forms.Padding(0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(1562, width_screen);
            this.panelRight.TabIndex = 1;
            // 
            // panelVideo
            // 
            this.panelVideo.Controls.Add(this.wmpVideo);
            this.panelVideo.Controls.Add(this.panelFooterVideo);
            this.panelVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVideo.Location = new System.Drawing.Point(0, 0);
            this.panelVideo.Name = "panelVideo";
            this.panelVideo.Size = new System.Drawing.Size(1920, 980);
            this.panelVideo.TabIndex = 1;
            // 
            // wmpVideo
            // 
            this.wmpVideo.Enabled = true;
            this.wmpVideo.Location = new System.Drawing.Point(0, 0);
            this.wmpVideo.Margin = new System.Windows.Forms.Padding(0);
            this.wmpVideo.Name = "wmpVideo";
            this.wmpVideo.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmpVideo.OcxState")));
            this.wmpVideo.Size = new System.Drawing.Size(75, 23);
            this.wmpVideo.TabIndex = 4;
            this.wmpVideo.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.wmpVideo_PlayStateChange);
            this.wmpVideo.MediaError += new AxWMPLib._WMPOCXEvents_MediaErrorEventHandler(this.wmpVideo_MediaError);
            // 
            // panelFooterVideo
            // 
            this.panelFooterVideo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(28)))), ((int)(((byte)(63)))));
            this.panelFooterVideo.Controls.Add(this.lblHello);
            this.panelFooterVideo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooterVideo.Location = new System.Drawing.Point(0, 920);
            this.panelFooterVideo.Name = "panelFooterVideo";
            this.panelFooterVideo.Size = new System.Drawing.Size(1920, 60);
            this.panelFooterVideo.TabIndex = 3;
            // 
            // lblHello
            // 
            this.lblHello.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHello.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(28)))), ((int)(((byte)(63)))));
            this.lblHello.Font = new System.Drawing.Font("Microsoft Sans Serif", 23F, System.Drawing.FontStyle.Bold);
            this.lblHello.ForeColor = System.Drawing.Color.White;
            this.lblHello.Location = new System.Drawing.Point(0, 0);
            this.lblHello.Name = "lblHello";
            this.lblHello.AutoSize = true;
            //this.lblHello.Size = new System.Drawing.Size(1920, 60);
            this.lblHello.TabIndex = 0;
            this.lblHello.Text = txtRun;
            this.lblHello.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.panel2);
            this.panelFooter.Controls.Add(this.panelBootomFooter);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 980);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1562, 100);
            this.panelFooter.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.panelCounter);
            this.panel2.Controls.Add(this.panelAfter);
            this.panel2.Controls.Add(this.panelTicket);
            this.panel2.Controls.Add(this.panelBegin);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1562, 81);
            this.panel2.TabIndex = 1;
            // 
            // panelCounter
            // 
            this.panelCounter.Controls.Add(this.lblCounter);
            this.panelCounter.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelCounter.Location = new System.Drawing.Point(426, 0);
            this.panelCounter.Name = "panelCounter";
            this.panelCounter.Size = new System.Drawing.Size(142, 81);
            this.panelCounter.TabIndex = 13;
            // 
            // lblCounter
            // 
            this.lblCounter.BackColor = System.Drawing.Color.White;
            this.lblCounter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", sizeNum, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblCounter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(28)))), ((int)(((byte)(63)))));
            this.lblCounter.Location = new System.Drawing.Point(0, 0);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(142, 81);
            this.lblCounter.TabIndex = 1;
            this.lblCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelAfter
            // 
            this.panelAfter.Controls.Add(this.lblAf);
            this.panelAfter.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelAfter.Location = new System.Drawing.Point(284, 0);
            this.panelAfter.Name = "panelAfter";
            this.panelAfter.Size = new System.Drawing.Size(142, 81);
            this.panelAfter.TabIndex = 12;
            // 
            // lblAf
            // 
            this.lblAf.BackColor = System.Drawing.Color.White;
            this.lblAf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAf.Font = new System.Drawing.Font("Microsoft Sans Serif", sizeXMS, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(28)))), ((int)(((byte)(63)))));
            this.lblAf.Location = new System.Drawing.Point(0, 0);
            this.lblAf.Name = "lblAf";
            this.lblAf.Size = new System.Drawing.Size(142, 81);
            this.lblAf.TabIndex = 1;
            this.lblAf.Text = "ĐẾN QUẦY";
            this.lblAf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTicket
            // 
            this.panelTicket.Controls.Add(this.lblTicket);
            this.panelTicket.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTicket.Location = new System.Drawing.Point(142, 0);
            this.panelTicket.Name = "panelTicket";
            this.panelTicket.Size = new System.Drawing.Size(142, 81);
            this.panelTicket.TabIndex = 11;
            // 
            // lblTicket
            // 
            this.lblTicket.BackColor = System.Drawing.Color.White;
            this.lblTicket.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", sizeNum, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTicket.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(28)))), ((int)(((byte)(63)))));
            this.lblTicket.Location = new System.Drawing.Point(0, 0);
            this.lblTicket.Name = "lblTicket";
            this.lblTicket.Size = new System.Drawing.Size(142, 81);
            this.lblTicket.TabIndex = 1;
            this.lblTicket.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelBegin
            // 
            this.panelBegin.BackColor = System.Drawing.Color.White;
            this.panelBegin.Controls.Add(this.labelBf);
            this.panelBegin.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelBegin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(28)))), ((int)(((byte)(63)))));
            this.panelBegin.Location = new System.Drawing.Point(0, 0);
            this.panelBegin.Name = "panelBegin";
            this.panelBegin.Size = new System.Drawing.Size(142, 81);
            this.panelBegin.TabIndex = 10;
            // 
            // labelBf
            // 
            this.labelBf.BackColor = System.Drawing.Color.White;
            this.labelBf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelBf.Font = new System.Drawing.Font("Microsoft Sans Serif",sizeXMS, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(28)))), ((int)(((byte)(63)))));
            this.labelBf.Location = new System.Drawing.Point(0, 0);
            this.labelBf.Name = "labelBf";
            this.labelBf.Size = new System.Drawing.Size(142, 81);
            this.labelBf.TabIndex = 0;
            this.labelBf.Text = "SỐ";
            this.labelBf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelBootomFooter
            // 
            this.panelBootomFooter.BackColor = System.Drawing.Color.White;
            this.panelBootomFooter.Controls.Add(this.label1);
            this.panelBootomFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBootomFooter.Location = new System.Drawing.Point(0, 81);
            this.panelBootomFooter.Margin = new System.Windows.Forms.Padding(3, 3, 3, 8);
            this.panelBootomFooter.Name = "panelBootomFooter";
            this.panelBootomFooter.Size = new System.Drawing.Size(1562, 19);
            this.panelBootomFooter.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(28)))), ((int)(((byte)(63)))));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1562, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Copyright © 2018 | Miraway.,JSC";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SCREEN
            // 
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(height_screen, width_screen);
            this.Controls.Add(this.panelAll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SCREEN";
            this.Text = "Form1";
            this.panelLeft.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.panelAll.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.panelVideo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wmpVideo)).EndInit();
            this.panelFooterVideo.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panelCounter.ResumeLayout(false);
            this.panelAfter.ResumeLayout(false);
            this.panelTicket.ResumeLayout(false);
            this.panelBegin.ResumeLayout(false);
            this.panelBootomFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Panel panelAll;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelVideo;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelCounter;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.Panel panelAfter;
        private System.Windows.Forms.Label lblAf;
        private System.Windows.Forms.Panel panelTicket;
        private System.Windows.Forms.Label lblTicket;
        private System.Windows.Forms.Panel panelBegin;
        private System.Windows.Forms.Label labelBf;
        private System.Windows.Forms.Panel panelBootomFooter;
        private System.Windows.Forms.Label label1;
        private AxWMPLib.AxWindowsMediaPlayer wmpVideo;
        private System.Windows.Forms.Panel panelFooterVideo;
        private System.Windows.Forms.Label lblHello;
        private System.Windows.Forms.PictureBox picLogo;






    }
}

