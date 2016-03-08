namespace GeneticsLab
{
    partial class MainForm
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
            this.bandCheckBox = new System.Windows.Forms.CheckBox();
            this.bandlengthBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.file1Box = new System.Windows.Forms.TextBox();
            this.sequence1Box = new System.Windows.Forms.TextBox();
            this.sequence2Box = new System.Windows.Forms.TextBox();
            this.file2Box = new System.Windows.Forms.TextBox();
            this.dataGridViewResults = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.processButton = new System.Windows.Forms.ToolStripButton();
            this.clearButton = new System.Windows.Forms.ToolStripButton();
            this.getGridButton = new System.Windows.Forms.Button();
            this.getGridTextbox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bandCheckBox
            // 
            this.bandCheckBox.Location = new System.Drawing.Point(933, 185);
            this.bandCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.bandCheckBox.Name = "bandCheckBox";
            this.bandCheckBox.Size = new System.Drawing.Size(20, 18);
            this.bandCheckBox.TabIndex = 3;
            // 
            // bandlengthBox
            // 
            this.bandlengthBox.Location = new System.Drawing.Point(933, 215);
            this.bandlengthBox.Margin = new System.Windows.Forms.Padding(4);
            this.bandlengthBox.Name = "bandlengthBox";
            this.bandlengthBox.Size = new System.Drawing.Size(99, 22);
            this.bandlengthBox.TabIndex = 2;
            this.bandlengthBox.Text = "5000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 388);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "File1 Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 425);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Sequence1:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 462);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Sequence2:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 498);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "File2 Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(837, 222);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Align Length:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(867, 185);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 17);
            this.label6.TabIndex = 1;
            this.label6.Text = "Banded:";
            // 
            // file1Box
            // 
            this.file1Box.Enabled = false;
            this.file1Box.Location = new System.Drawing.Point(153, 382);
            this.file1Box.Margin = new System.Windows.Forms.Padding(4);
            this.file1Box.Name = "file1Box";
            this.file1Box.Size = new System.Drawing.Size(665, 22);
            this.file1Box.TabIndex = 8;
            // 
            // sequence1Box
            // 
            this.sequence1Box.Enabled = false;
            this.sequence1Box.Font = new System.Drawing.Font("Courier New", 9F);
            this.sequence1Box.Location = new System.Drawing.Point(153, 418);
            this.sequence1Box.Margin = new System.Windows.Forms.Padding(4);
            this.sequence1Box.Name = "sequence1Box";
            this.sequence1Box.Size = new System.Drawing.Size(972, 24);
            this.sequence1Box.TabIndex = 9;
            // 
            // sequence2Box
            // 
            this.sequence2Box.Enabled = false;
            this.sequence2Box.Font = new System.Drawing.Font("Courier New", 9F);
            this.sequence2Box.Location = new System.Drawing.Point(153, 455);
            this.sequence2Box.Margin = new System.Windows.Forms.Padding(4);
            this.sequence2Box.Name = "sequence2Box";
            this.sequence2Box.Size = new System.Drawing.Size(972, 24);
            this.sequence2Box.TabIndex = 10;
            // 
            // file2Box
            // 
            this.file2Box.Enabled = false;
            this.file2Box.Location = new System.Drawing.Point(153, 492);
            this.file2Box.Margin = new System.Windows.Forms.Padding(4);
            this.file2Box.Name = "file2Box";
            this.file2Box.Size = new System.Drawing.Size(665, 22);
            this.file2Box.TabIndex = 11;
            // 
            // dataGridViewResults
            // 
            this.dataGridViewResults.AllowUserToAddRows = false;
            this.dataGridViewResults.AllowUserToDeleteRows = false;
            this.dataGridViewResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResults.Location = new System.Drawing.Point(16, 34);
            this.dataGridViewResults.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewResults.Name = "dataGridViewResults";
            this.dataGridViewResults.ReadOnly = true;
            this.dataGridViewResults.Size = new System.Drawing.Size(1140, 570);
            this.dataGridViewResults.TabIndex = 0;
            this.dataGridViewResults.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.cell_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 613);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1172, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusMessage
            // 
            this.statusMessage.Name = "statusMessage";
            this.statusMessage.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processButton,
            this.clearButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Size = new System.Drawing.Size(1172, 27);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // processButton
            // 
            this.processButton.BackColor = System.Drawing.Color.Green;
            this.processButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.processButton.ImageTransparentColor = System.Drawing.Color.Green;
            this.processButton.Name = "processButton";
            this.processButton.Size = new System.Drawing.Size(62, 24);
            this.processButton.Text = "Process";
            this.processButton.Click += new System.EventHandler(this.processButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.BackColor = System.Drawing.Color.Orange;
            this.clearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.clearButton.ImageTransparentColor = System.Drawing.Color.Orange;
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(47, 24);
            this.clearButton.Text = "Clear";
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // getGridButton
            // 
            this.getGridButton.Location = new System.Drawing.Point(840, 259);
            this.getGridButton.Name = "getGridButton";
            this.getGridButton.Size = new System.Drawing.Size(75, 23);
            this.getGridButton.TabIndex = 12;
            this.getGridButton.Text = "Get Grid:";
            this.getGridButton.UseVisualStyleBackColor = true;
            this.getGridButton.Click += new System.EventHandler(this.getGridButton_Click);
            // 
            // getGridTextbox
            // 
            this.getGridTextbox.Location = new System.Drawing.Point(921, 260);
            this.getGridTextbox.Name = "getGridTextbox";
            this.getGridTextbox.Size = new System.Drawing.Size(100, 22);
            this.getGridTextbox.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 635);
            this.Controls.Add(this.getGridTextbox);
            this.Controls.Add(this.getGridButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.bandlengthBox);
            this.Controls.Add(this.bandCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.file1Box);
            this.Controls.Add(this.sequence1Box);
            this.Controls.Add(this.sequence2Box);
            this.Controls.Add(this.file2Box);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dataGridViewResults);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Gene Sequence Alignment";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //added four labels, four textboxes for displaying results for clicked cells, a checkbox and another textbox for doing banded processing, and a clear button
        private System.Windows.Forms.DataGridView dataGridViewResults;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusMessage;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton processButton;
        private System.Windows.Forms.ToolStripButton clearButton;
        private System.Windows.Forms.TextBox file1Box;
        private System.Windows.Forms.TextBox sequence1Box;
        private System.Windows.Forms.TextBox sequence2Box;
        private System.Windows.Forms.TextBox file2Box;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox bandCheckBox;
        private System.Windows.Forms.TextBox bandlengthBox;
        private System.Windows.Forms.Button getGridButton;
        private System.Windows.Forms.TextBox getGridTextbox;
    }
}

