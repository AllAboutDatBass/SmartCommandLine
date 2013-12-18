/*
SmartCommandLine - A DOS commandline wrapper
Copyright (C) 2010  Daniel Randall

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

namespace SCL
{
    partial class MainWindow
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
			this.tbCommands = new System.Windows.Forms.TextBox();
			this.tbOutput = new System.Windows.Forms.RichTextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.lstFolder = new System.Windows.Forms.ListView();
			this.tbAddress = new System.Windows.Forms.TextBox();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbCommands
			// 
			this.tbCommands.BackColor = System.Drawing.Color.Black;
			this.tbCommands.CausesValidation = false;
			this.tbCommands.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tbCommands.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbCommands.ForeColor = System.Drawing.Color.White;
			this.tbCommands.Location = new System.Drawing.Point(0, 692);
			this.tbCommands.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tbCommands.Name = "tbCommands";
			this.tbCommands.Size = new System.Drawing.Size(1006, 31);
			this.tbCommands.TabIndex = 0;
			this.tbCommands.TabStop = false;
			this.tbCommands.TextChanged += new System.EventHandler(this.tbCommands_TextChanged);
			// 
			// tbOutput
			// 
			this.tbOutput.BackColor = System.Drawing.Color.Black;
			this.tbOutput.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.tbOutput.DetectUrls = false;
			this.tbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbOutput.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbOutput.ForeColor = System.Drawing.Color.White;
			this.tbOutput.Location = new System.Drawing.Point(0, 0);
			this.tbOutput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tbOutput.Name = "tbOutput";
			this.tbOutput.ReadOnly = true;
			this.tbOutput.Size = new System.Drawing.Size(503, 661);
			this.tbOutput.TabIndex = 1;
			this.tbOutput.TabStop = false;
			this.tbOutput.Text = "";
			this.tbOutput.WordWrap = false;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 31);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tbOutput);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.lstFolder);
			this.splitContainer1.Size = new System.Drawing.Size(1006, 661);
			this.splitContainer1.SplitterDistance = 503;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 2;
			this.splitContainer1.TabStop = false;
			// 
			// lstFolder
			// 
			this.lstFolder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstFolder.Location = new System.Drawing.Point(0, 0);
			this.lstFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.lstFolder.Name = "lstFolder";
			this.lstFolder.Size = new System.Drawing.Size(498, 661);
			this.lstFolder.TabIndex = 0;
			this.lstFolder.TabStop = false;
			this.lstFolder.UseCompatibleStateImageBehavior = false;
			// 
			// tbAddress
			// 
			this.tbAddress.AllowDrop = true;
			this.tbAddress.Dock = System.Windows.Forms.DockStyle.Top;
			this.tbAddress.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbAddress.Location = new System.Drawing.Point(0, 0);
			this.tbAddress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tbAddress.Name = "tbAddress";
			this.tbAddress.ShortcutsEnabled = false;
			this.tbAddress.Size = new System.Drawing.Size(1006, 31);
			this.tbAddress.TabIndex = 3;
			this.tbAddress.TabStop = false;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1006, 723);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.tbCommands);
			this.Controls.Add(this.tbAddress);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "MainWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Smart CommandLine";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbCommands;
        private System.Windows.Forms.RichTextBox tbOutput;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lstFolder;
        private System.Windows.Forms.TextBox tbAddress;
    }
}

