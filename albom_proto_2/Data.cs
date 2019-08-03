using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;


namespace albom_proto_2
{
    //public enum ExposureMode
    //{
    //    Manual,
    //    NormalProgram,
    //    AperturePriority,
    //    ShutterPriority,
    //    LowSpeedMode,
    //    HighSpeedMode,
    //    PortraitMode,
    //    LandscapeMode,
    //    Unknown
    //}


    public class ExifMetadata
    {
        BitmapMetadata _metadata;
        private Uri bitmap_source;

        public ExifMetadata(Uri imageUri)
        {
            BitmapFrame frame = BitmapFrame.Create(imageUri, BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
            _metadata = (BitmapMetadata)frame.Metadata;

        }

        private decimal ParseUnsignedRational(ulong exifValue)
        {
            return (decimal)(exifValue & 0xFFFFFFFFL) / (decimal)((exifValue & 0xFFFFFFFF00000000L) >> 32);
        }

        private decimal ParseSignedRational(long exifValue)
        {
            return (decimal)(exifValue & 0xFFFFFFFFL) / (decimal)((exifValue & 0x7FFFFFFF00000000L) >> 32);
        }

        private object QueryMetadata(string query)
        {
            try
            {
                if (_metadata.ContainsQuery(query))
                    return _metadata.GetQuery(query);
                else
                    return null;

            }
            catch
            { return null; }
            
        }

        public uint? Width
        {
            get
            {
                object val = QueryMetadata("/app1/ifd/exif/subifd:{uint=40962}");
                if (val == null)
                {
                    return (uint)Photo.i_widtht;
                }
                else
                {
                    if (val.GetType() == typeof(UInt32))
                    {
                        return (uint?)val;
                    }
                    else
                    {
                        return System.Convert.ToUInt32(val);
                    }
                }
            }
        }

        public uint? Height
        {
            get
            {
                object val = QueryMetadata("/app1/ifd/exif/subifd:{uint=40963}");
                if (val == null)
                {
                    return (uint)Photo.i_hight;
                }
                else
                {
                    if (val.GetType() == typeof(UInt32))
                    {
                        return (uint?)val;
                    }
                    else
                    {
                        return System.Convert.ToUInt32(val);
                    }
                }
            }
        }

            public uint?  Deptht
        {
            
            get
            {
                try
                {
                    bitmap_source = new Uri(Photo.path_f);
                    Image file_image = Image.FromFile(Photo.path_f);
                    //BitmapFrame.Create(bitmap_source);
                    object val = Image.GetPixelFormatSize(file_image.PixelFormat);
                    //QueryMetadata("/app1/ifd/exif/subifd:{uint=40961}");
                    file_image.Dispose();
                    if (val == null)
                    {
                        return (uint)Photo.i_Deptht;
                    }
                    else
                    {
                        if (val.GetType() == typeof(UInt32))
                        {
                            return (uint?)val;
                        }
                        else
                        {
                            return System.Convert.ToUInt32(val);
                        }
                    }
                    
                }

                catch(System.OutOfMemoryException j)
                {
                    Environment.FailFast(String.Format("Out of Memory: {0}",j.Message));
                    return null;
                }
            }
        }

        public DateTime? DateImageTaken
        {
            get
            {
                string date;
                object val = QueryMetadata("/app1/ifd/exif/subifd:{uint=36867}");
                if (val == null)
                {
                    return null;
                }
                else
                {
                    if ((string)val == "0000:00:00 00:00:00")
                        date = new DateTime().ToString();
                    else                       //string h=(DateTime)val.ToString("fff");
                        date = (string)val;
                    try
                    {
                        return new DateTime(
                            int.Parse(date.Substring(0, 4)),    // year
                            int.Parse(date.Substring(5, 2)),    // month
                            int.Parse(date.Substring(8, 2)),    // day
                            int.Parse(date.Substring(11, 2)),   // hour
                            int.Parse(date.Substring(14, 2)),   // minute
                            int.Parse(date.Substring(17, 2))    // second
                        );
                    }
                    catch (FormatException)
                    {
                        return null;
                    }
                    catch (OverflowException)
                    {
                        return null;
                    }
                    catch (ArgumentNullException)
                    {
                        return null;
                    }
                    catch (NullReferenceException)
                    {
                        return null;
                    }
                }
            }
        }

    

        //comment 2
        public string Placing
        {
            get
            {
                return null;
            }
        }

        //comment1
        public string About
        {
            get
            {
                string jj = "ggggggggggggggggggggggggggggggggg hhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh hhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh";
                return jj;
            }
        }

    }
}
