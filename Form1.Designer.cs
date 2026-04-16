namespace FileCompare
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            lvwLeftDir = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            pnlLeft2 = new Panel();
            txtLeftDir = new TextBox();
            btnLeftDir = new Button();
            pnlLeft1 = new Panel();
            lblAppname = new Label();
            btnCopyFromLeft = new Button();
            pnlRight2 = new Panel();
            txtRightDir = new TextBox();
            btnRightDir = new Button();
            pnlRight1 = new Panel();
            btnCopyFromRight = new Button();
            lvwRightDir = new ListView();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            pnlLeft2.SuspendLayout();
            pnlLeft1.SuspendLayout();
            pnlRight2.SuspendLayout();
            pnlRight1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Margin = new Padding(20);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lvwLeftDir);
            splitContainer1.Panel1.Controls.Add(pnlLeft2);
            splitContainer1.Panel1.Controls.Add(pnlLeft1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(lvwRightDir);
            splitContainer1.Panel2.Controls.Add(pnlRight2);
            splitContainer1.Panel2.Controls.Add(pnlRight1);
            splitContainer1.Size = new Size(1369, 684);
            splitContainer1.SplitterDistance = 638;
            splitContainer1.SplitterWidth = 20;
            splitContainer1.TabIndex = 0;
            // 
            // lvwLeftDir
            // 
            lvwLeftDir.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });
            lvwLeftDir.Dock = DockStyle.Fill;
            lvwLeftDir.FullRowSelect = true;
            lvwLeftDir.GridLines = true;
            lvwLeftDir.Location = new Point(0, 173);
            lvwLeftDir.Name = "lvwLeftDir";
            lvwLeftDir.Size = new Size(636, 509);
            lvwLeftDir.TabIndex = 2;
            lvwLeftDir.UseCompatibleStateImageBehavior = false;
            lvwLeftDir.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "이름";
            columnHeader1.Width = 300;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "크기";
            columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "수정일";
            columnHeader3.Width = 160;
            // 
            // pnlLeft2
            // 
            pnlLeft2.Controls.Add(txtLeftDir);
            pnlLeft2.Controls.Add(btnLeftDir);
            pnlLeft2.Dock = DockStyle.Top;
            pnlLeft2.Location = new Point(0, 96);
            pnlLeft2.Name = "pnlLeft2";
            pnlLeft2.Size = new Size(636, 77);
            pnlLeft2.TabIndex = 1;
            // 
            // txtLeftDir
            // 
            txtLeftDir.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtLeftDir.BorderStyle = BorderStyle.FixedSingle;
            txtLeftDir.Font = new Font("나눔고딕", 14.2499981F, FontStyle.Bold, GraphicsUnit.Point, 129);
            txtLeftDir.Location = new Point(11, 28);
            txtLeftDir.Name = "txtLeftDir";
            txtLeftDir.Size = new Size(482, 29);
            txtLeftDir.TabIndex = 1;
            // 
            // btnLeftDir
            // 
            btnLeftDir.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnLeftDir.FlatStyle = FlatStyle.Flat;
            btnLeftDir.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnLeftDir.Location = new Point(511, 20);
            btnLeftDir.Name = "btnLeftDir";
            btnLeftDir.Size = new Size(111, 43);
            btnLeftDir.TabIndex = 0;
            btnLeftDir.Text = "폴더선택";
            btnLeftDir.UseVisualStyleBackColor = true;
            btnLeftDir.Click += btnLeftDir_Click;
            // 
            // pnlLeft1
            // 
            pnlLeft1.Controls.Add(lblAppname);
            pnlLeft1.Controls.Add(btnCopyFromLeft);
            pnlLeft1.Dock = DockStyle.Top;
            pnlLeft1.Location = new Point(0, 0);
            pnlLeft1.Name = "pnlLeft1";
            pnlLeft1.Size = new Size(636, 96);
            pnlLeft1.TabIndex = 0;
            // 
            // lblAppname
            // 
            lblAppname.AutoSize = true;
            lblAppname.Font = new Font("나눔고딕", 35.9999962F, FontStyle.Bold, GraphicsUnit.Point, 129);
            lblAppname.Location = new Point(11, 12);
            lblAppname.Name = "lblAppname";
            lblAppname.Size = new Size(326, 55);
            lblAppname.TabIndex = 2;
            lblAppname.Text = "File Compare";
            // 
            // btnCopyFromLeft
            // 
            btnCopyFromLeft.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCopyFromLeft.FlatStyle = FlatStyle.Flat;
            btnCopyFromLeft.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnCopyFromLeft.Location = new Point(511, 11);
            btnCopyFromLeft.Name = "btnCopyFromLeft";
            btnCopyFromLeft.Size = new Size(111, 43);
            btnCopyFromLeft.TabIndex = 1;
            btnCopyFromLeft.Text = ">>>";
            btnCopyFromLeft.UseVisualStyleBackColor = true;
            // 
            // pnlRight2
            // 
            pnlRight2.Controls.Add(txtRightDir);
            pnlRight2.Controls.Add(btnRightDir);
            pnlRight2.Dock = DockStyle.Top;
            pnlRight2.Location = new Point(0, 96);
            pnlRight2.Name = "pnlRight2";
            pnlRight2.Size = new Size(709, 77);
            pnlRight2.TabIndex = 1;
            // 
            // txtRightDir
            // 
            txtRightDir.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtRightDir.BorderStyle = BorderStyle.FixedSingle;
            txtRightDir.Font = new Font("나눔고딕", 14.2499981F, FontStyle.Bold, GraphicsUnit.Point, 129);
            txtRightDir.Location = new Point(12, 28);
            txtRightDir.Margin = new Padding(15);
            txtRightDir.Name = "txtRightDir";
            txtRightDir.Size = new Size(556, 29);
            txtRightDir.TabIndex = 2;
            // 
            // btnRightDir
            // 
            btnRightDir.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRightDir.FlatStyle = FlatStyle.Flat;
            btnRightDir.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnRightDir.Location = new Point(586, 20);
            btnRightDir.Name = "btnRightDir";
            btnRightDir.Size = new Size(111, 43);
            btnRightDir.TabIndex = 3;
            btnRightDir.Text = "폴더선택";
            btnRightDir.UseVisualStyleBackColor = true;
            btnRightDir.Click += btnRightDir_Click;
            // 
            // pnlRight1
            // 
            pnlRight1.Controls.Add(btnCopyFromRight);
            pnlRight1.Dock = DockStyle.Top;
            pnlRight1.Location = new Point(0, 0);
            pnlRight1.Name = "pnlRight1";
            pnlRight1.Size = new Size(709, 96);
            pnlRight1.TabIndex = 0;
            // 
            // btnCopyFromRight
            // 
            btnCopyFromRight.FlatStyle = FlatStyle.Flat;
            btnCopyFromRight.Font = new Font("나눔고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnCopyFromRight.Location = new Point(12, 12);
            btnCopyFromRight.Name = "btnCopyFromRight";
            btnCopyFromRight.Size = new Size(111, 43);
            btnCopyFromRight.TabIndex = 2;
            btnCopyFromRight.Text = "<<<";
            btnCopyFromRight.UseVisualStyleBackColor = true;
            // 
            // lvwRightDir
            // 
            lvwRightDir.Columns.AddRange(new ColumnHeader[] { columnHeader4, columnHeader5, columnHeader6 });
            lvwRightDir.Dock = DockStyle.Fill;
            lvwRightDir.FullRowSelect = true;
            lvwRightDir.GridLines = true;
            lvwRightDir.Location = new Point(0, 173);
            lvwRightDir.Name = "lvwRightDir";
            lvwRightDir.Size = new Size(709, 509);
            lvwRightDir.TabIndex = 3;
            lvwRightDir.UseCompatibleStateImageBehavior = false;
            lvwRightDir.View = View.Details;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "이름";
            columnHeader4.Width = 300;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "크기";
            columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "수정일";
            columnHeader6.Width = 160;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1369, 684);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Text = "Form1";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            pnlLeft2.ResumeLayout(false);
            pnlLeft2.PerformLayout();
            pnlLeft1.ResumeLayout(false);
            pnlLeft1.PerformLayout();
            pnlRight2.ResumeLayout(false);
            pnlRight2.PerformLayout();
            pnlRight1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Panel pnlLeft1;
        private Panel pnlLeft2;
        private Panel pnlRight2;
        private Panel pnlRight1;
        private Button btnLeftDir;
        private Button btnCopyFromLeft;
        private Button btnRightDir;
        private Button btnCopyFromRight;
        private Label lblAppname;
        private TextBox txtLeftDir;
        private TextBox txtRightDir;
        private ListView lvwLeftDir;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ListView lvwRightDir;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
    }
}
