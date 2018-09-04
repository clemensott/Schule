
namespace ZedTest1
{
  partial class VertBarWin
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
      this.grc = new ZedGraph.ZedGraphControl();
      this.SuspendLayout();
      // 
      // grc
      // 
      this.grc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.grc.Location = new System.Drawing.Point(2, 4);
      this.grc.Name = "grc";
      this.grc.ScrollGrace = 0;
      this.grc.ScrollMaxX = 0;
      this.grc.ScrollMaxY = 0;
      this.grc.ScrollMaxY2 = 0;
      this.grc.ScrollMinX = 0;
      this.grc.ScrollMinY = 0;
      this.grc.ScrollMinY2 = 0;
      this.grc.Size = new System.Drawing.Size(554, 378);
      this.grc.TabIndex = 0;
      // 
      // OnlineCurveWin
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(558, 383);
      this.Controls.Add(this.grc);
      this.Name = "OnlineCurveWin";
      this.Text = "Form1";
      this.ResumeLayout(false);

    }

    #endregion

    public ZedGraph.ZedGraphControl grc;
  }
}

