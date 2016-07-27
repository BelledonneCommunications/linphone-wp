﻿/*
BugCollector.cs
Copyright (C) 2015  Belledonne Communications, Grenoble, France
This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.
This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;

namespace Linphone.Model
{
    /// <summary>
    /// Collects and reports uncatched exceptions
    /// </summary>
    public class BugCollector
    {
        const string exceptionsFileName = "exceptions.log";

        internal static void LogException(Exception e, string extra)
        {
            try
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (TextWriter output = new StreamWriter(store.OpenFile(exceptionsFileName, FileMode.Append)))
                    {
                        DateTime now = DateTime.Now;
                        output.WriteLine("Date: {0:dddd, MMMM d, yyyy, HH:mm:ss}", now);
                        output.WriteLine("Type: {0}", extra);
                        output.WriteLine("Message: {0}", e.Message);
                        foreach (KeyValuePair<string, string> kvp in e.Data)
                        {
                            output.WriteLine("Data: Key= {0}, Value= {1}", kvp.Key, kvp.Value);
                        }
                        output.WriteLine("Stacktrace: {0}", e.StackTrace);
                        output.WriteLine("-------------------------");
                        output.Flush();
                        output.Close();
                    }
                }
            }
            catch (Exception) { }
        }

        internal static bool HasExceptionToReport()
        {
            try
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    return store.FileExists(exceptionsFileName);
                }
            }
            catch (Exception) { }

            return false;
        }

        internal static void ReportExceptions(string url)
        {
            try
            {
                string subject = "Logs report";
                string body = "";
                body += "Version of the app: " + Linphone.Version.Number;
                body += "\r\n--------------------\r\n";

                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (store.FileExists(exceptionsFileName))
                    {
                        using (TextReader input = new StreamReader(store.OpenFile(exceptionsFileName, FileMode.Open)))
                        {
                            subject = "Exception report";
                            body += input.ReadToEnd();
                            input.Close();
                        }
                    }
                }
                if (url != "")
                {
                    body += "\r\n" + url;
                }
                EmailComposeTask email = new EmailComposeTask();
                email.To = "linphone-wphone@belledonne-communications.com";
                email.Subject = subject;
                email.Body = body;
                email.Show();
                DeleteFile();
            }
            catch (Exception) { }
        }

        internal static void DeleteFile()
        {
            try
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    store.DeleteFile(exceptionsFileName);
                }
            }
            catch (Exception) { }
        }
    }
}
