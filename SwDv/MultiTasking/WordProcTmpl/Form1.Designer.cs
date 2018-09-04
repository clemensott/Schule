namespace WordProc
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
      this._txtBox1 = new System.Windows.Forms.TextBox();
      this._txtBox2 = new System.Windows.Forms.TextBox();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.menueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.startWordProcessorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // _txtBox1
      // 
      this._txtBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)));
      this._txtBox1.Location = new System.Drawing.Point(12, 50);
      this._txtBox1.Multiline = true;
      this._txtBox1.Name = "_txtBox1";
      this._txtBox1.Size = new System.Drawing.Size(309, 518);
      this._txtBox1.TabIndex = 0;
      // 
      // _txtBox2
      // 
      this._txtBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this._txtBox2.Location = new System.Drawing.Point(327, 50);
      this._txtBox2.Multiline = true;
      this._txtBox2.Name = "_txtBox2";
      this._txtBox2.Size = new System.Drawing.Size(321, 518);
      this._txtBox2.TabIndex = 1;
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menueToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(660, 24);
      this.menuStrip1.TabIndex = 2;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // menueToolStripMenuItem
      // 
      this.menueToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startWordProcessorToolStripMenuItem});
      this.menueToolStripMenuItem.Name = "menueToolStripMenuItem";
      this.menueToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
      this.menueToolStripMenuItem.Text = "Menue";
      // 
      // startWordProcessorToolStripMenuItem
      // 
      this.startWordProcessorToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.startWordProcessorToolStripMenuItem.Name = "startWordProcessorToolStripMenuItem";
      this.startWordProcessorToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
      this.startWordProcessorToolStripMenuItem.Text = "Start Word Processor";
      this.startWordProcessorToolStripMenuItem.Click += new System.EventHandler(this.OnMenueStartWordProc);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(660, 580);
      this.Controls.Add(this._txtBox2);
      this.Controls.Add(this._txtBox1);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Form1";
      this.Text = "Form1";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox _txtBox1;
    private System.Windows.Forms.TextBox _txtBox2;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem menueToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem startWordProcessorToolStripMenuItem;
  }
}

