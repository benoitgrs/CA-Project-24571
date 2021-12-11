using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Cards
    {
        public int number { get; set; }
        public string[,] cards = new string[4, 13];
        public List<Card> cardGame { get; set; }
        public Cards(int _number)
        {
            cardGame = new List<Card>();
            //l1    access
            //l2    value
            //l3    display
            //l4    number of cards
            number = _number;
            cards[1, 0] = "11";
            for(int i=0; i < 13; i++)
            {
                cards[0, i] = Convert.ToString(i + 1);
            }
            for (int i = 1; i < 10; i++)
            {
                cards[1, i] = Convert.ToString(i + 1);
                cards[2, i] = "   "+Convert.ToString(i + 1)+"   ";
            }
            for (int i = 10; i < 13; i++)
            {
                cards[1, i] = Convert.ToString(10);
            }
            cards[2, 0] = " ace ";
            cards[2, 10] = " jack ";
            cards[2, 11] = "queen";
            cards[2, 12] = "king ";
            for(int i = 0; i < 13; i++)
            {
                cards[3, i] = Convert.ToString(number * 4);
            }
            for(int i = 0; i < number*4; i++)
            {
               for(int j=0; j < 13; j++)
               {
                    Card card = new Card(j);
                    cardGame.Add(card);
               }
                
            }
        }
        
    }
}
