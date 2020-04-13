/* Source: METANIT.COM: https://metanit.com/sharp/wpf/22.6.php */

namespace AppleMusicPlayer
{
    public interface IDialogService
    {
        void ShowMessage(string message);
        string FileName { get; set; }
        bool OpenFileDialog(string filter = "");
        bool SaveFileDialog(string filter = "");
    }
}
