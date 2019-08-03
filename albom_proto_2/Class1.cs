using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using albom_proto_2.Properties;
using System.Reflection;
using System.Drawing;

namespace albom_proto_2
{
    class Class1
    {
        //public static string current_dir = System.Environment.CurrentDirectory ;
        //public static string current_dir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/";
        public static string current_dir = Environment.CurrentDirectory + "\\";
        //public static string Root = Android.OS.Environment.GetExternalStoragePublicDirectory("").ToString();
        //public static PhotoCollection Photos;
        public static string password_memory;
        public static int lan_memory;
        public static string file_secret;
        public static string secret_;
        public static bool run_continue = false;
        public static int select_moge = 0;
        public static string rezult_operacion;
        public static string albom = "Albom&";
        public static string metadata_new;
        public static bool metadata_status;
        public static bool type_file;
        public static string[] error_images;
        public static int error_count=0;
        public static string[] ext_files=new string[]{"*.jpg,*.jpeg","*.jpg,*.bmp,*.gif,*.jpeg,*.png,*.tif"};
        public static bool foto_click = false;
        //пароль
        public static  string user_name;
        public static bool OutOfMemory=false;
        public static string Count_OutOfMemory;

        //all files in selected folder
        public static int kol, all_kol;
        public static int slider_value;

        const string out_path = "Read_me";
        public static string path_out = System.AppDomain.CurrentDomain.BaseDirectory + out_path;

        public static double screenHeight = SystemParameters.FullPrimaryScreenHeight;
        public static double screenWidth = SystemParameters.FullPrimaryScreenWidth;
        public static double kor=(screenWidth- ((double)Application.Current.FindResource("main_widht")+ (double)Application.Current.FindResource("titlt_widht")))/2;

        public const string begin_html = "\\index.html";
        public const string total_resources = "zip";
        public const string reference_resource = "reference";

        //string[] activity_local = new string[] { "MainActivity", "lic", "d0" };

        
        public static bool type_extension = false;
        public static bool click_button = false;

        public string Image { get; set; }
        public string Text { get; set; }

        public static bool path_exist = true;
        public static bool rez_oper_3, rez_oper_2;

        public static string[] nothing_metadata = new string[] { "No metadata", "没有元数据", "कोई मेटाडेटा नहीं", "Sin metadatos", "Метаданные отсутствуют", "Метадані відсутні", "Brak metadanych" };

        // old dir
        public static string selecting_path="";
        // new selecting image and dir
        public static string[] sel_img = new string[2];
        public const string sel_dir = "\\";

        public struct Name_flag_languare
        {
            public string[] name_languare;
            public string[] kod_languare;
            public String[] flag;
        }

        public struct e_mail
        {
            public string[] avtor;
            public string[] link;
            public string[] about;
            public string[] message;
        }

        public struct rezult
        {
            public string[] find_ok;
            public string[] find_cancel;
            public string[] del_ok;
            public string[] del_cancel;
            public string[] show_ok;
            public string[] show_cancel;
            public string[] create_ok;
            public string[] create_cancel;
        }

        //public struct single
        //{
        //    public string[] title;
        //    public string[] message;
        //}

        public struct error_message
        {
            public string[] title;
            public string[] message;
            public string[] exo;
        }

        public struct update
        {
            public string[] wait;
            public string[] sel_folder;
            public string[] sel_folder_jpg;
            //public string[] exo;
        }

        public struct image
        {
            public string[] no_found;
            public string[] found_image;
            public string[] found_images;
        }


        public struct move_file
        {
            public string[] title;
            public string[] exist;
            public string[] delete;
            public string[] message;
            public string[] unexpected;
            public string[] failed;
        }

        public struct metadata_all
        {
            public string[] title;
            //public string[] exist;
            //public string[] delete;
            public string[] message;
            //public string[] unexpected;
            public string[] failed;
        }


        //public static single single_app()
        //{
        //    string[] title = new string[] { "", "", "", "", "", "", "" };
        //    string[] message = new string[] { "", "", "", "", "", "", "" };
        //}


