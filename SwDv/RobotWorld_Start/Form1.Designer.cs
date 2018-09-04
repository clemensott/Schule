namespace RobotWorld
{
  partial class Form1
  {
    /// <summary>
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Vom Windows Form-Designer generierter Code

    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung.
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.menueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.createRobotsMenue = new System.Windows.Forms.ToolStripMenuItem();
      this.simOnOffMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this._prgNumEd = new System.Windows.Forms.TextBox();
      this.createObstaclesMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menueToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(803, 25);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // menueToolStripMenuItem
      // 
      this.menueToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createRobotsMenue,
            this.simOnOffMenu,
            this.createObstaclesMenu});
      this.menueToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.menueToolStripMenuItem.Name = "menueToolStripMenuItem";
      this.menueToolStripMenuItem.Size = new System.Drawing.Size(62, 21);
      this.menueToolStripMenuItem.Text = "Menue";
      // 
      // createRobotsMenue
      // 
      this.createRobotsMenue.CheckOnClick = true;
      this.createRobotsMenue.Name = "createRobotsMenue";
      this.createRobotsMenue.Size = new System.Drawing.Size(178, 22);
      this.createRobotsMenue.Text = "Create Robots";
      // 
      // simOnOffMenu
      // 
      this.simOnOffMenu.CheckOnClick = true;
      this.simOnOffMenu.Name = "simOnOffMenu";
      this.simOnOffMenu.Size = new System.Drawing.Size(178, 22);
      this.simOnOffMenu.Text = "Sim  On/Off";
      this.simOnOffMenu.CheckedChanged += new System.EventHandler(this.OnSimOnOffMenue);
      // 
      // timer1
      // 
      this.timer1.Tick += new System.EventHandler(this.OnTimer);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(101, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(51, 16);
      this.label1.TabIndex = 1;
      this.label1.Text = "label1";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(200, 9);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(76, 16);
      this.label2.TabIndex = 2;
      this.label2.Text = "ProgNum:";
      // 
      // _prgNumEd
      // 
      this._prgNumEd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._prgNumEd.Location = new System.Drawing.Point(282, 8);
      this._prgNumEd.Name = "_prgNumEd";
      this._prgNumEd.Size = new System.Drawing.Size(61, 22);
      this._prgNumEd.TabIndex = 3;
      // 
      // createObstaclesMenu
      // 
      this.createObstaclesMenu.CheckOnClick = true;
      this.createObstaclesMenu.Name = "createObstaclesMenu";
      this.createObstaclesMenu.Size = new System.Drawing.Size(178, 22);
      this.createObstaclesMenu.Text = "Create Obstacles";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(803, 646);
      this.Controls.Add(this._prgNumEd);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Form1";
      this.Text = "Form1";
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem menueToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem createRobotsMenue;
    private System.Windows.Forms.ToolStripMenuItem simOnOffMenu;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox _prgNumEd;
    private System.Windows.Forms.ToolStripMenuItem createObstaclesMenu;
  }
}

