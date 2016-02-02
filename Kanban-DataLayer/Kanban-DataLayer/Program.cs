using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban_DataLayer
{
    class Program
    {
        private static bool quit = false;
        
        static void Main(string[] args)
        {
            while (quit == false)
            {
                Console.WriteLine("What would you like to do? \"ADD CARD\", \"ADD LIST\", \"PRINT\" all, or \"QUIT\".");
                string userinput = Console.ReadLine().ToUpper();

                using (var db = new Entities())
                {
                    switch (userinput)
                    {
                        case "PRINT":
                            {
                                foreach (var list in db.Lists)
                                {
                                    Console.WriteLine(list.Name);

                                    foreach (var card in list.Cards)
                                    {
                                        Console.WriteLine("\t" + card.Text);
                                    }
                                    Console.WriteLine();
                                }
                            }
                            break;

                        case "ADD LIST":
                            {
                                Console.WriteLine("What would you like to name the list?");
                                var newListName = Console.ReadLine();
                                var newList = db.Set<List>();
                                newList.Add(new List { Name = newListName, CreatedDate = DateTime.Now });
                                db.SaveChanges();
                                Console.WriteLine();
                            }
                            break;

                        case "ADD CARD":
                            {
                                bool listName = true;
                                int whichListId = 0;
                                string whichListName = null;

                                while (listName)
                                {
                                    Console.WriteLine("Which list would you like to add a new card to?");
                                    whichListName = Console.ReadLine();
                                    foreach (var list in db.Lists)
                                    {
                                        if (whichListName == list.Name)
                                        {
                                            whichListId = list.ListId;
                                            listName = false;
                                        }
                                    }
                                    if (listName)
                                    {
                                        Console.WriteLine("That is not an existing list.");
                                    }
                                }

                                Console.WriteLine("What would you like the card text to be?");
                                var newCardText = Console.ReadLine();
                                db.Cards.Add(new Card { ListId = whichListId, CreatedDate = DateTime.Now, Text = newCardText });
                                db.SaveChanges();
                            }
                            break;

                        case "QUIT":
                            {
                                quit = true;
                            }
                            break;

                        default:
                            {
                                Console.WriteLine("Please type \"ADD CARD\", \"ADD LIST\", \"PRINT\", or \"QUIT\".");
                            }
                            break;
                    };
                }
            }
        }
    }
}
