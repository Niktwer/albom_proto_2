using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;



namespace albom_proto_2
{
    // створення секрету

    class Secret
    {
        private long[] number = new long[2];

        //TimeSpan SpanTime;

        //пароль Class1.user_name
        public static string password;// = "sd12345";
        long linecount_search;
        //шлях до файлу metadata
        public static string Path;
        string metadata =Class1.metadata_new;// = "d:/1/_1.pdf";
        public static string rez;
        /*public static string secret_;*/// = "d:/1/secret.mp3";
        // файл photoview.path_file
        static string[] par_file;
        static string  end_symbol = "?";
        string par = "";
        //string  password_kod;
        public int y = 0, op = 0;
        private const int DelayOnRetry = 300;
        public long nr;
        DateTime time_begin, time_stop;
        //private System.Timers.Timer tick_tak = new System.Timers.Timer();
    
        //знаходимо метадані

        ////new
        string[] kod = new string[2];


        public Secret()
        {
            password_update();

        }
        // два рази
        private void password_update()
        {
            if (Class1.user_name == null)
                password = Class1.assembly();
            else
                password = Class1.user_name;
            //Class1.password_memory;

            kod = new string[] { verife_kod(SHIFR.Shifrovka(password, password)), verife_kod(SHIFR.Shifrovka(end_symbol, password)) };


        }


        private string verife_kod(string old_kod)
        {
            string new_kod =  "";
            char[,] symbols = new char[8, 2] { { '+', '&' }, { '*', ':' }, { '?', ',' }, { '|', '-' }, { '$', '%' }, { '(', '<' }, { ')', '>' }, { '\\', '/' } };

            //for (int i = 0; i < old_kod.Length; i++)
            //{
                foreach (char h in old_kod)
                {
                    switch (h)
                    {
                        case '+':
                            new_kod = new_kod + '&';
                            break;

                        case '*':
                            new_kod = new_kod + ':';
                            break;
                        case '?':
                            new_kod = new_kod + ',';
                            break;
                        case '|':
                            new_kod = new_kod + '-';
                            break;
                        case '$':
                            new_kod = new_kod + '%';
                            break;
                        case '(':
                            new_kod = new_kod + '<';
                            break;
                        case ')':
                            new_kod = new_kod + '>';
                            break;
                        case '\\':
                            new_kod = new_kod + '/';
                            break;
                        default:
                            new_kod = new_kod + h;
                            break;
                    }


                }
            //}
            return new_kod;
        }


        private string FoundIt(string path)
        //знаходимо початок секрету
        {
            //string[] koor_metadata = new string[] { SHIFR.Shifrovka(password, password), SHIFR.Shifrovka("?", password) };
            find(path);
            return Find_secret(path, kod, linecount_search);
        }

