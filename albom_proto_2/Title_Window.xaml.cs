using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace albom_proto_2
{
    /// <summary>
    /// Логика взаимодействия для Title_Window.xaml
    /// </summary>
    public partial class Title_Window : Window
    {
        public static int current_languare_index;
        Photo _photo;

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        private const int GWL_STYLE = -16;

        private const int WS_MAXIMIZEBOX = 0x10000; //maximize button
        private const int WS_MINIMIZEBOX = 0x20000; //minimize button


        private static string[] instruc =  Class1.video_instr.Split('.');

        private static string ext = "."+instruc[instruc.Length-1] ;
        //".mp4"
        private string file_instruct = instruc[instruc.Length - 2] + ext;
        

        private string program;
        private string cur_dir = System.IO.Path.GetTempPath();
        //Environment.CurrentDirectory + "\\";


        public Title_Window()
        {
            InitializeComponent();

            this.Top = (Class1.screenHeight - this.Height) / 2;
            this.Left = 0 + Class1.kor;

            //Class1.type_file =(bool) jpeg.IsChecked;

            


            this.SourceInitialized += MainWindow_SourceInitialized;

            //user.Content = Class1.assembly();

          

            CultureInfo currLang = (app.Language);

            int wow = -1;
            foreach (string cur_lan in Class1.Set_lan().kod_languare)
            {
                wow++;
                if (cur_lan == currLang.Name)
                {
                    current_languare_index = wow;
                    goto yes;
                }

            }

            yes:
            if (currLang != null)
            {
                app.Language = currLang;
            }
        }


        private IntPtr _windowHandle;
        private void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            _windowHandle = new System.Windows.Interop.WindowInteropHelper(this).Handle;

            //disable minimize button
            DisableMinimizeButton();

        }


        protected void DisableMinimizeButton()
        {
            if (_windowHandle == IntPtr.Zero)
                throw new InvalidOperationException("The window has not yet been completely initialized");

            SetWindowLong(_windowHandle, GWL_STYLE, GetWindowLong(_windowHandle, GWL_STYLE) & ~WS_MAXIMIZEBOX);
            SetWindowLong(_windowHandle, GWL_STYLE, GetWindowLong(_windowHandle, GWL_STYLE) & ~WS_MINIMIZEBOX);
        }


        public string browser_navigation
        {
            //get { return textBox1.Text; }
            set { browser.Navigate(new System.Uri(value)); }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Visibility= Visibility.Hidden;

            e.Cancel = true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        public Photo SelectedPhoto
        {
            get { return _photo; }
            set { _photo = value; }
        }

        private void jpeg_Checked(object sender, RoutedEventArgs e)
        {
            //only jpg
            checked_radio();
        }

        private void other_Checked(object sender, RoutedEventArgs e)
        {
            //BMP, GIF, JPEG, PNG, TIFF
            checked_radio();
        }

        private void checked_radio()
        {
            Class1.type_file = false;

            if (Class1.selecting_path != "" || Class1.selecting_path == Class1.other_image().no_found[current_languare_index])
            {
                foreach (Window window in app.Current.Windows)
                {
                    //window.Background = new SolidColorBrush(Colors.Green);
                    this.IsEnabled = false;

                    if (window is MainWindow)
                    {
                        Class1.type_extension = true;
                        window.Activate();


                    }
                }
            }
            this.IsEnabled = true;
        }

      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // create instruction
            create_file();

            if (System.Windows.MessageBox.Show(FindResource("Instruction_opis")+"\n"+ FindResource("Instruction_opis2") + "\n(" + cur_dir + file_instruct + ")", FindResource("Instruction_title").ToString(), MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
            {
                //new
                FileAssociation fileAssocation = new FileAssociation();
                program = fileAssocation.Get(ext);
                String[] j = program.Split('\\');

                // no program
                if (j.Length == 1 || j[j.Length - 1] == "shell32.dll")
                {
                    System.Windows.MessageBox.Show(FindResource("Instruction_message") + " (" + cur_dir + file_instruct + ")", FindResource("Instruction_title").ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                
                // run file
                run_program(program);


            }

            else
                return;
            
        }

        private void create_file()
        {

            //is file
            if (System.IO.File.Exists(cur_dir + file_instruct))
                System.IO.File.Delete(cur_dir + file_instruct);


            if (System.IO.File.Exists(cur_dir + file_instruct))
            {
                System.Windows.MessageBox.Show("The file - "+ cur_dir + file_instruct + "is started by another program", "Error viewing instruction", MessageBoxButton.OK);
                
                return;
            }


            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(Class1.video_instr))
            {
                int f = (int)stream.Length;
                using (FileStream fileStream = new FileStream(cur_dir + file_instruct, FileMode.CreateNew))
                {
                    for (int i = 0; i < f; i++)
                        fileStream.WriteByte((byte)stream.ReadByte());
                }
            }

        }

        private void alter_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (Class1.selecting_path != "" || Class1.selecting_path == Class1.other_image().no_found[current_languare_index])
                {

                    foreach (Window window in app.Current.Windows)
                    {
                        if (window is MainWindow)
                        {
                            MainWindow.update_metadata = true;
                            window.Activate();
                        }
                    }
                }
            }


           
        }


        private void run_program(object file_name)
        {
            ProcessStartInfo stInfo = new ProcessStartInfo(@program, cur_dir+ file_instruct);

            stInfo.UseShellExecute = false;
            stInfo.CreateNoWindow = true;
            stInfo.WindowStyle = ProcessWindowStyle.Maximized;

            //создаем новый процесс
            Process proc = new Process();
            proc.StartInfo = stInfo;
            //Запускаем процесс
            proc.Start();

            //Ждем, пока запущен
            proc.WaitForExit();
            //System.Windows.MessageBox.Show("Код завершения: " + proc.ExitCode, "Завершение Код", MessageBoxButton.OK, MessageBoxImage.Information);

        }

    }
}
