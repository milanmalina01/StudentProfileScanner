namespace StudentProfileScanner
{
    partial class SummaryDisplay
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.NameColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timeINColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timeDeltacolumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timeOUTcolumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.activityColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mentorColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IndexColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameColumnHeader1,
            this.timeINColumnHeader1,
            this.timeDeltacolumnHeader1,
            this.timeOUTcolumnHeader1,
            this.activityColumnHeader1,
            this.mentorColumnHeader1,
            this.IndexColumnHeader1});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1204, 487);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            // 
            // NameColumnHeader1
            // 
            this.NameColumnHeader1.Text = "Name";
            this.NameColumnHeader1.Width = 200;
            // 
            // timeINColumnHeader1
            // 
            this.timeINColumnHeader1.Text = "Time/Date In";
            this.timeINColumnHeader1.Width = 200;
            // 
            // timeDeltacolumnHeader1
            // 
            this.timeDeltacolumnHeader1.Text = "How long";
            this.timeDeltacolumnHeader1.Width = 200;
            // 
            // timeOUTcolumnHeader1
            // 
            this.timeOUTcolumnHeader1.Text = "Time/Date Out";
            this.timeOUTcolumnHeader1.Width = 200;
            // 
            // activityColumnHeader1
            // 
            this.activityColumnHeader1.Text = "Activity";
            this.activityColumnHeader1.Width = 200;
            // 
            // mentorColumnHeader1
            // 
            this.mentorColumnHeader1.Text = "Mentor";
            this.mentorColumnHeader1.Width = 200;
            // 
            // IndexColumnHeader1
            // 
            this.IndexColumnHeader1.Text = "ReportID";
            // 
            // SummaryDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 487);
            this.Controls.Add(this.listView1);
            this.Name = "SummaryDisplay";
            this.Text = "SummaryDisplay";
            this.Load += new System.EventHandler(this.SummaryDisplay_Load);
            this.Resize += new System.EventHandler(this.SummaryDisplay_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader NameColumnHeader1;
        private System.Windows.Forms.ColumnHeader timeINColumnHeader1;
        private System.Windows.Forms.ColumnHeader timeDeltacolumnHeader1;
        private System.Windows.Forms.ColumnHeader timeOUTcolumnHeader1;
        private System.Windows.Forms.ColumnHeader activityColumnHeader1;
        private System.Windows.Forms.ColumnHeader mentorColumnHeader1;
        private System.Windows.Forms.ColumnHeader IndexColumnHeader1;
    }
}