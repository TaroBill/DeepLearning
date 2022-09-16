namespace TEST
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this._buttonSeeError = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(116, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "設定\r\n起點";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.StartToSetStartPoint);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(116, 63);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(48, 45);
            this.button2.TabIndex = 1;
            this.button2.Text = "設定\r\n終點";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.StartToSetEndPoint);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(116, 216);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(48, 45);
            this.button3.TabIndex = 2;
            this.button3.Text = "尋找\r\n路徑";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Find_Path);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(116, 318);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(48, 45);
            this.button4.TabIndex = 3;
            this.button4.Text = "停止尋路";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Pause);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(116, 114);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(48, 45);
            this.button5.TabIndex = 4;
            this.button5.Text = "生成\r\n牆壁";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.GenerateWallsRandomly);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(110, 542);
            this.richTextBox1.TabIndex = 5;
            this.richTextBox1.Text = "";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(116, 165);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(48, 45);
            this.button6.TabIndex = 6;
            this.button6.Text = "還原\r\n地圖";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.ClearMap);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(116, 369);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(48, 45);
            this.button7.TabIndex = 2;
            this.button7.Text = "儲存\r\n模型";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.SaveModel);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(116, 420);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(48, 45);
            this.button8.TabIndex = 3;
            this.button8.Text = "載入模型";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.LoadModel);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(116, 267);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(48, 45);
            this.button9.TabIndex = 3;
            this.button9.Text = "切換速度";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.ChangeSpeed);
            // 
            // _buttonSeeError
            // 
            this._buttonSeeError.Location = new System.Drawing.Point(116, 471);
            this._buttonSeeError.Name = "_buttonSeeError";
            this._buttonSeeError.Size = new System.Drawing.Size(48, 45);
            this._buttonSeeError.TabIndex = 3;
            this._buttonSeeError.Text = "查看誤差";
            this._buttonSeeError.UseVisualStyleBackColor = true;
            this._buttonSeeError.Click += new System.EventHandler(this.OpenErrorChart);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(668, 542);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this._buttonSeeError);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloseForm);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button _buttonSeeError;
    }
}

