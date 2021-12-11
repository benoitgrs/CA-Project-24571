using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Players
    {
        public string username { get; set; }

        public double balance { get; set; }

        public Players(string _username, double _balance)
        {
            username = _username;
            balance = _balance;
        }

        public void AddPlayerToFile()
        {
            string file = @"C:/Users/benoi/source/repos/BlackJack/players.txt";
            using (StreamWriter sw = File.AppendText(file))
            {
                sw.WriteLine($"{username}_{balance}");
            }
        }

        public void ChangeBalanceOnFile()
        {
            List<string> file = new List<string>();
            string way = @"C:/Users/benoi/source/repos/BlackJack/players.txt";
            using (StreamReader ReaderObject = new StreamReader(way))
            {
                string Line;
                while ((Line = ReaderObject.ReadLine()) != null)
                {
                    file.Add(Line);
                }
            }

            file[Program.VerifyPlayer(username)] = $"{username}_{balance}";
            File.Delete(way);            
            using (StreamWriter sw = new StreamWriter(way))
            {

                sw.WriteLine(file[0]);

            }
            for (int i = 1; i < file.Count; i++)
            {
                using (StreamWriter sw = File.AppendText(way))
                {
                    sw.WriteLine(file[i]);
                }
            }
        }
    }
}
