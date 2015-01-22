using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace ViperClient
{
    class OVPNLog
    {
        private string pathLogfile;

        public OVPNLog(string fname)
        {
            if (File.Exists(fname))
            {
                pathLogfile = fname;
            }
        }

        /// <summary>
        /// Tries to determine the last time a given OpenVPN connection was made succesfully based on the log entry.
        /// </summary>
        /// <returns>DateTime.MinValue when last connection is unknown. Correct DateTime of last successful connection.</returns>
        public DateTime LastConnection() {
            DateTime retval = DateTime.MinValue;

            using (StreamReader sr = File.OpenText(pathLogfile))
            {
                string s = "";
                // search for text "Initialization Sequence Completed"
                while ((s = sr.ReadLine()) != null)
                {
                    if (s.Contains("Initialization Sequence Completed"))
                    {
                        string dt = s.Replace("Initialization Sequence Completed", "");
                        dt = dt.Trim(); //.ToLower();
                        retval = DateTime.ParseExact(dt, "ddd MMM dd HH:mm:ss yyyy", null);  // Fri Jan 16 01:19:49 2015
                        //retval = DateTime.ParseExact(dt, "HH:mm:ss yyyy", CultureInfo.InvariantCulture);  // Fri Jan 16 01:19:49 2015
                    }
                }
            }
            return retval;
        }

    } // class
} // ns
