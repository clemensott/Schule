namespace LcdEmul
{
  partial class Form2
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
      this._line2 = new System.Windows.Forms.Label();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this._line1 = new System.Windows.Forms.Label();
      this.timer2 = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      // 
      // _line2
      // 
      this._line2.AutoSize = true;
      this._line2.Font = new System.Drawing.Font("Courier New", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._line2.Location = new System.Drawing.Point(47, 78);
      this._line2.Name = "_line2";
      this._line2.Size = new System.Drawing.Size(110, 36);
      this._line2.TabIndex = 0;
      this._line2.Text = "21:22";
      // 
      // timer1
      // 
      this.timer1.Tick += new System.EventHandler(this.OnBlinkTimer);
      // 
      // _line1
      // 
      this._line1.AutoSize = true;
      this._line1.Font = new System.Drawing.Font("Courier New", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._line1.Location = new System.Drawing.Point(47, 29);
      this._line1.Name = "_line1";
      this._line1.Size = new System.Drawing.Size(110, 36);
      this._line1.TabIndex = 1;
      this._line1.Text = "Hallo";
      // 
      // timer2
      // 
      this.timer2.Tick += new System.EventHandler(this.OnCommTimer);
      // 
      // Form2
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(321, 166);
      this.Controls.Add(this._line1);
      this.Controls.Add(this._line2);
      this.Name = "Form2";
      this.Text = "LCD Emulation";
      this.Load += new System.EventHandler(this.OnFormLoad);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label _line2;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.Label _line1;
    private System.Windows.Forms.Timer timer2;
  }
}

