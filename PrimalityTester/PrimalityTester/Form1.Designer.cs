namespace PrimalityTester
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.test_button = new System.Windows.Forms.Button();
            this.result_textbox = new System.Windows.Forms.TextBox();
            this.number_of_tests_textbox = new System.Windows.Forms.TextBox();
            this.candidate_number_textbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Candidate Number:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.test_button);
            this.panel1.Controls.Add(this.result_textbox);
            this.panel1.Controls.Add(this.number_of_tests_textbox);
            this.panel1.Controls.Add(this.candidate_number_textbox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(379, 321);
            this.panel1.TabIndex = 1;
            // 
            // test_button
            // 
            this.test_button.Location = new System.Drawing.Point(139, 215);
            this.test_button.Margin = new System.Windows.Forms.Padding(4);
            this.test_button.Name = "test_button";
            this.test_button.Size = new System.Drawing.Size(100, 28);
            this.test_button.TabIndex = 6;
            this.test_button.Text = "Test";
            this.test_button.UseVisualStyleBackColor = true;
            this.test_button.Click += new System.EventHandler(this.test_button_Click);
            // 
            // result_textbox
            // 
            this.result_textbox.Location = new System.Drawing.Point(97, 135);
            this.result_textbox.Margin = new System.Windows.Forms.Padding(4);
            this.result_textbox.MaximumSize = new System.Drawing.Size(1000, 1000);
            this.result_textbox.Multiline = true;
            this.result_textbox.Name = "result_textbox";
            this.result_textbox.Size = new System.Drawing.Size(211, 72);
            this.result_textbox.TabIndex = 5;
            // 
            // number_of_tests_textbox
            // 
            this.number_of_tests_textbox.Location = new System.Drawing.Point(156, 79);
            this.number_of_tests_textbox.Margin = new System.Windows.Forms.Padding(4);
            this.number_of_tests_textbox.Name = "number_of_tests_textbox";
            this.number_of_tests_textbox.Size = new System.Drawing.Size(152, 22);
            this.number_of_tests_textbox.TabIndex = 4;
            // 
            // candidate_number_textbox
            // 
            this.candidate_number_textbox.Location = new System.Drawing.Point(176, 32);
            this.candidate_number_textbox.Margin = new System.Windows.Forms.Padding(4);
            this.candidate_number_textbox.Name = "candidate_number_textbox";
            this.candidate_number_textbox.Size = new System.Drawing.Size(132, 22);
            this.candidate_number_textbox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 135);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Result:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 79);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Number of tests:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 321);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button test_button;
        private System.Windows.Forms.TextBox result_textbox;
        private System.Windows.Forms.TextBox number_of_tests_textbox;
        private System.Windows.Forms.TextBox candidate_number_textbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}

