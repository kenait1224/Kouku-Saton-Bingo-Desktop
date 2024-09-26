namespace Kouku_Saton_Bingo
{
    partial class SettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.iTalk_TabControl1 = new iTalk_TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.iTalk_TrackBar_Opacity = new iTalk_TrackBar();
            this.iTalk_TrackBar_FormSize = new iTalk_TrackBar();
            this.iTalk_Label1 = new iTalk_Label();
            this.iTalk_Label2 = new iTalk_Label();
            this.iTalk_TabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // iTalk_TabControl1
            // 
            this.iTalk_TabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.iTalk_TabControl1.Controls.Add(this.tabPage1);
            this.iTalk_TabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iTalk_TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.iTalk_TabControl1.Font = new System.Drawing.Font("Segoe UI Semilight", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iTalk_TabControl1.ItemSize = new System.Drawing.Size(44, 135);
            this.iTalk_TabControl1.Location = new System.Drawing.Point(0, 0);
            this.iTalk_TabControl1.Multiline = true;
            this.iTalk_TabControl1.Name = "iTalk_TabControl1";
            this.iTalk_TabControl1.SelectedIndex = 0;
            this.iTalk_TabControl1.Size = new System.Drawing.Size(378, 94);
            this.iTalk_TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.iTalk_TabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(139, 4);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(0, 3, 18, 3);
            this.tabPage1.Size = new System.Drawing.Size(235, 86);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Display";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.72036F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.27964F));
            this.tableLayoutPanel1.Controls.Add(this.iTalk_TrackBar_Opacity, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.iTalk_TrackBar_FormSize, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.iTalk_Label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.iTalk_Label2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 3);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(217, 80);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // iTalk_TrackBar_Opacity
            // 
            this.iTalk_TrackBar_Opacity.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.iTalk_TrackBar_Opacity.DrawHatch = true;
            this.iTalk_TrackBar_Opacity.DrawValueString = false;
            this.iTalk_TrackBar_Opacity.JumpToMouse = false;
            this.iTalk_TrackBar_Opacity.Location = new System.Drawing.Point(106, 49);
            this.iTalk_TrackBar_Opacity.Maximum = 100;
            this.iTalk_TrackBar_Opacity.Minimum = 40;
            this.iTalk_TrackBar_Opacity.MinimumSize = new System.Drawing.Size(37, 22);
            this.iTalk_TrackBar_Opacity.Name = "iTalk_TrackBar_Opacity";
            this.iTalk_TrackBar_Opacity.Size = new System.Drawing.Size(108, 22);
            this.iTalk_TrackBar_Opacity.TabIndex = 3;
            this.iTalk_TrackBar_Opacity.Text = "iTalk_TrackBar_Opacity";
            this.iTalk_TrackBar_Opacity.Value = 40;
            this.iTalk_TrackBar_Opacity.ValueColour = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.iTalk_TrackBar_Opacity.ValueDivison = iTalk_TrackBar.ValueDivisor.By1;
            this.iTalk_TrackBar_Opacity.ValueToSet = 40F;
            // 
            // iTalk_TrackBar_FormSize
            // 
            this.iTalk_TrackBar_FormSize.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.iTalk_TrackBar_FormSize.DrawHatch = true;
            this.iTalk_TrackBar_FormSize.DrawValueString = false;
            this.iTalk_TrackBar_FormSize.JumpToMouse = true;
            this.iTalk_TrackBar_FormSize.Location = new System.Drawing.Point(106, 9);
            this.iTalk_TrackBar_FormSize.Maximum = 8;
            this.iTalk_TrackBar_FormSize.Minimum = 0;
            this.iTalk_TrackBar_FormSize.MinimumSize = new System.Drawing.Size(37, 22);
            this.iTalk_TrackBar_FormSize.Name = "iTalk_TrackBar_FormSize";
            this.iTalk_TrackBar_FormSize.Size = new System.Drawing.Size(108, 22);
            this.iTalk_TrackBar_FormSize.TabIndex = 1;
            this.iTalk_TrackBar_FormSize.Text = "iTalk_TrackBar_FormSize";
            this.iTalk_TrackBar_FormSize.Value = 0;
            this.iTalk_TrackBar_FormSize.ValueColour = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.iTalk_TrackBar_FormSize.ValueDivison = iTalk_TrackBar.ValueDivisor.By10;
            this.iTalk_TrackBar_FormSize.ValueToSet = 0F;
            // 
            // iTalk_Label1
            // 
            this.iTalk_Label1.AutoSize = true;
            this.iTalk_Label1.BackColor = System.Drawing.Color.Transparent;
            this.iTalk_Label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iTalk_Label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iTalk_Label1.ForeColor = System.Drawing.Color.Gray;
            this.iTalk_Label1.Location = new System.Drawing.Point(3, 0);
            this.iTalk_Label1.Name = "iTalk_Label1";
            this.iTalk_Label1.Size = new System.Drawing.Size(97, 40);
            this.iTalk_Label1.TabIndex = 6;
            this.iTalk_Label1.Text = "Form Size";
            this.iTalk_Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // iTalk_Label2
            // 
            this.iTalk_Label2.AutoSize = true;
            this.iTalk_Label2.BackColor = System.Drawing.Color.Transparent;
            this.iTalk_Label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iTalk_Label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iTalk_Label2.ForeColor = System.Drawing.Color.Gray;
            this.iTalk_Label2.Location = new System.Drawing.Point(3, 40);
            this.iTalk_Label2.Name = "iTalk_Label2";
            this.iTalk_Label2.Size = new System.Drawing.Size(97, 40);
            this.iTalk_Label2.TabIndex = 7;
            this.iTalk_Label2.Text = "Opacity";
            this.iTalk_Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(378, 94);
            this.Controls.Add(this.iTalk_TabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.Text = "Setting";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.iTalk_TabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private iTalk_TabControl iTalk_TabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private iTalk_TrackBar iTalk_TrackBar_Opacity;
        private iTalk_TrackBar iTalk_TrackBar_FormSize;
        private iTalk_Label iTalk_Label1;
        private iTalk_Label iTalk_Label2;
    }
}