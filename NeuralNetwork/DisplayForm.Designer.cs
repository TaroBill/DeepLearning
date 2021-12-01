
namespace NeuralNetwork
{
    partial class DisplayForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
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
            this._trainButton = new System.Windows.Forms.Button();
            this._inputComboBox = new System.Windows.Forms.ComboBox();
            this._inputLabel = new System.Windows.Forms.Label();
            this._resultLabel = new System.Windows.Forms.Label();
            this._trainTimesTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._printWeightButton = new System.Windows.Forms.Button();
            this._layerLabel = new System.Windows.Forms.Label();
            this._layerIndexTextBox = new System.Windows.Forms.TextBox();
            this._inputTextBox1 = new System.Windows.Forms.TextBox();
            this._inputTextBox2 = new System.Windows.Forms.TextBox();
            this._ResultTextBox = new System.Windows.Forms.TextBox();
            this._addTrainDataButton = new System.Windows.Forms.Button();
            this._inputsTrainDataLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._expectOutputLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _trainButton
            // 
            this._trainButton.Location = new System.Drawing.Point(307, 267);
            this._trainButton.Name = "_trainButton";
            this._trainButton.Size = new System.Drawing.Size(135, 48);
            this._trainButton.TabIndex = 0;
            this._trainButton.Text = "開始訓練";
            this._trainButton.UseVisualStyleBackColor = true;
            this._trainButton.Click += new System.EventHandler(this.ClickTrainButton);
            // 
            // _inputComboBox
            // 
            this._inputComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._inputComboBox.FormattingEnabled = true;
            this._inputComboBox.Items.AddRange(new object[] {
            "0 0",
            "0 1",
            "1 0",
            "1 1"});
            this._inputComboBox.Location = new System.Drawing.Point(143, 135);
            this._inputComboBox.Name = "_inputComboBox";
            this._inputComboBox.Size = new System.Drawing.Size(121, 20);
            this._inputComboBox.TabIndex = 1;
            this._inputComboBox.SelectedIndexChanged += new System.EventHandler(this.SelectedIndexChangedInputComboBox);
            // 
            // _inputLabel
            // 
            this._inputLabel.AutoSize = true;
            this._inputLabel.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._inputLabel.Location = new System.Drawing.Point(141, 103);
            this._inputLabel.Name = "_inputLabel";
            this._inputLabel.Size = new System.Drawing.Size(47, 19);
            this._inputLabel.TabIndex = 2;
            this._inputLabel.Text = "輸入";
            // 
            // _resultLabel
            // 
            this._resultLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._resultLabel.AutoSize = true;
            this._resultLabel.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._resultLabel.Location = new System.Drawing.Point(659, 125);
            this._resultLabel.Name = "_resultLabel";
            this._resultLabel.Size = new System.Drawing.Size(0, 27);
            this._resultLabel.TabIndex = 3;
            // 
            // _trainTimesTextBox
            // 
            this._trainTimesTextBox.Location = new System.Drawing.Point(143, 293);
            this._trainTimesTextBox.Name = "_trainTimesTextBox";
            this._trainTimesTextBox.Size = new System.Drawing.Size(100, 22);
            this._trainTimesTextBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(141, 267);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "訓練次數";
            // 
            // _printWeightButton
            // 
            this._printWeightButton.Location = new System.Drawing.Point(307, 356);
            this._printWeightButton.Name = "_printWeightButton";
            this._printWeightButton.Size = new System.Drawing.Size(135, 49);
            this._printWeightButton.TabIndex = 6;
            this._printWeightButton.Text = "輸出權重";
            this._printWeightButton.UseVisualStyleBackColor = true;
            // 
            // _layerLabel
            // 
            this._layerLabel.AutoSize = true;
            this._layerLabel.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._layerLabel.Location = new System.Drawing.Point(141, 357);
            this._layerLabel.Name = "_layerLabel";
            this._layerLabel.Size = new System.Drawing.Size(102, 19);
            this._layerLabel.TabIndex = 8;
            this._layerLabel.Text = "第N層Layer";
            // 
            // _layerIndexTextBox
            // 
            this._layerIndexTextBox.Location = new System.Drawing.Point(143, 383);
            this._layerIndexTextBox.Name = "_layerIndexTextBox";
            this._layerIndexTextBox.Size = new System.Drawing.Size(100, 22);
            this._layerIndexTextBox.TabIndex = 7;
            // 
            // _inputTextBox1
            // 
            this._inputTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._inputTextBox1.Location = new System.Drawing.Point(514, 354);
            this._inputTextBox1.Name = "_inputTextBox1";
            this._inputTextBox1.Size = new System.Drawing.Size(111, 22);
            this._inputTextBox1.TabIndex = 9;
            // 
            // _inputTextBox2
            // 
            this._inputTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._inputTextBox2.Location = new System.Drawing.Point(514, 382);
            this._inputTextBox2.Name = "_inputTextBox2";
            this._inputTextBox2.Size = new System.Drawing.Size(111, 22);
            this._inputTextBox2.TabIndex = 9;
            // 
            // _ResultTextBox
            // 
            this._ResultTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._ResultTextBox.Location = new System.Drawing.Point(664, 354);
            this._ResultTextBox.Name = "_ResultTextBox";
            this._ResultTextBox.Size = new System.Drawing.Size(119, 22);
            this._ResultTextBox.TabIndex = 9;
            // 
            // _addTrainDataButton
            // 
            this._addTrainDataButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._addTrainDataButton.Location = new System.Drawing.Point(664, 382);
            this._addTrainDataButton.Name = "_addTrainDataButton";
            this._addTrainDataButton.Size = new System.Drawing.Size(119, 23);
            this._addTrainDataButton.TabIndex = 10;
            this._addTrainDataButton.Text = "加入訓練資料按鈕";
            this._addTrainDataButton.UseVisualStyleBackColor = true;
            this._addTrainDataButton.Click += new System.EventHandler(this.ClickAddTrainDataButton);
            // 
            // _inputsTrainDataLabel
            // 
            this._inputsTrainDataLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._inputsTrainDataLabel.AutoSize = true;
            this._inputsTrainDataLabel.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._inputsTrainDataLabel.Location = new System.Drawing.Point(510, 312);
            this._inputsTrainDataLabel.Name = "_inputsTrainDataLabel";
            this._inputsTrainDataLabel.Size = new System.Drawing.Size(142, 19);
            this._inputsTrainDataLabel.TabIndex = 5;
            this._inputsTrainDataLabel.Text = "加入訓練資料集";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(660, 312);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "加入輸出結果";
            // 
            // _expectOutputLabel
            // 
            this._expectOutputLabel.AutoSize = true;
            this._expectOutputLabel.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._expectOutputLabel.Location = new System.Drawing.Point(369, 126);
            this._expectOutputLabel.Name = "_expectOutputLabel";
            this._expectOutputLabel.Size = new System.Drawing.Size(0, 27);
            this._expectOutputLabel.TabIndex = 11;
            // 
            // DisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 485);
            this.Controls.Add(this._expectOutputLabel);
            this.Controls.Add(this._addTrainDataButton);
            this.Controls.Add(this._ResultTextBox);
            this.Controls.Add(this._inputTextBox2);
            this.Controls.Add(this._inputTextBox1);
            this.Controls.Add(this._layerLabel);
            this.Controls.Add(this._layerIndexTextBox);
            this.Controls.Add(this._printWeightButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._inputsTrainDataLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._trainTimesTextBox);
            this.Controls.Add(this._resultLabel);
            this.Controls.Add(this._inputLabel);
            this.Controls.Add(this._inputComboBox);
            this.Controls.Add(this._trainButton);
            this.Name = "DisplayForm";
            this.Text = "神經網路";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _trainButton;
        private System.Windows.Forms.ComboBox _inputComboBox;
        private System.Windows.Forms.Label _inputLabel;
        private System.Windows.Forms.Label _resultLabel;
        private System.Windows.Forms.TextBox _trainTimesTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _printWeightButton;
        private System.Windows.Forms.Label _layerLabel;
        private System.Windows.Forms.TextBox _layerIndexTextBox;
        private System.Windows.Forms.TextBox _inputTextBox1;
        private System.Windows.Forms.TextBox _inputTextBox2;
        private System.Windows.Forms.TextBox _ResultTextBox;
        private System.Windows.Forms.Button _addTrainDataButton;
        private System.Windows.Forms.Label _inputsTrainDataLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label _expectOutputLabel;
    }
}

