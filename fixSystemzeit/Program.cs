using Newtonsoft.Json;
using System;
using System.Net;

namespace fixSystemzeit
{
    internal class Program
    {
        public static DateTime FromUnixTime(string epoc)
        {
            DateTime exactDT = new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(Convert.ToDouble(epoc));
            return (exactDT.AddHours(1));//UTC + 1;
        }

        public static string getDatum(string epoc)
        {
            DateTime date = FromUnixTime(epoc);
            return date.ToString("dd.MM.yyyy");
        }

        public static string getTime(string epoc)
        {
            DateTime date = FromUnixTime(epoc);
            return date.ToString("HH:mm:ss");
        }

        private static void Main(string[] args)
        {
            var d = "";
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("https://www.uhrzeit.org/time/sync.php");

                dynamic stuff = JsonConvert.DeserializeObject(wc.DownloadString("https://www.uhrzeit.org/time/sync.php"));

                string epoc = stuff.time;

                var test1 = getDatum(epoc);
                var test2 = getTime(epoc);

                setDate(getDatum(epoc));

                setTime(getTime(epoc));
            }
        }

        private static void setDate(string Datum)
        {
            var proc = new System.Diagnostics.ProcessStartInfo();
            proc.UseShellExecute = true;
            proc.WorkingDirectory = @"C:\Windows\System32";
            proc.CreateNoWindow = true;
            proc.FileName = @"C:\Windows\System32\cmd.exe";
            proc.Verb = "runas";
            proc.Arguments = "/C date " + Datum;
            try
            {
                System.Diagnostics.Process.Start(proc);
            }
            catch
            {
                Console.WriteLine("Konnte Uhrzeit nicht fixen");
            }
        }

        private static void setTime(string Zeit)
        {
            var proc = new System.Diagnostics.ProcessStartInfo();
            proc.UseShellExecute = true;
            proc.WorkingDirectory = @"C:\Windows\System32";
            proc.CreateNoWindow = true;
            proc.FileName = @"C:\Windows\System32\cmd.exe";
            proc.Verb = "runas";
            proc.Arguments = "/C time " + Zeit;
            try
            {
                System.Diagnostics.Process.Start(proc);
            }
            catch
            {
                Console.WriteLine("Konnte Uhrzeit nicht fixen");
            }
        }
    }
}