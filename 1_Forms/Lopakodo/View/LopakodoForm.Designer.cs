namespace Lopakodo.View
{
    partial class LopakodoForm
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
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this._fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._newGameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._basementMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._showersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._schoolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._saveGameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._loadGameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._quitGameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this._saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this._menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuStrip
            // 
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._fileMenuItem});
            this._menuStrip.Location = new System.Drawing.Point(0, 0);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.Size = new System.Drawing.Size(464, 24);
            this._menuStrip.TabIndex = 0;
            this._menuStrip.Text = "_menuStrip";
            // 
            // _fileMenuItem
            // 
            this._fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._newGameMenuItem,
            this.toolStripSeparator1,
            this._saveGameMenuItem,
            this._loadGameMenuItem,
            this.toolStripSeparator2,
            this._quitGameMenuItem});
            this._fileMenuItem.Name = "_fileMenuItem";
            this._fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this._fileMenuItem.Text = "File";
            // 
            // _newGameMenuItem
            // 
            this._newGameMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._basementMenuItem,
            this._showersMenuItem,
            this._schoolMenuItem});
            this._newGameMenuItem.Name = "_newGameMenuItem";
            this._newGameMenuItem.Size = new System.Drawing.Size(133, 22);
            this._newGameMenuItem.Text = "New game";
            // 
            // _basementMenuItem
            // 
            this._basementMenuItem.Name = "_basementMenuItem";
            this._basementMenuItem.Size = new System.Drawing.Size(126, 22);
            this._basementMenuItem.Text = "Basement";
            this._basementMenuItem.Click += new System.EventHandler(this.BasementMenuItem_Click);
            // 
            // _showersMenuItem
            // 
            this._showersMenuItem.Name = "_showersMenuItem";
            this._showersMenuItem.Size = new System.Drawing.Size(126, 22);
            this._showersMenuItem.Text = "Showers";
            this._showersMenuItem.Click += new System.EventHandler(this.ShowersMenuItem_Click);
            // 
            // _schoolMenuItem
            // 
            this._schoolMenuItem.Name = "_schoolMenuItem";
            this._schoolMenuItem.Size = new System.Drawing.Size(126, 22);
            this._schoolMenuItem.Text = "School";
            this._schoolMenuItem.Click += new System.EventHandler(this.SchoolMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(130, 6);
            // 
            // _saveGameMenuItem
            // 
            this._saveGameMenuItem.Name = "_saveGameMenuItem";
            this._saveGameMenuItem.Size = new System.Drawing.Size(133, 22);
            this._saveGameMenuItem.Text = "Save game";
            this._saveGameMenuItem.Click += new System.EventHandler(this.SaveGameMenuItem_Click);
            // 
            // _loadGameMenuItem
            // 
            this._loadGameMenuItem.Name = "_loadGameMenuItem";
            this._loadGameMenuItem.Size = new System.Drawing.Size(133, 22);
            this._loadGameMenuItem.Text = "Load game";
            this._loadGameMenuItem.Click += new System.EventHandler(this.LoadGameMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(130, 6);
            // 
            // _quitGameMenuItem
            // 
            this._quitGameMenuItem.Name = "_quitGameMenuItem";
            this._quitGameMenuItem.Size = new System.Drawing.Size(133, 22);
            this._quitGameMenuItem.Text = "Quit";
            this._quitGameMenuItem.Click += new System.EventHandler(this.QuitGameMenuItem_Click);
            // 
            // _openFileDialog
            // 
            this._openFileDialog.Filter = "Lopakodo save (*.txt)|*.txt";
            this._openFileDialog.Title = "Load saved game";
            // 
            // _saveFileDialog
            // 
            this._saveFileDialog.Filter = "Lopakodo save (*.txt)|*.txt";
            this._saveFileDialog.Title = "Save current game";
            // 
            // LopakodoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 489);
            this.Controls.Add(this._menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this._menuStrip;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(480, 528);
            this.MinimumSize = new System.Drawing.Size(480, 528);
            this.Name = "LopakodoForm";
            this.Text = "Lopakodo Game";
            this.Load += new System.EventHandler(this.LopakodoForm_Load);
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _menuStrip;
        private System.Windows.Forms.ToolStripMenuItem _fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _newGameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _basementMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _showersMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _schoolMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem _saveGameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _loadGameMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem _quitGameMenuItem;
        private System.Windows.Forms.OpenFileDialog _openFileDialog;
        private System.Windows.Forms.SaveFileDialog _saveFileDialog;
    }
}