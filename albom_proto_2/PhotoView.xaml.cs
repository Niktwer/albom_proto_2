using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Windows.Input;

namespace albom_proto_2
{
    public partial class PhotoView : Window
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr HWnd, GetWindow_Cmd cmd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, [Out] StringBuilder lParam);

        const int WM_SETTEXT = 12;
        const int WM_GETTEXT = 13;

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_STYLE = -16;

        private const int WS_MAXIMIZEBOX = 0x10000; //maximize button
        private const int WS_MINIMIZEBOX = 0x20000; //minimize button

        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_CLOSE = 0xF060;

        public enum GetWindow_Cmd : uint
        {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
            GW_ENABLEDPOPUP = 6,
            WM_GETTEXT = 0x000D
        }

        public static int current_languare_index;
        public PhotoCollection Photos;
        Photo _photo;
        ImageSource img_original, img_oper;
        public static string current_dir,current_metadata;
        string root, disc;
        bool sel_yesno = false;
        public static string path_file;

        public PhotoView()
        {
            InitializeComponent();
            this.SourceInitialized += MainWindow_SourceInitialized;
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

            Class1.foto_click = true;

            
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


        public Photo SelectedPhoto
        {
            get { return _photo; }
            set { _photo = value; }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            ViewedPhoto.Source = _photo.Image;
            string[] mm = ViewedCaption.Content.ToString().Split(':');

            if(_photo.Image!=null)
            path_file = Regex.Split((_photo.Image.ToString()), "///")[1];
            disc = path_file.Split(':')[0];
            //ViewedCaption.Content = ViewedCaption.Content.ToString() + " " + path_file;
            this.Title = this.Title + " - " + path_file;
            if (current_metadata!=null)
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(_photo.Image.ToString());
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

            //end test
            img_original = bitmap;
            img_oper = img_original;

            root = disc + ":\\" + Class1.albom + Class1.assembly();


                //if (current_metadata.Length != 0)
                //{ root_albom.Content = FindResource("SI_4").ToString() + " " + current_metadata; }
                ////FindResource("Folder").ToString();
                //else

                if (Class1.metadata_new == null)
                    root_albom.Text = root_albom.Text + " " + root;
                else
                    root_albom.Text = FindResource("SI_4").ToString() + " " + Class1.metadata_new;
            //root;

            B_Reset.IsEnabled = false;

            }
                this.Title = this.Title + ". "+ FindResource("SI_4").ToString()+" "+ current_metadata ;

            //test

           
        }

        public static BitmapImage BitmapFromUri(Uri source)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = source;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            return bitmap;
        }

        //private void Reset(object sender, RoutedEventArgs e)
        //{
        //    ViewedPhoto.Source = img_original;
        //    img_oper = img_original;
        //    B_Reset.IsEnabled = false;
        //}

        //private void Crop_up(object sender, RoutedEventArgs e)
        //{
        //    BitmapSource img = (BitmapSource)(img_oper);

        //    int halfWidth = img.PixelWidth / 2;
        //    int halfHeight = img.PixelHeight / 2;
        //    img_oper = BitmapFrame.Create(new CroppedBitmap(img, new Int32Rect((halfWidth - (halfWidth / 2)), (halfHeight - (halfHeight / 2)), halfWidth, halfHeight)));
        //    B_Reset.IsEnabled = true;
        //    ViewedPhoto.Source = img_oper;
        //}

        //private void Rotate(object sender, RoutedEventArgs e)
        //{
        //    BitmapSource img = (BitmapSource)img_oper;

        //    CachedBitmap cache = new CachedBitmap(img, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
        //    img_oper = BitmapFrame.Create(new TransformedBitmap(cache, new RotateTransform(90.0)));
        //    B_Reset.IsEnabled = true;
        //    ViewedPhoto.Source = img_oper;

        //}

        private void MetadataImageClick(object sender, RoutedEventArgs e)
        {
                            //press button
                            Class1.click_button = true;
                            close_browserfolder();
            if (e!=null)
            {
                            
                            unlock();
            }
        }


        private void  unlock()
        {
            Object folder = new Form();
            System.IO.Directory.CreateDirectory(root);

            var shellType = Type.GetTypeFromProgID("Shell.Application");
            var shell = Activator.CreateInstance(shellType);

            CultureInfo lang = new CultureInfo(Class1.Set_lan().kod_languare[PhotoView.current_languare_index]);

            folder = shellType.InvokeMember("BrowseForFolder", BindingFlags.InvokeMethod, null, shell, new object[] { 0, Class1.other_message().sel_folder[PhotoView.current_languare_index], 0, @root + Class1.sel_dir }, null, lang, null);


            if (folder != null)
            {
                var title = folder.GetType().InvokeMember("Self", BindingFlags.GetProperty, null, folder, null);


                Class1.metadata_new = title.GetType().InvokeMember("Path", BindingFlags.GetProperty, null, title, null) as string;
                change_metadata();

                if (Class1.metadata_new.Length > 260)
                {
                    System.Windows.MessageBox.Show("The selected path length contains more than 260 characters.", "The path is very long", MessageBoxButton.OK);
                    Class1.metadata_new = null;
                    change_metadata();
                    return;
                }

                Class1.metadata_status = (bool)Metadata_checkbox.IsChecked;
            }
            else
            {
                //if (Class1.metadata_new = null && Class1.metadata_new = (FindResource("write metadat").ToString() + root))
                //Class1.metadata_new = null;
                //change_metadata();
                Class1.metadata_status = false;
                return; // User clicked cancel

            }


        }
        
        

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int LoM=0;
            //створємо або видаляємо метадані
            sel_yesno = true;
            //close_browserfolder();
            MetadataImageClick(null,null);

            Secret path_metadata = new Secret();
            

            switch (Metadata_checkbox.IsChecked)
            {
                case true:
                    //видаляємо
                    Class1.metadata_new = null;
                    change_metadata();
                    Secret.Path = path_file;
                    path_metadata.operacion(3, "");
                    break;
                case false:
                    
                    //cтворюємо
                    if (Class1.metadata_new != null)
                    {
                        kj:
                        path_metadata.operacion(2, "");
                        if (Class1.rez_oper_2 == false && LoM<6)
                            {
                                LoM++;
                                goto kj;
                        }

                        if(LoM > 5)
                         System.Windows.MessageBox.Show(FindResource("rE_message").ToString(), FindResource("rE_title").ToString() , MessageBoxButton.OK,MessageBoxImage.Stop);

                    }
                    
                    break;
            }

            MainWindow.update_metadata = true;
            Class1.foto_click = false;

            //string pattern = FindResource("Title_main").ToString();

            //foreach (Window window in System.Windows.Application.Current.Windows)
            //{
            //    //matches = Regex.Matches(window.Title, pattern);
            //    //matches = rx.Matches(window.Title);
            //    string[] jj = Regex.Split(window.Title, pattern);


            //    if (jj.Length > 1)
            //    {
            //        window.WindowState = WindowState.Normal;
                    
            //    }
            //}

            //this.WindowState = WindowState.Minimized;
        }

        //private void Window_Closed(object sender, EventArgs e)
        //{ }

        private void Metadata_checkbox_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)Metadata_checkbox.IsChecked)
            {
                Metadata.IsEnabled = false;
                //Class1.metadata_new = null;
                root_albom.Text = FindResource("write metadat").ToString() + " " + root;
                //root_albom.Content + " " +root;
            }

        }

        private void Metadata_checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            //if (!(bool)Metadata_checkbox.IsChecked)
                Metadata.IsEnabled = true;
                change_metadata();
        }

        private void close_browserfolder()
        {
            var allWindows = TopLevelWindowUtils.FindWindows(w => w.GetWindowText() != "");
            string title_name;

            foreach (WindowHandle windowHandle in allWindows)
            {
                title_name = windowHandle.GetWindowText();
                find_window(windowHandle.RawPtr, title_name);
            }
        }

        private void MetadataImageClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //press button
            Class1.click_button = true;
            close_browserfolder();
            if (e != null)
            {

                unlock();
            }
        }

        private void Crop_up(object sender, RoutedEventArgs e)
        {
            BitmapSource img = (BitmapSource)(img_oper);

            int halfWidth = img.PixelWidth / 2;
            int halfHeight = img.PixelHeight / 2;
            img_oper = BitmapFrame.Create(new CroppedBitmap(img, new Int32Rect((halfWidth - (halfWidth / 2)), (halfHeight - (halfHeight / 2)), halfWidth, halfHeight)));
            B_Reset.IsEnabled = true;
            ViewedPhoto.Source = img_oper;
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            ViewedPhoto.Source = img_original;
            img_oper = img_original;
            B_Reset.IsEnabled = false;
        }

        private void Rotate(object sender, RoutedEventArgs e)
        {
            BitmapSource img = (BitmapSource)img_oper;

            CachedBitmap cache = new CachedBitmap(img, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            img_oper = BitmapFrame.Create(new TransformedBitmap(cache, new RotateTransform(90.0)));
            B_Reset.IsEnabled = true;
            ViewedPhoto.Source = img_oper;
        }

        

        private void BUTTON_Click(object sender, RoutedEventArgs e)
        {
            var allWindows = TopLevelWindowUtils.FindWindows(w => w.GetWindowText() != "");
            string title_name;

            foreach (WindowHandle windowHandle in allWindows)
            {
                title_name = windowHandle.GetWindowText();
                find_window(windowHandle.RawPtr, title_name);
            }
        }

        private void find_window(IntPtr ptr, string title_n)
        {
            string[] gg = new string[16];
            IntPtr[] child = new IntPtr[15];

            child[0] = GetWindow(ptr, GetWindow_Cmd.GW_CHILD);

            StringBuilder title = new StringBuilder(90);

            for (int i = 1; i <= 14; i++)
            {
                child[i] = GetWindow(ptr, GetWindow_Cmd.GW_HWNDNEXT);

                /* ! */
                SendMessage(child[i - 1], Convert.ToInt32(GetWindow_Cmd.WM_GETTEXT), (IntPtr)title.Capacity, title);

                gg[i] = title.ToString();
            }

            for (int ii = 0; ii <= gg.Length - 1; ii++)
            {
                if (gg[ii] == Class1.other_message().sel_folder[PhotoView.current_languare_index])
                {
                    int iHandle = FindWindow(null, title_n);
                    StringBuilder sb = new StringBuilder(0);
                    SendMessage((IntPtr)iHandle, WM_SYSCOMMAND, (IntPtr)SC_CLOSE,  sb);
                    return;
                }

            }

        }


        private void change_metadata()
        {
            if ( Class1.metadata_new!=null)
            { root_albom.Text = FindResource("SI_4").ToString() + " " + Class1.metadata_new; }
            else
            { root_albom.Text = root_albom.Text + " " + root; }
        }
        //Class1.metadata_new.Length != 0 &&
    }
}