        public static metadata_all all_metadata()
        {
            string[] title = new string[] { "Record metadata to files", "将元数据记录到文件", "फ़ाइलों के लिए मेटाडेटा रिकॉर्ड करें", "Grabar metadatos en archivos", "Запись метаданных в файлах", "Запис метаданих до файлів", "Zapisz metadane do plików" };
            string[] message = new string[] { "Confirm the metadata record - to the files in the folder -", "确认元数据记录 - 文件夹中的文件 - ", "मेटाडेटा रिकॉर्ड की पुष्टि करें - फ़ोल्डर में फ़ाइलों के लिए -", "Confirme el registro de metadatos - a los archivos en la carpeta -", "Подтвердите запись метаданных - к файлам в папке -", "Підтвердіть запис метаданих -  до файлів з папки -", "Potwierdź rekord metadanych - do plików w folderze -" };
            //string[] exist = new string[] { "The original file exists -", "原始文件存在 -", "मूल फ़ाइल मौजूद है -", "El archivo original existe -", "Исходный файл существует -", "Оригінальний файл існує -", "Oryginalny plik istnieje -" };
            //string[] delete = new string[] { ". Delete it ?", "。 删除它 ？", "। इसे मिटाओ ?", ". Удалить его ?", "", ". Видалити його ?", ". Usuń to ?" };
            //string[] unexpected = new string[] { "The original file still exists, which is unexpected.", "原始文件仍然存在，这是意外的。", "मूल फ़ाइल अभी भी मौजूद है, जो अप्रत्याशित है।", "El archivo original todavía existe, lo cual es inesperado.", "Исходный файл все еще существует, что является неожиданным.", "Оригінальний файл все ще існує, що є несподіваним.", "Oryginalny plik nadal istnieje, co jest nieoczekiwane." };
            string[] failed = new string[] { "The process failed:", "该过程失败：", "प्रक्रिया विफल:", "El proceso falló:", "Процесс завершился неудачно:", "Процес завершився невдало:", "Proces się nie powiódł:" };

            metadata_all[] metadata = new metadata_all[1];
            metadata[0].title = title;
            metadata[0].message = message;
            //metadata[0].exist = exist;
            //metadata[0].delete = delete;
            //metadata[0].unexpected = unexpected;
            metadata[0].failed = failed;
            return metadata[0];
        }


        public static move_file move_up()
        {
            string[] title = new string[] { "Moving files to a folder -", "将文件移动到文件夹 -", "फ़ाइलों को एक फ़ोल्डर में स्थानांतरित करना -", "Mover archivos a una carpeta -", "Перемещение файлов в папку -", "Переміщення файлів до папки -", "Przenoszenie plików do folderu -" };
            string[] message = new string[] { "Confirm the transfer of the selected files to the folder according to metadata", "根据元数据确认将所选文件传输到文件夹", "मेटाडेटा के अनुसार फ़ोल्डर में चयनित फ़ाइलों के स्थानांतरण की पुष्टि करें", "Confirme la transferencia de los archivos seleccionados a la carpeta de acuerdo con los metadatos", "Подтвердите перемещения выбранных файлов до папки согласно метаданных", "Підтвердіть переміщення вибраних файлів до папки згідно метаданих", "Potwierdź transfer wybranych plików do folderu zgodnie z metadanymi" };
            string[] exist = new string[] { "The original file exists -", "原始文件存在 -", "मूल फ़ाइल मौजूद है -", "El archivo original existe -", "Исходный файл существует -", "Оригінальний файл існує -", "Oryginalny plik istnieje -" };
            string[] delete = new string[] { ". Delete it ?", "。 删除它 ？", "। इसे मिटाओ ?", ". Удалить его ?", "", ". Видалити його ?", ". Usuń to ?" };
            string[] unexpected = new string[] { "The original file still exists, which is unexpected.", "原始文件仍然存在，这是意外的。", "मूल फ़ाइल अभी भी मौजूद है, जो अप्रत्याशित है।", "El archivo original todavía existe, lo cual es inesperado.", "Исходный файл все еще существует, что является неожиданным.", "Оригінальний файл все ще існує, що є несподіваним.", "Oryginalny plik nadal istnieje, co jest nieoczekiwane." };
            string[] failed = new string[] { "The process failed:", "该过程失败：", "प्रक्रिया विफल:", "El proceso falló:", "Процесс завершился неудачно:", "Процес завершився невдало:", "Proces się nie powiódł:" };

            move_file[] move = new move_file[1];
            move[0].title = title;
            move[0].message= message;
            move[0].exist = exist;
            move[0].delete = delete;
            move[0].unexpected = unexpected;
            move[0].failed = failed;
            return move[0];
        }


