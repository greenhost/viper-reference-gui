using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace ViperClient
{
    class Tools
    {
        public static string AppDataPath {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Viper");
            }
        }

        public static bool MakeSureAppDataPathExists()
        {
            if (Directory.Exists(ViperClient.Tools.AppDataPath))
                return true;
            else
            {
                DirectoryInfo di = Directory.CreateDirectory(ViperClient.Tools.AppDataPath);
                return di.Exists;
            }
        }

        public static string[] GetConnectionNames()
        {
            string root = ViperClient.Tools.AppDataPath;
            string []dirs = Directory.GetDirectories( root );
            List<string> retval = new List<string>();
            foreach(string d in dirs) {
                retval.Add( Path.GetFileName(d) );
            }

            return retval.ToArray();
        }

        public static string GetConfigFromConnectionName(string name)
        {
            DirectoryInfo di = new DirectoryInfo(Path.Combine(ViperClient.Tools.AppDataPath, name));
            return Path.Combine(di.FullName, name + ".ovpn");
        }

        public static string GetLogFromConnectionName(string name)
        {
            DirectoryInfo di = new DirectoryInfo(Path.Combine(ViperClient.Tools.AppDataPath, name));
            return Path.Combine(di.FullName, "openvpn.log");
        }

        public static bool AddConnection(string name, string cfgpath) 
        {
            DirectoryInfo di = Directory.CreateDirectory(Path.Combine(ViperClient.Tools.AppDataPath, name));
            string dstFile = Path.Combine(di.FullName, name + ".ovpn");
            File.Copy(cfgpath, dstFile, true);
            return File.Exists( dstFile );
        }

        public static bool RemoveConnection(string name)
        {
            string path = Path.Combine(ViperClient.Tools.AppDataPath, name);
            Directory.Delete(path, true);
            DirectoryInfo di = new DirectoryInfo(path);
            return !di.Exists;
        }

        protected static string GetFileChecksum(string file)
        {
            using (FileStream stream = File.OpenRead(file))
            {
                var sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", String.Empty);
            }
        }

        public static string GetConnectionFingerprint(string name)
        {
            string cfgFile = ViperClient.Tools.GetConfigFromConnectionName(name);
            return ViperClient.Tools.GetFileChecksum(cfgFile);
        }

        /// <summary>
        /// Converts the specified DateTime to its relative date.
        /// </summary>
        /// <param name=”dateTime”>The DateTime to convert.</param>
        /// <returns>A string value based on the relative date
        /// of the datetime as compared to the current date.</returns>
        public static string ToFriendlyDate(DateTime dt)
        {
            var timeSpan = new TimeSpan(DateTime.UtcNow.Ticks - dt.Ticks);
 
            // span is less than or equal to 60 seconds, measure in seconds.
            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                return timeSpan.Seconds + " seconds ago";
            }

            // span is less than or equal to 60 minutes, measure in minutes.
            if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                return timeSpan.Minutes > 1
                    ? "about " + timeSpan.Minutes + " minutes ago" : "about a minute ago";
            }

            // span is less than or equal to 24 hours, measure in hours.
            if (timeSpan <= TimeSpan.FromHours(24))
            {
                return timeSpan.Hours > 1
                    ? "about " + timeSpan.Hours + " hours ago" : "about an hour ago";
            }

            // span is less than or equal to 30 days (1 month), measure in days.
            if (timeSpan <= TimeSpan.FromDays(30))
            {
                return timeSpan.Days > 1
                    ? "about " + timeSpan.Days + " days ago" : "about a day ago";
            }
            // span is less than or equal to 365 days (1 year), measure in months.
            if (timeSpan <= TimeSpan.FromDays(365))
            {
                return timeSpan.Days > 30
                    ? "about " + timeSpan.Days / 30 + " months ago" : "about a month ago";
            }
 
            // span is greater than 365 days (1 year), measure in years.
            return (timeSpan.Days > 365) ? "about " + timeSpan.Days / 365 + " years ago" : "about a year ago";
        }


    } // class
} // ns


/*
 * // from: http://stackoverflow.com/questions/2681878/associate-file-extension-with-application?rq=1
    public static void SetAssociation(string Extension, string KeyName, string OpenWith, string FileDescription)
    {
        RegistryKey BaseKey;
        RegistryKey OpenMethod;
        RegistryKey Shell;
        RegistryKey CurrentUser;

        BaseKey = Registry.ClassesRoot.CreateSubKey(Extension);
        BaseKey.SetValue("", KeyName);

        OpenMethod = Registry.ClassesRoot.CreateSubKey(KeyName);
        OpenMethod.SetValue("", FileDescription);
        OpenMethod.CreateSubKey("DefaultIcon").SetValue("", "\"" + OpenWith + "\",0");
        Shell = OpenMethod.CreateSubKey("Shell");
        Shell.CreateSubKey("edit").CreateSubKey("command").SetValue("", "\"" + OpenWith + "\"" + " \"%1\"");
        Shell.CreateSubKey("open").CreateSubKey("command").SetValue("", "\"" + OpenWith + "\"" + " \"%1\"");
        BaseKey.Close();
        OpenMethod.Close();
        Shell.Close();

        // Delete the key instead of trying to change it
        CurrentUser = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\.ovpn", true);
        CurrentUser.DeleteSubKey("UserChoice", false);
        CurrentUser.Close();

        // Tell explorer the file association has been changed
        SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
    }

    [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);
 
 */
