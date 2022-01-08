using EF_Activity001;
using InventoryHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Activity0801_Sorting_Filtering_Paging
{
    class Program
    {
        private static IConfigurationRoot _configuration;
        private static DbContextOptionsBuilder<AdventureWorksContext> _optionsBuilder;

        static void Main(string[] args)
        {
            BuildOptions();
            Console.WriteLine("List People Then Order and Take");
            ListPeopleThenOrderAndTake();
            Console.WriteLine("Query People, order, then list and take");
            QueryPeopleOrderedToListAndTake();
        }

        private static void QueryPeopleOrderedToListAndTake()
        {
            using (var db = new AdventureWorksContext(_optionsBuilder.Options))
            {
                var query = db.People.OrderByDescending(x => x.LastName).Take(10);
                
                foreach (var person in query)
                {
                    Console.WriteLine($"{person.FirstName} {person.LastName}");
                }
            }
        }

        private static void ListPeopleThenOrderAndTake()
        {
            using (var db = new AdventureWorksContext(_optionsBuilder.Options))
            {

                var people = db.People.ToList().OrderByDescending(x => x.LastName);
                foreach (var person in people.Take(10))
                {
                    Console.WriteLine($"{person.FirstName} {person.LastName}");
                }
            }
        }

        static void BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
            _optionsBuilder = new DbContextOptionsBuilder<AdventureWorksContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("AdventureWorks"));
        }
    }
}