        public static e_mail About()
        {
            string[] avtor = new string[] { "Author: ", "著者： ", "लेखक: ", "Autor: ", "Автор: ", "Автор: ", "Autor: " };
            string[] link = new string[] { "Contact me - ", "关于该计划： ", "मुझसे संपर्क करें - ", "Contáctame - ", "Связаться со мной - ", "Зв'язатися зі мною - ", "Skontaktuj się ze mną - " };
            string[] about = new string[] { "About the program: ", "关于该计划： ", "कार्यक्रम के बारे में: ", "Sobre el programa: ", "О программе: ", "Про програму: ", "O programie: " };
            string[] message = new string[] { "About the program: ", "О программе: ", "À propos du programme: ", "Sobre el programa: ", "Über das Programm: ", "O programie: ", "Про програму: ", "プログラムについて： ", "关于该计划： " };

            e_mail[] Title = new e_mail[1];
            Title[0].avtor = avtor;
            Title[0].link = link;
            Title[0].about = about;
            Title[0].message = message;
            return Title[0];
        }

        public static update  other_message()
        {
            string[] wait = new string[] {"Wait! Data is updated. ", "等一下！ 数据已更新。", "प्रतीक्षा करें! डेटा अपडेट किया गया है।", "Espera! Los datos se actualizan", "Подождите! Данные обновляются.", "Зачекайте! Дані оновлюються.", "Zaczekaj! Dane są aktualizowane." };
            string[] sel_folder =new string[] { "Specify or create a folder in which the file will be stored.", "指定或创建将存储文件的文件夹。", "एक फ़ोल्डर निर्दिष्ट या बनाएँ जिसमें फ़ाइल संग्रहीत की जाएगी।", "Especifique o cree una carpeta en la que se almacenará el archivo.", "Укажите или создайте папку, в которой будет храниться файл.", "Вкажіть або створіть папку, в якій буде зберігатись файл.", "Określ lub utwórz folder, w którym plik będzie przechowywany." };
            string[] sel_folder_jpg = new string[] { "Select folder with jpg files","选择包含jpg文件的文件夹", "Jpg फ़ाइलों के साथ फ़ोल्डर का चयन करें", "Seleccionar carpeta con archivos jpg", "Выбрать папку с файлами jpg", "Виберіть папку з файлами jpg", "Wybierz folder z plikami jpg" };
            update[] other = new update[1];
            other[0].wait = wait;
            other[0].sel_folder = sel_folder;
            other[0].sel_folder_jpg = sel_folder_jpg;
            return other[0];
        }

