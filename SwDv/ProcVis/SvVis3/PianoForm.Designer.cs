
namespace ZedHL
{
  partial class PianoForm
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
      this._lbl1 = new System.Windows.Forms.Label();
      this._holdChk = new System.Windows.Forms.CheckBox();
      this._polyChk = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // _lbl1
      // 
      this._lbl1.AutoSize = true;
      this._lbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._lbl1.Location = new System.Drawing.Point(12, 8);
      this._lbl1.Name = "_lbl1";
      this._lbl1.Size = new System.Drawing.Size(41, 13);
      this._lbl1.TabIndex = 0;
      this._lbl1.Text = "label1";
      // 
      // _holdChk
      // 
      this._holdChk.AutoSize = true;
      this._holdChk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._holdChk.Location = new System.Drawing.Point(114, 7);
      this._holdChk.Name = "_holdChk";
      this._holdChk.Size = new System.Drawing.Size(52, 17);
      this._holdChk.TabIndex = 1;
      this._holdChk.Text = "Hold";
      this._holdChk.UseVisualStyleBackColor = true;
      // 
      // _polyChk
      // 
      this._polyChk.AutoSize = true;
      this._polyChk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._polyChk.Location = new System.Drawing.Point(209, 7);
      this._polyChk.Name = "_polyChk";
      this._polyChk.Size = new System.Drawing.Size(85, 17);
      this._polyChk.TabIndex = 2;
      this._polyChk.Text = "Polyphone";
      this._polyChk.UseVisualStyleBackColor = true;
      // 
      // PianoForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(488, 66);
      this.Controls.Add(this._polyChk);
      this.Controls.Add(this._holdChk);
      this.Controls.Add(this._lbl1);
      this.Name = "PianoForm";
      this.Text = "PianoForm";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label _lbl1;
    private System.Windows.Forms.CheckBox _holdChk;
    private System.Windows.Forms.CheckBox _polyChk;
  }
}