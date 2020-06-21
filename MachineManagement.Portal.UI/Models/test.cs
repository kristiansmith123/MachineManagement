using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace MachineManagement.Portal.UI.Models
{
    public class test
    {
        public string RequestInfo(string remoteHost, string userName, string password)
        {
            m_szFeedback = "Feedback from: " + remoteHost + "\r\n";

            //  ProcessStartInfo psi = new ProcessStartInfo("echo y | C:\\plink\\plink.exe")
            ProcessStartInfo psi = new ProcessStartInfo(@"C:\Users\kristian.smith\Downloads\plink.exe")
            {
                Arguments = String.Format($"-ssh {remoteHost} -D 127.0.0.1:8889 -l {userName} -pw bi&&estofms175"),
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process p = Process.Start(psi);

            m_objLock = new Object();
            m_blnDoRead = true;

            AsyncReadFeedback(p.StandardOutput); // start the async read of stdout
            AsyncReadFeedback(p.StandardError); // start the async read of stderr

            StreamWriter strw = p.StandardInput;

            strw.WriteLine("exit"); // send exit command at the end

            p.WaitForExit(); // block thread until remote operations are done
            return m_szFeedback;
        }

        private String m_szFeedback; // hold feedback data
        private Object m_objLock; // lock object
        private Boolean m_blnDoRead; // boolean value keeping up the read (may be used to interrupt the reading process)

        public void AsyncReadFeedback(StreamReader strr)
        {
            Thread trdr = new Thread(new ParameterizedThreadStart(__ctReadFeedback));
            trdr.Start(strr);
        }

        private void __ctReadFeedback(Object objStreamReader)
        {
            StreamReader strr = (StreamReader)objStreamReader;
            string line;
            while (!strr.EndOfStream && m_blnDoRead)
            {
                line = strr.ReadLine();
                // lock the feedback buffer (since we don't want some messy stdout/err mix string in the end)
                lock (m_objLock) { m_szFeedback += line + "\r\n"; }
            }
        }
    }
}