            public static rezult secret_result()
        {
            string[] find_ok = new string[] { "a secret found.", "секрет найден.", "un secret trouvé.", "un secreto encontrado.", "ein Geheimnis gefunden.", "znaleziono sekret.", "секрет знайдено.", "秘密が見つかりました。", "发现了一个秘密。" };
            string[] find_cancel = new string[] { "Error! The secret was not found.", "Ошибка! Секрет не найден.", "Erreur! Le secret n'a pas été trouvé.", "Error! El secreto no fue encontrado.", "Fehler! Das Geheimnis wurde nicht gefunden.", "Błąd! Tajemnicy nie znaleziono.", "Помилка! Секрет не знайдено.", "エラー！ 秘密は見つかりませんでした。", "错误！ 秘密没有找到。" };
            string[] create_ok = new string[] { "a secret was created.", "секрет создан.", "un secret a été créé.", "un secreto fue creado.", "Ein Geheimnis wurde geschaffen.", "sekret został stworzony.", "секрет створено.", "秘密が作成されました。", "创造了一个秘密。" };
            string[] create_cancel = new string[] { "Error! The secret was not created.", "Ошибка! Секрет не удалось создать.", "Erreur! Le secret n'a pas été créé.", "Error! El secreto no fue creado.", "Fehler! Das Geheimnis wurde nicht erstellt.", "Błąd! Tajemnica nie została stworzona.", "Помилка! Секрет не вдалось створити.", "エラー！ 秘密は作成されませんでした。", "错误！ 秘密没有被创造。" };
            string[] del_ok = new string[] { "the secret is destroyed.", "секрет уничтожен.", "le secret est détruit.", "el secreto está destruido", "Das Geheimnis ist zerstört.", "sekret jest zniszczony.", "секрет знищено.", "秘密は破壊される。", "秘密被破坏。" };
            string[] del_cancel = new string[] { "Error! The secret was not destroyed.", "Ошибка! Секрет не удалось уничтожить.", "Erreur! Le secret n'a pas été détruit.", "Error! El secreto no fue destruido.", "Fehler! Das Geheimnis wurde nicht zerstört.", "Błąd! Sekret nie został zniszczony.", "Помилка! Секрет не вдалось знищити.", "エラー！ 秘密は破壊されませんでした。", "错误！ 秘密没有被破坏。" };
            string[] show_ok = new string[] { "The secret is in the folder:", "секрет находится в папке:", "Le secret est dans le dossier:", "El secreto está en la carpeta:", "Das Geheimnis ist in dem Ordner:", "Sekret znajduje się w folderze:", "секрет знаходиться в папці:", "秘密はフォルダにあります：", "秘密是在文件夹中：" };
            string[] show_cancel = new string[] { "Error! Could not show the secret.", "Ошибка!Не удалось показать секрет.", "Erreur! Impossible de montrer le secret.", "Error! No pudo mostrar el secreto.", "Fehler! Konnte das Geheimnis nicht zeigen.", "Błąd! Nie można pokazać sekretu.", "Помилка! Не вдалось показати секрет.", "エラー！ 秘密を表示できませんでした。", "错误！ 无法显示秘密。" };



            rezult[] Title = new rezult[1];
            Title[0].find_ok = find_ok;
            Title[0].find_cancel = find_cancel;
            Title[0].del_ok = del_ok;
            Title[0].del_cancel = del_cancel;
            Title[0].show_ok = show_ok;
            Title[0].show_cancel = show_cancel;
            Title[0].create_ok = create_ok;
            Title[0].create_cancel = create_cancel;
            return Title[0];
        }

        public static image other_image()
        {
            string[] no_found = new string[] { "(no found images)", "（没有找到图像）", "(कोई छवि नहीं मिली)", "(no se encontraron imágenes)", "(нет найденных изображений)", "(не знайдено зображень)", "(brak znalezionych obrazów)" };
            string[] found_image = new string[] { "(found image)", "（找到的图像）", "(छवि मिली)", "(imagen encontrada)", "(найдено изображение)", "(знайдено зображення)", "(znaleziony obraz)" };
            string[] found_images = new string[] { "(found  images - 2)", "（找到图片 - 2）", "(छवियों को मिला - 2)", "(imágenes encontradas - 2)", "(найдено изображений - 2)", "(знайдені зображення - 2)", "(znaleziono obrazy - 2)" };
            image[] other_image = new image[1];
            other_image[0].no_found = no_found;
            other_image[0].found_image = found_image;
            other_image[0].found_images = found_images;
            return other_image[0];
        }


