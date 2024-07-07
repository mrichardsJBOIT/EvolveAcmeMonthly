using AcmeMonthly.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using CrypticWizard.RandomWordGenerator;
using static CrypticWizard.RandomWordGenerator.WordGenerator; //for brevity, not required

Console.WriteLine("Hello, World!  Welcome to Acme.");

string firstArg = "";
if (args.Length > 0)
{
    Console.WriteLine(args.Length);
    firstArg = Environment.GetCommandLineArgs()[1];
    Console.WriteLine($"You Said: '{firstArg}'!");
}

DBReport reporter = new();
DBSeeder seeder = new();
SubscritionSender subscritionSender = new();
Facade facade = new Facade(reporter, seeder, subscritionSender);
facade.getUserOperation(firstArg);

//public class Operator
//{
//    public void doOperation()
//    {
//        Console.WriteLine($"Operator Doing Work");
//    }
//}

public class DBReport //: Operator
{
    public void doOperation()
    {
        Console.WriteLine($"{GetType().Name} Doing {MethodBase.GetCurrentMethod().Name}");

        using var db = new AcmeContext();

        // Note: This sample requires the database to be created before running.
        Console.WriteLine("\n------------------------\n");
        Console.WriteLine($"Database path: {db.DbPath}.\n");

        // Read Publications
        Console.WriteLine("\n------------------------\n");
        Console.WriteLine("Querying for a publictions");
        foreach (var publication in db.Publications.OrderBy(publication => publication.Id))
        {
            Console.WriteLine($"Publication: '{publication.Name}', can be said:'{publication.Description}' ");
        }

        // Read Companies
        Console.WriteLine("\n------------------------\n");
        Console.WriteLine("Querying for a Companies");
        foreach (var company in db.Companies)
        {
            Console.WriteLine($"Company: '{company.Name}', can be said:'{company.Description}' ");
        }

        // Read Customers
        Console.WriteLine("\n------------------------\n");
        Console.WriteLine("Querying for a Customers");
        foreach (var customer in db.Customers)
        {
            Console.WriteLine($"Customer: '{customer.Name}'");
        }

        // Read Countries
        Console.WriteLine("\n------------------------\n");
        Console.WriteLine("Querying for a Counties");
        foreach (var country in db.Countries)
        {
            Console.WriteLine($"Country: '{country.Name}', '{country.CountryCode}'");
        }

        // Read Addresses
        Console.WriteLine("\n------------------------\n");
        Console.WriteLine("Querying for a Addresses");
        foreach (var address in db.Addresses)
        {
            Console.WriteLine($"Address: '{address.AddressLine1}', '{address.Type}'");
        }

        // Read Subscriptions
        Console.WriteLine("\n------------------------\n");
        Console.WriteLine("Querying for a Subscriptions");
        foreach (var subscription in db.Subscriptions)
        {
            Console.WriteLine($"Subscription: '{subscription.Customer.Name}', '{subscription.Publication.Name}', '{subscription.DeliveryAddress.Country.Name}'");
        }
    }

}

public class DBSeeder //: Operator
{

    /*
    Just Incase... 
    BEGIN TRANSACTION;
        DELETE FROM Addresses;
        DELETE FROM Countries;
        DELETE FROM Subscriptions;
        DELETE FROM Companies;
        DELETE FROM Publications;
        DELETE FROM Customers;
    COMMIT;
     */
    public void doOperation()
    {
        Console.WriteLine($"{GetType().Name} Doing {MethodBase.GetCurrentMethod().Name}");
        seedSingleEntities();
        seedCompanies();
        seedSubscriptions();
    }

