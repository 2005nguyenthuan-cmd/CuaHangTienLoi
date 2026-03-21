namespace demo.Control
{
    partial class UC_TongQuan
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.flowTop = new System.Windows.Forms.FlowLayoutPanel();
            this.panelChartLeft = new System.Windows.Forms.Panel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panelChartRight = new System.Windows.Forms.Panel();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panelChartLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panelChartRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // flowTop
            // 
            this.flowTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowTop.Location = new System.Drawing.Point(0, 0);
            this.flowTop.Name = "flowTop";
            this.flowTop.Size = new System.Drawing.Size(799, 143);
            this.flowTop.TabIndex = 0;
            // 
            // panelChartLeft
            // 
            this.panelChartLeft.Controls.Add(this.chart1);
            this.panelChartLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChartLeft.Location = new System.Drawing.Point(0, 143);
            this.panelChartLeft.Name = "panelChartLeft";
            this.panelChartLeft.Size = new System.Drawing.Size(163, 315);
            this.panelChartLeft.TabIndex = 1;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(163, 315);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // panelChartRight
            // 
            this.panelChartRight.Controls.Add(this.chart2);
            this.panelChartRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelChartRight.Location = new System.Drawing.Point(163, 143);
            this.panelChartRight.Name = "panelChartRight";
            this.panelChartRight.Size = new System.Drawing.Size(636, 315);
            this.panelChartRight.TabIndex = 2;
            // 
            // chart2
            // 
            chartArea2.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea2);
            this.chart2.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chart2.Legends.Add(legend2);
            this.chart2.Location = new System.Drawing.Point(0, 0);
            this.chart2.Name = "chart2";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart2.Series.Add(series2);
            this.chart2.Size = new System.Drawing.Size(636, 315);
            this.chart2.TabIndex = 0;
            this.chart2.Text = "chart2";
            // 
            // UC_TongQuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelChartLeft);
            this.Controls.Add(this.panelChartRight);
            this.Controls.Add(this.flowTop);
            this.Name = "UC_TongQuan";
            this.Size = new System.Drawing.Size(799, 458);
            this.panelChartLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panelChartRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowTop;
        private System.Windows.Forms.Panel panelChartLeft;
        private System.Windows.Forms.Panel panelChartRight;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
    }
}
