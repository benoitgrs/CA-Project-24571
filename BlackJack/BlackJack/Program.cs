using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:/Users/benoi/source/repos/BlackJack/players.txt";
            List<Players> listPlayers = new List<Players>();
            if (File.Exists(path))
            {
                listPlayers = RecreateListOfPlayers();
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine("Players List :");

                }
            }

            Console.WriteLine("starting");
            string x = "";
            while (x != "x")
            {
                if (x == "1")
                {
                    Console.WriteLine("please enter your username");
                    string username = Console.ReadLine();
                    int indice = VerifyPlayer(username);
                    if (indice!=0)
                    {
                        Cards card = new Cards(2);
                        for(int i = 0; i < 4; i++)
                        {
                            Console.WriteLine();
                            for(int j = 0; j < 13; j++)
                            {
                                if (i == 2)
                                {
                                    Console.Write(card.cards[i, j]);
                                }
                                else
                                {
                                    Console.Write(card.cards[i, j] + "      ");
                                }
                            }
                        }
                        Console.ReadKey();

                        Game game = new Game(listPlayers[indice-1]);
                        game.Play();
                    }
                    else
                    {
                        Console.WriteLine("wrong username, try again");
                    }
                }
                if (x == "2")
                {
                    Console.WriteLine("enter your username and then how much money you want to put in");
                    string username = Console.ReadLine();
                    double balance = Convert.ToDouble(Console.ReadLine());
                    Players player = new Players(username, balance);
                    player.AddPlayerToFile();
                    listPlayers.Add(player);
                    Console.WriteLine("done, now login");

                }
                if (x == "3")
                {
                    Console.WriteLine("please enter your username");
                    string username = Console.ReadLine();
                    int indice = VerifyPlayer(username);
                    if (indice != 0)
                    {
                        Console.WriteLine("Do you want: 1: add\t2: withraw\tkey: to quit");
                        string v = Console.ReadLine();
                        Players player = listPlayers[indice - 1];
                        if (v == "1")
                        {
                            Console.WriteLine("How much do you want to add?");
                            double amount = Convert.ToDouble(Console.ReadLine());
                            player.balance += amount;
                            player.ChangeBalanceOnFile();
                            Console.WriteLine($"done, your new balance is now: {player.balance}");
                        }
                        if (v == "2")
                        {
                            Console.WriteLine("Your balance account is at: " + player.balance);
                            Console.WriteLine("How much do you want to withraw?");
                            double amount = Convert.ToDouble(Console.ReadLine());
                            if (amount <= player.balance)
                            {
                                player.balance -= amount;
                                player.ChangeBalanceOnFile();
                                Console.WriteLine($"done, your new balance is now: {player.balance}");

                            }
                            else { Console.WriteLine("you don't have enough money on your account!"); }
                            
                            
                        }
                        Console.ReadKey();
                        Console.Clear();
                    }

                }
                Console.Clear();
                Console.WriteLine("1: login\t2: register\t3: add or withdraw money on your account\tx: to quit");
                x = Console.ReadLine();

            }
            
        }
        public static int VerifyPlayer(string username)
        {
            int verify = 0;
            List<string> liste = new List<string>();
            string path = @"C:/Users/benoi/source/repos/BlackJack";
            string way = path + "/" + "players.txt";     //+nom fichier liste
            using (StreamReader ReaderObject = new StreamReader(way))
            {
                string Line;
                while ((Line = ReaderObject.ReadLine()) != null)
                {
                    liste.Add(Line);
                }
            }
            for (int j = 1; j < liste.Count(); j++)
            {
                string[] attributs = liste[j].Split('_');
                if (username.Equals(attributs[0]))
                {
                    verify = j;
                }
                
            }
            return verify;
        }
        
        public static List<Players> RecreateListOfPlayers()
        {
            List<Players> listPlayers = new List<Players>();
            List<string> liste = new List<string>();
            string path = @"C:/Users/benoi/source/repos/BlackJack";
            string way = path + "/" + "players.txt";     //+nom fichier liste
            using (StreamReader ReaderObject = new StreamReader(way))
            {
                string Line;
                while ((Line = ReaderObject.ReadLine()) != null)
                {
                    liste.Add(Line);
                }
            }
            for (int j = 1; j < liste.Count(); j++)
            {
                string[] attributs = liste[j].Split('_');
                string username = attributs[0];
                double balance = Convert.ToDouble(attributs[1]);
                Players player = new Players(username,balance);
                listPlayers.Add(player);
            }
            return listPlayers;
        }
    }
}
