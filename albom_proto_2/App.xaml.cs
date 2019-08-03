using System;
using System.Windows;
using System.Windows.Data;
using System.Xml;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace albom_proto_2
{
    public partial class app : Application
    { 
        private static List<CultureInfo> m_Languages = new List<CultureInfo>();

        public static List<CultureInfo> Languages
        {
            get
            {
                return m_Languages;
            }
        }

        public app()
        {
            InitializeComponent();
            app.LanguageChanged += app_LanguageChanged;

            m_Languages.Clear();

            foreach (string lan in Class1.Set_lan().kod_languare)
            {
                m_Languages.Add(new CultureInfo(lan));
            }
            //m_Languages.Add(new CultureInfo("en")); //Нейтральная культура для этого проекта
            //m_Languages.Add(new CultureInfo("ru"));


            Language = albom_proto_2.Properties.Settings.Default.DefaultLanguage;
        }

        //Евент для оповещения всех окон приложения
        public static event EventHandler LanguageChanged;

        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;

                //1. Меняем язык приложения:
                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                //2. Создаём ResourceDictionary для новой культуры
                ResourceDictionary dict = new ResourceDictionary();
                switch (value.Name)
                {
                    case "ru":
                        dict.Source = new Uri(String.Format("Resources/lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    case "uk":
                        dict.Source = new Uri(String.Format("Resources/lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    case "es":
                        dict.Source = new Uri(String.Format("Resources/lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    case "pl":
                        dict.Source = new Uri(String.Format("Resources/lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    case "zh":
                        dict.Source = new Uri(String.Format("Resources/lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    case "hi":
                        dict.Source = new Uri(String.Format("Resources/lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    default:
                        dict.Source = new Uri("Resources/lang.xaml", UriKind.Relative);
                        break;
                }

                //3. Находим старую ResourceDictionary и удаляем его и добавляем новую ResourceDictionary
                ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                                              where d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang.")
                                              select d).First();
                if (oldDict != null)
                {
                    int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                    Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                }

                //4. Вызываем евент для оповещения всех окон.
                LanguageChanged(Application.Current, new EventArgs());
            }
        }

        private void app_LanguageChanged(Object sender, EventArgs e)
        {
            albom_proto_2.Properties.Settings.Default.DefaultLanguage = Language;
            albom_proto_2.Properties.Settings.Default.Save();
        }


        void OnApplicationStartup(object sender, StartupEventArgs args)
        {

            MainWindow mainWindow = new MainWindow();
            //mainWindow.Visibility= Visibility.Hidden;
            mainWindow.Show();
            mainWindow.Photos = (PhotoCollection)(Resources["Photos"] as ObjectDataProvider).Data;
            //mainWindow.Photos.Path = Environment.CurrentDirectory + "\\images";
            //Title_Window title= new Title_Window();
            //title.Show();
        }




    }
}
