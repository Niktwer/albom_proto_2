using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace albom_proto_2
{
    class FileAssociation
    {
        public FileAssociation()
        { }

        [DllImport("Shlwapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint AssocQueryString(AssocF flags, AssocStr str, string pszAssoc, string pszExtra,
        [Out] StringBuilder pszOut, [In][Out] ref uint pcchOut);



        public string Get(string doctype)
        {
            uint pcchOut = 0;

            AssocQueryString(AssocF.Verify, AssocStr.Executable, doctype, null, null, ref pcchOut);

            StringBuilder pszOut = new StringBuilder((int)pcchOut);

            AssocQueryString(AssocF.Verify, AssocStr.Executable, doctype, null, pszOut, ref pcchOut);

            string doc = pszOut.ToString();

            return doc;
        }

        [Flags]

        public enum AssocF

        {

            Init_NoRemapCLSID = 0x1,

            Init_ByExeName = 0x2,

            Open_ByExeName = 0x2,

            Init_DefaultToStar = 0x4,

            Init_DefaultToFolder = 0x8,

            NoUserSettings = 0x10,

            NoTruncate = 0x20,

            Verify = 0x40,

            RemapRunDll = 0x80,

            NoFixUps = 0x100,

            IgnoreBaseClass = 0x200

        }



        public enum AssocStr

        {

            Command = 1,

            Executable,

            FriendlyDocName,

            FriendlyAppName,

            NoOpen,

            ShellNewValue,

            DDECommand,

            DDEIfExec,

            DDEApplication,

            DDETopic

        }
    }
}
