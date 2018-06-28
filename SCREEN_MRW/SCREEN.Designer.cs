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
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SCREEN));
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.panelfooterLogo = new System.Windows.Forms.Panel();
            this.panelFooterVideo = new System.Windows.Forms.Panel();
            this.lblHello = new System.Windows.Forms.Label();
            this.panelAll = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panelVideo = new System.Windows.Forms.Panel();
            this.wmpVideo = new AxWMPLib.AxWindowsMediaPlayer();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.panelCounter = new System.Windows.Forms.Panel();
            this.lblCounter = new System.Windows.Forms.Label();
            this.panelAfter = new System.Windows.Forms.Panel();
            this.lblAf = new System.Windows.Forms.Label();
            this.panelTicket = new System.Windows.Forms.Panel();
            this.lblTicket = new System.Windows.Forms.Label();
            this.panelBegin = new System.Windows.Forms.Panel();
            this.labelBf = new System.Windows.Forms.Label();
            this.panelLeft.SuspendLayout();
            this.panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.panelFooterVideo.SuspendLayout();
            this.panelAll.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelVideo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wmpVideo)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.panelCounter.SuspendLayout();
            this.panelAfter.SuspendLayout();
            this.panelTicket.SuspendLayout();
            this.panelBegin.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(69)))), ((int)(((byte)(139)))));
            this.panelLeft.Controls.Add(this.panelLogo);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Margin = new System.Windows.Forms.Padding(0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(358, 510);
            this.panelLeft.TabIndex = 0;
            // 
            // panelLogo
            // 
            this.panelLogo.Controls.Add(this.picLogo);
            this.panelLogo.Controls.Add(this.panelfooterLogo);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Margin = new System.Windows.Forms.Padding(0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(358, 100);
            this.panelLogo.TabIndex = 0;
            // 
            // picLogo
            // 
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picLogo.Image = global::SCREEN_MRW.Properties.Resources.logo1;
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(358, 90);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 1;
            this.picLogo.TabStop = false;
            this.picLogo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.picLogo_MouseDoubleClick);
            // 
            // panelfooterLogo
            // 
            this.panelfooterLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(23)))), ((int)(((byte)(109)))));
            this.panelfooterLogo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelfooterLogo.Location = new System.Drawing.Point(0, 90);
            this.panelfooterLogo.Name = "panelfooterLogo";
            this.panelfooterLogo.Size = new System.Drawing.Size(358, 10);
            this.panelfooterLogo.TabIndex = 0;
            // 
            // panelFooterVideo
            // 
            this.panelFooterVideo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(23)))), ((int)(((byte)(109)))));
            this.panelFooterVideo.Controls.Add(this.lblHello);
            this.panelFooterVideo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooterVideo.Location = new System.Drawing.Point(0, 350);
            this.panelFooterVideo.Name = "panelFooterVideo";
            this.panelFooterVideo.Size = new System.Drawing.Size(474, 60);
            this.panelFooterVideo.TabIndex = 1;
            // 
            // lblHello
            // 
            this.lblHello.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHello.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblHello.ForeColor = System.Drawing.Color.White;
            this.lblHello.Location = new System.Drawing.Point(118, 0);
            this.lblHello.Name = "lblHello";
            this.lblHello.Size = new System.Drawing.Size(247, 50);
            this.lblHello.TabIndex = 0;
            this.lblHello.Text = "Chào mừng đến với bệnh viện K cơ sở Tân Triều...";
            this.lblHello.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelAll
            // 
            this.panelAll.Controls.Add(this.panelRight);
            this.panelAll.Controls.Add(this.panelLeft);
            this.panelAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAll.Location = new System.Drawing.Point(0, 0);
            this.panelAll.Margin = new System.Windows.Forms.Padding(0);
            this.panelAll.Name = "panelAll";
            this.panelAll.Size = new System.Drawing.Size(832, 510);
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
            this.panelRight.Size = new System.Drawing.Size(474, 510);
            this.panelRight.TabIndex = 1;
            // 
            // panelVideo
            // 
            this.panelVideo.Controls.Add(this.panelFooterVideo);
            this.panelVideo.Controls.Add(this.wmpVideo);
            this.panelVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVideo.Location = new System.Drawing.Point(0, 0);
            this.panelVideo.Name = "panelVideo";
            this.panelVideo.Size = new System.Drawing.Size(474, 410);
            this.panelVideo.TabIndex = 1;
            // 
            // wmpVideo
            // 
            this.wmpVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wmpVideo.Enabled = true;
            this.wmpVideo.Location = new System.Drawing.Point(0, 0);
            this.wmpVideo.Margin = new System.Windows.Forms.Padding(0);
            this.wmpVideo.Name = "wmpVideo";
            this.wmpVideo.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wmpVideo.OcxState")));
            this.wmpVideo.Size = new System.Drawing.Size(474, 410);
            this.wmpVideo.TabIndex = 0;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(57)))), ((int)(((byte)(52)))));
            this.panelFooter.Controls.Add(this.panelCounter);
            this.panelFooter.Controls.Add(this.panelAfter);
            this.panelFooter.Controls.Add(this.panelTicket);
            this.panelFooter.Controls.Add(this.panelBegin);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 410);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(474, 100);
            this.panelFooter.TabIndex = 0;
            // 
            // panelCounter
            // 
            this.panelCounter.Controls.Add(this.lblCounter);
            this.panelCounter.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelCounter.Location = new System.Drawing.Point(426, 0);
            this.panelCounter.Name = "panelCounter";
            this.panelCounter.Size = new System.Drawing.Size(142, 100);
            this.panelCounter.TabIndex = 9;
            // 
            // lblCounter
            // 
            this.lblCounter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 80.6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblCounter.ForeColor = System.Drawing.Color.White;
            this.lblCounter.Location = new System.Drawing.Point(13, 0);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(126, 100);
            this.lblCounter.TabIndex = 1;
            this.lblCounter.Text = "label4";
            this.lblCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelAfter
            // 
            this.panelAfter.Controls.Add(this.lblAf);
            this.panelAfter.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelAfter.Location = new System.Drawing.Point(284, 0);
            this.panelAfter.Name = "panelAfter";
            this.panelAfter.Size = new System.Drawing.Size(142, 100);
            this.panelAfter.TabIndex = 8;
            // 
            // lblAf
            // 
            this.lblAf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAf.Font = new System.Drawing.Font("Microsoft Sans Serif", 30.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAf.ForeColor = System.Drawing.Color.White;
            this.lblAf.Location = new System.Drawing.Point(0, 3);
            this.lblAf.Name = "lblAf";
            this.lblAf.Size = new System.Drawing.Size(136, 97);
            this.lblAf.TabIndex = 1;
            this.lblAf.Text = "ĐẾN CỬA";
            this.lblAf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTicket
            // 
            this.panelTicket.Controls.Add(this.lblTicket);
            this.panelTicket.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTicket.Location = new System.Drawing.Point(142, 0);
            this.panelTicket.Name = "panelTicket";
            this.panelTicket.Size = new System.Drawing.Size(142, 100);
            this.panelTicket.TabIndex = 7;
            // 
            // lblTicket
            // 
            this.lblTicket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 80.6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTicket.ForeColor = System.Drawing.Color.White;
            this.lblTicket.Location = new System.Drawing.Point(10, 0);
            this.lblTicket.Name = "lblTicket";
            this.lblTicket.Size = new System.Drawing.Size(126, 100);
            this.lblTicket.TabIndex = 1;
            this.lblTicket.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelBegin
            // 
            this.panelBegin.Controls.Add(this.labelBf);
            this.panelBegin.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelBegin.Location = new System.Drawing.Point(0, 0);
            this.panelBegin.Name = "panelBegin";
            this.panelBegin.Size = new System.Drawing.Size(142, 100);
            this.panelBegin.TabIndex = 6;
            // 
            // labelBf
            // 
            this.labelBf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelBf.Font = new System.Drawing.Font("Microsoft Sans Serif", 30.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBf.ForeColor = System.Drawing.Color.White;
            this.labelBf.Location = new System.Drawing.Point(3, 3);
            this.labelBf.Name = "labelBf";
            this.labelBf.Size = new System.Drawing.Size(136, 97);
            this.labelBf.TabIndex = 0;
            this.labelBf.Text = "MỜI SỐ";
            this.labelBf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SCREEN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 510);
            this.Controls.Add(this.panelAll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SCREEN";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelLeft.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.panelFooterVideo.ResumeLayout(false);
            this.panelAll.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.panelVideo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wmpVideo)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.panelCounter.ResumeLayout(false);
            this.panelAfter.ResumeLayout(false);
            this.panelTicket.ResumeLayout(false);
            this.panelBegin.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Panel panelfooterLogo;
        private System.Windows.Forms.Panel panelFooterVideo;
        private System.Windows.Forms.Label lblHello;
        private System.Windows.Forms.Panel panelAll;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelVideo;
        private AxWMPLib.AxWindowsMediaPlayer wmpVideo;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Panel panelCounter;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.Panel panelAfter;
        private System.Windows.Forms.Label lblAf;
        private System.Windows.Forms.Panel panelTicket;
        private System.Windows.Forms.Label lblTicket;
        private System.Windows.Forms.Panel panelBegin;
        private System.Windows.Forms.Label labelBf;






    }
}