        public static error_message messages(int number)
        {
            //Invalid password - 0
            string[] password_message = new string[] { "Password must have more than 4 characters but no more than 12 characters.", "Пароль должен иметь более 4 символа, но не более 12.", "Le mot de passe doit comporter plus de 4 caractères mais pas plus de 12 caractères.", "La contraseña debe tener más de 4 caracteres pero no más de 12 caracteres.", "Das Passwort muss aus mehr als 4 Zeichen, aber nicht mehr als 12 Zeichen bestehen.", "Hasło musi mieć więcej niż 4 znaki, ale nie więcej niż 12 znaków.", "Пароль повинен мати більш ніж 4 символа, але не більше ніж 12.", "パスワードは4文字以上12文字以下でなければなりません。", "密码必须超过4个字符但不超过12个字符。" };
            string[] password_title = new string[] { "Error! Invalid password.", "Ошибка! Неверный пароль.", "Erreur! Mot de passe invalide.", "Error! Contraseña inválida", "Fehler! Ungültiges Passwort", "Błąd! Nieprawidłowe hasło.", "Помилка! Невірний пароль.", "エラー！ パスワードが無効です。", "错误！ 密码无效。" };
            string[] password_exo = new string[] { "Choose another password!", "Выберите другой пароль!", "Choisissez un autre mot de passe!", "Elige otra contraseña!", "Wähle ein anderes Passwort!", "Wybierz inne hasło!", "Виберіть інший пароль!", "別のパスワードを選択してください！", "选择另一个密码" };

            //Enough free disk space - 1
            string[] free_disk_space_message = new string[] { "Free space on disk.To work the program is not less", "Освободите место на диске. Для работы программы необходимо не менее ", "Espace libre sur le disque. Travailler le programme n'est pas moins ", "Espacio libre en el disco. Para trabajar el programa no es menos ", "Freier Speicherplatz auf der Festplatte. Um das Programm zu arbeiten, ist nicht weniger ", "Wolne miejsce na dysku. Do pracy program nie jest mniejszy ", "Недостатньо місця на диску - ", "ディスクの空き容量 プログラムを動作させることは少なくない ", "磁盘上的可用空间。 工作的方案是不少的 " };
            string[] free_disk_space_title = new string[] { "Not enough free disk space - ", "Недостаточно свободного места на диске - ", "Pas assez d'espace disque libre - ", "No hay suficiente espacio libre en el disco ", "Nicht genügend freier Speicherplatz - ", "Za mało wolnego miejsca na dysku - ", "Для роботи рограми необхідно вільного місця не менше - ", "十分な空きディスク容量 -  ", "没有足够的可用磁盘空间 -  " };
            string[] free_disk_space_exo = new string[] { "Not enough free disk space", "Недостаточно свободного места на диске", "Pas assez d'espace disque libre", "No hay suficiente espacio libre en el disco", "Nicht genügend freier Speicherplatz", "Za mało wolnego miejsca na dysku", "Для роботи рограми необхідно вільного місця не менше", "十分な空きディスク容量 ", "没有足够的可用磁盘空间 " };

            //licensija no select
            string[] lic_message = new string[] { "For work it is necessary to review and confirm the consent with the license of the User!", "Для работы необходимо просмотреть и подтвердить согласие с лицензией пользователя!", "Pour le travail, il est nécessaire de vérifier et de confirmer le consentement avec la licence de l'utilisateur!", "¡Para el trabajo es necesario revisar y confirmar el consentimiento con la licencia del Usuario!", "Für die Arbeit ist es notwendig, die Zustimmung mit der Lizenz des Benutzers zu überprüfen und zu bestätigen!", "Do pracy należy przejrzeć i potwierdzić zgodę za pomocą licencji Użytkownika!", "Для роботи необхідно переглянути і підтвердити згоду з ліцензією Користувача!", "仕事のためには、ユーザーの使用許諾を確認し、同意を確認する必要があります！", "为了工作，有必要检查并确认用户许可的同意！" };
            string[] lic_title = new string[] { "Error! It is necessary to confirm the consent with the license of the User.", "Ошибка! Необходимо подтвердить согласие с лицензией пользователя.", "Erreur! Il est nécessaire de confirmer le consentement avec la licence de l'utilisateur.", "Error! Es necesario confirmar el consentimiento con la licencia del Usuario.", "Fehler! Es ist notwendig, die Zustimmung mit der Lizenz des Benutzers zu bestätigen.", "Błąd! Konieczne jest potwierdzenie zgody za pomocą licencji Użytkownika.", "Помилка! Необхідно підтвердити згоду з ліцензією Користувача.", "エラー！ 利用者の許諾を得て同意を得る必要があります。", "错误！ 有必要确认用户许可的同意。" };
            string[] lic_exo = new string[] { "", "", "", "", "", "", "", "", "" };


            error_message[] mess = new error_message[1];

            switch (number)
            {
                case 0:
                    mess[0].title = password_title;
                    mess[0].message = password_message;
                    mess[0].exo = password_exo;
                    break;
                case 1:
                    mess[0].title = free_disk_space_title;
                    mess[0].message = free_disk_space_message;
                    mess[0].exo = free_disk_space_exo;
                    break;
                case 3:
                    mess[0].title = lic_title;
                    mess[0].message = lic_message;
                    mess[0].exo = lic_exo;
                    break;
            }

            return mess[0];
        }


