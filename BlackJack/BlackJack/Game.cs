using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Game
    {
        public Players player { get; set; }
        public Cards cardset { get; set; }        
        public Stack<Card> cardgame { get; set; }
        public int bet { get; set; }
        public int[,] tab1 { get; set; }
        public int[,] tab2 { get; set; }
        public int[,] tab3 { get; set; }
        public Game(Players _player)
        {
            bet = 0;
            cardgame = new Stack<Card>();
            player = _player;
            Console.WriteLine("play");
            Console.ReadKey();
            string w = "";
            while (w != "x")
            {
                Console.Clear();
                Console.WriteLine("Whith how many set of playing cards do you want to play?");
                w = Console.ReadLine();
                if (w.All(char.IsDigit))
                {
                    int number = Convert.ToInt32(w);
                    cardset = new Cards(number);
                    w = "x";
                }
                else
                {
                    Console.WriteLine("please enter a number");
                }
                Console.Clear();
                Shuffle();
                
            }
            Tables();
        }
        public void Shuffle()
        {                       
            while (cardset.cardGame.Count != 0)
            {
                Random rdm = new Random();
                int sh = rdm.Next(0, cardset.cardGame.Count);
                cardgame.Push(cardset.cardGame[sh]);
                cardset.cardGame.RemoveAt(sh);
            }
        }
        public void Play()
        {
            string fan = "";
            while (fan != "x")
            {
                if (cardgame.Count < 12)
                {
                    Console.WriteLine("insufficiant amount of cards, new game");
                    Console.WriteLine("How many set of games do you want?");
                    int count = Convert.ToInt32(Console.ReadLine());
                    Game game = new Game(player);
                }
                
                while (bet > player.balance/2||bet==0)
                {
                    Console.WriteLine("your balance is at " + player.balance+" euros.");
                    Console.WriteLine("How much do you want to play?");
                    string _bet = Console.ReadLine();
                    if (_bet.All(char.IsDigit))
                    {
                        bet = Convert.ToInt32(_bet);
                    }
                    else 
                    {
                        bet = 0; 
                        Console.WriteLine("not a valid amount");
                        Console.ReadKey();
                    }
                    
                }
                
                List<Card> cardsCroupier =CardsCroupier();
                Console.ReadKey();
                List<Card> cardsPlayer =CardsPlayerInitial();
                Affichage(cardsPlayer, cardsCroupier, 0);
                int somme = cardsPlayer[0].value + cardsPlayer[1].value;
                if (cardsPlayer[0].access == cardsPlayer[1].access)
                {
                    Console.WriteLine("do you want to Split? 1: yes\t 2: no");
                    string lan = Console.ReadLine();
                    if (lan == "1")
                    {
                        List<Card> cardsPlayer2 = new List<Card>();
                        cardsPlayer2.Add(cardsPlayer[1]);
                        cardsPlayer.RemoveAt(1);
                        Card card1 = cardgame.Pop();
                        cardsPlayer.Add(card1);
                        Card card2 = cardgame.Pop();
                        cardsPlayer2.Add(card2);
                        Console.WriteLine("game 1");
                        Classic(cardsPlayer, cardsCroupier);
                        Console.ReadKey();
                        Console.WriteLine("game 2");
                        Classic(cardsPlayer2, cardsCroupier);


                    }
                    else 
                    {
                        Classic(cardsPlayer, cardsCroupier);
                    }

                }
                else if (somme == 9 || somme == 10 || somme == 11)
                {
                    Console.WriteLine("Do you want to double your bet? 1: yes\t 2: no");
                    string t = Console.ReadLine();
                    if (t == "1")
                    {
                        Card card1 = cardgame.Pop();
                        cardsPlayer.Add(card1);
                        bet *= 2;
                        Affichage(cardsPlayer, cardsCroupier, 1);
                        End(cardsCroupier, cardsPlayer);
                    }
                    else
                    {
                        Classic(cardsPlayer, cardsCroupier);
                    }
                }
                else
                {
                    Classic(cardsPlayer, cardsCroupier);
                }





                //End(cardsCroupier, cardsPlayer);

                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Any Key: To Play Again\tx: To Stop");
                fan = Console.ReadLine();
                bet = 0;
            }
        }
        public void End(List<Card> cardsCroupier, List<Card> cardsPlayer)
        {
            Console.ReadKey();
            int scoreCroupier = 0;
            int aces = 0;
            for (int i = 0; i < cardsCroupier.Count; i++)
            {
                if (cardsCroupier[i].value == 11) { aces++; }
                scoreCroupier += cardsCroupier[i].value;
                if (aces != 0 && scoreCroupier > 21)
                {
                    aces--;
                    scoreCroupier -= 10;
                }
            }
            int ace = 0;
            int scorePlayer = 0;
            for (int i = 0; i < cardsPlayer.Count; i++)
            {
                if (cardsPlayer[i].value == 11) { ace++; }
                scorePlayer += cardsPlayer[i].value;
                if (ace != 0 && scorePlayer > 21)
                {
                    ace--;
                    scorePlayer -= 10;
                }
            }

            double money = 0;
            Console.WriteLine($"your score: {scorePlayer}\tCroupier score: {scoreCroupier}");
            if (scorePlayer == 21)
            {

                if (cardsPlayer.Count == 2)
                {
                    Console.WriteLine("BlackJack");
                    money = bet * 3 / 2;
                }
                else
                {
                    Console.WriteLine("you won");
                    money = bet * 2;
                }
                player.balance += money;
            }
            if (scorePlayer > 21)
            {
                Console.WriteLine("you lose");
                player.balance -= bet;
            }
            if (scorePlayer < scoreCroupier && scoreCroupier <= 21)
            {
                Console.WriteLine("you lose");
                player.balance -= bet;
            }
            if (scoreCroupier > 21 && scorePlayer < 21)
            {
                Console.WriteLine("you won");
                money = bet * 2;
                player.balance += money;
            }
            if (scorePlayer > scoreCroupier && scorePlayer < 21)
            {
                Console.WriteLine("you won");
                money = bet * 2;
                player.balance += money;
            }
            if (scorePlayer == scoreCroupier)
            {
                Console.WriteLine("draw");
            }
            player.ChangeBalanceOnFile();
            Console.ReadKey();
        }
        public List<Card> CardsCroupier()
        {
            List<Card> cardsCroupier = new List<Card>();
            int somme = 0;
            int aces = 0;
            while (somme < 17)
            {
                Card card = cardgame.Pop();
                if (card.value == 11) { aces++; }
                cardsCroupier.Add(card);
                somme += card.value;
                if (somme > 21 && aces != 0)
                {
                    aces--;
                    somme -= 10;
                }
            }
            return cardsCroupier;
        }
        public List<Card> CardsPlayerInitial()
        {
            List<Card> cardsPlayer = new List<Card>();
            
            Card card1 = cardgame.Pop();
            cardsPlayer.Add(card1);
            Card card2 = cardgame.Pop();
            cardsPlayer.Add(card2);
            
            return cardsPlayer;

        }
        

        public void Classic( List<Card> cardsPlayer, List<Card> cardsCroupier)
        {
            string fan = "";
            int aces = 0;
            int somme = 0;
            for(int i = 0; i < cardsPlayer.Count; i++)
            {
                somme += cardsPlayer[i].value;
                if (cardsPlayer[i].value == 11) { aces++; }
            }
            
            while (fan != "2")
            {
                if (fan == "1")
                {
                    Card card = cardgame.Pop();
                    if (card.value == 11) { aces++; }
                    cardsPlayer.Add(card);
                    somme += card.value;
                    if (card.value == 11) { aces++; }
                    if (somme > 21 && aces != 0)
                    {
                        somme -= 10;
                        aces--;
                    }
                }

                Affichage(cardsPlayer, cardsCroupier, 0);
                Console.ReadKey();
                Console.WriteLine($"you're now at {somme}\t1: Add an other card\t2: Stop here");
                fan = Console.ReadLine();
                if (somme >= 21)
                {
                    fan = "2";
                }
            }
            Affichage(cardsPlayer, cardsCroupier, 1);

            End(cardsCroupier, cardsPlayer);

        }
        public void Affichage(List<Card> cardsPlayer, List<Card> cardsCroupier, int step)
        {
            Console.Clear();
            Console.WriteLine("\t\tGame:");
            Console.WriteLine("Croupier Game:");
            if (step == 0)
            {
                Console.WriteLine($"\t|  {cardsCroupier[0].display}  |\t----");
                for (int i = 0; i < 10; i++) { Console.WriteLine(); }
                Console.WriteLine("Player Game:");
                foreach(Card card in cardsPlayer)
                {
                    Console.Write("\t|  " + card.display+"  |");
                }
                Advise(cardsPlayer,cardsCroupier);
            }
            if (step == 1)
            {
                foreach (Card card in cardsCroupier)
                {
                    Console.Write("\t|  " + card.display + "  |");
                }
                for (int i = 0; i < 10; i++) { Console.WriteLine(); }
                Console.WriteLine("Player Game:");

                foreach (Card card in cardsPlayer)
                {
                    Console.Write("\t|  " + card.display + "  |");
                }
            }
            Console.WriteLine("");

        }
        public void Advise(List<Card> cardsPlayer, List<Card> cardsCroupier)
        {
            int number;
            int aces = 0;
            int somme = 0;
            foreach (Card card in cardsPlayer)
            {
                somme += card.value;
                if (card.value == 11) { aces++; }
            }
            while (somme > 21 && aces != 0)
            {
                somme -= 10;
            }
            if (cardsPlayer[1] == cardsPlayer[0]&&cardsPlayer.Count==2)
            {
                int ind1 = 12 - cardsPlayer[1].value;
                int ind2 = cardsCroupier[0].value - 1;
                number = tab3[ind1, ind2];
            }
            else if ((cardsPlayer[0].value == 11 || cardsPlayer[1].value == 11)&&cardsPlayer.Count==2)
            {

                int ind=1;
                if(cardsPlayer[1].value == 11) { ind = 0; }
                int ind1 = 11 - cardsPlayer[ind].value;
                int ind2 = cardsCroupier[0].value - 1;
                number = tab2[ind1, ind2];

            }
            
            else
            {
                
                
                int ind1 = 22 - somme;
                int ind2 = cardsCroupier[0].value - 1;
                number = tab1[ind1, ind2];

            }
            if ((number == 2 || number == 3) && cardsPlayer.Count != 2)
            {
                if (somme < 17) { number = 0; }
                else { number = 1; }
            }
            WriteAdvise(number);
        }
        public void WriteAdvise(int number)
        {
            Console.WriteLine();
            if (number ==0) { Console.WriteLine("You should take an other card "); }
            if (number ==1) { Console.WriteLine("You should stay on it"); }
            if (number == 2) { Console.WriteLine("You should double your bet"); }
            if (number ==3) { Console.WriteLine("You should split your game"); }

        }
        public void Tables()
        {
            string path = @"C:/Users/benoi/source/repos/BlackJack/BlackJack/";
            tab3 = new int[11, 11];
            string way = path + "tab3.txt";
            int j = 0;
            using (StreamReader ReaderObject = new StreamReader(way))
            {
                string Line;
                while ((Line = ReaderObject.ReadLine()) != null)
                {
                    for (int i = 0; i < 11; i++)
                    {
                        tab3[j, i] = Convert.ToInt32(Line.Split(';')[i]);
                    }
                    j++;
                }
            }
            tab2 = new int[10, 11];
            string way2 = path + "tab2.txt";
            int k = 0;
            using (StreamReader ReaderObject = new StreamReader(way2))
            {
                string Line;
                while ((Line = ReaderObject.ReadLine()) != null)
                {
                    for (int i = 0; i < 11; i++)
                    {
                        tab2[k, i] = Convert.ToInt32(Line.Split(';')[i]);
                    }
                    k++;
                }
            }
            tab1 = new int[19, 11];
            string way3 = path + "tab1.txt";
            int l = 0;
            using (StreamReader ReaderObject = new StreamReader(way))
            {
                string Line;
                while ((Line = ReaderObject.ReadLine()) != null)
                {
                    for (int i = 0; i < 11; i++)
                    {
                        tab1[l, i] = Convert.ToInt32(Line.Split(';')[i]);
                    }
                    l++;
                }
            }
        }

       
    }
}
