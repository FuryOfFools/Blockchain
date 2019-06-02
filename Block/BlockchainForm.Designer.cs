namespace Program
{
    partial class BlockchainForm
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
            this.LoadBlockchain = new System.Windows.Forms.Button();
            this.AddDataInBlockchain = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Info = new System.Windows.Forms.TextBox();
            this.ShowAllBlocks = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // LoadBlockchain
            // 
            this.LoadBlockchain.Location = new System.Drawing.Point(12, 12);
            this.LoadBlockchain.Name = "LoadBlockchain";
            this.LoadBlockchain.Size = new System.Drawing.Size(109, 23);
            this.LoadBlockchain.TabIndex = 0;
            this.LoadBlockchain.Text = "Load blockchain";
            this.LoadBlockchain.UseVisualStyleBackColor = true;
            this.LoadBlockchain.Click += new System.EventHandler(this.LoadBlockchain_Click);
            // 
            // AddDataInBlockchain
            // 
            this.AddDataInBlockchain.Enabled = false;
            this.AddDataInBlockchain.Location = new System.Drawing.Point(12, 65);
            this.AddDataInBlockchain.Name = "AddDataInBlockchain";
            this.AddDataInBlockchain.Size = new System.Drawing.Size(109, 36);
            this.AddDataInBlockchain.TabIndex = 1;
            this.AddDataInBlockchain.Text = "Add data in blockchain";
            this.AddDataInBlockchain.UseVisualStyleBackColor = true;
            this.AddDataInBlockchain.Click += new System.EventHandler(this.AddDataInBlockchain_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(127, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Not loaded";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 5;
            // 
            // Info
            // 
            this.Info.Location = new System.Drawing.Point(12, 130);
            this.Info.Multiline = true;
            this.Info.Name = "Info";
            this.Info.Size = new System.Drawing.Size(553, 238);
            this.Info.TabIndex = 9;
            // 
            // ShowAllBlocks
            // 
            this.ShowAllBlocks.Location = new System.Drawing.Point(232, 101);
            this.ShowAllBlocks.Name = "ShowAllBlocks";
            this.ShowAllBlocks.Size = new System.Drawing.Size(105, 23);
            this.ShowAllBlocks.TabIndex = 10;
            this.ShowAllBlocks.Text = "Show all blocks";
            this.ShowAllBlocks.UseVisualStyleBackColor = true;
            this.ShowAllBlocks.Click += new System.EventHandler(this.ShowAllBlocks_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 42);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 17);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Default path";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // BlockchainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 384);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.ShowAllBlocks);
            this.Controls.Add(this.Info);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AddDataInBlockchain);
            this.Controls.Add(this.LoadBlockchain);
            this.Name = "BlockchainForm";
            this.Text = "BlockchainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BlockchainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadBlockchain;
        private System.Windows.Forms.Button AddDataInBlockchain;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Info;
        private System.Windows.Forms.Button ShowAllBlocks;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}