namespace ZedTest1
{
  partial class Form1
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
      this.m_GC = new ZedGraph.ZedGraphControl();
      this.button1 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // m_GC
      // 
      this.m_GC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.m_GC.Location = new System.Drawing.Point(12, 4);
      this.m_GC.Name = "m_GC";
      this.m_GC.ScrollGrace = 0;
      this.m_GC.ScrollMaxX = 0;
      this.m_GC.ScrollMaxY = 0;
      this.m_GC.ScrollMaxY2 = 0;
      this.m_GC.ScrollMinX = 0;
      this.m_GC.ScrollMinY = 0;
      this.m_GC.ScrollMinY2 = 0;
      this.m_GC.Size = new System.Drawing.Size(490, 299);
      this.m_GC.TabIndex = 0;
      // 
      // button1
      // 
      this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.button1.Location = new System.Drawing.Point(12, 351);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(61, 23);
      this.button1.TabIndex = 1;
      this.button1.Text = "Step";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.OnStepButton);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(514, 386);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.m_GC);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);

    }

    #endregion

    private ZedGraph.ZedGraphControl m_GC;
    private System.Windows.Forms.Button button1;
  }
}

