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
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Polytunnel");
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

        public static string GetFriendlyDate(DateTime d)
        {
	        // 1.
	        // Get time span elapsed since the date.
	        TimeSpan s = DateTime.Now.Subtract(d);

	        // 2.
	        // Get total number of days elapsed.
	        int dayDiff = (int)s.TotalDays;

	        // 3.
	        // Get total number of seconds elapsed.
	        int secDiff = (int)s.TotalSeconds;

	        // 4.
	        // Don't allow out of range values.
	        if (dayDiff < 0 || dayDiff >= 31)
	        {
	            return null;
	        }

	        // 5.
	        // Handle same-day times.
	        if (dayDiff == 0)
	        {
	            // A.
	            // Less than one minute ago.
	            if (secDiff < 60)
	            {
		        return "just now";
	            }
	            // B.
	            // Less than 2 minutes ago.
	            if (secDiff < 120)
	            {
		        return "1 minute ago";
	            }
	            // C.
	            // Less than one hour ago.
	            if (secDiff < 3600)
	            {
		        return string.Format("{0} minutes ago",
		            Math.Floor((double)secDiff / 60));
	            }
	            // D.
	            // Less than 2 hours ago.
	            if (secDiff < 7200)
	            {
		        return "1 hour ago";
	            }
	            // E.
	            // Less than one day ago.
	            if (secDiff < 86400)
	            {
		        return string.Format("{0} hours ago",
		            Math.Floor((double)secDiff / 3600));
	            }
	        }

	        // 6.
	        // Handle previous days.
	        if (dayDiff == 1)
	        {
	            return "yesterday";
	        }
	        if (dayDiff < 7)
	        {
	            return string.Format("{0} days ago",
		        dayDiff);
	        }
	        if (dayDiff < 31)
	        {
	            return string.Format("{0} weeks ago",
		        Math.Ceiling((double)dayDiff / 7));
	        }
	        return null;
            }
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
