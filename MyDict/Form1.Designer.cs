namespace MyDict
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
            this.definitionBox = new System.Windows.Forms.RichTextBox();
            this.wordBox = new System.Windows.Forms.TextBox();
            this.updateButton = new System.Windows.Forms.Button();
            this.reverseButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.searchButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.instructionBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();

            // 
            // wordBox
            // 
            this.wordBox.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.wordBox.Location = new System.Drawing.Point(19, 354);
            this.wordBox.Name = "wordBox";
            this.wordBox.Size = new System.Drawing.Size(558, 30);
            this.wordBox.TabIndex = 0;
            // 
            // definitionBox
            // 
            this.definitionBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(56)))), ((int)(((byte)(60)))));
            this.definitionBox.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.definitionBox.ForeColor = System.Drawing.Color.White;
            this.definitionBox.Location = new System.Drawing.Point(19, 26);
            this.definitionBox.Name = "definitionBox";
            this.definitionBox.Size = new System.Drawing.Size(663, 313);
            this.definitionBox.TabIndex = 1;
            this.definitionBox.Text = "";
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(125, 399);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(83, 27);
            this.updateButton.TabIndex = 2;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Visible = false;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // reverseButton
            // 
            this.reverseButton.Location = new System.Drawing.Point(243, 400);
            this.reverseButton.Name = "reverseButton";
            this.reverseButton.Size = new System.Drawing.Size(80, 26);
            this.reverseButton.TabIndex = 3;
            this.reverseButton.Text = "Reverse";
            this.reverseButton.UseVisualStyleBackColor = true;
            this.reverseButton.Visible = false;
            this.reverseButton.Click += new System.EventHandler(this.reverseButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(364, 399);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(95, 26);
            this.addButton.TabIndex = 4;
            this.addButton.Text = "Add";
            this.addButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Visible = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(489, 399);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(86, 25);
            this.searchButton.TabIndex = 5;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Visible = false;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(19, 400);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(79, 26);
            this.deleteButton.TabIndex = 6;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Visible = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(647, 373);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(91, 27);
            this.clearButton.TabIndex = 7;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Visible = false;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // instructionBox
            // 
            this.instructionBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.instructionBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.instructionBox.Location = new System.Drawing.Point(715, 38);
            this.instructionBox.Name = "instructionBox";
            this.instructionBox.Size = new System.Drawing.Size(249, 275);
            this.instructionBox.TabIndex = 8;
            this.instructionBox.Text = "";
            
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(991, 450);
            this.Controls.Add(this.instructionBox);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.reverseButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.wordBox);
            this.Controls.Add(this.definitionBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox definitionBox;
        private System.Windows.Forms.TextBox wordBox;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Button reverseButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.RichTextBox instructionBox;
    }
}

