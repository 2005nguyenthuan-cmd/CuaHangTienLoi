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
            this.pnDashboard = new System.Windows.Forms.Panel();
            this.pnCenter = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chartRevenueByDate = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartCategory = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnBottom = new System.Windows.Forms.Panel();
            this.splitBottom = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvTopProducts = new System.Windows.Forms.DataGridView();
            this.dgvLowStock = new System.Windows.Forms.DataGridView();
            this.dgvExpiry = new System.Windows.Forms.DataGridView();
            this.pnKPI = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pnHeader = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnToday = new System.Windows.Forms.Button();
            this.btn7Days = new System.Windows.Forms.Button();
            this.btn30Days = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cardRevenue = new demo.Control.UC_Card_KPI();
            this.cardOrders = new demo.Control.UC_Card_KPI();
            this.cardProfit = new demo.Control.UC_Card_KPI();
            this.cardStock = new demo.Control.UC_Card_KPI();
            this.pnDashboard.SuspendLayout();
            this.pnCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueByDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCategory)).BeginInit();
            this.pnBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitBottom)).BeginInit();
            this.splitBottom.Panel1.SuspendLayout();
            this.splitBottom.Panel2.SuspendLayout();
            this.splitBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpiry)).BeginInit();
            this.pnKPI.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.pnHeader.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnDashboard
            // 
            this.pnDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.pnDashboard.Controls.Add(this.pnCenter);
            this.pnDashboard.Controls.Add(this.pnBottom);
            this.pnDashboard.Controls.Add(this.pnKPI);
            this.pnDashboard.Controls.Add(this.pnHeader);
            this.pnDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDashboard.Location = new System.Drawing.Point(0, 0);
            this.pnDashboard.Name = "pnDashboard";
            this.pnDashboard.Size = new System.Drawing.Size(1101, 518);
            this.pnDashboard.TabIndex = 0;
            // 
            // pnCenter
            // 
            this.pnCenter.Controls.Add(this.splitContainer1);
            this.pnCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnCenter.Location = new System.Drawing.Point(0, 235);
            this.pnCenter.Name = "pnCenter";
            this.pnCenter.Size = new System.Drawing.Size(1101, 83);
            this.pnCenter.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chartRevenueByDate);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chartCategory);
            this.splitContainer1.Size = new System.Drawing.Size(1101, 83);
            this.splitContainer1.SplitterDistance = 535;
            this.splitContainer1.TabIndex = 0;
            // 
            // chartRevenueByDate
            // 
            chartArea1.Name = "ChartArea1";
            this.chartRevenueByDate.ChartAreas.Add(chartArea1);
            this.chartRevenueByDate.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartRevenueByDate.Legends.Add(legend1);
            this.chartRevenueByDate.Location = new System.Drawing.Point(0, 0);
            this.chartRevenueByDate.Name = "chartRevenueByDate";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartRevenueByDate.Series.Add(series1);
            this.chartRevenueByDate.Size = new System.Drawing.Size(535, 83);
            this.chartRevenueByDate.TabIndex = 0;
            this.chartRevenueByDate.Text = "chart1";
            // 
            // chartCategory
            // 
            chartArea2.Name = "ChartArea1";
            this.chartCategory.ChartAreas.Add(chartArea2);
            this.chartCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chartCategory.Legends.Add(legend2);
            this.chartCategory.Location = new System.Drawing.Point(0, 0);
            this.chartCategory.Name = "chartCategory";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartCategory.Series.Add(series2);
            this.chartCategory.Size = new System.Drawing.Size(562, 83);
            this.chartCategory.TabIndex = 0;
            this.chartCategory.Text = "chart1";
            // 
            // pnBottom
            // 
            this.pnBottom.Controls.Add(this.splitBottom);
            this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnBottom.Location = new System.Drawing.Point(0, 318);
            this.pnBottom.Name = "pnBottom";
            this.pnBottom.Size = new System.Drawing.Size(1101, 200);
            this.pnBottom.TabIndex = 3;
            // 
            // splitBottom
            // 
            this.splitBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitBottom.Location = new System.Drawing.Point(0, 0);
            this.splitBottom.Name = "splitBottom";
            // 
            // splitBottom.Panel1
            // 
            this.splitBottom.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitBottom.Panel2
            // 
            this.splitBottom.Panel2.Controls.Add(this.dgvExpiry);
            this.splitBottom.Size = new System.Drawing.Size(1101, 200);
            this.splitBottom.SplitterDistance = 547;
            this.splitBottom.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvTopProducts);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgvLowStock);
            this.splitContainer2.Size = new System.Drawing.Size(547, 200);
            this.splitContainer2.SplitterDistance = 261;
            this.splitContainer2.TabIndex = 0;
            // 
            // dgvTopProducts
            // 
            this.dgvTopProducts.AllowUserToAddRows = false;
            this.dgvTopProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTopProducts.Location = new System.Drawing.Point(0, 0);
            this.dgvTopProducts.Name = "dgvTopProducts";
            this.dgvTopProducts.RowHeadersWidth = 51;
            this.dgvTopProducts.RowTemplate.Height = 24;
            this.dgvTopProducts.Size = new System.Drawing.Size(261, 200);
            this.dgvTopProducts.TabIndex = 1;
            // 
            // dgvLowStock
            // 
            this.dgvLowStock.AllowUserToAddRows = false;
            this.dgvLowStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLowStock.Location = new System.Drawing.Point(0, 0);
            this.dgvLowStock.Name = "dgvLowStock";
            this.dgvLowStock.RowHeadersWidth = 51;
            this.dgvLowStock.RowTemplate.Height = 24;
            this.dgvLowStock.Size = new System.Drawing.Size(282, 200);
            this.dgvLowStock.TabIndex = 1;
            // 
            // dgvExpiry
            // 
            this.dgvExpiry.AllowUserToAddRows = false;
            this.dgvExpiry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExpiry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExpiry.Location = new System.Drawing.Point(0, 0);
            this.dgvExpiry.Name = "dgvExpiry";
            this.dgvExpiry.RowHeadersWidth = 51;
            this.dgvExpiry.RowTemplate.Height = 24;
            this.dgvExpiry.Size = new System.Drawing.Size(550, 200);
            this.dgvExpiry.TabIndex = 1;
            // 
            // pnKPI
            // 
            this.pnKPI.Controls.Add(this.flowLayoutPanel1);
            this.pnKPI.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnKPI.Location = new System.Drawing.Point(0, 107);
            this.pnKPI.Name = "pnKPI";
            this.pnKPI.Size = new System.Drawing.Size(1101, 128);
            this.pnKPI.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cardRevenue);
            this.flowLayoutPanel1.Controls.Add(this.cardOrders);
            this.flowLayoutPanel1.Controls.Add(this.cardProfit);
            this.flowLayoutPanel1.Controls.Add(this.cardStock);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1101, 128);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // pnHeader
            // 
            this.pnHeader.BackColor = System.Drawing.Color.White;
            this.pnHeader.Controls.Add(this.tableLayoutPanel2);
            this.pnHeader.Controls.Add(this.tableLayoutPanel1);
            this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnHeader.Location = new System.Drawing.Point(0, 0);
            this.pnHeader.Name = "pnHeader";
            this.pnHeader.Padding = new System.Windows.Forms.Padding(15);
            this.pnHeader.Size = new System.Drawing.Size(1101, 107);
            this.pnHeader.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.Controls.Add(this.dtTo, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtFrom, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 15);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(626, 77);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // dtTo
            // 
            this.dtTo.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtTo.Location = new System.Drawing.Point(468, 0);
            this.dtTo.Margin = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(158, 22);
            this.dtTo.TabIndex = 2;
            this.dtTo.ValueChanged += new System.EventHandler(this.dtTo_ValueChanged);
            // 
            // dtFrom
            // 
            this.dtFrom.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtFrom.Location = new System.Drawing.Point(212, 0);
            this.dtFrom.Margin = new System.Windows.Forms.Padding(0, 0, 40, 0);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(166, 22);
            this.dtFrom.TabIndex = 1;
            this.dtFrom.ValueChanged += new System.EventHandler(this.dtFrom_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "DASHBOARD";
            // 
            // btnToday
            // 
            this.btnToday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnToday.Location = new System.Drawing.Point(3, 3);
            this.btnToday.Name = "btnToday";
            this.btnToday.Padding = new System.Windows.Forms.Padding(5);
            this.btnToday.Size = new System.Drawing.Size(94, 71);
            this.btnToday.TabIndex = 4;
            this.btnToday.Tag = "filter";
            this.btnToday.Text = "Hôm nay";
            this.btnToday.UseVisualStyleBackColor = true;
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // btn7Days
            // 
            this.btn7Days.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn7Days.Location = new System.Drawing.Point(103, 3);
            this.btn7Days.Name = "btn7Days";
            this.btn7Days.Padding = new System.Windows.Forms.Padding(5);
            this.btn7Days.Size = new System.Drawing.Size(94, 71);
            this.btn7Days.TabIndex = 5;
            this.btn7Days.Tag = "filter";
            this.btn7Days.Text = "Tuần";
            this.btn7Days.UseVisualStyleBackColor = true;
            this.btn7Days.Click += new System.EventHandler(this.btn7Days_Click);
            // 
            // btn30Days
            // 
            this.btn30Days.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn30Days.Location = new System.Drawing.Point(203, 3);
            this.btn30Days.Name = "btn30Days";
            this.btn30Days.Padding = new System.Windows.Forms.Padding(5);
            this.btn30Days.Size = new System.Drawing.Size(94, 71);
            this.btn30Days.TabIndex = 6;
            this.btn30Days.Tag = "filter";
            this.btn30Days.Text = "Tháng";
            this.btn30Days.UseVisualStyleBackColor = true;
            this.btn30Days.Click += new System.EventHandler(this.btn30Days_Click);
            // 
            // btnExport
            // 
            this.btnExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExport.Location = new System.Drawing.Point(303, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Padding = new System.Windows.Forms.Padding(5);
            this.btnExport.Size = new System.Drawing.Size(94, 71);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Xuất báo cáo";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.btnExport, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn30Days, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnToday, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn7Days, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(686, 15);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(400, 77);
            this.tableLayoutPanel2.TabIndex = 8;
            // 
            // cardRevenue
            // 
            this.cardRevenue.Location = new System.Drawing.Point(13, 13);
            this.cardRevenue.Name = "cardRevenue";
            this.cardRevenue.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.cardRevenue.Size = new System.Drawing.Size(306, 112);
            this.cardRevenue.TabIndex = 0;
            // 
            // cardOrders
            // 
            this.cardOrders.Location = new System.Drawing.Point(325, 13);
            this.cardOrders.Name = "cardOrders";
            this.cardOrders.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.cardOrders.Size = new System.Drawing.Size(257, 112);
            this.cardOrders.TabIndex = 1;
            // 
            // cardProfit
            // 
            this.cardProfit.Location = new System.Drawing.Point(588, 13);
            this.cardProfit.Name = "cardProfit";
            this.cardProfit.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.cardProfit.Size = new System.Drawing.Size(265, 112);
            this.cardProfit.TabIndex = 2;
            // 
            // cardStock
            // 
            this.cardStock.Location = new System.Drawing.Point(859, 13);
            this.cardStock.Name = "cardStock";
            this.cardStock.Size = new System.Drawing.Size(231, 112);
            this.cardStock.TabIndex = 3;
            // 
            // UC_TongQuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnDashboard);
            this.Name = "UC_TongQuan";
            this.Size = new System.Drawing.Size(1101, 518);
            this.Load += new System.EventHandler(this.UC_TongQuan_Load);
            this.pnDashboard.ResumeLayout(false);
            this.pnCenter.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueByDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCategory)).EndInit();
            this.pnBottom.ResumeLayout(false);
            this.splitBottom.Panel1.ResumeLayout(false);
            this.splitBottom.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitBottom)).EndInit();
            this.splitBottom.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpiry)).EndInit();
            this.pnKPI.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.pnHeader.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnDashboard;
        private System.Windows.Forms.Panel pnKPI;
        private System.Windows.Forms.Panel pnHeader;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private UC_Card_KPI cardRevenue;
        private UC_Card_KPI cardOrders;
        private UC_Card_KPI cardProfit;
        private UC_Card_KPI cardStock;
        private System.Windows.Forms.Panel pnCenter;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenueByDate;
        private System.Windows.Forms.Panel pnBottom;
        private System.Windows.Forms.SplitContainer splitBottom;
        private System.Windows.Forms.DataGridView dgvExpiry;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgvTopProducts;
        private System.Windows.Forms.DataGridView dgvLowStock;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCategory;
        private System.Windows.Forms.Button btn30Days;
        private System.Windows.Forms.Button btn7Days;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}