        public static Name_flag_languare Set_lan()
        {

            string[] languare = new string[] { "en", "zh", "hi", "es", "ru", "uk", "pl" };

            string[] flag_country = new string[languare.Length];
            string[] name = new string[languare.Length];

            for (int h = 0; h <= languare.Length - 1; h++)
            {
                flag_country[h] = "/flags/" + languare[h] + ".png";
            }


            for (int h = 0; h <= languare.Length - 1; h++)
            {
                System.Globalization.CultureInfo newCultureInfo = new System.Globalization.CultureInfo(languare[h]);
                name[h] = newCultureInfo.NativeName;
            }

            Name_flag_languare[] N_F_L = new Name_flag_languare[1];

            N_F_L[0].name_languare = name;
            N_F_L[0].kod_languare = languare;
            N_F_L[0].flag = flag_country;

            return N_F_L[0];
        }

        public static string assembly()
        {

            // name (user)
            //return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            //name (user)
            return Environment.UserName;
        }

        public string[] Disk(string current_dir, bool L)
        {
            string[] Property_disk = new string[2];
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo disk in allDrives)
            {
                if (disk.Name.ToString() == current_dir)
                {


                    Property_disk[0] = disk.VolumeLabel.ToString();
                    if (L)
                    {//IN

                        Property_disk[1] = disk.TotalSize.ToString();
                    }
                    else
                    {//OUT

                        Property_disk[1] = disk.TotalFreeSpace.ToString();
                    }

                }
            }
            return new string[] { Property_disk[0], Property_disk[1] };
        }

        public bool Read_write_file(bool read_write)
        {

            bool file_out = false;
            string[] disk_info = new string[2];
            disk_info = Disk(current_dir, true);


            switch (read_write)
            {

                // read
                case true:
                    {
                        if (System.IO.File.Exists(current_dir + Resources.ini_file))
                        {
                            string ini = System.IO.File.ReadAllText(current_dir + Resources.ini_file);
                            //string[] split = ini.Split('-');

                            if (ini == SHIFR.Shifrovka(disk_info[1], disk_info[0]))
                            { file_out = true; }
                            else { file_out = false; }
                            break;
                        }
                        else
                        { file_out = false; break; }

                    }
                //write
                case false:
                    {


                        string out_byte;
                        out_byte = SHIFR.Shifrovka(disk_info[1], disk_info[0]);
                        System.IO.File.WriteAllText(current_dir + Resources.ini_file, out_byte);

                        file_out = true; break;

                    }

            }
            return file_out;
        }


