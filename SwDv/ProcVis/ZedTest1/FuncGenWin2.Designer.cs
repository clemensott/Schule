namespace ZedTest1
{
  partial class FuncGenWin2
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
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.m_AmplBar = new System.Windows.Forms.TrackBar();
      this.label1 = new System.Windows.Forms.Label();
      this.m_UpdCheck = new System.Windows.Forms.CheckBox();
      this.button1 = new System.Windows.Forms.Button();
      this.m_ShowGraphChk = new System.Windows.Forms.CheckBox();
      this.m_occ = new ZedHL.OnlineCurveControl();
      ((System.ComponentModel.ISupportInitialize)(this.m_AmplBar)).BeginInit();
      this.SuspendLayout();
      // 
      // timer1
      // 
      this.timer1.Tick += new System.EventHandler(this.OnTimer);
      // 
      // m_AmplBar
      // 
      this.m_AmplBar.Location = new System.Drawing.Point(12, 12);
      this.m_AmplBar.Maximum = 100;
      this.m_AmplBar.Name = "m_AmplBar";
      this.m_AmplBar.Size = new System.Drawing.Size(186, 42);
      this.m_AmplBar.TabIndex = 1;
      this.m_AmplBar.Value = 50;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(215, 22);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(41, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "label1";
      // 
      // m_UpdCheck
      // 
      this.m_UpdCheck.AutoSize = true;
      this.m_UpdCheck.Checked = true;
      this.m_UpdCheck.CheckState = System.Windows.Forms.CheckState.Checked;
      this.m_UpdCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_UpdCheck.Location = new System.Drawing.Point(12, 60);
      this.m_UpdCheck.Name = "m_UpdCheck";
      this.m_UpdCheck.Size = new System.Drawing.Size(110, 17);
      this.m_UpdCheck.TabIndex = 3;
      this.m_UpdCheck.Text = "Update On/Off";
      this.m_UpdCheck.UseVisualStyleBackColor = true;
      this.m_UpdCheck.CheckedChanged += new System.EventHandler(this.OnUpdateChkChanged);
      // 
      // button1
      // 
      this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.button1.Location = new System.Drawing.Point(178, 57);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(117, 23);
      this.button1.TabIndex = 4;
      this.button1.Text = "Call AxisChanged";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.OnAxisChangedButton);
      // 
      // m_ShowGraphChk
      // 
      this.m_ShowGraphChk.AutoSize = true;
      this.m_ShowGraphChk.Checked = true;
      this.m_ShowGraphChk.CheckState = System.Windows.Forms.CheckState.Checked;
      this.m_ShowGraphChk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_ShowGraphChk.Location = new System.Drawing.Point(12, 87);
      this.m_ShowGraphChk.Name = "m_ShowGraphChk";
      this.m_ShowGraphChk.Size = new System.Drawing.Size(137, 17);
      this.m_ShowGraphChk.TabIndex = 5;
      this.m_ShowGraphChk.Text = "Show/Hide Graphic";
      this.m_ShowGraphChk.UseVisualStyleBackColor = true;
      this.m_ShowGraphChk.CheckedChanged += new System.EventHandler(this.OnShowHideChkChanged);
      // 
      // m_occ
      // 
      this.m_occ.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.m_occ.IsAutoScrollRange = true;
      this.m_occ.IsShowHScrollBar = true;
      this.m_occ.Location = new System.Drawing.Point(12, 126);
      this.m_occ.Name = "m_occ";
      this.m_occ.ScrollGrace = 0;
      this.m_occ.ScrollMaxX = 0;
      this.m_occ.ScrollMaxY = 0;
      this.m_occ.ScrollMaxY2 = 0;
      this.m_occ.ScrollMinX = 0;
      this.m_occ.ScrollMinY = 0;
      this.m_occ.ScrollMinY2 = 0;
      this.m_occ.Size = new System.Drawing.Size(710, 338);
      this.m_occ.TabIndex = 6;
      // 
      // FuncGenWin2
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(734, 476);
      this.Controls.Add(this.m_occ);
      this.Controls.Add(this.m_ShowGraphChk);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.m_UpdCheck);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.m_AmplBar);
      this.Name = "FuncGenWin2";
      this.Text = "FuncGen";
      ((System.ComponentModel.ISupportInitialize)(this.m_AmplBar)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.TrackBar m_AmplBar;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.CheckBox m_UpdCheck;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.CheckBox m_ShowGraphChk;
    private ZedHL.OnlineCurveControl m_occ;
  }
}

