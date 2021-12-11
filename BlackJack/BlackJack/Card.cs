using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Card
    {
        public int access { get; set; }
        public int value { get; set; }
        public string display { get; set; }
        public string[,] cards { get; set; }

        public Card(int _access)
        {
            cards = new string[4, 13];
            int number = 1;
            cards[1, 0] = "11";
            for (int i = 0; i < 13; i++)
            {
                cards[0, i] = Convert.ToString(i + 1);
            }
            for (int i = 1; i < 10; i++)
            {
                cards[1, i] = Convert.ToString(i + 1);
                cards[2, i] = Convert.ToString(i + 1);
            }
            for (int i = 10; i < 13; i++)
            {
                cards[1, i] = Convert.ToString(10);
            }
            cards[2, 0] = "ace";
            cards[2, 10] = "jack";
            cards[2, 11] = "queen";
            cards[2, 12] = "king";
            for (int i = 0; i < 13; i++)
            {
                cards[3, i] = Convert.ToString(number * 4);
            }
            access = _access;
            value = Convert.ToInt32(cards[1, access]);
            display = cards[2, access];

        }

        public static void one()
        {
            Console.WriteLine();


        }
    }
}
