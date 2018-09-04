
namespace vis1
{
  partial class VisForm3
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
      this.components = new System.ComponentModel.Container();
      this.label1 = new System.Windows.Forms.Label();
      this.m_SendEd = new System.Windows.Forms.TextBox();
      this.m_Disp1 = new System.Windows.Forms.Label();
      this.m_Disp2 = new System.Windows.Forms.Label();
      this.m_Disp3 = new System.Windows.Forms.Label();
      this.m_DispTimer = new System.Windows.Forms.Timer(this.components);
      this.m_MsgLb = new System.Windows.Forms.ListBox();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.controMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.acqOnOffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.emptyReceiceBufferMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.clearMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.acqPointMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.curveWinMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.keyBoardMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.barWinMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_Disp4 = new System.Windows.Forms.Label();
      this.m_Disp5 = new System.Windows.Forms.Label();
      this.m_Disp6 = new System.Windows.Forms.Label();
      this._sb = new ZedHL.SliderBank();
      this.m_Disp7 = new System.Windows.Forms.Label();
      this.m_Disp8 = new System.Windows.Forms.Label();
      this.m_Disp9 = new System.Windows.Forms.Label();
      this._decodeTimer = new System.Windows.Forms.Timer(this.components);
      this.test1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(6, 34);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(48, 16);
      this.label1.TabIndex = 0;
      this.label1.Text = "Send:";
      // 
      // m_SendEd
      // 
      this.m_SendEd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_SendEd.Location = new System.Drawing.Point(61, 34);
      this.m_SendEd.Name = "m_SendEd";
      this.m_SendEd.Size = new System.Drawing.Size(145, 22);
      this.m_SendEd.TabIndex = 1;
      this.m_SendEd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnSendEditKeyDown);
      // 
      // m_Disp1
      // 
      this.m_Disp1.AutoSize = true;
      this.m_Disp1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_Disp1.Location = new System.Drawing.Point(267, 45);
      this.m_Disp1.Name = "m_Disp1";
      this.m_Disp1.Size = new System.Drawing.Size(50, 16);
      this.m_Disp1.TabIndex = 3;
      this.m_Disp1.Text = "V1111";
      // 
      // m_Disp2
      // 
      this.m_Disp2.AutoSize = true;
      this.m_Disp2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_Disp2.Location = new System.Drawing.Point(267, 76);
      this.m_Disp2.Name = "m_Disp2";
      this.m_Disp2.Size = new System.Drawing.Size(50, 16);
      this.m_Disp2.TabIndex = 4;
      this.m_Disp2.Text = "V2222";
      // 
      // m_Disp3
      // 
      this.m_Disp3.AutoSize = true;
      this.m_Disp3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_Disp3.Location = new System.Drawing.Point(267, 107);
      this.m_Disp3.Name = "m_Disp3";
      this.m_Disp3.Size = new System.Drawing.Size(50, 16);
      this.m_Disp3.TabIndex = 5;
      this.m_Disp3.Text = "V3333";
      // 
      // m_DispTimer
      // 
      this.m_DispTimer.Tick += new System.EventHandler(this.OnDispTimer);
      // 
      // m_MsgLb
      // 
      this.m_MsgLb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.m_MsgLb.FormattingEnabled = true;
      this.m_MsgLb.Location = new System.Drawing.Point(336, 36);
      this.m_MsgLb.Name = "m_MsgLb";
      this.m_MsgLb.Size = new System.Drawing.Size(165, 290);
      this.m_MsgLb.TabIndex = 11;
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.controMenuItem,
            this.windowToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(501, 24);
      this.menuStrip1.TabIndex = 12;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // controMenuItem
      // 
      this.controMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acqOnOffMenuItem,
            this.emptyReceiceBufferMenuItem,
            this.clearMessagesMenuItem,
            this.acqPointMenuItem,
            this.test1ToolStripMenuItem});
      this.controMenuItem.Name = "controMenuItem";
      this.controMenuItem.Size = new System.Drawing.Size(54, 20);
      this.controMenuItem.Text = "Control";
      // 
      // acqOnOffMenuItem
      // 
      this.acqOnOffMenuItem.CheckOnClick = true;
      this.acqOnOffMenuItem.Name = "acqOnOffMenuItem";
      this.acqOnOffMenuItem.Size = new System.Drawing.Size(185, 22);
      this.acqOnOffMenuItem.Text = "Acq. On/Off";
      this.acqOnOffMenuItem.Click += new System.EventHandler(this.OnAcqOnOffMenue);
      // 
      // emptyReceiceBufferMenuItem
      // 
      this.emptyReceiceBufferMenuItem.Name = "emptyReceiceBufferMenuItem";
      this.emptyReceiceBufferMenuItem.Size = new System.Drawing.Size(185, 22);
      this.emptyReceiceBufferMenuItem.Text = "Empty ReceiceBuffer";
      this.emptyReceiceBufferMenuItem.Click += new System.EventHandler(this.OnEmptyReceiveBufferMenue);
      // 
      // clearMessagesMenuItem
      // 
      this.clearMessagesMenuItem.Name = "clearMessagesMenuItem";
      this.clearMessagesMenuItem.Size = new System.Drawing.Size(185, 22);
      this.clearMessagesMenuItem.Text = "Clear Messages";
      this.clearMessagesMenuItem.Click += new System.EventHandler(this.OnClearMessagesMenue);
      // 
      // acqPointMenuItem
      // 
      this.acqPointMenuItem.CheckOnClick = true;
      this.acqPointMenuItem.Name = "acqPointMenuItem";
      this.acqPointMenuItem.Size = new System.Drawing.Size(185, 22);
      this.acqPointMenuItem.Text = "AcqPoints On/Off";
      this.acqPointMenuItem.Click += new System.EventHandler(this.OnAcqPointsOnOffMenue);
      // 
      // windowToolStripMenuItem
      // 
      this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.curveWinMenuItem,
            this.keyBoardMenuItem,
            this.barWinMenuItem});
      this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
      this.windowToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
      this.windowToolStripMenuItem.Text = "Window";
      // 
      // curveWinMenuItem
      // 
      this.curveWinMenuItem.CheckOnClick = true;
      this.curveWinMenuItem.Name = "curveWinMenuItem";
      this.curveWinMenuItem.Size = new System.Drawing.Size(169, 22);
      this.curveWinMenuItem.Text = "CurveWin On/Off";
      this.curveWinMenuItem.Click += new System.EventHandler(this.OnCurveWinOnOffMenue);
      // 
      // keyBoardMenuItem
      // 
      this.keyBoardMenuItem.CheckOnClick = true;
      this.keyBoardMenuItem.Name = "keyBoardMenuItem";
      this.keyBoardMenuItem.Size = new System.Drawing.Size(169, 22);
      this.keyBoardMenuItem.Text = "KeyBoard On/Off";
      this.keyBoardMenuItem.Click += new System.EventHandler(this.OnKeyBoardMenue);
      // 
      // barWinMenuItem
      // 
      this.barWinMenuItem.CheckOnClick = true;
      this.barWinMenuItem.Name = "barWinMenuItem";
      this.barWinMenuItem.Size = new System.Drawing.Size(169, 22);
      this.barWinMenuItem.Text = "BarWin On/Off";
      this.barWinMenuItem.Click += new System.EventHandler(this.OnBarWinMenue);
      // 
      // m_Disp4
      // 
      this.m_Disp4.AutoSize = true;
      this.m_Disp4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_Disp4.Location = new System.Drawing.Point(267, 138);
      this.m_Disp4.Name = "m_Disp4";
      this.m_Disp4.Size = new System.Drawing.Size(50, 16);
      this.m_Disp4.TabIndex = 16;
      this.m_Disp4.Text = "V4444";
      // 
      // m_Disp5
      // 
      this.m_Disp5.AutoSize = true;
      this.m_Disp5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_Disp5.Location = new System.Drawing.Point(267, 169);
      this.m_Disp5.Name = "m_Disp5";
      this.m_Disp5.Size = new System.Drawing.Size(50, 16);
      this.m_Disp5.TabIndex = 17;
      this.m_Disp5.Text = "V5555";
      // 
      // m_Disp6
      // 
      this.m_Disp6.AutoSize = true;
      this.m_Disp6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_Disp6.Location = new System.Drawing.Point(267, 200);
      this.m_Disp6.Name = "m_Disp6";
      this.m_Disp6.Size = new System.Drawing.Size(50, 16);
      this.m_Disp6.TabIndex = 18;
      this.m_Disp6.Text = "V6666";
      // 
      // _sb
      // 
      this._sb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)));
      this._sb.Location = new System.Drawing.Point(9, 64);
      this._sb.Name = "_sb";
      this._sb.Size = new System.Drawing.Size(243, 269);
      this._sb.TabIndex = 19;
      // 
      // m_Disp7
      // 
      this.m_Disp7.AutoSize = true;
      this.m_Disp7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_Disp7.Location = new System.Drawing.Point(267, 231);
      this.m_Disp7.Name = "m_Disp7";
      this.m_Disp7.Size = new System.Drawing.Size(50, 16);
      this.m_Disp7.TabIndex = 20;
      this.m_Disp7.Text = "V7777";
      // 
      // m_Disp8
      // 
      this.m_Disp8.AutoSize = true;
      this.m_Disp8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_Disp8.Location = new System.Drawing.Point(267, 262);
      this.m_Disp8.Name = "m_Disp8";
      this.m_Disp8.Size = new System.Drawing.Size(50, 16);
      this.m_Disp8.TabIndex = 21;
      this.m_Disp8.Text = "V8888";
      // 
      // m_Disp9
      // 
      this.m_Disp9.AutoSize = true;
      this.m_Disp9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_Disp9.Location = new System.Drawing.Point(268, 294);
      this.m_Disp9.Name = "m_Disp9";
      this.m_Disp9.Size = new System.Drawing.Size(50, 16);
      this.m_Disp9.TabIndex = 22;
      this.m_Disp9.Text = "V9999";
      // 
      // _decodeTimer
      // 
      this._decodeTimer.Tick += new System.EventHandler(this.OnDecodeTimer);
      // 
      // test1ToolStripMenuItem
      // 
      this.test1ToolStripMenuItem.Name = "test1ToolStripMenuItem";
      this.test1ToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
      this.test1ToolStripMenuItem.Text = "Test1";
      this.test1ToolStripMenuItem.Click += new System.EventHandler(this.OnMenueTest1);
      // 
      // VisForm3
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(501, 335);
      this.Controls.Add(this.m_Disp9);
      this.Controls.Add(this.m_Disp8);
      this.Controls.Add(this.m_Disp7);
      this.Controls.Add(this._sb);
      this.Controls.Add(this.m_Disp6);
      this.Controls.Add(this.m_Disp5);
      this.Controls.Add(this.m_Disp4);
      this.Controls.Add(this.m_MsgLb);
      this.Controls.Add(this.m_SendEd);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.m_Disp3);
      this.Controls.Add(this.m_Disp2);
      this.Controls.Add(this.m_Disp1);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "VisForm3";
      this.Text = "SvVis3  V2.2";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox m_SendEd;
    private System.Windows.Forms.Label m_Disp1;
    private System.Windows.Forms.Label m_Disp2;
    private System.Windows.Forms.Label m_Disp3;
    private System.Windows.Forms.Timer m_DispTimer;
    private System.Windows.Forms.ListBox m_MsgLb;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem controMenuItem;
    private System.Windows.Forms.ToolStripMenuItem acqOnOffMenuItem;
    private System.Windows.Forms.ToolStripMenuItem emptyReceiceBufferMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clearMessagesMenuItem;
    private System.Windows.Forms.ToolStripMenuItem acqPointMenuItem;
    private System.Windows.Forms.Label m_Disp4;
    private System.Windows.Forms.Label m_Disp5;
    private System.Windows.Forms.Label m_Disp6;
    private ZedHL.SliderBank _sb;
    private System.Windows.Forms.Label m_Disp7;
    private System.Windows.Forms.Label m_Disp8;
    private System.Windows.Forms.Label m_Disp9;
    private System.Windows.Forms.Timer _decodeTimer;
    private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem curveWinMenuItem;
    private System.Windows.Forms.ToolStripMenuItem keyBoardMenuItem;
    private System.Windows.Forms.ToolStripMenuItem barWinMenuItem;
    private System.Windows.Forms.ToolStripMenuItem test1ToolStripMenuItem;
  }
}

