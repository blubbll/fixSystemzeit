using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace fixSystemzeit
{
    class Program
    {
        static void Main(string[] args)
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


             static void setDate(string dateInYourSystemFormat)
        {
            var proc = new System.Diagnostics.ProcessStartInfo();
            proc.UseShellExecute = true;
            proc.WorkingDirectory = @"C:\Windows\System32";
            proc.CreateNoWindow = true;
            proc.FileName = @"C:\Windows\System32\cmd.exe";
            proc.Verb = "runas";
            proc.Arguments = "/C date " + dateInYourSystemFormat;
            try
            {
                System.Diagnostics.Process.Start(proc);
            }
            catch
            {
                Console.WriteLine("Konnte Uhrzeit nicht fixen");
            }
        }
        static void setTime(string timeInYourSystemFormat)
        {
            var proc = new System.Diagnostics.ProcessStartInfo();
            proc.UseShellExecute = true;
            proc.WorkingDirectory = @"C:\Windows\System32";
            proc.CreateNoWindow = true;
            proc.FileName = @"C:\Windows\System32\cmd.exe";
            proc.Verb = "runas";
            proc.Arguments = "/C time " + timeInYourSystemFormat;
            try
            {
                System.Diagnostics.Process.Start(proc);
            }
            catch
            {
                Console.WriteLine("Konnte Uhrzeit nicht fixen");
            }
        }


        public static string getDatum(string epoc)
        {
            DateTime date = FromUnixTime(epoc);
            //return (date.Day + "." + date.Month + "." + date.Year);
            return date.ToString("dd.MM.yyyy");
        }

        public static string getTime(string epoc)
        {
            DateTime date = FromUnixTime(epoc);
            //return (date.Hour + ":" + date.Minute + ":" + date.Second + "," + date.Millisecond);
            return date.ToString("HH:mm:ss");
        }

        public static DateTime FromUnixTime(string unixTimeStamp)
        {
            DateTime exactDT = new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(Convert.ToDouble(unixTimeStamp));
            return (exactDT.AddHours(1));//UTC + 1;
        }

    }

}
