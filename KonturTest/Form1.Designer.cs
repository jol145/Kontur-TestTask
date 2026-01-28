namespace KonturTest
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
            button1 = new Button();
            richTextBox1 = new RichTextBox();
            txtName = new TextBox();
            txtSurname = new TextBox();
            txtAmount = new TextBox();
            txtMonth = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            btnAdd = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = SystemColors.InactiveCaption;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button1.Location = new Point(30, 50);
            button1.Name = "button1";
            button1.Size = new Size(136, 59);
            button1.TabIndex = 0;
            button1.Text = "Рассчитать зарплату";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Font = new Font("Courier New", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            richTextBox1.Location = new Point(172, 50);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(418, 273);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // txtName
            // 
            txtName.Location = new Point(172, 370);
            txtName.Name = "txtName";
            txtName.Size = new Size(100, 23);
            txtName.TabIndex = 3;
            // 
            // txtSurname
            // 
            txtSurname.Location = new Point(278, 370);
            txtSurname.Name = "txtSurname";
            txtSurname.Size = new Size(100, 23);
            txtSurname.TabIndex = 4;
            // 
            // txtAmount
            // 
            txtAmount.Location = new Point(384, 370);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(100, 23);
            txtAmount.TabIndex = 5;
            // 
            // txtMonth
            // 
            txtMonth.Location = new Point(490, 370);
            txtMonth.Name = "txtMonth";
            txtMonth.Size = new Size(100, 23);
            txtMonth.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(172, 352);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 7;
            label1.Text = "Имя";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(278, 352);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 8;
            label2.Text = "Фамилия";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(384, 352);
            label3.Name = "label3";
            label3.Size = new Size(45, 15);
            label3.TabIndex = 9;
            label3.Text = "Сумма";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(490, 352);
            label4.Name = "label4";
            label4.Size = new Size(43, 15);
            label4.TabIndex = 10;
            label4.Text = "Месяц";
            // 
            // btnAdd
            // 
            btnAdd.BackColor = SystemColors.ControlDarkDark;
            btnAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAdd.ForeColor = SystemColors.ButtonHighlight;
            btnAdd.Location = new Point(30, 334);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(136, 59);
            btnAdd.TabIndex = 11;
            btnAdd.Text = "Добавить сотрудника";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 405);
            Controls.Add(btnAdd);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtMonth);
            Controls.Add(txtAmount);
            Controls.Add(txtSurname);
            Controls.Add(txtName);
            Controls.Add(richTextBox1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Тестовое задание Контур";
            Load += Form1_Load_1;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private RichTextBox richTextBox1;
        private TextBox txtName;
        private TextBox txtSurname;
        private TextBox txtAmount;
        private TextBox txtMonth;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button btnAdd;
    }
}
