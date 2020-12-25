
namespace TestingCombinatoriale
{
    partial class TestingCombinatorio
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.openFileDialogClass = new System.Windows.Forms.OpenFileDialog();
            this.cbMetodi = new System.Windows.Forms.ComboBox();
            this.lblMetodi = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.caricaClasseJavaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnProcedi = new System.Windows.Forms.Button();
            this.lblCTWedge = new System.Windows.Forms.Label();
            this.txtK = new System.Windows.Forms.TextBox();
            this.lblKway = new System.Windows.Forms.Label();
            this.lblNomeClasse = new System.Windows.Forms.Label();
            this.lblSrc = new System.Windows.Forms.Label();
            this.txtSrc = new System.Windows.Forms.TextBox();
            this.txtBin = new System.Windows.Forms.TextBox();
            this.lblBin = new System.Windows.Forms.Label();
            this.folderBrowserDialogSrc = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialogBin = new System.Windows.Forms.FolderBrowserDialog();
            this.cbParallelismo = new System.Windows.Forms.ComboBox();
            this.lblParallelismo = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cbTimeout = new System.Windows.Forms.CheckBox();
            this.txtTimeout = new System.Windows.Forms.TextBox();
            this.cbSalta = new System.Windows.Forms.CheckBox();
            this.systemInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.progBar = new System.Windows.Forms.ProgressBar();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialogClass
            // 
            this.openFileDialogClass.FileName = "openFileDialog1";
            this.openFileDialogClass.Filter = "File di codice sorgente java (*.java) | *.java;";
            // 
            // cbMetodi
            // 
            this.cbMetodi.Enabled = false;
            this.cbMetodi.ForeColor = System.Drawing.Color.Black;
            this.cbMetodi.FormattingEnabled = true;
            this.cbMetodi.Location = new System.Drawing.Point(12, 91);
            this.cbMetodi.Name = "cbMetodi";
            this.cbMetodi.Size = new System.Drawing.Size(776, 24);
            this.cbMetodi.TabIndex = 0;
            this.cbMetodi.MouseHover += new System.EventHandler(this.cbMetodi_MouseHover);
            // 
            // lblMetodi
            // 
            this.lblMetodi.AutoSize = true;
            this.lblMetodi.ForeColor = System.Drawing.Color.Black;
            this.lblMetodi.Location = new System.Drawing.Point(12, 71);
            this.lblMetodi.Name = "lblMetodi";
            this.lblMetodi.Size = new System.Drawing.Size(88, 17);
            this.lblMetodi.TabIndex = 1;
            this.lblMetodi.Text = "Lista Metodi:";
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.infoToolStripMenuItem,
            this.systemInfoToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 28);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip1";
            this.menuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.caricaClasseJavaToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // caricaClasseJavaToolStripMenuItem
            // 
            this.caricaClasseJavaToolStripMenuItem.Name = "caricaClasseJavaToolStripMenuItem";
            this.caricaClasseJavaToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.caricaClasseJavaToolStripMenuItem.Text = "Carica Classe Java";
            this.caricaClasseJavaToolStripMenuItem.Click += new System.EventHandler(this.caricaClasseJavaToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // btnProcedi
            // 
            this.btnProcedi.ForeColor = System.Drawing.Color.Black;
            this.btnProcedi.Location = new System.Drawing.Point(15, 356);
            this.btnProcedi.Name = "btnProcedi";
            this.btnProcedi.Size = new System.Drawing.Size(75, 28);
            this.btnProcedi.TabIndex = 3;
            this.btnProcedi.Text = "Procedi";
            this.btnProcedi.UseVisualStyleBackColor = true;
            this.btnProcedi.Click += new System.EventHandler(this.btnProcedi_Click);
            this.btnProcedi.MouseHover += new System.EventHandler(this.btnProcedi_MouseHover);
            // 
            // lblCTWedge
            // 
            this.lblCTWedge.AutoSize = true;
            this.lblCTWedge.ForeColor = System.Drawing.Color.Black;
            this.lblCTWedge.Location = new System.Drawing.Point(12, 399);
            this.lblCTWedge.Name = "lblCTWedge";
            this.lblCTWedge.Size = new System.Drawing.Size(769, 17);
            this.lblCTWedge.TabIndex = 4;
            this.lblCTWedge.Text = "Per la generazione viene sfruttato CTWedge, quindi bisogna rispettare la sintassi" +
    " di tale strumento durante la procedura.";
            this.lblCTWedge.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtK
            // 
            this.txtK.ForeColor = System.Drawing.Color.Black;
            this.txtK.Location = new System.Drawing.Point(12, 145);
            this.txtK.Name = "txtK";
            this.txtK.Size = new System.Drawing.Size(140, 22);
            this.txtK.TabIndex = 6;
            this.txtK.MouseHover += new System.EventHandler(this.txtK_MouseHover);
            // 
            // lblKway
            // 
            this.lblKway.AutoSize = true;
            this.lblKway.ForeColor = System.Drawing.Color.Black;
            this.lblKway.Location = new System.Drawing.Point(12, 125);
            this.lblKway.Name = "lblKway";
            this.lblKway.Size = new System.Drawing.Size(140, 17);
            this.lblKway.TabIndex = 7;
            this.lblKway.Text = "Inserire il valore di K:";
            // 
            // lblNomeClasse
            // 
            this.lblNomeClasse.AutoSize = true;
            this.lblNomeClasse.Location = new System.Drawing.Point(14, 38);
            this.lblNomeClasse.Name = "lblNomeClasse";
            this.lblNomeClasse.Size = new System.Drawing.Size(0, 17);
            this.lblNomeClasse.TabIndex = 8;
            // 
            // lblSrc
            // 
            this.lblSrc.AutoSize = true;
            this.lblSrc.ForeColor = System.Drawing.Color.Black;
            this.lblSrc.Location = new System.Drawing.Point(12, 183);
            this.lblSrc.Name = "lblSrc";
            this.lblSrc.Size = new System.Drawing.Size(423, 17);
            this.lblSrc.TabIndex = 11;
            this.lblSrc.Text = "Inserisci il percorso della cartella contenente i sorgenti (files .java)";
            // 
            // txtSrc
            // 
            this.txtSrc.ForeColor = System.Drawing.Color.Black;
            this.txtSrc.Location = new System.Drawing.Point(12, 203);
            this.txtSrc.Name = "txtSrc";
            this.txtSrc.Size = new System.Drawing.Size(776, 22);
            this.txtSrc.TabIndex = 12;
            this.txtSrc.Click += new System.EventHandler(this.txtSrc_Click);
            this.txtSrc.TextChanged += new System.EventHandler(this.txtSrc_TextChanged);
            // 
            // txtBin
            // 
            this.txtBin.ForeColor = System.Drawing.Color.Black;
            this.txtBin.Location = new System.Drawing.Point(12, 255);
            this.txtBin.Name = "txtBin";
            this.txtBin.Size = new System.Drawing.Size(776, 22);
            this.txtBin.TabIndex = 14;
            this.txtBin.Visible = false;
            this.txtBin.Click += new System.EventHandler(this.txtBin_Click);
            this.txtBin.TextChanged += new System.EventHandler(this.txtBin_TextChanged);
            // 
            // lblBin
            // 
            this.lblBin.AutoSize = true;
            this.lblBin.ForeColor = System.Drawing.Color.Black;
            this.lblBin.Location = new System.Drawing.Point(12, 235);
            this.lblBin.Name = "lblBin";
            this.lblBin.Size = new System.Drawing.Size(552, 17);
            this.lblBin.TabIndex = 13;
            this.lblBin.Text = "Inserisci il percorso della cartella che conterrà i risultati della compilazione " +
    "(files .class)";
            this.lblBin.Visible = false;
            this.lblBin.Click += new System.EventHandler(this.lblBin_Click);
            // 
            // cbParallelismo
            // 
            this.cbParallelismo.ForeColor = System.Drawing.Color.Black;
            this.cbParallelismo.FormattingEnabled = true;
            this.cbParallelismo.Location = new System.Drawing.Point(12, 313);
            this.cbParallelismo.Name = "cbParallelismo";
            this.cbParallelismo.Size = new System.Drawing.Size(121, 24);
            this.cbParallelismo.TabIndex = 15;
            this.cbParallelismo.SelectedIndexChanged += new System.EventHandler(this.cbParallelismo_SelectedIndexChanged);
            this.cbParallelismo.MouseHover += new System.EventHandler(this.cbParallelismo_MouseHover);
            // 
            // lblParallelismo
            // 
            this.lblParallelismo.AutoSize = true;
            this.lblParallelismo.ForeColor = System.Drawing.Color.Black;
            this.lblParallelismo.Location = new System.Drawing.Point(12, 293);
            this.lblParallelismo.Name = "lblParallelismo";
            this.lblParallelismo.Size = new System.Drawing.Size(268, 17);
            this.lblParallelismo.TabIndex = 16;
            this.lblParallelismo.Text = "Numero di threads per la minimizzazione:";
            this.lblParallelismo.Click += new System.EventHandler(this.lblParallelismo_Click);
            // 
            // cbTimeout
            // 
            this.cbTimeout.AutoSize = true;
            this.cbTimeout.Location = new System.Drawing.Point(350, 293);
            this.cbTimeout.Name = "cbTimeout";
            this.cbTimeout.Size = new System.Drawing.Size(450, 21);
            this.cbTimeout.TabIndex = 17;
            this.cbTimeout.Text = "Attiva timeout nell\'esecuzione dei singoli test (espresso in secondi)";
            this.cbTimeout.UseVisualStyleBackColor = true;
            this.cbTimeout.CheckedChanged += new System.EventHandler(this.cbTimeout_CheckedChanged);
            // 
            // txtTimeout
            // 
            this.txtTimeout.Enabled = false;
            this.txtTimeout.Location = new System.Drawing.Point(350, 315);
            this.txtTimeout.Name = "txtTimeout";
            this.txtTimeout.Size = new System.Drawing.Size(310, 22);
            this.txtTimeout.TabIndex = 18;
            this.txtTimeout.TextChanged += new System.EventHandler(this.txtTimeout_TextChanged);
            // 
            // cbSalta
            // 
            this.cbSalta.AutoSize = true;
            this.cbSalta.Location = new System.Drawing.Point(350, 363);
            this.cbSalta.Name = "cbSalta";
            this.cbSalta.Size = new System.Drawing.Size(290, 21);
            this.cbSalta.TabIndex = 19;
            this.cbSalta.Text = "Salta la fase di generazione dei test case";
            this.cbSalta.UseVisualStyleBackColor = true;
            this.cbSalta.CheckedChanged += new System.EventHandler(this.cbSalta_CheckedChanged);
            // 
            // systemInfoToolStripMenuItem
            // 
            this.systemInfoToolStripMenuItem.Name = "systemInfoToolStripMenuItem";
            this.systemInfoToolStripMenuItem.Size = new System.Drawing.Size(100, 24);
            this.systemInfoToolStripMenuItem.Text = "System Info";
            this.systemInfoToolStripMenuItem.Click += new System.EventHandler(this.systemInfoToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 435);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(774, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "Assicurarsi, mediante la voce di menù \"System Info\", che la versione di Java sia " +
    "maggiore o uguale alla versione di Javac.";
            // 
            // progBar
            // 
            this.progBar.Location = new System.Drawing.Point(16, 473);
            this.progBar.Maximum = 10000;
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(769, 51);
            this.progBar.TabIndex = 21;
            this.progBar.Visible = false;
            // 
            // TestingCombinatorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 538);
            this.Controls.Add(this.progBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbSalta);
            this.Controls.Add(this.txtTimeout);
            this.Controls.Add(this.cbTimeout);
            this.Controls.Add(this.lblParallelismo);
            this.Controls.Add(this.cbParallelismo);
            this.Controls.Add(this.txtBin);
            this.Controls.Add(this.lblBin);
            this.Controls.Add(this.txtSrc);
            this.Controls.Add(this.lblSrc);
            this.Controls.Add(this.lblNomeClasse);
            this.Controls.Add(this.lblKway);
            this.Controls.Add(this.txtK);
            this.Controls.Add(this.lblCTWedge);
            this.Controls.Add(this.btnProcedi);
            this.Controls.Add(this.lblMetodi);
            this.Controls.Add(this.cbMetodi);
            this.Controls.Add(this.menuStrip);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "TestingCombinatorio";
            this.Text = "Testing Combinatorio";
            this.Load += new System.EventHandler(this.TestingCombinatorio_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialogClass;
        private System.Windows.Forms.ComboBox cbMetodi;
        private System.Windows.Forms.Label lblMetodi;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem caricaClasseJavaToolStripMenuItem;
        private System.Windows.Forms.Button btnProcedi;
        private System.Windows.Forms.Label lblCTWedge;
        private System.Windows.Forms.TextBox txtK;
        private System.Windows.Forms.Label lblKway;
        private System.Windows.Forms.Label lblNomeClasse;
        private System.Windows.Forms.Label lblSrc;
        private System.Windows.Forms.TextBox txtSrc;
        private System.Windows.Forms.TextBox txtBin;
        private System.Windows.Forms.Label lblBin;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogSrc;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogBin;
        private System.Windows.Forms.ComboBox cbParallelismo;
        private System.Windows.Forms.Label lblParallelismo;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox cbTimeout;
        private System.Windows.Forms.TextBox txtTimeout;
        private System.Windows.Forms.CheckBox cbSalta;
        private System.Windows.Forms.ToolStripMenuItem systemInfoToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progBar;
    }
}

