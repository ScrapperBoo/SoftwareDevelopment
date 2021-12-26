using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDictionary
{
    class Programm
    {

        static void Main(string[] args)
        {
            var hashtb = new CustomDic<int, string>();
            KeyValuePair<int, string> elem1 = new KeyValuePair<int, string>(10, "DOG");
            hashtb.Add(elem1);

            KeyValuePair<int, string> elem2 = new KeyValuePair<int, string>(10, "Bogd");
            hashtb.Add(elem2);
            KeyValuePair<int, string> elem3 = new KeyValuePair<int, string>(11, "Bogd");
            hashtb.Add(elem3);

            foreach (var elem in hashtb)
            {
                Console.WriteLine(" {0} {1}", elem.Key, elem.Value);
            }

        }

    }
}
