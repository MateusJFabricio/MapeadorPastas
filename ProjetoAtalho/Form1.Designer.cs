namespace ProjetoAtalho
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeView = new System.Windows.Forms.TreeView();
            this.btnMapear = new System.Windows.Forms.Button();
            this.btnCompilar = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnAtalho = new System.Windows.Forms.Button();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.btnAbrirPasta = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView.Location = new System.Drawing.Point(5, 106);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(363, 444);
            this.treeView.TabIndex = 0;
            // 
            // btnMapear
            // 
            this.btnMapear.Location = new System.Drawing.Point(5, 14);
            this.btnMapear.Name = "btnMapear";
            this.btnMapear.Size = new System.Drawing.Size(90, 40);
            this.btnMapear.TabIndex = 1;
            this.btnMapear.Text = "Mapear Pasta";
            this.btnMapear.UseVisualStyleBackColor = true;
            this.btnMapear.Click += new System.EventHandler(this.btnMapear_Click);
            // 
            // btnCompilar
            // 
            this.btnCompilar.Location = new System.Drawing.Point(278, 14);
            this.btnCompilar.Name = "btnCompilar";
            this.btnCompilar.Size = new System.Drawing.Size(90, 40);
            this.btnCompilar.TabIndex = 2;
            this.btnCompilar.Text = "Compilar";
            this.btnCompilar.UseVisualStyleBackColor = true;
            this.btnCompilar.Click += new System.EventHandler(this.btnCompilar_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnAtalho
            // 
            this.btnAtalho.Location = new System.Drawing.Point(187, 14);
            this.btnAtalho.Name = "btnAtalho";
            this.btnAtalho.Size = new System.Drawing.Size(90, 40);
            this.btnAtalho.TabIndex = 3;
            this.btnAtalho.Text = "Criar Atalho";
            this.btnAtalho.UseVisualStyleBackColor = true;
            this.btnAtalho.Click += new System.EventHandler(this.btnAtalho_Click);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Location = new System.Drawing.Point(96, 14);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(90, 40);
            this.btnAtualizar.TabIndex = 4;
            this.btnAtualizar.Text = "Atualizar";
            this.btnAtualizar.UseVisualStyleBackColor = true;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // btnAbrirPasta
            // 
            this.btnAbrirPasta.Location = new System.Drawing.Point(5, 60);
            this.btnAbrirPasta.Name = "btnAbrirPasta";
            this.btnAbrirPasta.Size = new System.Drawing.Size(90, 40);
            this.btnAbrirPasta.TabIndex = 5;
            this.btnAbrirPasta.Text = "Abrir Pasta";
            this.btnAbrirPasta.UseVisualStyleBackColor = true;
            this.btnAbrirPasta.Click += new System.EventHandler(this.btnAbrirPasta_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 596);
            this.Controls.Add(this.btnAbrirPasta);
            this.Controls.Add(this.btnAtualizar);
            this.Controls.Add(this.btnAtalho);
            this.Controls.Add(this.btnCompilar);
            this.Controls.Add(this.btnMapear);
            this.Controls.Add(this.treeView);
            this.Name = "Form1";
            this.Text = "Programa doido";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Button btnMapear;
        private System.Windows.Forms.Button btnCompilar;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnAtalho;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.Button btnAbrirPasta;
    }
}