    private void seedSingleEntities(){

        using (var db = new AcmeContext())
        {
            db.Database.EnsureCreated();

            #region PublicationSeed

            List<Publication> seedPublications = 
            [
                new Publication { Name = "Acme EF Coding For Dummies", Description = "You'd better off using the internet", Id = 0 }, //Id's should be a sequence on the database...
                new Publication { Name = "How To Lead Teams", Description = "Practical Advice For Getting Burned", Id = 0 },
                new Publication { Name = "The Customer Is Not Right ", Description = "But don't tell them", Id = 0 }
            ];
            int i = db.Publications.AsEnumerable().DefaultIfEmpty(seedPublications.Last()).Max(p => p.Id)+1;
            seedPublications.ForEach(p => p.Id = i++);   
            seedPublications.ForEach(p => db.Publications.Add(p));
            db.SaveChanges();
            #endregion

            #region CountrySeed
            List<Country> seedCountries = [
               new Country { Name = "Afghanistan", CountryCode = "AFG", Id = 0 }, //Id's should be a sequence on the database...
                new Country { Name = "Albania", CountryCode = "SLB", Id = 0 },
                new Country { Name = "Algeria", CountryCode = "DZA", Id = 0 },
                new Country { Name = "Andorra", CountryCode = "AND", Id = 0 },
                new Country { Name = "Angola", CountryCode = "AGO", Id = 0 },
                new Country { Name = "Antigua and Barbuda", CountryCode = "ATG", Id = 0 },
                new Country { Name = "Argentina", CountryCode = "ARG", Id = 0 },
                new Country { Name = "Armenia", CountryCode = "ARM", Id = 0 },
                new Country { Name = "Aruba", CountryCode = "ABW", Id = 0 },
                new Country { Name = "Australia", CountryCode = "AUS", Id = 0 },
                new Country { Name = "Austria", CountryCode = "AUT", Id = 0 },
                new Country { Name = "New Zealand", CountryCode = "NZL", Id = 0 }
            ];

            i = db.Countries.AsEnumerable().DefaultIfEmpty(seedCountries.Last()).Max(c => c.Id)+1;
            seedCountries.ForEach(c => c.Id = i++);
            seedCountries.ForEach(c => db.Countries.Add(c));

            db.SaveChanges();
            #endregion

            #region CustomerSeed

            List<Customer> seedCustomers = [
                new Customer { Name = "Dave Davies", Id = 1 },
                new Customer  { Name = "Bob Smith", Id = 2 },
                new Customer { Name = "Rachel Jenkins", Id = 3 }
            ];

            i = db.Customers.AsEnumerable().DefaultIfEmpty(seedCustomers.Last()).Max(c => c.Id)+1;
            seedCustomers.ForEach(c => c.Id = i++);
            seedCustomers.ForEach(c => db.Customers.Add(c));
           
            db.SaveChanges();
            #endregion
        }   
    }

    private void seedCompanies()
    {
        using (var db = new AcmeContext())
        {
            db.Database.EnsureCreated();

            #region Companies Seed
            List<Company> seedCompanies = [
                new Company { Id = 0, Name = "Best Aus Media Printer", Description = "AUS: Does what is says on the tine, all over Australia, and NZ!" },
                new Company { Id = 0, Name = "NZ Big Media Printer", Description = "NZL: All over NZ " }
            ];

            int i = db.Companies.AsEnumerable().DefaultIfEmpty(seedCompanies.Last()).Max(c => c.Id)+1;
            seedCompanies.ForEach(c => c.Id=i++); //fix up id's as not done at server level

            #endregion

            #region AddressSeed
            //an address must be linked to a Country, so we will assume they have already been created
            List<Address> seedAddresses = [
                new Address { Id = 0, AddressLine1 = "Test AUS", AddressLine2 = "99 Long Road", City = "Edge City", PostCode = "4000", Type = AddressTypes.Company, 
                                    Country = db.Countries.Where( c => c.CountryCode.Equals("AUS")).First()},
                new Address { Id = 0, AddressLine1 = "Test NZL", AddressLine2 = "1 Steep Hill", City = "Of Sails", PostCode = "12345", Type = AddressTypes.Company, 
                                    Country = db.Countries.Where( c => c.CountryCode.Equals("NZL")).First()},
            ];

            i = db.Addresses.AsEnumerable().DefaultIfEmpty(seedAddresses.Last()).Max(a => a.Id)+1; 
            seedAddresses.ForEach(a => a.Id=i++);

            //Link address to companies
            //joining on address.country.CountryCode == company.description.substr(3).
            //Will not add to much error checking or default as seed data is present above.
            seedCompanies.ForEach(comp => comp.Address = 
                                                    seedAddresses.Where(
                                                                            addr => addr.Country.CountryCode.Equals(comp.Description?[..3])).First());
            #endregion

            //ToDo: Delivery Addresses
            /*
                DeliveryCountryList = new List<CompanyDeliveryCountry>
                {
                  new CompanyDeliveryCountry { Id = 1, DeliveryCountry = new Country { Id = 11 }, PrintDistributor = new Company { Id = 1} }, //This company delivers to AUS
                  new CompanyDeliveryCountry { Id = 2, DeliveryCountry = new Country { Id = 12 }, PrintDistributor = new Company { Id = 1} } // AND delivers to NZ
                }

                DeliveryCountryList = new List<CompanyDeliveryCountry>
                {
                  new CompanyDeliveryCountry { Id = 3, DeliveryCountry = { Id = 12 }, PrintDistributor = { Id = 2} } // This co delivers to NZ
                }
            */

            //add companies and addresses to db
            seedAddresses.ForEach(a => db.Addresses.Add(a));
            seedCompanies.ForEach(c => db.Companies.Add(c));

            db.SaveChanges();
        }
    }

