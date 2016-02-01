using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban_DataLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new Entities())
            {
                foreach (var list in db.Lists)
                {
                    Console.WriteLine(list.Name);

                    foreach (var card in list.Cards)
                    {
                        Console.WriteLine("\t" + card.Text);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
