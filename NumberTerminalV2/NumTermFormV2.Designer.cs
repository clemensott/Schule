namespace NumberTerminal
{
  partial class NumTermFormV2
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
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.m_SendEd = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.m_TxtLine = new System.Windows.Forms.Label();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.menue1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.addNumberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.addSeperatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.clerViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.test1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sinleLineMenue = new System.Windows.Forms.ToolStripMenuItem();
      this.m_TxtLine2 = new System.Windows.Forms.Label();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // textBox1
      // 
      this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBox1.Location = new System.Drawing.Point(2, 95);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(309, 391);
      this.textBox1.TabIndex = 0;
      // 
      // timer1
      // 
      this.timer1.Tick += new System.EventHandler(this.OnTimer);
      // 
      // m_SendEd
      // 
      this.m_SendEd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_SendEd.Location = new System.Drawing.Point(55, 30);
      this.m_SendEd.Name = "m_SendEd";
      this.m_SendEd.Size = new System.Drawing.Size(87, 20);
      this.m_SendEd.TabIndex = 4;
      this.m_SendEd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnSendEdKeyDown);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(9, 30);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(40, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "Send:";
      // 
      // m_TxtLine
      // 
      this.m_TxtLine.AutoSize = true;
      this.m_TxtLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_TxtLine.Location = new System.Drawing.Point(148, 30);
      this.m_TxtLine.Name = "m_TxtLine";
      this.m_TxtLine.Size = new System.Drawing.Size(127, 16);
      this.m_TxtLine.TabIndex = 7;
      this.m_TxtLine.Text = "Single Line Text1";
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menue1ToolStripMenuItem,
            this.settingsToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(315, 24);
      this.menuStrip1.TabIndex = 8;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // menue1ToolStripMenuItem
      // 
      this.menue1ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNumberToolStripMenuItem,
            this.addSeperatorToolStripMenuItem,
            this.clerViewToolStripMenuItem,
            this.test1ToolStripMenuItem});
      this.menue1ToolStripMenuItem.Name = "menue1ToolStripMenuItem";
      this.menue1ToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
      this.menue1ToolStripMenuItem.Text = "Actions";
      // 
      // addNumberToolStripMenuItem
      // 
      this.addNumberToolStripMenuItem.Name = "addNumberToolStripMenuItem";
      this.addNumberToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
      this.addNumberToolStripMenuItem.Text = "AddNumber";
      this.addNumberToolStripMenuItem.Click += new System.EventHandler(this.OnAddNumberMen);
      // 
      // addSeperatorToolStripMenuItem
      // 
      this.addSeperatorToolStripMenuItem.Name = "addSeperatorToolStripMenuItem";
      this.addSeperatorToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
      this.addSeperatorToolStripMenuItem.Text = "AddSeperator";
      this.addSeperatorToolStripMenuItem.Click += new System.EventHandler(this.OnAddSeperatorMen);
      // 
      // clerViewToolStripMenuItem
      // 
      this.clerViewToolStripMenuItem.Name = "clerViewToolStripMenuItem";
      this.clerViewToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
      this.clerViewToolStripMenuItem.Text = "ClerView";
      this.clerViewToolStripMenuItem.Click += new System.EventHandler(this.OnClearViewMen);
      // 
      // test1ToolStripMenuItem
      // 
      this.test1ToolStripMenuItem.Name = "test1ToolStripMenuItem";
      this.test1ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
      this.test1ToolStripMenuItem.Text = "Test1";
      this.test1ToolStripMenuItem.Click += new System.EventHandler(this.OnTest1Men);
      // 
      // settingsToolStripMenuItem
      // 
      this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sinleLineMenue});
      this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
      this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
      this.settingsToolStripMenuItem.Text = "Settings";
      // 
      // sinleLineMenue
      // 
      this.sinleLineMenue.CheckOnClick = true;
      this.sinleLineMenue.Name = "sinleLineMenue";
      this.sinleLineMenue.Size = new System.Drawing.Size(146, 22);
      this.sinleLineMenue.Text = "SinleLine Text";
      // 
      // m_TxtLine2
      // 
      this.m_TxtLine2.AutoSize = true;
      this.m_TxtLine2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_TxtLine2.Location = new System.Drawing.Point(148, 62);
      this.m_TxtLine2.Name = "m_TxtLine2";
      this.m_TxtLine2.Size = new System.Drawing.Size(127, 16);
      this.m_TxtLine2.TabIndex = 9;
      this.m_TxtLine2.Text = "Single Line Text2";
      // 
      // NumTermFormV2
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(315, 492);
      this.Controls.Add(this.m_TxtLine2);
      this.Controls.Add(this.m_TxtLine);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.m_SendEd);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "NumTermFormV2";
      this.Text = "NumberTerminalV2";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.TextBox m_SendEd;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label m_TxtLine;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem menue1ToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem addNumberToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem addSeperatorToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem clerViewToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem test1ToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem sinleLineMenue;
    private System.Windows.Forms.Label m_TxtLine2;
  }
}