    private void seedSubscriptions()
    {
        using (var db = new AcmeContext())
        {
            db.Database.EnsureCreated();

            /* GET:
             * 1. Existing Publication -> every publication will have a subscription
             * 2. Existing Customer
             * 3. New Delivery Address For the Customer.  This is not a customer address, it's the address to send the publication.  AUS, NZL, BRA?
             */
            var random = new Random();
            WordGenerator myWordGenerator = new WordGenerator();
            List<Subscription> seedSubscriptions = new List<Subscription>();
            string[] deliveryCountryCodes = { "AUS", "NZL", "ARG" };

            foreach (var publication in db.Publications)
            {
                string countryCode = deliveryCountryCodes.ElementAt(random.Next(0, 2));

                Subscription sub = new Subscription
                {
                    Id = 0,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(random.Next(1, 13)),
                    Publication = publication,
                    Customer = db.Customers.ElementAt(random.Next(1, db.Customers.Count())),

                    DeliveryAddress = new Address
                    {
                        Id = 0,
                        Type = AddressTypes.Delivery,
                        AddressLine1 = $"{random.Next(1, 999)} {myWordGenerator.GetWord(PartOfSpeech.noun)} Street",
                        City = $"{myWordGenerator.GetWord(PartOfSpeech.verb)} City",
                        PostCode = $"{random.Next(1, 9999)}".PadLeft(4, '0'),
                        Country = db.Countries.Where(c => c.CountryCode.Equals(countryCode)).First()
                    },
                    Status = SubscriptionStatus.Active
                    
                };

                sub.Id = db.Subscriptions.AsEnumerable().DefaultIfEmpty(sub).Max(s => s.Id) + 1;
                sub.DeliveryAddress.Id = db.Addresses.AsEnumerable().DefaultIfEmpty(sub.DeliveryAddress).Max(a => a.Id) + 1;

                db.Subscriptions.Add(sub);
                db.SaveChanges();
            };     
        }
    }
}

[Keyless]
public class SubscriptionPrintDistributors
{
    public string? PublicationName {  get; set; }
    public string? CustomerName { get; set; }
    public string? DeliveryAddressLine1 { get; set; }
    public string? DeliveryAddressLine2 { get; set; }
    public string? DeliveryAddressLine3 { get; set; }

    public string? DeliveryCity { get; set; }
    public string? DeliveryPostCode { get; set; }
    public string? DeliveryCountry { get; set; }
    public string? PrintDistributorsName { get; set; }

    public string deliveryAddressAsOneString()
    {
        return $"{DeliveryAddressLine1}, {DeliveryAddressLine2}, {DeliveryAddressLine3}, {DeliveryCity}, {DeliveryPostCode}, {DeliveryCountry}";
    }
}

public class SubscritionSender //: Operator
{
    public void doOperation()
    {
        Console.WriteLine($"{GetType().Name} Doing {MethodBase.GetCurrentMethod().Name}");
        getActiveSubscriptions();
    }

