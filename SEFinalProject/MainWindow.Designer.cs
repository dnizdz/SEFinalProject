namespace SEFinalProject {
    partial class MainWindow {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timeTextBox = new System.Windows.Forms.TextBox();
            this.timeLabel = new System.Windows.Forms.Label();
            this.operationTextBox = new System.Windows.Forms.TextBox();
            this.operationLabel = new System.Windows.Forms.Label();
            this.roleTextBox = new System.Windows.Forms.TextBox();
            this.roleLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(3, 3);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(478, 414);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 561);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.timeTextBox);
            this.panel1.Controls.Add(this.timeLabel);
            this.panel1.Controls.Add(this.operationTextBox);
            this.panel1.Controls.Add(this.operationLabel);
            this.panel1.Controls.Add(this.roleTextBox);
            this.panel1.Controls.Add(this.roleLabel);
            this.panel1.Controls.Add(this.nameTextBox);
            this.panel1.Controls.Add(this.nameLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 423);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(478, 135);
            this.panel1.TabIndex = 1;
            // 
            // timeTextBox
            // 
            this.timeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeTextBox.Location = new System.Drawing.Point(94, 103);
            this.timeTextBox.Name = "timeTextBox";
            this.timeTextBox.ReadOnly = true;
            this.timeTextBox.Size = new System.Drawing.Size(375, 26);
            this.timeTextBox.TabIndex = 7;
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.Location = new System.Drawing.Point(9, 106);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(43, 20);
            this.timeLabel.TabIndex = 6;
            this.timeLabel.Text = "Time";
            // 
            // operationTextBox
            // 
            this.operationTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.operationTextBox.Location = new System.Drawing.Point(94, 71);
            this.operationTextBox.Name = "operationTextBox";
            this.operationTextBox.ReadOnly = true;
            this.operationTextBox.Size = new System.Drawing.Size(375, 26);
            this.operationTextBox.TabIndex = 5;
            // 
            // operationLabel
            // 
            this.operationLabel.AutoSize = true;
            this.operationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.operationLabel.Location = new System.Drawing.Point(9, 74);
            this.operationLabel.Name = "operationLabel";
            this.operationLabel.Size = new System.Drawing.Size(79, 20);
            this.operationLabel.TabIndex = 4;
            this.operationLabel.Text = "Operation";
            // 
            // roleTextBox
            // 
            this.roleTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roleTextBox.Location = new System.Drawing.Point(94, 39);
            this.roleTextBox.Name = "roleTextBox";
            this.roleTextBox.ReadOnly = true;
            this.roleTextBox.Size = new System.Drawing.Size(375, 26);
            this.roleTextBox.TabIndex = 3;
            // 
            // roleLabel
            // 
            this.roleLabel.AutoSize = true;
            this.roleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roleLabel.Location = new System.Drawing.Point(9, 42);
            this.roleLabel.Name = "roleLabel";
            this.roleLabel.Size = new System.Drawing.Size(42, 20);
            this.roleLabel.TabIndex = 2;
            this.roleLabel.Text = "Role";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameTextBox.Location = new System.Drawing.Point(94, 7);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.ReadOnly = true;
            this.nameTextBox.Size = new System.Drawing.Size(375, 26);
            this.nameTextBox.TabIndex = 1;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(9, 10);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(51, 20);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 561);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Attendance System";
            this.Activated += new System.EventHandler(this.MainWindow_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox roleTextBox;
        private System.Windows.Forms.Label roleLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox timeTextBox;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.TextBox operationTextBox;
        private System.Windows.Forms.Label operationLabel;
    }
}

