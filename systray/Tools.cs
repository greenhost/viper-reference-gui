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

        protected static string GetConfigFromConnectionName(string name)
        {
            DirectoryInfo di = new DirectoryInfo(Path.Combine(ViperClient.Tools.AppDataPath, name));
            return Path.Combine(di.FullName, name + ".ovpn");
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
