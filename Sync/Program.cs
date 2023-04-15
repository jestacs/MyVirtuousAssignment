using CsvHelper;
using Sync.DataFolder;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Sync
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Sync().GetAwaiter().GetResult();
        }

        private static async Task Sync()
        {
            var apiKey = ConfigurationSettings.AppSettings["VirtuousApiKey"];
            var configuration = new Configuration(apiKey);
            var virtuousService = new VirtuousService(configuration);

            var skip = 0;
            var take = 100;
            var maxContacts = 1000;

            try
            {
                using (var context = new AppDbContext())
                {
                    DeletePreviousRecords(context);
                    do
                    {
                        var contacts = await virtuousService.GetContactsAsync(skip, take);
                        skip += take;
                        if (skip > maxContacts)
                        {
                            break;
                        }
                        context.Contacts.AddRange(contacts.List);
                        context.SaveChanges();
                    }
                    while (!(skip > maxContacts));
                }
                Console.WriteLine("Contacts were successfully saved.");
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured during saving Contacts data.");
            }
            Console.ReadLine();
        }

        //Delete the current records first to avoid violation of primary key constraints.
        private static void DeletePreviousRecords(AppDbContext context)
        {
            var itemsToDelete = context.Set<AbbreviatedContact>();
            context.Contacts.RemoveRange(itemsToDelete);
            context.SaveChanges();
        }
    }
}