        private string Find_secret(string path, string[] kod, long begin_find)
        {
            

            //verife kod
            //kod=verife_kod(kod);

            //ALTERNATIVE
            String Alter = "",jjk="";

            //begin
            //if (path.Length < 10240)
            //    return jjk;

            using (FileStream stream_ = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                //using (StreamReader streamReader = new StreamReader(stream))
                //{
                //знаходимо початок пошуку в залежності від розміру
                if (stream_.CanSeek)
                {
                    stream_.Seek(begin_find, SeekOrigin.Begin);
                    //begin_find
                }

                //TEST
                byte[] b = new byte[(int)stream_.Length - (int)begin_find];

                //while (fs.Read(b, 0, b.Length) > 0)
                //{
                //    Console.WriteLine(temp.GetString(b));
                //}

                stream_.Read(b, 0, (int)stream_.Length - (int)begin_find);

                Alter = System.Text.Encoding.Default.GetString(b);

                Regex regex = new Regex(kod[0]);
                //MatchCollection matches = regex.Matches(Alter);
                if (regex.IsMatch(Alter))
                {
                    string ff = regex.Split(Alter)[1];
                    Regex regex_end = new Regex(kod[1]);
                    if (regex_end.IsMatch(ff))
                    {
                       jjk = regex_end.Split(ff)[0];
                    }

                }
                stream_.Close();
                return jjk;

                //    string HJ = "", pr = "";
                //    int yy = 0, i = 0, t = 0;
                //    char c0;

                //    // створюємо потік
                //    using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                //    {

                //        using (StreamReader streamReader = new StreamReader(stream))
                //        {
                //            //знаходимо початок пошуку в залежності від розміру
                //            if (streamReader.BaseStream.CanSeek)
                //            {
                //                streamReader.BaseStream.Seek(begin_find, SeekOrigin.Begin);
                //                //begin_find
                //            }

                //            while (streamReader.Peek() >= 0)
                //            {
                //                c0 = (char)streamReader.Read();
                //                yy++;

                //                switch (t)
                //                {
                //                    // співпадає пароль
                //                    case 0:
                //                        if (kod[t].Substring(i, 1) == c0.ToString())
                //                        {
                //                            if (i == (kod[t].Length - 1))
                //                            {
                //                                number[0] = begin_find + yy - kod[t].Length - 1;

                //                                t = 1;
                //                                i = -1;

                //                            }

                //                            i++;
                //                        }
                //                        break;

                //                    case 1:
                //                        // шукаємо ключ

                //                        if (kod[t].Substring(i, 1) == c0.ToString())
                //                        {
                //                            pr = pr + c0.ToString();

                //                            if (i == (kod[t].Length - 1))
                //                            {
                //                                number[1] = begin_find + yy - 1;
                //                                goto end;

                //                            }
                //                            i++;
                //                        }

                //                        else
                //                        {
                //                            i = 0;
                //                            HJ = HJ + pr + c0.ToString();
                //                            pr = "";

                //                        }

                //                        break;
                //                }

                //            }


                //        }
                //    }
                //    end:
                //    return HJ;

                
            }


        }
        private void find(string path)

        //де починати пошук допоміжна функція
        {
            // створюємо потік
            using (FileStream stream_find = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader streamReader = new StreamReader(stream_find))
                {
                    //знаходимо початок пошуку в залежності від розміру
                   long linecount = streamReader.BaseStream.Length;
                    //if (linecount > 160064)
                    //      linecount_search = true_find(linecount-16000);

                    //else
                    //      linecount_search = true_find(linecount );


                    //1mg
                    if (linecount > 10048576)
                        linecount_search = true_find( linecount - 100000);
                    else if (linecount > 1048576)
                        linecount_search = true_find(linecount - 10000);
                    else
                        linecount_search = true_find(linecount - 1000);
                }

                stream_find.Close();
            }
        }


         


        //1mg
        //if (linecount > 10048576)
        //    linecount_search = linecount - 100000;
        //else if (linecount > 1048576)
        //    linecount_search = linecount - 10000;
        //else
        //    linecount_search = linecount - 1000;


        

        private long true_find(long find)
        {
            float my_find = find / (float)4;
            while (my_find % 1 != 0)
            {
                find--;
                my_find = find / (float)4;
            }
            return find;
        }

