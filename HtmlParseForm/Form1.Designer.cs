namespace HtmlParseForm
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_Url = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.txt_xPath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ck_unicode = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txt_lastID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_Url
            // 
            this.txt_Url.Location = new System.Drawing.Point(53, 12);
            this.txt_Url.Name = "txt_Url";
            this.txt_Url.Size = new System.Drawing.Size(307, 22);
            this.txt_Url.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "Url : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "xPath : ";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(8, 96);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(597, 248);
            this.textBox2.TabIndex = 11;
            // 
            // txt_xPath
            // 
            this.txt_xPath.Location = new System.Drawing.Point(53, 40);
            this.txt_xPath.Name = "txt_xPath";
            this.txt_xPath.Size = new System.Drawing.Size(307, 22);
            this.txt_xPath.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "查詢";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn_query_Click);
            // 
            // ck_unicode
            // 
            this.ck_unicode.AutoSize = true;
            this.ck_unicode.Checked = true;
            this.ck_unicode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_unicode.Location = new System.Drawing.Point(90, 79);
            this.ck_unicode.Name = "ck_unicode";
            this.ck_unicode.Size = new System.Drawing.Size(79, 16);
            this.ck_unicode.TabIndex = 15;
            this.ck_unicode.Text = "啟用UTF-8";
            this.ck_unicode.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(863, 11);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Reddit 爬列表";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txt_lastID
            // 
            this.txt_lastID.Location = new System.Drawing.Point(520, 73);
            this.txt_lastID.Name = "txt_lastID";
            this.txt_lastID.Size = new System.Drawing.Size(79, 22);
            this.txt_lastID.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(474, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "接關 : ";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(863, 41);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 23);
            this.button3.TabIndex = 19;
            this.button3.Text = "Reddit 爬內頁";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(863, 71);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(87, 23);
            this.button4.TabIndex = 20;
            this.button4.Text = "Reddit 爬回覆";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(863, 101);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(87, 27);
            this.button5.TabIndex = 21;
            this.button5.Text = "Reddit 匯出";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Excel檔案|*.xlsx";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(633, 10);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(109, 24);
            this.button6.TabIndex = 22;
            this.button6.Text = "formosa 爬列表";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(633, 40);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(109, 24);
            this.button7.TabIndex = 23;
            this.button7.Text = "formosa 爬內頁";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(633, 71);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(109, 24);
            this.button8.TabIndex = 24;
            this.button8.Text = "formosa 爬回復";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(633, 101);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(109, 27);
            this.button9.TabIndex = 25;
            this.button9.Text = "匯出 formosa";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(748, 101);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(109, 27);
            this.button10.TabIndex = 29;
            this.button10.Text = "晨 匯出";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(748, 71);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(109, 24);
            this.button11.TabIndex = 28;
            this.button11.Text = "晨  爬回復";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(748, 40);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(109, 24);
            this.button12.TabIndex = 27;
            this.button12.Text = "晨 爬內頁";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(748, 10);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(109, 24);
            this.button13.TabIndex = 26;
            this.button13.Text = "晨 爬列表";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(748, 134);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(109, 24);
            this.button14.TabIndex = 30;
            this.button14.Text = "晨 作者url";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(748, 164);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(109, 24);
            this.button15.TabIndex = 31;
            this.button15.Text = "晨 作者profile";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 374);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_lastID);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ck_unicode);
            this.Controls.Add(this.txt_Url);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.txt_xPath);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Html解析器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Url;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox txt_xPath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox ck_unicode;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txt_lastID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
    }
}

