using System;
using System.Windows.Forms;


namespace Program
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            //BlockchainForm bf = new BlockchainForm();
            //bf.ShowDialog();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BlockchainForm());
        }
    }
}