        private void move_image(string input, string output)
        {
            try
            {
                string []out_file=input.Split('\\');
                string output_file = output + Class1.sel_dir + out_file[out_file.Length - 1];

                if(input==output_file)
                {
                    System.Windows.MessageBox.Show(Class1.move_up().exist[MainWindow.current_languare_index] + " " + out_file[out_file.Length - 1] , Class1.move_up().title[MainWindow.current_languare_index] + output, MessageBoxButton.OK);
                    return;
                }

                FileInfo fileInf = new FileInfo(input);

                if (fileInf.Exists)
                {
                    if (!System.IO.Directory.Exists(output))
                        System.IO.Directory.CreateDirectory(output);

                    if (System.IO.File.Exists(output_file))
                    {
                        if (System.Windows.MessageBox.Show(Class1.move_up().exist[MainWindow.current_languare_index] + " " + out_file[out_file.Length - 1] + " " + Class1.move_up().delete[MainWindow.current_languare_index], Class1.move_up().title[MainWindow.current_languare_index] + output, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            File.Delete(output_file);
                        else
                            return;
                    }

                    //    // Move the file

                    //var fileName = (string)e.Argument;
                    var fsize = fileInf.Length;
                    var bytesForPercent = fsize / 100;

                    using (var inputFs = new FileStream(input, FileMode.Open, FileAccess.Read))
                    using (var outputFs = new FileStream(output_file, FileMode.Create, FileAccess.Write))
                    {
                        int counter = 0;
                        while (inputFs.Position < inputFs.Length)
                        {
                            byte b = (byte)inputFs.ReadByte();
                            outputFs.WriteByte(b);
                            counter++;

                            //if (counter % bytesForPercent == 0)
                            //    bgwCopyFile.ReportProgress(counter / (int)bytesForPercent);
                        }
                    }

                    //    //using (var stream = File.Move(output_file, input))
                    //    //{
                    //    //    // Use stream
                    //    //}
                    //Thread.Sleep(DelayOnRetry);
                    ////    //GC.Collect();
                    //fileInf.MoveTo(output_file);

                }


                // See if the original exists now.
                if (File.Exists(input))
                    System.Windows.MessageBox.Show(Class1.move_up().unexpected[MainWindow.current_languare_index], Class1.move_up().title[MainWindow.current_languare_index] + output, MessageBoxButton.OK);
                
                non:;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(Class1.move_up().failed[MainWindow.current_languare_index] + " {0}" + e.ToString(), Class1.move_up().title[MainWindow.current_languare_index] + output, MessageBoxButton.OK);
            }

            

        }


        public void  operacion(int oper, string root)

            //операції з секретом
        {
            //шлях до файлу
            //string dir = root; 
            //Class1.current_dir;

            //файл з яким працюємо
            //Path = PhotoView.path_file;

            password_update();

            //if (Class1.user_name == null)
            //    password = Class1.assembly();
            //else
            //    password = Class1.user_name;
            ////Class1.password_memory;

            //kod = new string[] { SHIFR.Shifrovka(password, password), SHIFR.Shifrovka(end_symbol, password) };


            switch (oper)
            {
                case 0:
                    //move files for oper=0 root=selected file
                    //find disc
                    string output= root.Split(':')[0]+":"+SHIFR.DeShifrovka(FoundIt(root), password);
                    move_image(@root, @output);
                    break;

                case 1:
                    //find metadata

                    //файл з яким працюємо
                    //Path = root;

                    string rez_find = FoundIt(root);
                    
                    if (rez_find == "")
                        rez = Class1.nothing_metadata[MainWindow.current_languare_index];
                    else
                        rez= SHIFR.DeShifrovka(rez_find, password);

                    break;

                case 2:

                    //create metedata
                    byte[] data_secret, array;

                    //файл з яким працюємо
                    //old
                    Path = PhotoView.path_file;

                    //new
                    Path = Class1.selecting_path + Class1.sel_dir + Class1.sel_img[0];

                    Class1.rez_oper_2 = false;


                    if (FoundIt(Path) != "")
                    {
                        // is old metadata oper #3
                        del_secret();

                    }

                    // no old metadata

                    //OLD
                    //PhotoView.path_file

                     using (FileStream stream_2 = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            using (StreamReader streamReader = new StreamReader(stream_2))
                            {
                                //byte[] 
                                data_secret = new byte[streamReader.BaseStream.Length];
                                stream_2.Read(data_secret, 0, data_secret.Length);

                                // преобразуем строку в байты                        
                                array = System.Text.Encoding.Default.GetBytes(verife_kod(SHIFR.Shifrovka(password, password)) + SHIFR.Shifrovka(Class1.metadata_new.Split(':')[1], password)+ verife_kod(SHIFR.Shifrovka("?", password)));

                            }
                        stream_2.Close();
                        }
rep:
                    try
                    {
                        
                            using (FileStream fstream_2 = new FileStream(Path, FileMode.Open, FileAccess.Write, FileShare.Write))
                            {
                             fstream_2.Write(data_secret, 0, data_secret.Length);
                             fstream_2.Write(array, 0, array.Length);
                            fstream_2.Close();
                            }

                        Class1.rez_oper_2 = true;
                    }
                    catch(InvalidCastException e)
                    {
                        if(ErrorIsSharingViolation(e))
                            Thread.Sleep(DelayOnRetry);
                        goto rep;

                    }

                    
                    catch (System.IO.IOException g)
                    {
                        //Thread.Sleep(DelayOnRetry);
                        //goto rep;

                    }



                        break;

                case 3:
                    Class1.rez_oper_3 = false;

                    //delete old metadata
                    if (FoundIt(Path)!="")
                    del_secret();
                    Class1.rez_oper_3 = true;
                    break;

            }
        }

        static bool ErrorIsSharingViolation(Exception exception)
        {
            const int ERROR_SHARING_VIOLATION = unchecked((int)0xFFFFFFFF80070020);
            return exception.GetType() == new IOException().GetType() && Marshal.GetHRForException(exception) ==
                ERROR_SHARING_VIOLATION;
        }

        private byte[] step1_massiv()
        {
            try
            {
                using (FileStream stream_massiv = new FileStream(  Path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    byte[] data = new byte[stream_massiv.Length];
                    stream_massiv.Read(data, 0, (int)stream_massiv.Length);
                    stream_massiv.Close();
                    return data;
                }
            }
            catch
            {
                byte[] error = new byte[0];
                return error;
            }
        }

        private void del_secret()
        {

            int t = 0, k = 0;
            long begin = 0, end = 0;


            //1. begin
            byte[] n = step1_massiv();

            // already there
           // kod = new string[] { SHIFR.Shifrovka(password, password), SHIFR.Shifrovka(end_symbol, password) };

            //2.find secret
            while (k <= n.Length - 1  && t != 2)
                //- kod[1].Length
            {
                if ((char)n[k] == char.Parse(kod[t].Substring(0, 1)))
                {

                    //2 read massiv
                    string kl = "";
                    for (int j = k; j <= kod[t].Length - 1 + k; j++)
                    {
                        kl = kl + (char)n[j];
                    }

                    //3.verife key
                    if (kl == kod[t])
                    {
                        if (t == 0)
                        {
                            begin = k;
                            //new test
                            k++;
                            kl = "";
                            t = 1;
                        }
                        else
                        {
                            end = k + kod[t].Length;
                            t = 2;
                        }
                    }
                    else
                    {
                        kl = "";
                        k++;
                    }
                }
                else
                {
                    k++;
                }
            }

            //4. out_data

            if (end - begin > 0)
            {
                byte[] data_ = new byte[begin + (n.Length - end)];

                for (long fg = 0; fg <= begin - 1; fg++)
                {
                    data_[fg] = n[fg];
                }

                if (n.Length - end > 0)
                {
                    long dh = begin;
                    for (long fg = end; fg <= n.Length - 1; fg++)
                    {
                        data_[dh] = n[fg];
                        dh++;
                    }

                }

                //5. save file

                step5_save(Path, data_);

            }
        }


        private void step5_save(string path, byte[] data_)
        {
            int tp = 0;
            repit:
            try
            {
                
                if (File.Exists(@path))
                {
                    Thread.Sleep(DelayOnRetry);
                    StreamWriter Writer = new StreamWriter(path, false, Encoding.UTF8);
                    Writer.WriteLine("");
                    Writer.Close();

                    using (FileStream fstream_step = new FileStream(path, FileMode.Open, FileAccess.Write, FileShare.Write))
                    {
                        fstream_step.Write(data_, 0, data_.Length);
                        fstream_step.Close();
                    }
                }

                else
                {
                    using (FileStream fstream_step = File.Create(@path))
                    {
                        fstream_step.Write(data_, 0, data_.Length);
                        fstream_step.Close();
                    }
                }
            }

            catch (InvalidCastException e)
            {
                //error
                MessageBox.Show("IOException source: " + e.Source, "Error save", MessageBoxButton.OK);
            }

            catch (System.IO.IOException f)
            {
                //tp++;
                //Thread.Sleep(DelayOnRetry);
                //goto repit;

            }
        }

        //end all

    }


}