    private List<Subscription> getActiveSubscriptions()
    {
        DateTime dt = DateTime.Now;
        Console.WriteLine(dt.Month);

        /* NOTES:
         * Get print distribution company's country 
         * select comp.*, '--', compaddr.*, '--', cont.* from Companies comp
                left join Addresses compaddr on comp.AddressId = compaddr.id
                left join Countries cont on compaddr.CountryId = cont.id

           Get active subscriptions including delivery country
            SELECT sub.*, '--', cust.*, '--', deladdr.*, '--', c.* from Subscriptions sub
                Left join Customers cust on sub.CustomerId = cust.id
                LEFT join Addresses deladdr on sub.DeliveryAddressId = deladdr.id  and deladdr.Type = 0
                left join Countries c on deladdr.CountryId = c.id
                -- left join  
            where sub.Status=0

            BIG JOIN

            SELECT 
                sub.*, '--', 
                cust.*, '--', 
                deladdr.*, '--', 
                delcont.*, '-', 
                printdistributors.* 
                from Subscriptions sub
                Left join Customers cust on sub.CustomerId = cust.id
                LEFT join Addresses deladdr on sub.DeliveryAddressId = deladdr.id  and deladdr.Type = 0
                left join Countries delcont on deladdr.CountryId = delcont.id
                left join (
                    select comp.*, compaddr.*, cont.* from Companies comp
                        left join Addresses compaddr on comp.AddressId = compaddr.id and compaddr.Type = 1
                        left join Countries cont on compaddr.CountryId = cont.id
                ) as printdistributors on delcont.CountryCode = printdistributors.CountryCode
                where sub.Status=0
         */

        using var db = new AcmeContext();

        string getSubsPrintDistributor =
                        @$"SELECT 
                            pubs.name as PublicationName, 
                            cust.name as CustomerName, 
                            deladdr.AddressLine1 as DeliveryAddressLine1, 
                            deladdr.AddressLine2 as DeliveryAddressLine2, 
                            deladdr.AddressLine3 as DeliveryAddressLine3,
                            deladdr.City as DeliveryCity, 
                            deladdr.PostCode as DeliveryPostCode, 
                            delcont.name as DeliveryCountry,  
                            printdistributors.name as PrintDistributorsName
                            from Subscriptions sub
                                Left join Customers cust on sub.CustomerId = cust.id
                                LEFT join Addresses deladdr on sub.DeliveryAddressId = deladdr.id  and deladdr.Type = 0
                                left join Publications pubs on sub.PublicationId = pubs.id
                                left join Countries delcont on deladdr.CountryId = delcont.id
                                left join (
                                select comp.*, compaddr.*, cont.* from Companies comp
                                                left join Addresses compaddr on comp.AddressId = compaddr.id and compaddr.Type = 1
                                                left join Countries cont on compaddr.CountryId = cont.id
                                ) as printdistributors on delcont.CountryCode = printdistributors.CountryCode
                            where 
                                 sub.Status=0 and 
                                JULIANDAY(sub.EndDate) - JULIANDAY('NOW') > 0 "; //has business logic, should be a view?

        var subscriptionsAndTheirPrintDistributors = db.Database.SqlQueryRaw<SubscriptionPrintDistributors>(getSubsPrintDistributor).ToList();


        // Write Subscriptions Print Distributors
        Console.WriteLine("\n------------------------\n");
        Console.WriteLine("Here are Active subscriptions That would be sent the provider in the details");
        string message = "Publication: '{0}', has a subscription by '{1}' for delivery to: {2}. By this distributor {3}";
        foreach (var subscription in subscriptionsAndTheirPrintDistributors)
        {
            Console.WriteLine(message, subscription.PublicationName?.Trim(), subscription.CustomerName?.Trim(), 
                                    subscription.deliveryAddressAsOneString()?.Trim(), subscription.PrintDistributorsName);

        }

        return null;
    }

}

public class Facade
{
    protected DBReport r_reporter;
    protected DBSeeder c_seeder;
    protected SubscritionSender s_subscritionSender;

    public Facade(DBReport reporter, DBSeeder seeder, SubscritionSender subscritionSender)
    {
        this.r_reporter = reporter;
        this.c_seeder = seeder;
        this.s_subscritionSender = subscritionSender;
    }

    public void getUserOperation(string arg)
    {
        string optionSeclected = arg;
        if (arg == null || arg.Length != 1)
        {

            // Ask the user to choose an operation.
            Console.WriteLine("AcmeMonthly Data Controller\r");
            Console.WriteLine("------------------------\n");
            Console.WriteLine("Choose an operation from the following list:");
            Console.WriteLine("\tc - Create Seed Data");
            Console.WriteLine("\tr - Report");
            Console.WriteLine("\ts - Send Monthly Publication Subscriptions");
            Console.WriteLine("\tq - Quit");
            Console.Write("Type a option, and then press Enter. ");

            optionSeclected = Console.ReadLine();
        }        

        switch (optionSeclected)
        {
            case "c":
                Console.WriteLine($"Seeding The Database...");
                c_seeder.doOperation();
                break;
            case "r":
                Console.WriteLine($"Database Report");
                r_reporter.doOperation();
                break;
            case "s":
                Console.WriteLine($"Sending Subscriptions");
                s_subscritionSender.doOperation();
                break;
            case "q":
                Console.WriteLine($"Goodbye");
                System.Environment.Exit(0);
                break;
            default:
                Console.WriteLine($"Did no recognise {optionSeclected}");
                break;
        }
        // Wait for the user to respond before closing.
        Console.Write("Press any key to close the Acme app...");
        Console.ReadKey();

    }
}