namespace LcdEmul
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
      this._line2 = new System.Windows.Forms.Label();
      this._num1 = new System.Windows.Forms.NumericUpDown();
      this._num2 = new System.Windows.Forms.NumericUpDown();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      ((System.ComponentModel.ISupportInitialize)(this._num1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this._num2)).BeginInit();
      this.SuspendLayout();
      // 
      // _line2
      // 
      this._line2.AutoSize = true;
      this._line2.Font = new System.Drawing.Font("Courier New", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._line2.Location = new System.Drawing.Point(67, 63);
      this._line2.Name = "_line2";
      this._line2.Size = new System.Drawing.Size(110, 36);
      this._line2.TabIndex = 0;
      this._line2.Text = "21:22";
      // 
      // _num1
      // 
      this._num1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._num1.Location = new System.Drawing.Point(220, 29);
      this._num1.Name = "_num1";
      this._num1.Size = new System.Drawing.Size(57, 22);
      this._num1.TabIndex = 1;
      this._num1.ValueChanged += new System.EventHandler(this.OnNum1ValCh);
      // 
      // _num2
      // 
      this._num2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._num2.Location = new System.Drawing.Point(220, 63);
      this._num2.Name = "_num2";
      this._num2.Size = new System.Drawing.Size(57, 22);
      this._num2.TabIndex = 2;
      this._num2.ValueChanged += new System.EventHandler(this.OnNum2ValCh);
      // 
      // timer1
      // 
      this.timer1.Tick += new System.EventHandler(this.OnBlinkTimer);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(321, 166);
      this.Controls.Add(this._num2);
      this.Controls.Add(this._num1);
      this.Controls.Add(this._line2);
      this.Name = "Form1";
      this.Text = "Form1";
      this.Load += new System.EventHandler(this.OnFormLoad);
      ((System.ComponentModel.ISupportInitialize)(this._num1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this._num2)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label _line2;
    private System.Windows.Forms.NumericUpDown _num1;
    private System.Windows.Forms.NumericUpDown _num2;
    private System.Windows.Forms.Timer timer1;
  }
}

