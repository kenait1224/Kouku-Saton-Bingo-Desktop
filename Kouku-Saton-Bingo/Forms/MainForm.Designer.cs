namespace Kouku_Saton_Bingo
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ControlRenderer controlRenderer1 = new ControlRenderer();
            MSColorTable msColorTable1 = new MSColorTable();
            this.notifyIcon_main = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip_main = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.iTalk_ContextMenuStrip_main = new iTalk_ContextMenuStrip();
            this.SuspendLayout();
            // 
            // notifyIcon_main
            // 
            this.notifyIcon_main.Text = "notifyIcon1";
            this.notifyIcon_main.Visible = true;
            // 
            // contextMenuStrip_main
            // 
            this.contextMenuStrip_main.Font = new System.Drawing.Font("Segoe UI Semilight", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip_main.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip_main.Name = "contextMenuStrip_main";
            this.contextMenuStrip_main.Size = new System.Drawing.Size(61, 4);
            // 
            // iTalk_ContextMenuStrip_main
            // 
            this.iTalk_ContextMenuStrip_main.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.iTalk_ContextMenuStrip_main.Name = "iTalk_ContextMenuStrip_main";
            controlRenderer1.ColorTable = msColorTable1;
            controlRenderer1.RoundedEdges = true;
            this.iTalk_ContextMenuStrip_main.Renderer = controlRenderer1;
            this.iTalk_ContextMenuStrip_main.Size = new System.Drawing.Size(61, 4);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(680, 575);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Opacity = 0D;
            this.Text = "Kouku-Saton-Bingo Toolkit (BETA)";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon_main;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_main;
        private iTalk_ContextMenuStrip iTalk_ContextMenuStrip_main;
    }
}