        /*public void message(Activity activity, int lan, int type)
        {

            switch (type)
            {
                case 0:
                    String message = About().avtor[lan] + activity.GetString(Resource.String.user) + ".\r\n" + About().link[lan] + activity.GetString(Resource.String.e_mail);

                    new AlertDialog.Builder(activity, AlertDialog.ThemeHoloLight)
                    .SetIcon(Resource.Drawable.message_about)
                    .SetTitle(About().about[lan] + activity.GetString(Resource.String.app_name) + " " + assembly())
                    .SetMessage(message)
                    .SetPositiveButton("OK", (sende, args) => { })
                    .Show();
                    break;

                case 1:
                    new AlertDialog.Builder(activity, AlertDialog.ThemeDeviceDefaultDark)
                    .SetIcon(Resource.Drawable.message_error)
                    .SetTitle(messages(0).title[lan])
                    .SetMessage(messages(0).message[lan])
                    .SetPositiveButton("OK", (sende, args) => { })
                    .Show();
                    break;

                case 2:
                    new AlertDialog.Builder(activity, AlertDialog.ThemeDeviceDefaultDark)
                    .SetIcon(Resource.Drawable.message_error)
                    .SetTitle(messages(1).title[lan] + (System.Environment.CurrentDirectory).Substring(0, 2))
                    .SetMessage(messages(1).message[lan] + activity.GetString(Resource.String.free_memory) + " Mb")
                    .SetPositiveButton("OK", (sende, args) => { })
                    .Show();
                    break;

                case 3:
                    new AlertDialog.Builder(activity, AlertDialog.ThemeDeviceDefaultDark)
                    .SetIcon(Resource.Drawable.message_error)
                    .SetTitle(messages(3).title[lan])
                    .SetMessage(messages(3).message[lan])
                    .SetPositiveButton("OK", (sende, args) => { })
                    .Show();
                    break;
                case 4:
                    String mess = current_dir + Resource.String.ini_file;
                    new AlertDialog.Builder(activity, AlertDialog.ThemeHoloLight)
                    .SetIcon(Resource.Drawable.message_about)
                    .SetTitle("Path lic")
                    .SetMessage(mess)
                    .SetPositiveButton("OK", (sende, args) => { })
                    .Show();
                    break;

                case 5:

                    new AlertDialog.Builder(activity, AlertDialog.ThemeHoloLight)
                    .SetIcon(Resource.Drawable.message_about)
                    .SetTitle("Path lic")
                    .SetMessage("read_write_lic")
                    .SetPositiveButton("OK", (sende, args) => { })
                    .Show();
                    break;



                    break;
            }
        }*/

        public static string[] move_files()
        {
            return new string[] { move_up().title[MainWindow.current_languare_index], move_up().message[MainWindow.current_languare_index] };
        }


        public static string[] all_metadata_files()
        {
            return new string[] { all_metadata().title[MainWindow.current_languare_index], all_metadata().message[MainWindow.current_languare_index] };
        }



        public static string[] show()
        {
            return new string[] { (About().avtor[MainWindow.current_languare_index] + " NikTwer" + ". " + About().link[MainWindow.current_languare_index] + " NikTwer@Yahoo.com"), About().about[MainWindow.current_languare_index] };
        }

        static void ConcatenateFiles(string outputFile, string inputFile)
        {
            using (Stream output = System.IO.File.OpenWrite(outputFile))
            {
                using (Stream input = System.IO.File.OpenRead(inputFile))
                {
                    input.CopyTo(output);
                }
            }
        }

        public static void copy_resources_to_disc(string name)
        {
            

            //создаем Assembly
            Assembly _assembly = Assembly.GetExecutingAssembly();
            Stream _Stream = _assembly.GetManifestResourceStream(name);

            string[] Y = name.Split('.');
            string OUT_NAME = Y[Y.Length - 2];

            if (Y[Y.Length - 1] == "html")
            {
                using (var fileStream = File.Create(path_out + "\\" + OUT_NAME + ".html"))
                {
                    _Stream.Seek(0, SeekOrigin.Begin);
                    _Stream.CopyTo(fileStream);
                }
            }

            else
            {
                switch (Y[Y.Length - 1])
                {
                    case "jpg":
                        Image im = new Bitmap(_Stream);
                        //сохраняем рисунок в файле, который будем использовать в html коде браузера
                        im.Save(path_out + "\\" + OUT_NAME + ".jpg");
                        break;
                }
            }

        }



        // all end
    }

}
