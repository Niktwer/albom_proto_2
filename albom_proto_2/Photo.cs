using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace albom_proto_2
{

    /// <summary>
    /// This class describes a single photo - its location, the image and 
    /// the metadata extracted from the image.
    /// </summary>
    public class Photo
    {
        public static double i_hight, i_widtht, i_Deptht;
        public static string path_f;

        public bool yes_error(string file)
        {
            foreach (string gg in Class1.error_images)
            {
                if (file == gg)
                    return true;
            }
            return false;
        }

        public Photo(string path)
        {
            //int error_count=0;
            Class1.OutOfMemory = false;

            try
            {

                _path = path;
                _source = new Uri(path);
                //NOT lock


                _image = BitmapFrame.Create(_source, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                i_hight = _image.Height;
                i_widtht = _image.Width;
                _metadata = new ExifMetadata(_source);
                //new memory
                _image.Freeze();

            }

            catch(System.IO.FileFormatException)
            {
                string[] file_name = _source.ToString().Split('/');

                //foreach (string gg in Class1.error_images)
                //{
                //    if (file_name[file_name.Length - 1] == gg)
                //       goto err;
                //}
                if (yes_error(file_name[file_name.Length - 1]))
                    goto err;

                Class1.error_images[Class1.error_count] = file_name[file_name.Length - 1];
                Class1.error_count++;
                err:;
            }

            catch (System.ArgumentException e)
            {
                _image = BitmapFrame.Create(_source, BitmapCreateOptions.DelayCreation | BitmapCreateOptions.PreservePixelFormat | BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.Default);
                // BitmapCreateOptions.DelayCreation | BitmapCreateOptions.PreservePixelFormat | BitmapCreateOptions.IgnoreColorProfile
                i_hight = _image.Height;
                i_widtht = _image.Width;
                _metadata = new ExifMetadata(_source);
                string[] file_name = _source.ToString().Split('/');

                if (yes_error(file_name[file_name.Length - 1]))
                    goto err;

                Class1.error_images[Class1.error_count] = file_name[file_name.Length - 1];
                Class1.error_count++;
                err:;
            }

            catch (System.NotSupportedException d)
            {
                string[] file_name = _source.ToString().Split('/');

                if (yes_error(file_name[file_name.Length - 1]))
                    goto err;

                Class1.error_images[Class1.error_count] = file_name[file_name.Length - 1];
                Class1.error_count++;
                err:;
                _image = null;
                // BitmapCreateOptions.DelayCreation | BitmapCreateOptions.PreservePixelFormat | BitmapCreateOptions.IgnoreColorProfile
                i_hight = 0;
                i_widtht = 0;
                _metadata = null;

            }

            
            catch (System.OutOfMemoryException c)
            {
                //string[] file_name = _source.ToString().Split('/');
                //Class1.error_images[Class1.error_count] = file_name[file_name.Length - 1];
                //Class1.error_count++;
                //_image = null;
                //// BitmapCreateOptions.DelayCreation | BitmapCreateOptions.PreservePixelFormat | BitmapCreateOptions.IgnoreColorProfile
                //i_hight = 0;
                //i_widtht = 0;
                //_metadata = null;
                //System.Windows.MessageBox.Show(FindResource("Insufficient_memory_title").ToString() , FindResource("Insufficient_memory_message").ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
                string[] file_name = _source.ToString().Split('/');
                Class1.OutOfMemory = true;
                if (yes_error(file_name[file_name.Length - 1]))
                    goto err;
                
                Class1.error_images[Class1.error_count] = file_name[file_name.Length - 1];
                Class1.error_count++;
                err:;
                
                return;

            }


        }

        public override string ToString()
        {
            return _source.ToString();
        }

        private string _path;
        float file_lenght;
        private Uri _source, bitmap_source;

        //private static readonly ImageConverter _imageConverter = new ImageConverter();

        public string[] Source
        {
            get
            {
                path_f = _path;
                bitmap_source = new Uri(_path);

                file_lenght = new System.IO.FileInfo(_path).Length;
                string mod = new System.IO.FileInfo(_path).LastWriteTime.ToString();
                string[] source_file = { _path.Split('\\')[_path.Split('\\').Length - 1], (file_lenght / 1048576).ToString("F2"), mod };
                //file_lenght = new System.IO.FileInfo("Y:\\new_test\\t_1+.jpg").Length;
                //bitmap_source = new Uri("Y:\\new_test\\t_1+.jpg");
                return source_file;
            }

        }

        

        private BitmapFrame _image;
        public BitmapFrame Image { get { return _image; } set { _image = value; } }

        private ExifMetadata _metadata;
        public ExifMetadata Metadata { get { return _metadata; } }

    }

    /// <summary>
    /// This class represents a collection of photos in a directory.
    /// </summary>
    public class PhotoCollection : ObservableCollection<Photo>
    {
        public int kol;
        public static float file_lenght;
        public PhotoCollection() { }

        public PhotoCollection(string path) : this(new DirectoryInfo(path)) { }

        public PhotoCollection(DirectoryInfo directory)
        {
            _directory = directory;
            Update();
        }

        public string Path
        {
            set
            {
                _directory = new DirectoryInfo(value);
                Update();
            }
            get { return _directory.FullName; }
        }

        public DirectoryInfo Directory
        {
            set
            {
                _directory = value;
                Update();
            }
            get { return _directory; }
        }

        private void Update()
        {
            this.Clear();
            try
            {
                Class1.all_kol = 0;
                kol = 0;
                string ext_files;
                if (Class1.type_file)
                    ext_files = Class1.ext_files[0];
                //"*.jpg,*.jpeg";
                else
                    ext_files = Class1.ext_files[1];
                //"*.jpg,*.bmp,*.gif,*.jpeg,*.png,*.tiff";

                for (int j = 0; j <= ext_files.Split(',').Length - 1; j++)
                {

                    foreach (FileInfo fk in _directory.GetFiles(ext_files.Split(',')[j]))
                    {
                        Class1.all_kol++;

                    }
                }

                if (Class1.slider_value < Class1.all_kol && Class1.slider_value != 0)
                    Class1.all_kol = Class1.slider_value;

                for (int i = 0; i <= ext_files.Split(',').Length - 1; i++)
                {

                    foreach (FileInfo f in _directory.GetFiles(ext_files.Split(',')[i]))
                    {
                        if (kol>=Class1.all_kol && Class1.slider_value != 0)
                            return;

                        // размер файлу більше  або дорівнює 10 кілобайт
                        if (f.Length < 10240)
                            goto ver;


                        if (Class1.OutOfMemory)
                        {
                            Class1.Count_OutOfMemory = (Math.Round((decimal)kol / 10) * 10).ToString();
                            return;
                        }
                        else
                        {
                            Add(new Photo(f.FullName));
                            kol++;

                            Update_dir();
                        }
                            
                        

                        // no verification
                        ver:;
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {
                System.Windows.MessageBox.Show("No Such Directory");
            }
        }

        DirectoryInfo _directory;

        public void Update_dir()
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate { }));
        }

    }
}

