using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Input;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Page;
using System.Windows.Threading;
using System.ComponentModel;
using System.Threading;

namespace albom_proto_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public PhotoCollection Photos;
        public static int current_languare_index;
        int kol_image;
        //string dir;
        Title_Window pvTitle = new Title_Window();
        Secret path_metadata = new Secret();
        public static bool update_metadata = false;
        //write cur_met default Metadata if true
        public static bool default_Metadata = false;
        private int up = 0;
        private int Teek = 0;
        string ffg ;

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        DispatcherTimer dispatcherTimer_blocked = new DispatcherTimer();
        DispatcherTimer dispatcherTimer_wait = new DispatcherTimer();


        //new
        BackgroundWorker bw;

        public MainWindow()
        {
            //Properties.Settings.Default.pr_disc = "d:\\";
            single();
            InitializeComponent();
            this.Title = this.Title + " "+ Assembly.GetExecutingAssembly().GetName().Version;
            // run with help
            //Properties.Settings.Default.run = false;
            Dispatcher_ui();
            Dispatcher_blocked();
            //Dispatcher_wait();

            location_window();

            Class1.type_file = (bool)jpeg.IsChecked;

            if (!Directory.Exists(Class1.path_out))
            { Class1.path_exist = false; }

            ImagesDir.Text = "";

            Select_lang.DataContext = new ObservableCollection<Class1>()
            {
                    new Class1() { Image=@Class1.Set_lan().flag[0], Text =Class1.Set_lan().name_languare[0]},
                    new Class1() { Image=@Class1.Set_lan().flag[1], Text = Class1.Set_lan().name_languare[1]},
                    new Class1() { Image=@Class1.Set_lan().flag[2], Text = Class1.Set_lan().name_languare[2]},
                    new Class1() { Image=@Class1.Set_lan().flag[3], Text = Class1.Set_lan().name_languare[3]},
                    new Class1() { Image=@Class1.Set_lan().flag[4], Text = Class1.Set_lan().name_languare[4]},
                    new Class1() { Image=@Class1.Set_lan().flag[5], Text = Class1.Set_lan().name_languare[5]},
                    new Class1() { Image=@Class1.Set_lan().flag[6], Text = Class1.Set_lan().name_languare[6]}
            };
            //Oval.ContextMenu = (System.Windows.Controls.ContextMenu)FindResource("Menu_M");

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
                Select_lang.SelectedIndex = wow;
            }

            string mmk=FindResource("Folder").ToString();
            user.Text = Class1.assembly();
            Avtor.Text = Class1.show()[0];
            Avtor.ToolTip= Class1.show()[1];

            //new
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }

        //new
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            
            int u=1;
            while (u <= kol_image)
            {
                double K = Convert.ToDouble(u) / kol_image * 100;
                bw.ReportProgress((int)K,u-1);
                path_metadata.operacion(2, "");

                u++;
                //Thread.Sleep(10);
            }
         }


        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            
            Class1.sel_img[1] = PhotosListBox.SelectedIndex.ToString();
            Class1.sel_img[0] = Name_file.Text;

            //(Photo)PhotosListBox.SelectedItem++;

            if(progress.Maximum== PhotosListBox.Items.Count)
                progress.Value = PhotosListBox.SelectedIndex;
            else
                progress.Value =Convert.ToDouble( e.ProgressPercentage);
            
            long prog_val = Convert.ToInt64(Convert.ToDouble(PhotosListBox.SelectedIndex + 1) / PhotosListBox.Items.Count * 100);

            ImagesDir.Text= ffg+" ("+ Convert.ToInt64(Convert.ToDouble(PhotosListBox.SelectedIndex+1)/ PhotosListBox.Items.Count*100).ToString()+"%)";
            PhotosListBox.SelectedIndex++ ;
            //Text.Content = k.ToString();
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Text.Content = "Работа окончена";
            progress.Visibility = Visibility.Hidden;
            ImagesDir.Text=ImagesDir.Tag.ToString() ;
            //progress.IsIndeterminate = false;
        }

        
        private void single()
        {

            string ff = app.ResourceAssembly.GetName().Name;
            if (System.Diagnostics.Process.GetProcessesByName(ff).Length > 1)
            {
                System.Windows.MessageBox.Show("The application - Comments to the image is already running", "Restart the application - Comments to the image", MessageBoxButton.OK, MessageBoxImage.Warning);
                System.Windows.Application.Current.Shutdown();
            }
        }


        public MainWindow(string path)
        {
            InitializeComponent();
        }

        private void location_window()
        {
            this.Height = (double)FindResource("main_height");
            this.Width = (double)FindResource("main_widht");

            this.Top = (Class1.screenHeight - this.Height) / 2;
            this.Left = (double)FindResource("titlt_widht") + Class1.kor;
            
            
            //pvTitle
            pvTitle.Top = (Class1.screenHeight - this.Height) / 2;
            pvTitle.Left = 0 + Class1.kor;
        }

        private void Select_lang_SelectionChanged(Object sender, EventArgs e)
        {

            System.Windows.Controls.ComboBox mi = sender as System.Windows.Controls.ComboBox;
            current_languare_index = mi.SelectedIndex;
            CultureInfo lang = new CultureInfo(Class1.Set_lan().kod_languare[mi.SelectedIndex]);

            var comboItem = (Class1)Select_lang.SelectedValue;

            if (lang != null)
            {
                app.Language = lang;
            }
            //dir
            if(Class1.selecting_path != null)
            show_picture();

            browser_begin();
            //Title_Window pvTitle = new Title_Window();
            pvTitle.browser_navigation = "file://" + Class1.path_out + Class1.begin_html;

            if (Properties.Settings.Default.run==false)
            {
                pvTitle.Show();
                Properties.Settings.Default.run = true;

            }
            //if(kol_image>0)
            //PhotosListBox_MouseUp(null,null);

            Avtor.Text = Class1.show()[0];
            Avtor.ToolTip = Class1.show()[1];
            this.Title = FindResource("Title_main").ToString()+ " " + Assembly.GetExecutingAssembly().GetName().Version;;

        }

        private void OnPhotoClick(object sender, RoutedEventArgs e)
        {
            string pattern = FindResource("SI_4").ToString();
            //Regex rx = new Regex(pattern);

            foreach (Window window in System.Windows.Application.Current.Windows)
            {
                string[] jj = Regex.Split(window.Title, pattern);


                if (jj.Length>1)
                {
                    this.WindowState = WindowState.Minimized;
                    return;
                }
            }

            if (Class1.foto_click)
            {
                
                Class1.foto_click = false;
                return;
            }
            
            foreach (string file in Class1.error_images)
            {
                if(file== Name_file.Text)
                {
                    System.Windows.MessageBox.Show("Метадані неможливо зберігати  в зображенні." + " ("+file+")", "Помилка в зображенні", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            PhotoView.current_dir = ImagesDir.Text + Class1.sel_dir + Name_file.Text;
            PhotoView.current_metadata = cur_met.Text.ToString();
            PhotoView pvWindow = new PhotoView();00

            pvWindow.SelectedPhoto = (Photo)PhotosListBox.SelectedItem;

            pvWindow.WindowState = WindowState.Maximized;
            pvWindow.Show();
        }

        private void OnPhotoClickMove(object sender, RoutedEventArgs e)
        {
        //new work
            if (verife_metadata())
            {
                move_image(false);
            }

            else
            {
                System.Windows.MessageBox.Show(Class1.nothing_metadata[current_languare_index]+" ("+Name_file.Text+")", FindResource("Move").ToString(), MessageBoxButton.OK);
                return;
            }
        }

        public void SetBar (int j)
        {
            progress.Maximum =j; 
        }

        
        private void move_image(bool count)
        {
            //dispatcherTimer_wait.Start();
            progreebar_continious(true,0);
            Secret secret_oper = new Secret();
            switch (count)
            {
                case false:
                    //dir
                    secret_oper.operacion(0, Class1.selecting_path +Class1.sel_dir + Name_file.Text);
                    update_dir();
                    PhotosListBox.SelectedIndex = -1;
                    break;

                case true:
                    //dir
                    show_find(Class1.selecting_path);
                    //Secret secret_oper = new Secret();
                    string ext_file = "";
                    if (Class1.type_file)
                        ext_file = Class1.ext_files[0];
                    //."*.jpg,*.jpeg";
                    else
                        ext_file = Class1.ext_files[1];
                    //"*.jpg,*.bmp,*.gif,*.jpeg,*.png,*.tif";

                    //string[] files = Directory.GetFiles(dir);

                    int r = 0;

                    string hj = PhotoView.path_file;


                    for (int i = 0; i <= ext_file.Split(',').Length - 1; i++)
                    {
                        //dir
                        foreach (string s in Directory.GetFiles(Class1.selecting_path, ext_file.Split(',')[i]))
                        {
                            string[] hh = s.Split('\\');
                            foreach (string gg in Class1.error_images)
                            {
                                if (hh[hh.Length - 1] == gg)
                                {
                                    Oval.IsEnabled = false;
                                    goto err;
                                }
                            }

                            secret_oper.operacion(1, s);

                            if (Secret.rez != Class1.nothing_metadata[current_languare_index])
                                secret_oper.operacion(0, s);
                           r++;
                            err:;
                            //secret_oper.operacion(0, s);
                        }
                    }

                    if (r == 0)
                        System.Windows.MessageBox.Show(Class1.nothing_metadata[current_languare_index], FindResource("Moves").ToString(), MessageBoxButton.OK);


                    break;


            }
            //dispatcherTimer_wait.Stop();
            progreebar_continious(false,0);


        }

        // не треба
        private void editPhoto(object sender, RoutedEventArgs e)
        {
            PhotoView pvWindow = new PhotoView();
            pvWindow.SelectedPhoto = (Photo)PhotosListBox.SelectedItem;
            pvWindow.Show();
        }

       
        public void show_find(string path_find)
        {
            //old
            ImagesDir.Text = Class1.other_message().wait[MainWindow.current_languare_index];
            
            progress.Value = 0;
           
            Photos.Path = path_find;
            
            
            kol_image = Photos.kol;
           
            show_picture();
            
        }

  

        private void update_dir()
        {
            PhotosListBox.UpdateLayout();
                       
            ImagesDir.Text = Class1.other_message().wait[MainWindow.current_languare_index];
            

            //"Обновляем данные";
                Photos.Path= Class1.selecting_path;
                kol_image = Photos.kol;
            
                show_picture();
        }


        private void SettingClick(object sender, RoutedEventArgs e)
        {
           
            location_window();

            browser_begin();
            pvTitle.browser_navigation = "file://" + Class1.path_out + Class1.begin_html;
            pvTitle.Visibility = Visibility.Visible;

        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.MessageBox.Show(Class1.show()[0], Class1.show()[1], MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {

       
        }


        public void show_picture()
        {
            //if (kol_image != 0)
            //    //sel_image();
                state_move();
                switch (kol_image)
                {
                    case 0:
                    //dir
                        ImagesDir.Text = Class1.selecting_path + " " + Class1.other_image().no_found[MainWindow.current_languare_index];

                        break;
                    case 1:
                        //dir
                        ImagesDir.Text = Class1.selecting_path + " " + Class1.other_image().found_image[MainWindow.current_languare_index];
                        break;
                    default:
                    //dir
                        string other = Class1.other_image().found_images[MainWindow.current_languare_index];
                        ImagesDir.Text = Class1.selecting_path + " " + other.Replace("2", Photos.kol.ToString());
                        break;

                }
            //else
            //    ImagesDir.Text = "";



                                enable_button(true);
                System.Windows.Input.Mouse.OverrideCursor = null;

        }

        public void state_move()
        {
            switch (kol_image)
            { 
                case 0:
                        Move.IsEnabled = false;
                        break;
                case 1:
                        if(cur_met.Text!=FindResource("default Metadata").ToString())
                                Move.IsEnabled = true;
                            else
                                Move.IsEnabled = false;
                        break;
                 default:   
                        Move.IsEnabled = true;
                        break;

            }
        }

        public void see_picture()
        {
            //if (progress.Value != 0)
            //sel_image();

            switch (progress.Value)
            {
                case 0:
                    ImagesDir.Text = Class1.other_message().wait[MainWindow.current_languare_index] + " " + Class1.other_image().no_found[MainWindow.current_languare_index];
                    break;
                case 1:

                    ImagesDir.Text = Class1.other_message().wait[MainWindow.current_languare_index] + " " + Class1.other_image().found_image[MainWindow.current_languare_index];
                    break;
                default:
                    string other = Class1.other_image().found_images[MainWindow.current_languare_index];
                    ImagesDir.Text = Class1.other_message().wait[MainWindow.current_languare_index] + " " + other.Replace("2", Photos.kol.ToString());
                    break;

            }

        }

            private void sel_image()
        {
            //if (PhotosListBox.SelectedIndex == -1)
            //{
            //    PhotosListBox.SelectedIndex = 0;

            //}
            //PhotosListBox_MouseUp(null, null);

        }
      
        private void enable_button(bool key)
        {
            
               
                Change.IsEnabled = key;
                state_move();
             //if(kol_image!=0) Move.IsEnabled = key;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
            //прекращает работу приложения, даже если выполняются другие потоки.

        }

        private void browser_begin()
        {
            if (System.IO.Directory.Exists(Class1.path_out))
                System.IO.Directory.Delete(Class1.path_out, true);

            if (!System.IO.Directory.Exists(Class1.path_out))
                System.IO.Directory.CreateDirectory(Class1.path_out);
            browser();
        }

        private void browser()
        {

            // "en", "zh", "hi", "es", "ru", "uk", "pl"

            foreach (string find_resources in this.GetType().Assembly.GetManifestResourceNames())
            {
                if (find_resources.Split('.')[1] == Class1.reference_resource)
                {
                    if (find_resources.Split('.')[2] == Class1.Set_lan().kod_languare[current_languare_index] || find_resources.Split('.')[2] == Class1.total_resources)
                    {
                        Class1.copy_resources_to_disc(find_resources);
                    }
                }
            }

        }

       
        private bool verife_metadata()
        {
            //PhotosListBox_MouseUp(null, null);
            if (cur_met.Text.ToString() == Class1.nothing_metadata[current_languare_index])
                return false;
            else
                return true;

        }

           

        private void Window_LostFocus(object sender, RoutedEventArgs e)
        {
            cur_met.Text = FindResource("default Metadata").ToString();
        }

        

        private void user_SelectionChanged(object sender, RoutedEventArgs e)
        {
           

        }


        private void checked_radio()
        {
            if (Class1.selecting_path != "" || Class1.selecting_path == Class1.other_image().no_found[current_languare_index])
            {
                    this.IsEnabled = false;
                dispatcherTimer.Start();
                show_find(Class1.selecting_path);
                dispatcherTimer.Stop();

                
                    this.IsEnabled = true;
            }
        }

        private void type_image(object sender, RoutedEventArgs e)
        {
            Class1.type_file = (bool)jpeg.IsChecked;
            checked_radio();
        }

       

        private void user_LostFocus(object sender, RoutedEventArgs e)
        {
            if(user.Text.Length<5 && user.Text!= Class1.assembly())
            {
                user.Text = Class1.assembly();
                Class1.user_name = user.Text;
                cur_met.Text = FindResource("default Metadata").ToString();
                PhotosListBox.SelectedIndex = -1;
            }
            
        }

        private void user_TextChanged(object sender, TextChangedEventArgs e)
        {
            PhotosListBox.SelectedIndex = -1;
            Class1.user_name = user.Text;

        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            user.Text = Class1.assembly();
            Class1.user_name = user.Text;
            cur_met.Text = FindResource("default Metadata").ToString();
            PhotosListBox.SelectedIndex = -1;
        }

        private void PhotosListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (progress.IsVisible)
                return;

            foreach(string gg in Class1.error_images)
            {
                if (Name_file.Text == gg)
                {
                    Oval.IsEnabled = false;
                    goto yes;
                }
            }

            Oval.IsEnabled = true;
            //Class1.error_images[Class1.error_count]

            yes:            //new
            
                

            if (PhotosListBox.SelectedIndex == -1)
                cur_met.Text = FindResource("default Metadata").ToString();
            else
            {
                IsLocked(Class1.selecting_path + Class1.sel_dir + Name_file.Text);
                //name and index selection file
                Class1.sel_img[0] = Name_file.Text;
                Class1.sel_img[1] = PhotosListBox.SelectedIndex.ToString();
                // click on photo for see metadata
                Secret secret_oper = new Secret();
                //dir
                secret_oper.operacion(1, Class1.selecting_path + Class1.sel_dir + Name_file.Text);

                cur_met.Text = Secret.rez.ToString();
                PhotoView.current_metadata = Secret.rez.ToString();
                state_move();
            }

            

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            // change other extension
            if (Class1.type_extension == true)
            {
                dispatcherTimer.Start();
                show_find(Class1.selecting_path);
                dispatcherTimer.Stop();
                
            }


            if (update_metadata == true)
            {
                PhotosListBox_SelectionChanged(null, null);

                //PhotosListBox_MouseUp(null, null);
                update_metadata = false;
            }
        }

        private void StatusBarItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            location_window();

            browser_begin();
            pvTitle.browser_navigation = "file://" + Class1.path_out + Class1.begin_html;
            pvTitle.Visibility = Visibility.Visible;

            //this.Mov.IsEnabled = false;
        }

        private void Count_image_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Class1.slider_value = (Int32)Count_image.Value;

            if (Count_image.Value == 0)
            {
                look_image.Content = FindResource("Count_image");
                Count_image.ToolTip = FindResource("Count_image").ToString();
            }
               
            else
            {
                look_image.Content = FindResource("Count_image").ToString().Split('-')[0]+" - "+ Count_image.Value;
                Count_image.ToolTip= FindResource("Count_image").ToString().Split('-')[0] + " - " + Count_image.Value;

            }
                


        }

 



        private void Dispatcher_ui()
        {
            //DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 5);
        }

        

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (Count_image.Value == 0)
                    {
                        progress.Maximum = Class1.all_kol;
                        progress.Minimum = 1;
                    }

                    else
                    {
                        progress.Maximum = Count_image.Value;
                    
                    }
                        progress.Minimum = 1;


                    if (progress.Maximum > progress.Value)
                    {
                        progress.Value = Photos.kol;
                        see_picture();
                    }
                }), DispatcherPriority.Background);
        }

        private void Dispatcher_blocked()
        {
            //DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer_blocked.Tick += new EventHandler(dispatcherTimer_blocked_Tick);
            dispatcherTimer_blocked.Interval = new TimeSpan(0, 0, 0, 1);
        }

        private void dispatcherTimer_blocked_Tick(object sender, EventArgs e)
        {
            Teek++;
            Dispatcher.BeginInvoke(new Action(() =>
            {
                IsLocked(Class1.selecting_path + Class1.sel_dir + Name_file.Text);
            }), DispatcherPriority.Background);
        }


        private void Sel_folder()
        {
            if (Class1.selecting_path!="" )
            {
                Class1.error_images = null;

                System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(Class1.selecting_path);
                Array.Resize(ref Class1.error_images, directoryInfo.GetFiles().Length);
                Class1.error_count = 0;
                //dispatcherTimer.Start();
                show_find(Class1.selecting_path);
                //dispatcherTimer.Stop();
                Properties.Settings.Default.pr_disc = Class1.selecting_path.Split(':')[0]+":";
                Properties.Settings.Default.Save();
            }

        }

        

        private void Change_updateFolder(object sender, RoutedEventArgs e)
        {
            Count_image.IsEnabled = false;
            Change.IsEnabled = false;
            jpeg.IsEnabled = false;
            other.IsEnabled = false;
            //dispatcherTimer.Start();
            progreebar_continious(true, 1);
            ImagesDir.Text = Class1.other_message().wait[MainWindow.current_languare_index];
            progress.Value = 0;
            Sel_folder();
            show_picture();
            PhotosListBox.SelectedIndex = -1;
            Change.IsEnabled = true;
            Count_image.IsEnabled = true;
            jpeg.IsEnabled = true;
            other.IsEnabled = true;
            //dispatcherTimer.Stop();
            progreebar_continious(false, 1);
        }

        private void progreebar_continious(bool state, int timer)
        {

            Dispatcher.Invoke((Action)(() =>
            {
                switch (state)
                {
                    case true:
                        System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                        progress.Visibility = Visibility.Visible;
                        enable_button(false);

                        if (dispatcherTimer.IsEnabled == false)
                            dispatcherTimer.IsEnabled = true;
                        dispatcherTimer.Start();
                        dispatcherTimer.Tag = state.ToString();

                        switch (timer)
                        {
                            case 0:
                                //progress.IsIndeterminate = true;
                                break;

                            case 1:
                                if (Count_image.Value == 0)
                                {
                                    progress.Maximum = Class1.all_kol;
                                    progress.Minimum = 1;
                                }

                                else
                                {
                                    progress.Maximum = Count_image.Value;
                                    progress.Minimum = 1;
                                }
                                if (progress.IsIndeterminate == true)
                                    progress.IsIndeterminate = false;
                                break;

                            default:

                                break;
                        }

                        break;

                    case false:
                        System.Windows.Input.Mouse.OverrideCursor = null;
                        progress.Visibility = Visibility.Hidden;
                        enable_button(true);

                        if (progress.IsIndeterminate == true)
                            progress.IsIndeterminate = false;
                        {
                            dispatcherTimer.Stop();
                            dispatcherTimer.Tag = state.ToString();
                        }


                        switch (timer)
                        {
                            case 0:
                                //progress.IsIndeterminate = false;

                                break;

                            case 1:
                                //progress.IsIndeterminate = false;
                                //dispatcherTimer.Stop();
                                break;

                            default:

                                break;
                        }

                        break;

                }

            }));

            switch (state)
            {
                case true:
                    System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                    progress.Visibility = Visibility.Visible;
                    enable_button(false);

                    if (dispatcherTimer.IsEnabled == false)
                        dispatcherTimer.IsEnabled = true;
                    dispatcherTimer.Start();
                    dispatcherTimer.Tag = state.ToString();

                    switch (timer)
                    {
                        case 0:
                            //progress.IsIndeterminate = true;
                            break;

                        case 1:
                            if (Count_image.Value == 0)
                            {
                                progress.Maximum = Class1.all_kol;
                                progress.Minimum = 1;
                            }

                            else
                            {
                                progress.Maximum = Count_image.Value;
                                progress.Minimum = 1;
                            }
                            if (progress.IsIndeterminate == true)
                                progress.IsIndeterminate = false;
                            break;

                        default:

                            break;
                    }

                    break;

                case false:
                    System.Windows.Input.Mouse.OverrideCursor = null;
                    progress.Visibility = Visibility.Hidden;
                    enable_button(true);

                    if (progress.IsIndeterminate == true)
                        progress.IsIndeterminate = false;
                    //if (dispatcherTimer.Tag.ToString() == "True")
                    {
                        dispatcherTimer.Stop();
                        dispatcherTimer.Tag = state.ToString();
                    }


                    switch (timer)
                    {
                        case 0:
                            //progress.IsIndeterminate = false;
                            
                            break;

                        case 1:
                            //progress.IsIndeterminate = false;
                            //dispatcherTimer.Stop();
                            break;

                        default:

                            break;
                    }

                    break;

            }
            
        }

     

        private void SettingClick(object sender, MouseButtonEventArgs e)
        {
            location_window();

            browser_begin();
            pvTitle.browser_navigation = "file://" + Class1.path_out + Class1.begin_html;
            pvTitle.Visibility = Visibility.Visible;
        }



        private void Change_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            string OutOfMemory = "";
            Change.IsEnabled = false;
            Move.IsEnabled = false;
            Count_image.IsSelectionRangeEnabled = false;
            Count_image.IsEnabled = false;
            Change.IsEnabled = false;
            jpeg.IsEnabled = false;
            other.IsEnabled = false;
           
            CommonOpenFileDialog dialog_new = new CommonOpenFileDialog();
            dialog_new.Title = Class1.other_message().sel_folder_jpg[MainWindow.current_languare_index];
            if (Properties.Settings.Default.TimesRun == 0)
            {
                Properties.Settings.Default.TimesRun = 1;
                Properties.Settings.Default.Save();

                dialog_new.InitialDirectory = System.Environment.CurrentDirectory.Split(':')[0] + ":" + Class1.sel_dir;

            }
            else
            { dialog_new.InitialDirectory = Properties.Settings.Default.pr_disc + Class1.sel_dir;

            }
                
            dialog_new.IsFolderPicker = true;
            dialog_new.Multiselect = false;
            dialog_new.AllowNonFileSystemItems = false;

            if (dialog_new.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Oval.IsEnabled = false;
                Count_image.Value = 0;
                Count_image.Maximum = 100;

                //"Обновляем данные";
                Class1.selecting_path = dialog_new.FileName;

                progress.Value = 0;
                progreebar_continious(true, 1);


                Sel_folder();

            }
            show_picture();
            Count_image.Maximum = kol_image;
            Count_image.IsEnabled = true;
            Move.IsEnabled = true;
            jpeg.IsEnabled = true;
            other.IsEnabled = true;
            Change.IsEnabled = true;
            PhotosListBox.SelectedIndex = -1;
            status.Stroke = Brushes.DarkGray;

            Count_image.IsSelectionRangeEnabled = true;
            Count_image.SelectionStart = 10;


            if (Class1.OutOfMemory)
            {
                Class1.OutOfMemory = false;
                dispatcherTimer.Stop();
                string first = FindResource("Insufficient_memory_message").ToString().Replace("1", FindResource("Count_image").ToString().Split('-')[0].Trim());
                string first_end = first.Replace("2", FindResource("Folder_refresh").ToString().Trim());

                if (Class1.Count_OutOfMemory == "0")
                    OutOfMemory = FindResource("Count_image").ToString();
                else
                    OutOfMemory = Class1.Count_OutOfMemory.ToString();

                string two = FindResource("Insufficient_memory_message_1").ToString().Replace("1", FindResource("Count_image").ToString().Split('-')[0].Trim()) + " - " + OutOfMemory;
                System.Windows.MessageBox.Show(first_end + "'.\n" + two + ".", FindResource("Insufficient_memory_title").ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
                Count_image.Value = Int32.Parse( Class1.Count_OutOfMemory);
                Count_image.Maximum= Int32.Parse(Class1.Count_OutOfMemory);
                Count_image.SelectionEnd= Int32.Parse(Class1.Count_OutOfMemory);
                show_picture();
            }
            else
            {
                Count_image.Value = kol_image;
                Count_image.Maximum = kol_image;
                Count_image.SelectionEnd = kol_image;

            }
                progreebar_continious(false,1);
                
        }

        private void   All_OnMetadataClick(object sender, RoutedEventArgs e)
        {
            if (kol_image == 0)
            {

                return;
            }
            

            Count_image.Value = kol_image;

            if (System.Windows.MessageBox.Show(Class1.all_metadata().message[current_languare_index].Split('-')[0] + "- " + Class1.metadata_new + " " + Class1.all_metadata().message[current_languare_index].Split('-')[1] + Class1.selecting_path + Class1.sel_dir, Class1.all_metadata().title[current_languare_index], MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel)
            {
                return;
            }

            dispatcherTimer.IsEnabled=false;
            progress.IsIndeterminate = false;
            progress.Value = 0;

            if (kol_image <= 100)
                progress.Maximum = kol_image;
            else
                progress.Maximum = 100;

            progress.Visibility = Visibility.Visible;
            PhotosListBox.SelectedIndex = 0;
            ImagesDir.Tag = ImagesDir.Text;
            ImagesDir.Text = Class1.other_message().wait[MainWindow.current_languare_index];
            ffg = ImagesDir.Text;

            Class1.sel_img[1] = PhotosListBox.SelectedIndex.ToString();
            Class1.sel_img[0] = Name_file.Text;

            bw.RunWorkerAsync(Tuple.Create(kol_image));
            
        }


        private void OnMetadataClick(object sender, RoutedEventArgs e)
        {

            int LoM = 0;
            if (Name_file.Text == "")
                return;

            PhotoView.path_file = Name_file.Text;


            kj:
            //cтворюємо
            path_metadata.operacion(2, "");


            if (Class1.rez_oper_2 == false && LoM < 6)
            {
                LoM++;
                goto kj;
            }

            if (LoM > 5)
                System.Windows.MessageBox.Show(FindResource("rE_message").ToString(), FindResource("rE_title").ToString(), MessageBoxButton.OK, MessageBoxImage.Stop);

            if (PhotosListBox.Items.Count > Int32.Parse(Class1.sel_img[1]) + 1)
            {
                PhotosListBox.SelectedIndex = Int32.Parse(Class1.sel_img[1]) + 1;
                PhotosListBox_SelectionChanged(null, null);

            }

            if (PhotosListBox.Items.Count == Int32.Parse(Class1.sel_img[1]) + 1)
            {
                PhotosListBox.SelectedIndex = Int32.Parse(Class1.sel_img[1]);
                PhotosListBox_SelectionChanged(null, null);
            }
        }

        

     

        private void OnMovedClick(object sender, RoutedEventArgs e)
        {
            //умови виконання
            if (kol_image == 0)
                return;
            //dir
            if (System.Windows.MessageBox.Show(Class1.move_up().message[current_languare_index], Class1.move_up().title[current_languare_index] + " " + Class1.selecting_path.Split(':')[0] + ":\\" + Class1.albom + Class1.assembly(), MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                progreebar_continious(true,0);
                ImagesDir.Text = Class1.other_message().wait[MainWindow.current_languare_index];
                PhotosListBox.SelectedIndex = -1;
                move_image(true);
                progreebar_continious(false,0);
            }
            else
                return;

            update_dir();

            if (kol_image != 0)
            { }
            else
                cur_met.Text = FindResource("default Metadata").ToString();

        }

        private void Image_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            location_window();

            browser_begin();
            pvTitle.browser_navigation = "file://" + Class1.path_out + Class1.begin_html;
            pvTitle.Visibility = Visibility.Visible;
        }

        public bool IsLocked(string fileName)
        {
            int gg= PhotosListBox.SelectedIndex;
            try
            {
                using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    fs.Close();
                    Teek = 0;
                    status.Width = 200;
                    dispatcherTimer_blocked.Stop();

                    status.Stroke= Brushes.DarkSlateBlue;
                    status.ToolTip = "ok";
                    if (PhotosListBox.SelectedIndex == -1)
                        Move.IsEnabled = false;
                    else
                        state_move();

                    if (Class1.metadata_new == null)
                    {
                        Move1.ToolTip = FindResource("Metadane").ToString() + ": " + Class1.nothing_metadata[current_languare_index];
                        Move1.IsEnabled = false;
                    }

                    else
                    {
                        Move1.ToolTip = FindResource("Metadane").ToString() + ": " + Class1.metadata_new;
                        Move1.IsEnabled = true;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                dispatcherTimer_blocked.Start();

                if((status.Width - Teek * 0.5)>0)
                    status.Width = status.Width - Teek*0.5;
                else
                {
                    status.Width = 200;
                    dispatcherTimer_blocked.Stop();
                }


                Move.IsEnabled = false;
                Move1.IsEnabled = false;
                status.ToolTip = ex.Message;
                if (ex.HResult == -2147024894 || ex.HResult == -2147024864)
                {
                    status.Stroke = Brushes.Red;
                    
                }
                else
                {
                    status.Stroke = Brushes.Yellow;
                }
                return false;
            }

            
        }

        private void status_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(Name_file.Text!="")
            IsLocked(Class1.selecting_path + Class1.sel_dir + Name_file.Text);
        }

        private void Count_image_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Count_image.IsEnabled = false;
            jpeg.IsEnabled = false;
            other.IsEnabled = false;

            if (Count_image.Value < Count_image.SelectionStart)
                Count_image.Value = Count_image.SelectionStart;

            if (Count_image.Value > Count_image.SelectionEnd)
                Count_image.Value = Count_image.SelectionEnd;


            progress.Value = 0;
            Sel_folder();
            Count_image.IsEnabled = true;
            jpeg.IsEnabled = true;
            other.IsEnabled = true;
        }
    }
}