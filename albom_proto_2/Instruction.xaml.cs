using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;
using System.IO;
using System.Windows.Threading;

namespace albom_proto_2
{
    /// <summary>
    /// Логика взаимодействия для Instruction.xaml
    /// </summary>
    public partial class Instruction : Window
    {
            const string instr = Class1.video_instr;
            string cur_dir = Environment.CurrentDirectory + "\\"; 
            //System.IO.Path.GetTempPath();

            //Environment.CurrentDirectory+"\\";

        public Instruction()
        {
            InitializeComponent();
            create_file();
        }


        private void create_file()
        {
            mePlayer.Source = new Uri(cur_dir + instr.Split('.')[2] + "." + instr.Split('.')[3]);

            //timer
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            //is file
            if (System.IO.File.Exists(cur_dir + instr.Split('.')[2] + "." + instr.Split('.')[3]))
                File.Delete(cur_dir + instr.Split('.')[2] + "." + instr.Split('.')[3]);


            if (System.IO.File.Exists(cur_dir + instr.Split('.')[2] + "." + instr.Split('.')[3]))
            {
                System.Windows.MessageBox.Show("file is exist", "error", MessageBoxButton.OK);
                // button player set hidden
                return;
            }


            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(instr))
            {
                int f = (int)stream.Length;
                using (FileStream fileStream = new FileStream(cur_dir + instr.Split('.')[2] + "." + instr.Split('.')[3], FileMode.CreateNew))
                {
                    for (int i = 0; i < f; i++)
                        fileStream.WriteByte((byte)stream.ReadByte());
                }
            }

            btnPlay_Click(null, null);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (mePlayer.Source != null)
            {
                if (mePlayer.NaturalDuration.HasTimeSpan)
                    this.Title = String.Format("{0} / {1}", mePlayer.Position.ToString(@"mm\:ss"), mePlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            }
            //else
            ////lblStatus.Content = "No file selected...";

            if (mePlayer.NaturalDuration == mePlayer.Position)
                mePlayer.Stop();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            mePlayer.Play();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mePlayer.Pause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mePlayer.Stop();
        }
           
    }
}
