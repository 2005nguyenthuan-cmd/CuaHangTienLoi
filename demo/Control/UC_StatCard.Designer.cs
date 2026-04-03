namespace demo.Control
{
    partial class UC_StatCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlCards = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlCards
            // 
            this.pnlCards.Location = new System.Drawing.Point(32, 48);
            this.pnlCards.Name = "pnlCards";
            this.pnlCards.Size = new System.Drawing.Size(922, 150);
            this.pnlCards.TabIndex = 0;
            // 
            // UC_StatCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlCards);
            this.Name = "UC_StatCard";
            this.Size = new System.Drawing.Size(1007, 618);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCards;
    }
}
