namespace MaciLaci_WinForms
{
    partial class GameForm
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
            checkBox1 = new CheckBox();
            comboBox1 = new ComboBox();
            newgame = new Button();
            pause = new Button();
            label1 = new Label();
            label2 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            SuspendLayout();
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Segoe UI", 12F);
            checkBox1.Location = new Point(891, 104);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(141, 25);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "use tested maps";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "11x11", "15x15", "20x20" });
            comboBox1.Location = new Point(891, 162);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(86, 23);
            comboBox1.TabIndex = 5;
            // 
            // newgame
            // 
            newgame.Location = new Point(891, 227);
            newgame.Name = "newgame";
            newgame.Size = new Size(86, 39);
            newgame.TabIndex = 6;
            newgame.Text = "New Game";
            newgame.UseVisualStyleBackColor = true;
            newgame.Click += newgame_Click;
            // 
            // pause
            // 
            pause.Location = new Point(891, 311);
            pause.Name = "pause";
            pause.Size = new Size(86, 39);
            pause.TabIndex = 7;
            pause.Text = "Pause";
            pause.UseVisualStyleBackColor = true;
            pause.Click += pause_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F);
            label1.Location = new Point(891, 386);
            label1.Name = "label1";
            label1.Size = new Size(110, 28);
            label1.TabIndex = 8;
            label1.Text = "Eltelt Idő: 0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15F);
            label2.Location = new Point(790, 462);
            label2.Name = "label2";
            label2.Size = new Size(211, 28);
            label2.TabIndex = 9;
            label2.Text = "Megszerzett kosarak: 0";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Location = new Point(53, 23);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(686, 686);
            tableLayoutPanel1.TabIndex = 10;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1070, 818);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pause);
            Controls.Add(newgame);
            Controls.Add(comboBox1);
            Controls.Add(checkBox1);
            KeyPreview = true;
            Name = "GameForm";
            Text = "MaciLaci";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox checkBox1;
        private ComboBox comboBox1;
        private Button newgame;
        private Button pause;
        private Label label1;
        private Label label2;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
