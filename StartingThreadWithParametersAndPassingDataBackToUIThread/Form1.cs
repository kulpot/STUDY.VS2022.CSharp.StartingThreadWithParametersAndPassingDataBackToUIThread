using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

//ref link:https://www.youtube.com/watch?v=fxylJGbEsmE&list=PLAIBPfq19p2EJ6JY0f5DyQfybwBGVglcR&index=77
//Starting a thread with parameters and passing data back to the UI Thread

namespace StartingThreadWithParametersAndPassingDataBackToUIThread
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if(openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ParameterizedThreadStart start = new ParameterizedThreadStart(LoadFile);
                    Thread thread = new Thread(start);
                    thread.Start(openFileDialog.FileName);
                }
            }
        }

        private void LoadFile(object fileName)
        {
            try
            {
                string content = File.ReadAllText(fileName.ToString());
                SetTextBoxTextCallBack(content); //textBox.Invoke(new MethodInvoker(delegate { textBox.Text = content; })); // this is shorter line ....replace this line and remove its method

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Set textbox text in a thread safe manner using method invoker
        /// </summary>
        private void SetTextBoxTextCallBack(string text)
        {
            MethodInvoker invoker = new MethodInvoker(delegate { textBox.Text = text; });
            textBox.Invoke(invoker);

            //textBox.Invoke(new MethodInvoker(delegate { textBox.Text = text; }));
        }
    }
}
