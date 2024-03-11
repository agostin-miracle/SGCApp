
namespace SCGApp
{
    partial class frmPrincipal
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.fcliente = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tvwdata = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_lsterros = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.print_table = new System.Windows.Forms.Button();
            this.btn_dic = new System.Windows.Forms.Button();
            this.btn_csharp = new System.Windows.Forms.Button();
            this.btn_proc = new System.Windows.Forms.Button();
            this.btn_printschema = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.fcliente);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(841, 57);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Cliente";
            // 
            // fcliente
            // 
            this.fcliente.FormattingEnabled = true;
            this.fcliente.Items.AddRange(new object[] {
            "FarmGoats",
            "InterLivre"});
            this.fcliente.Location = new System.Drawing.Point(17, 20);
            this.fcliente.Margin = new System.Windows.Forms.Padding(4);
            this.fcliente.Name = "fcliente";
            this.fcliente.Size = new System.Drawing.Size(769, 24);
            this.fcliente.TabIndex = 0;
            this.fcliente.SelectedIndexChanged += new System.EventHandler(this.fcliente_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tvwdata);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 57);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(601, 658);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Objetos";
            // 
            // tvwdata
            // 
            this.tvwdata.CheckBoxes = true;
            this.tvwdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwdata.Location = new System.Drawing.Point(3, 17);
            this.tvwdata.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tvwdata.Name = "tvwdata";
            this.tvwdata.Size = new System.Drawing.Size(595, 639);
            this.tvwdata.TabIndex = 0;
            this.tvwdata.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvwdata_AfterCheck);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_lsterros);
            this.groupBox2.Controls.Add(this.btn_close);
            this.groupBox2.Controls.Add(this.print_table);
            this.groupBox2.Controls.Add(this.btn_dic);
            this.groupBox2.Controls.Add(this.btn_csharp);
            this.groupBox2.Controls.Add(this.btn_proc);
            this.groupBox2.Controls.Add(this.btn_printschema);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(606, 57);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(235, 658);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opções";
            // 
            // btn_lsterros
            // 
            this.btn_lsterros.Image = global::SCGApp.Properties.Resources.Loop;
            this.btn_lsterros.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_lsterros.Location = new System.Drawing.Point(5, 401);
            this.btn_lsterros.Name = "btn_lsterros";
            this.btn_lsterros.Size = new System.Drawing.Size(200, 34);
            this.btn_lsterros.TabIndex = 21;
            this.btn_lsterros.Text = "Catálogo de Erros";
            this.btn_lsterros.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_lsterros.UseVisualStyleBackColor = true;
            this.btn_lsterros.Click += new System.EventHandler(this.btn_lsterros_Click);
            // 
            // btn_close
            // 
            this.btn_close.Image = global::SCGApp.Properties.Resources.Go_Out;
            this.btn_close.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_close.Location = new System.Drawing.Point(7, 478);
            this.btn_close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(200, 34);
            this.btn_close.TabIndex = 0;
            this.btn_close.Text = "Sai&r";
            this.btn_close.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // print_table
            // 
            this.print_table.Image = global::SCGApp.Properties.Resources.Dots;
            this.print_table.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.print_table.Location = new System.Drawing.Point(5, 281);
            this.print_table.Margin = new System.Windows.Forms.Padding(4);
            this.print_table.Name = "print_table";
            this.print_table.Size = new System.Drawing.Size(200, 34);
            this.print_table.TabIndex = 20;
            this.print_table.Text = "Tabelas";
            this.print_table.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.print_table.UseVisualStyleBackColor = true;
            this.print_table.Click += new System.EventHandler(this.print_table_Click);
            // 
            // btn_dic
            // 
            this.btn_dic.Image = global::SCGApp.Properties.Resources.Write3;
            this.btn_dic.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_dic.Location = new System.Drawing.Point(5, 209);
            this.btn_dic.Margin = new System.Windows.Forms.Padding(4);
            this.btn_dic.Name = "btn_dic";
            this.btn_dic.Size = new System.Drawing.Size(200, 34);
            this.btn_dic.TabIndex = 19;
            this.btn_dic.Text = "Dicionário de Dados";
            this.btn_dic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_dic.UseVisualStyleBackColor = true;
            this.btn_dic.Click += new System.EventHandler(this.btn_dic_Click);
            // 
            // btn_csharp
            // 
            this.btn_csharp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_csharp.Image = global::SCGApp.Properties.Resources.Wizard;
            this.btn_csharp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_csharp.Location = new System.Drawing.Point(5, 84);
            this.btn_csharp.Margin = new System.Windows.Forms.Padding(4);
            this.btn_csharp.Name = "btn_csharp";
            this.btn_csharp.Size = new System.Drawing.Size(200, 34);
            this.btn_csharp.TabIndex = 18;
            this.btn_csharp.Text = "Gerar C# Code";
            this.btn_csharp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_csharp.UseVisualStyleBackColor = true;
            this.btn_csharp.Click += new System.EventHandler(this.btn_csharp_Click);
            // 
            // btn_proc
            // 
            this.btn_proc.Image = global::SCGApp.Properties.Resources.Database;
            this.btn_proc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_proc.Location = new System.Drawing.Point(7, 22);
            this.btn_proc.Margin = new System.Windows.Forms.Padding(4);
            this.btn_proc.Name = "btn_proc";
            this.btn_proc.Size = new System.Drawing.Size(200, 34);
            this.btn_proc.TabIndex = 17;
            this.btn_proc.Text = "Gerar Procedures";
            this.btn_proc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_proc.UseVisualStyleBackColor = true;
            this.btn_proc.Click += new System.EventHandler(this.btn_proc_Click);
            // 
            // btn_printschema
            // 
            this.btn_printschema.Image = global::SCGApp.Properties.Resources.Dots_Up;
            this.btn_printschema.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_printschema.Location = new System.Drawing.Point(5, 244);
            this.btn_printschema.Margin = new System.Windows.Forms.Padding(4);
            this.btn_printschema.Name = "btn_printschema";
            this.btn_printschema.Size = new System.Drawing.Size(200, 34);
            this.btn_printschema.TabIndex = 16;
            this.btn_printschema.Text = "Procedures";
            this.btn_printschema.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_printschema.UseVisualStyleBackColor = true;
            this.btn_printschema.Click += new System.EventHandler(this.btn_printschema_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 715);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SGC-App";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox fcliente;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button print_table;
        private System.Windows.Forms.Button btn_dic;
        private System.Windows.Forms.Button btn_csharp;
        private System.Windows.Forms.Button btn_proc;
        private System.Windows.Forms.Button btn_printschema;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.TreeView tvwdata;
        private System.Windows.Forms.Button btn_lsterros;
    }
}

