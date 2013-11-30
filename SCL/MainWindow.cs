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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace SCL
{
    public partial class MainWindow : Form
    {
        protected Process cmd;
        protected StreamWriter input;
        
        protected List<string> history;
        protected int historyIterator = 0;
        
        protected List<string> files;
        protected int fileIterator = 0;
        
        delegate void CloseCallback();

        delegate void WriteLineCallback(
            RichTextBox textBox,
            string text,
            string wasDir,
            Color color);
        
        delegate void SetReadOnlyCallback(
            TextBox textBox,
            bool state);
            
        private string _wasDir = "";
        
        public MainWindow()
        {
            InitializeComponent();

            history = new List<string>();
            files = new List<string>();

            tbCommands.PreviewKeyDown += 
                new PreviewKeyDownEventHandler(tbCommands_PreviewKeyDown);

            Process cmd;
            cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.CreateNoWindow = true;

            cmd.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
            cmd.Exited += new EventHandler(cmd_Exited);
            
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.OutputDataReceived += new 
                DataReceivedEventHandler(cmd_OutputDataReceived);
            
            cmd.StartInfo.RedirectStandardError = true;
            cmd.ErrorDataReceived += 
                new DataReceivedEventHandler(cmd_ErrorDataReceived);
            
            cmd.StartInfo.RedirectStandardInput = true;

            cmd.Start();

            input = cmd.StandardInput;
            
            cmd.BeginOutputReadLine();
            cmd.BeginErrorReadLine();
        }

        void cmd_Exited(object sender, EventArgs e)
        {
            this.Invoke(new CloseCallback(Close), null);    
        }

        static void AddItem(
            ListBox listBox,
            string text)
        {
            if (text.Equals("\f"))
            {
                // "cls" command returned character for clear screen:
                listBox.Items.Clear();
            }
            else
            {
                int nItem = listBox.Items.Add(text);
                listBox.TopIndex = nItem;
            }
        }

        static void SetReadOnly(
            TextBox textBox,
            bool state)
        {
            textBox.ReadOnly = state;
            textBox.BackColor = state ? SystemColors.GrayText : SystemColors.WindowText;
            textBox.ForeColor = state ? SystemColors.WindowText : SystemColors.Window;
            
            if (!state)
            {
                textBox.Clear();
            }
        }

        void cmd_ErrorDataReceived(
            object sender, 
            DataReceivedEventArgs e)
        {
            if (null == e.Data)
            {
                this.Invoke(new CloseCallback(Close), null);    
            }
            else
            {
                this.Invoke(new WriteLineCallback(WriteLine), new object[] { tbOutput, e.Data, _wasDir, Color.Red });
                this.Invoke(new SetReadOnlyCallback(SetReadOnly), new object[]{tbCommands, false});
            }
        }

        private void cmd_OutputDataReceived(
            object sendingProcess, 
            DataReceivedEventArgs e)
        {
            if (null == e.Data)
            {
                this.Invoke(new CloseCallback(Close), null);    
            }
            else
            {
                this.Invoke(new WriteLineCallback(WriteLine), new object[] { tbOutput, e.Data, _wasDir, Color.White });
                this.Invoke(new SetReadOnlyCallback(SetReadOnly), new object[] { tbCommands, false });
            }
        }

        static void WriteLine(
            RichTextBox textBox,
            string text,
            string wasDir,
            Color color)
        {
            if (text.Length < 1)
            {
                return;
            }
            
            if (text.Equals("\f"))
            {
                // "cls" command returned character for clear screen:
                textBox.ResetText();
            }
            else
            {
                string curDir = Directory.GetCurrentDirectory();

                if ((wasDir.Length > 0) && text.StartsWith(wasDir))
                {
                    text = text.Replace(wasDir, "");

                    textBox.SelectionColor = Color.LightGreen;
                }
                else
                {
                    textBox.SelectionColor = color;
                }

                textBox.SelectionStart = textBox.Text.Length;

                textBox.SelectionLength = 0;

                if (!(text.Contains('\r') || text.Contains('\n')))
                {
                    text += "\r\n";
                }
                
                textBox.AppendText(text);

                textBox.ScrollToCaret();
            }
        }

        void tbCommands_PreviewKeyDown(
            object sender, 
            PreviewKeyDownEventArgs e)
        {
            bool fTabbing = false;
            
            switch (e.KeyCode)
            {
                case Keys.Enter:
                {
                    // retain where we were:
                    // _wasDir = cmd   ;

                    _wasDir = ;
                    
                    // strip out the special whitespace characters:
                    tbCommands.Text = tbCommands.Text.Replace((char)0x00A0, ' ');
                    
                    input.WriteLine(tbCommands.Text);
                    history.Add(tbCommands.Text);
                    //tbCommands.Clear();
                    SetReadOnly(tbCommands, true);
                    
                    
                    break; 
                }
                case Keys.Up:
                {
                    tbCommands.Text = GetHistory(true);
                    
                    break; 
                }
                case Keys.Down:
                {
                    tbCommands.Text = GetHistory(false);
                    tbCommands.SelectionStart = tbCommands.Text.Length;
                    tbCommands.SelectionLength = 0;
                    
                    break; 
                }
                case Keys.Escape:
                {
                    tbCommands.Clear();
                    
                    break; 
                }
                case Keys.Tab:
                {
                    fTabbing = true;
                    
                    string substring = "";
                    
                    int space = tbCommands.Text.LastIndexOf(' ') + 1;

                    if (space > 1)
                    {
                        if (space < tbCommands.Text.Length)
                        {
                            substring = tbCommands.Text.Substring(space);
                            
                            tbCommands.Text = tbCommands.Text.Remove(space);
                        }
                    }
                    else
                    {
                        substring = tbCommands.Text;
                        
                        tbCommands.Text = "";
                    }

                    if (e.Shift)
                    {
                        tbCommands.Text += GetFiles(substring, false);
                    }
                    else
                    {
                        tbCommands.Text += GetFiles(substring, true);
                    }

                    tbCommands.SelectionStart = tbCommands.Text.Length;
                    tbCommands.SelectionLength = 0;

                    break; 
                }
            }
            
            if (!fTabbing)
            {
                files.Clear();
            }
        }
        
        protected string GetHistory(
            bool fUp)
        {
            int dir = fUp ? -1 : 1;
            
            historyIterator += dir;
            
            int range = history.Count;
                    
            if (range > 0)
            {
                if (historyIterator < 0)
                {
                    historyIterator = range - 1;    
                }
                
                if (historyIterator < range)
                {
                    return history[historyIterator];    
                }
                else 
                {
                    historyIterator = 0;
                    
                    return history[historyIterator];    
                }
            }
            
            return "";
        }
        
        protected string GetFiles(
            string search,
            bool fUp)
        {
            if (files.Count < 1)
            {
                if (search.Length < 1)
                {
                    // search for all files:
                    search = "*.*";
                }
                else if ((!search.Contains('*')) && (!search.Contains('?')))
                {
                    // append wildcard for prefix search:
                    search += "*";    
                }
                
                fileIterator = 0;
                
                // collect files:
                string[] list = Directory.GetFiles("C:\\Temp\\", search, SearchOption.TopDirectoryOnly);
                
                foreach (string filename in list)
                {
                    string wrapped = Path.GetFileName(filename);
                    
                    if (wrapped.Contains(' '))
                    {
                        // wrap filename in quotes:
                        wrapped = "\"" + wrapped + "\"";
                    }
                    
                    files.Add(wrapped.Replace(' ', (char)0x00A0));    
                }    
            }
            
            int range = files.Count;
            
            if (range > 0)
            {
                if (fUp)
                {
                    fileIterator++;

                    if (fileIterator >= range)
                    {
                        // wrap around to head of list:
                        fileIterator = 0;
                    }    
                }
                else
                {
                    fileIterator--;
                    
                    if (fileIterator < 0)
                    {
                        // wrap around to end of list:
                        fileIterator += range;
                    }    
                }
                
                return files[fileIterator];    
            }

            return "";
        }
        
        private void tbCommands_TextChanged(
            object sender, 
            EventArgs e)
        {
        }
    }
}
