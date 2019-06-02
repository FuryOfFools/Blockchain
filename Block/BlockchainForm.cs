using System;
using System.Drawing;
using System.Windows.Forms;
using BC;
using Interactions;

namespace Program
{
    public partial class BlockchainForm : Form
    {
        Blockchain chain;
        string BlockchainPath = "Blockchain";
        bool Loaded = false;

        public BlockchainForm()
        {
            InitializeComponent();
        }
        
        // загружает блокчейн по клику
        private void LoadBlockchain_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                chain = LocalInteractions.LoadBlockchain(BlockchainPath);
            else
            {
                openFileDialog1.InitialDirectory = "C:\\";
                openFileDialog1.Title = "Open Blockchain";
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                BlockchainPath = openFileDialog1.FileName;
                chain = LocalInteractions.LoadBlockchain(BlockchainPath);
            }
            label1.Text = "Loaded ";
            if (chain.IsValid())
            {
                label1.Text += "and Blockchain is valid";
                label1.ForeColor = Color.Green;
            }
            else
            {
                label1.Text += "and Blockchain is not valid";
                label1.ForeColor = Color.Red;
            }
            Loaded = true;
            ShowAllBlocks.Enabled = true;
            AddDataInBlockchain.Enabled = true;

        }

        // добавляет данные в блокчейн по клику
        private void AddDataInBlockchain_Click(object sender, EventArgs e)
        {
                openFileDialog1.InitialDirectory = "C:\\";

                label2.Text = "Processing ";
                label2.ForeColor = Color.Orange;
            
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                string fileName = openFileDialog1.FileName;

                byte[] data = LocalInteractions.ReadBytesFromFile(fileName);
                chain.AddData(data);
            
                label2.Text = "Completed ";
                if (chain.IsValid())
                {
                    label2.Text += "and Blockchain is valid";
                    label2.ForeColor = Color.Green;
                }
                else
                {
                    label2.Text += "and Blockchain is not valid";
                    label2.ForeColor = Color.Red;
                }
        }
        
        // сохранение блокчейна по закрытию формы
        private void BlockchainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Loaded)
                chain.SaveLocal(BlockchainPath);
        }

        // отображает информацию о блоках по нажатию
        private void ShowAllBlocks_Click(object sender, EventArgs e)
        {
            try
            {
                Info.Text = "";
                Info.ScrollBars = ScrollBars.Vertical;
                for (int i = 1; i < chain.Count; i++)
                {
                    Info.Text += chain[i].ToString();
                    Info.Text += Environment.NewLine;
                }
            }
            catch
            {
                Info.Text = "empty";
            }
        }
    }
}
