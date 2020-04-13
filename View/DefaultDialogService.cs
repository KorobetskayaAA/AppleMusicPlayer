/* Source: METANIT.COM: https://metanit.com/sharp/wpf/22.6.php */
using System.Windows;
using Microsoft.Win32;

namespace AppleMusicPlayer
{
    public class DefaultDialogService : IDialogService
    {
        public string FileName { get; set; }

        public bool OpenFileDialog(string filter = "")
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            openFileDialog.Filter = filter;
            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        public bool SaveFileDialog(string filter = "")
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            saveFileDialog.Filter = filter;
            if (saveFileDialog.ShowDialog() == true)
            {
                FileName = saveFileDialog.FileName;
                return true;
            }
            return false;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
