using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MbedBinToBoard
{
    public partial class MainWindow : Window
    {
        private const string pathsFilename = "Paths.txt";

        private NotifyIcon notifyIcon;
        private IContainer components;

        private DateTime latestCopiedBinCreateTime;
        private DateTime actionTime;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            latestCopiedBinCreateTime = new DateTime();

            try
            {
                Icon = new BitmapImage(new Uri(Path.GetFullPath("Icon.ico")));
            }
            catch { }

            try
            {
                string[] paths = File.ReadAllLines(pathsFilename);

                tbxDownloadFolderPath.Text = paths[0];
                tbxMbedBoardPath.Text = paths[1];
            }
            catch { }

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.IsEnabled = true;
            timer.Tick += Timer_Elapsed;
            timer.Start();

            components = new Container();
            notifyIcon = new NotifyIcon(components);

            notifyIcon.Icon = SystemIcons.Asterisk;
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
            notifyIcon.MouseMove += NotifyIcon_MouseMove;
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            WindowState = WindowState.Normal;
            Focus();
            Activate();
        }

        private void NotifyIcon_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            notifyIcon.Text = tblAction.Text + "\n" + tblTime.Text;
        }

        private void Timer_Elapsed(object sender, EventArgs e)
        {
            try
            {
                FileInfo latestBin;
                string binsPaths = tbxDownloadFolderPath.Text;
                string mbedPath = tbxMbedBoardPath.Text;

                UpdateActionTimeSpan();

                if (cbxChecking.IsChecked != true) return;
                if (!Directory.Exists(mbedPath)) return;

                latestBin = GetLatestBin(binsPaths);

                if (latestBin.CreationTime == latestCopiedBinCreateTime) return;

                DeleteMbedBins(mbedPath);
                Copy(latestBin, mbedPath);

                notifyIcon.ShowBalloonTip(1000, "Progamm kopiert", latestBin.Name, ToolTipIcon.Info);
            }
            catch (Exception exc)
            {
                tblAction.Text = exc.Message;
                SetActionTime();
            }
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileInfo latestBin = GetLatestBin(tbxDownloadFolderPath.Text);

                DeleteMbedBins(tbxMbedBoardPath.Text);
                Copy(latestBin, tbxMbedBoardPath.Text);

                tblAction.Text = latestBin.Name;
            }
            catch (Exception exc)
            {
                tblAction.Text = exc.Message;
                SetActionTime();
            }
        }

        private FileInfo GetLatestBin(string paths)
        {
            var binDownFiles = Directory.GetFiles(paths).Where(x => Path.GetExtension(x) == ".bin");

            return binDownFiles.Select(f => new FileInfo(f)).OrderBy(f => f.CreationTime).Last();
        }

        private void DeleteMbedBins(string mbedPath)
        {
            var binMbedFiles = Directory.GetFiles(mbedPath).Where(x => Path.GetExtension(x) == ".bin");

            foreach (string file in binMbedFiles) File.Delete(file);
        }

        private void Copy(FileInfo binFilePath, string mbedPath)
        {
            string destPath = Path.Combine(mbedPath, binFilePath.Name);
            File.Copy(binFilePath.FullName, destPath);

            latestCopiedBinCreateTime = binFilePath.CreationTime;
            SetActionTime();

            tblAction.Text = binFilePath.Name;
        }

        private void SetActionTime()
        {
            actionTime = DateTime.Now;
            UpdateActionTimeSpan();
        }

        private void UpdateActionTimeSpan()
        {
            if (actionTime.Ticks == 0) tblTime.Text = "Not yet";
            else tblTime.Text = (DateTime.Now - actionTime).ToString("hh\\:mm\\:ss");
        }

        private void Path_Changed(object sender, TextChangedEventArgs e)
        {
            string[] paths = new string[] { tbxDownloadFolderPath.Text, tbxMbedBoardPath.Text };

            File.WriteAllLines(pathsFilename, paths);
        }
    }
}
