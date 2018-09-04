namespace VirtJoyStick
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
            this._stc = new JoyStickLib.StickControl();
            this._MsgLb = new System.Windows.Forms.ListBox();
            this._timer1 = new System.Windows.Forms.Timer(this.components);
            this.m_panel = new System.Windows.Forms.Panel();
            this.m_lbl_CurDistance = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _stc
            // 
            this._stc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._stc.Location = new System.Drawing.Point(80, 65);
            this._stc.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this._stc.Name = "_stc";
            this._stc.Size = new System.Drawing.Size(692, 265);
            this._stc.TabIndex = 0;
            // 
            // _MsgLb
            // 
            this._MsgLb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._MsgLb.FormattingEnabled = true;
            this._MsgLb.ItemHeight = 20;
            this._MsgLb.Location = new System.Drawing.Point(838, 109);
            this._MsgLb.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._MsgLb.Name = "_MsgLb";
            this._MsgLb.Size = new System.Drawing.Size(334, 744);
            this._MsgLb.TabIndex = 2;
            // 
            // _timer1
            // 
            this._timer1.Tick += new System.EventHandler(this.OnTimer);
            // 
            // m_panel
            // 
            this.m_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_panel.Location = new System.Drawing.Point(80, 366);
            this.m_panel.Name = "m_panel";
            this.m_panel.Size = new System.Drawing.Size(692, 477);
            this.m_panel.TabIndex = 3;
            this.m_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.m_panel_Paint);
            // 
            // m_lbl_CurDistance
            // 
            this.m_lbl_CurDistance.AutoSize = true;
            this.m_lbl_CurDistance.Location = new System.Drawing.Point(721, 338);
            this.m_lbl_CurDistance.Name = "m_lbl_CurDistance";
            this.m_lbl_CurDistance.Size = new System.Drawing.Size(27, 20);
            this.m_lbl_CurDistance.TabIndex = 4;
            this.m_lbl_CurDistance.Text = "10";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 855);
            this.Controls.Add(this.m_lbl_CurDistance);
            this.Controls.Add(this.m_panel);
            this.Controls.Add(this._MsgLb);
            this.Controls.Add(this._stc);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private JoyStickLib.StickControl _stc;
    private System.Windows.Forms.ListBox _MsgLb;
    private System.Windows.Forms.Timer _timer1;
        private System.Windows.Forms.Panel m_panel;
        private System.Windows.Forms.Label m_lbl_CurDistance;
    }
}

