using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

public class Consumer
{
    [Key]
    public int ConsumerId { get; set; }
    public string Password { get; set; }
    public string ConsumerName { get; set; }
    public string City { get; set; } 
    public int BillId { get; set; }
    public int OldReading { get; set; }
    public int NewReading { get; set; }
    public string MobileNumber { get; set; }

    public double Bill { get; set; }
}
public class MsebOfficer
{
    [Key]
    public int OfficerId { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string MobileNumber { get; set; }
}
public class Admin
{
    [Key]
    public int AdminId { get; set; }
    public string Password { get; set; }
    public string Name { get; set; } 
    public string MobileNumber { get; set; }
}

public class DataContext : DbContext
{
    public DbSet<Consumer> Consumers { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<MsebOfficer> MsebOfficers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "server=localhost;port=3306;user=root;password=root;database=project3;";
        optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 26)));
    }
}


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("----------Welcome to the WattSmart (Energy Bill Management System)!--------------");

        while (true)
        {
            Console.WriteLine("Options: ");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            Console.Write("Select an option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    RegisterOption();
                    break;
                case "2":
                    LoginOption();
                    break;
                case "3":
                    Console.WriteLine("Exiting...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    static void RegisterOption()
    {
        Console.WriteLine("Register Options:");
        Console.WriteLine("1. Register as Consumer");
        Console.WriteLine("2. Register as Admin");
        Console.WriteLine("3. Register as MSEB Officer");
        Console.WriteLine("4. Go Back");
        Console.Write("Select an option: ");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                RegisterConsumer();
                break;
            case "2":
                RegisterAdmin();
                break;
            case "3":
                RegisterMsebOfficer();
                break;
            case "4":
                return; // Go back to main menu
            default:
                Console.WriteLine("Invalid option. Try again.");
                break;
        }
    }

    static void LoginOption()
    {
        Console.WriteLine("Login Options:");
        Console.WriteLine("1. Login as Consumer");
        Console.WriteLine("2. Login as Admin");
        Console.WriteLine("3. Login as MSEB Officer");
        Console.WriteLine("4. Go Back");
        Console.Write("Select an option: ");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                Login("Consumer");
                break;
            case "2":
                Login("Admin");
                break;
            case "3":
                Login("MsebOfficer");
                break;
            case "4":
                return; // Go back to main menu
            default:
                Console.WriteLine("Invalid option. Try again.");
                break;
        }
    }

    static void RegisterConsumer()
    {
        Console.WriteLine("Please enter the following details for Consumer Registration:");
        Console.Write("Consumer ID: ");
        int consumerId = int.Parse(Console.ReadLine());
        Console.Write("Password: ");
        string password = Console.ReadLine();
        Console.Write("Consumer Name: ");
        string consumerName;
        while (!IsAlphabetic(consumerName = Console.ReadLine()))
        {
            Console.WriteLine("Invalid input. Name must contain only alphabets. Try again.");
            Console.Write("Name: ");
        }
        Console.Write("City: ");
        string city;
        while (!IsAlphabetic(city = Console.ReadLine()))
        {
            Console.WriteLine("Invalid input. City must contain only alphabets. Try again.");
            Console.Write("City: ");
        }
        Console.Write("Bill ID: ");
        int billId = int.Parse(Console.ReadLine());
        Console.Write("Old Reading: ");
        int oldReading = int.Parse(Console.ReadLine());
        Console.Write("New Reading: ");
        int newReading = int.Parse(Console.ReadLine());
        Console.Write("Mobile Number: ");
        string mobileNumber;
        while (!IsValidMobileNumber(mobileNumber = Console.ReadLine()))
        {
            Console.WriteLine("Invalid input. Mobile number must be 10 digits. Try again.");
            Console.Write("Mobile Number: ");
        }
        double billAmount = (newReading - oldReading) * 5;


        var consumer = new Consumer
        {
            ConsumerId = consumerId,
            Password = password,
            ConsumerName = consumerName,
            City = city,
            BillId = billId,
            OldReading = oldReading,
            NewReading = newReading,
            MobileNumber = mobileNumber,
            Bill = billAmount
        };

        using (var context = new DataContext())
        {
            context.Consumers.Add(consumer);
            context.SaveChanges();
        }

        Console.WriteLine("Consumer registration successful!");
        Console.WriteLine("----------------------");
    }

    static void RegisterAdmin()
    {
        Console.WriteLine("Please enter the following details for Admin Registration:");
        Console.Write("Admin ID: ");
        int adminId;
        while (!int.TryParse(Console.ReadLine(), out adminId))
        {
            Console.WriteLine("Invalid input. Admin ID must be numeric. Try again.");
            Console.Write("Admin ID: ");
        }

        Console.Write("Password: ");
        string password = Console.ReadLine();

        Console.Write("Name: ");
        string name;
        while (!IsAlphabetic(name = Console.ReadLine()))
        {
            Console.WriteLine("Invalid input. Name must contain only alphabets. Try again.");
            Console.Write("Name: ");
        }


        Console.Write("Mobile Number: ");
        string mobileNumber;
        while (!IsValidMobileNumber(mobileNumber = Console.ReadLine()))
        {
            Console.WriteLine("Invalid input. Mobile number must be 10 digits. Try again.");
            Console.Write("Mobile Number: ");
        }

        var admin = new Admin
        {
            AdminId = adminId,
            Password = password,
            Name = name,
            MobileNumber = mobileNumber
        };

        using (var context = new DataContext())
        {
            context.Admins.Add(admin);
            context.SaveChanges();
        }

        Console.WriteLine("Admin registration successful!");
        Console.WriteLine("----------------------");
    }

    static void RegisterMsebOfficer()
    {
        Console.WriteLine("Please enter the following details for MSEB Officer Registration:");
        Console.Write("Officer ID: ");
        int officerId;
        while (!int.TryParse(Console.ReadLine(), out officerId))
        {
            Console.WriteLine("Invalid input. Officer ID must be numeric. Try again.");
            Console.Write("Officer ID: ");
        }

        Console.Write("Password: ");
        string password = Console.ReadLine();

        Console.Write("Name: ");
        string name;
        while (!IsAlphabetic(name = Console.ReadLine()))
        {
            Console.WriteLine("Invalid input. Name must contain only alphabets. Try again.");
            Console.Write("Name: ");
        }

        Console.Write("City: ");
        string city;
        while (!IsAlphabetic(city = Console.ReadLine()))
        {
            Console.WriteLine("Invalid input. City must contain only alphabets. Try again.");
            Console.Write("City: ");
        }

        Console.Write("Mobile Number: ");
        string mobileNumber;
        while (!IsValidMobileNumber(mobileNumber = Console.ReadLine()))
        {
            Console.WriteLine("Invalid input. Mobile number must be 10 digits. Try again.");
            Console.Write("Mobile Number: ");
        }

        var msebOfficer = new MsebOfficer
        {
            OfficerId = officerId,
            Password = password,
            Name = name,
            City = city,
            MobileNumber = mobileNumber
        };

        using (var context = new DataContext())
        {
            context.MsebOfficers.Add(msebOfficer);
            context.SaveChanges();
        }

        Console.WriteLine("MSEB Officer registration successful!");
        Console.WriteLine("----------------------");
    }


    static void Login(string userType)
    {
        Console.Write($"{userType} ID: ");
        int userId = int.Parse(Console.ReadLine());
        Console.Write("Password: ");
        string password = Console.ReadLine();

        try
        {
            using (var context = new DataContext())
            {
                if (userType == "Consumer")
                {
                    var consumer = context.Consumers
                        .SingleOrDefault(c => c.ConsumerId == userId && c.Password == password);

                    if (consumer == null)
                    {
                        throw new Exception("User not found");
                    }

                    Console.WriteLine($"{userType} login successful!");
                    Console.WriteLine("----------------------");

                    // Logged-in consumer menu options...
                    ConsumerMenuOptions(consumer);
                }
                else if (userType == "Admin")
                {
                    var admin = context.Admins
                        .SingleOrDefault(a => a.AdminId == userId && a.Password == password);

                    if (admin == null)
                    {
                        throw new Exception("Admin not found");
                    }

                    Console.WriteLine($"{userType} login successful!");
                    Console.WriteLine("----------------------");

                    // Logged-in admin menu options...
                    AdminMenuOptions();
                }
                else if (userType == "MsebOfficer")
                {
                    var msebOfficer = context.MsebOfficers
                        .SingleOrDefault(o => o.OfficerId == userId && o.Password == password);

                    if (msebOfficer == null)
                    {
                        throw new Exception("MSEB Officer not found");
                    }

                    Console.WriteLine($"{userType} login successful!");
                    Console.WriteLine("----------------------");

                    // Logged-in MSEB officer menu options...
                    MsebOfficerMenuOptions(msebOfficer);
                }
                else
                {
                    throw new Exception("Invalid user type");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }


    static void AdminMenuOptions()
    {
        while (true)
        {
            Console.WriteLine("\nAdmin Options:");
            Console.WriteLine("1. Display Consumer Data");
            Console.WriteLine("2. Display MSEB Officer Data");
            Console.WriteLine("3. Delete Consumer Account");
            Console.WriteLine("4. Delete MSEB Officer Account");
            Console.WriteLine("5. Update Consumer Data");
            Console.WriteLine("6. Update MSEB Officer Data");
            Console.WriteLine("7. Log Out");
            Console.Write("Select an option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    DisplayConsumerData();
                    break;
                case "2":
                    DisplayOfficerData();
                    break;

                case "3":
                    DeleteConsumerAccount();
                    break;
                case "4":
                    DeleteOfficerAccount();
                    break;
                case "5":
                    UpdateConsumerDataByAdmin();
                    break;
                case "6":
                    UpdateMsebOfficerDataByAdmin();
                    break;
                case "7":
                    Console.WriteLine("Logging out...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }


    static void MsebOfficerMenuOptions(MsebOfficer officer)
    {
        string officerCity = officer.City;
        while (true)
        {
            Console.WriteLine("\nMSEB Officer Options:");
            Console.WriteLine("1. Display Consumer Data");
            Console.WriteLine("2. Delete Consumer Account");
            Console.WriteLine("3. Display Account Information");
            Console.WriteLine("4. Update Account Information");
            Console.WriteLine("5. Update Consumer Data (Old Reading and New Reading)");
            Console.WriteLine("6. Log Out");
            Console.Write("Select an option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    DisplayConsumerData();
                    break;
                case "2":
                    DeleteConsumerAccount();
                    break;
                case "3":
                    DisplayOfficerInfo(officer);
                    break;
                case "4":
                    UpdateOfficerInfo(officer);
                    break;
                case "5":
                    UpdateConsumerDataByMsebOfficer();
                    break;
                case "6":
                    Console.WriteLine("Logging out...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
    static void ConsumerMenuOptions(Consumer consumer)
    {
        while (true)
        {
            Console.WriteLine("\n------WelCome to HomePage------");
            Console.WriteLine("1. Display Account Information");
            Console.WriteLine("2. Update Account Information");
            Console.WriteLine("3. Show Bill details");
            Console.WriteLine("4. Pay Amount");
            Console.WriteLine("5. Log Out");
            //Console.WriteLine("5. Delete Account");
            Console.Write("Select an option: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    DisplayUserInfo(consumer);
                    break;
                case "2":
                    UpdateUserInfo(consumer);
                    break;
                case "3":
                    ShowBill(consumer);
                    break;
                case "4":
                    PayAmount(consumer);
                    break;
                case "5":
                    Console.WriteLine("Logging out...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    static void DisplayOfficerInfo(MsebOfficer officer)
    {
        Console.WriteLine("\nMSEB Officer Information:");
        Console.WriteLine($"Officer ID: {officer.OfficerId}");
        Console.WriteLine($"Name: {officer.Name}");
        Console.WriteLine($"City: {officer.City}");
        Console.WriteLine($"Mobile Number: {officer.MobileNumber}");
    }

    static void DisplayUserInfo(Consumer consumer)
    {
        Console.WriteLine("\nUser Information:");
        Console.WriteLine($"User ID: {consumer.ConsumerId}");
        Console.WriteLine($"Consumer Name: {consumer.ConsumerName}");
        Console.WriteLine($"Bill ID: {consumer.BillId}");
        Console.WriteLine($"City: {consumer.City}");
        Console.WriteLine($"Old Reading: {consumer.OldReading}");
        Console.WriteLine($"New Reading: {consumer.NewReading}");
  
    }

    static void UpdateUserInfo(Consumer consumer)
    {
        Console.WriteLine("\nEnter updated information:");
        Console.Write("Name: ");
        string name;
        while (!IsAlphabetic(name = Console.ReadLine()))
        {
            Console.WriteLine("Invalid input. Name must contain only alphabets. Try again.");
            Console.Write("Name: ");
        }

        Console.Write("City: ");
        string city;
        while (!IsAlphabetic(city = Console.ReadLine()))
        {
            Console.WriteLine("Invalid input. City must contain only alphabets. Try again.");
            Console.Write("City: ");
        }
        // Console.Write("Bill ID: ");
        // consumer.BillId = int.Parse(Console.ReadLine());
        /* Console.Write("Old Reading: ");
         int oldReading = int.Parse(Console.ReadLine());
         Console.Write("New Reading: ");
         int newReading = int.Parse(Console.ReadLine());

         // Calculate and update the bill amount
         consumer.Bill = CalculateBillAmount(oldReading, newReading);

         consumer.OldReading = oldReading;
         consumer.NewReading = newReading;*/

        using (var context = new DataContext())
        {
            context.Consumers.Update(consumer);
            context.SaveChanges();
        }

        Console.WriteLine("User information updated successfully!");
    }

    static void UpdateOfficerInfo(MsebOfficer officer)
    {
        Console.WriteLine("\nEnter updated information:");
        Console.Write("Name: ");
        string name;
        while (!IsAlphabetic(name = Console.ReadLine()))
        {
            Console.WriteLine("Invalid input. Name must contain only alphabets. Try again.");
            Console.Write("Name: ");
        }

        Console.Write("City: ");
        string city;
        while (!IsAlphabetic(city = Console.ReadLine()))
        {
            Console.WriteLine("Invalid input. City must contain only alphabets. Try again.");
            Console.Write("City: ");
        };
        Console.Write("Mobile Number: ");
        string mobileNumber;
        while (!IsValidMobileNumber(mobileNumber = Console.ReadLine()))
        {
            Console.WriteLine("Invalid input. Mobile number must be 10 digits. Try again.");
            Console.Write("Mobile Number: ");
        }

        using (var context = new DataContext())
        {
            context.MsebOfficers.Update(officer);
            context.SaveChanges();
        }

        Console.WriteLine("MSEB Officer information updated successfully!");
    }


    static void ShowBill(Consumer consumer)
    {
        
            double price = consumer.Bill;

            Console.WriteLine("\nEnergy Bill:");
            Console.WriteLine($"Bill Amount: Rs.{price}");
        


        /*if (unitsConsumed < 30)
        {
            price = unitsConsumed * 3;
            Console.WriteLine($"Units Consumed: {unitsConsumed}");
            Console.WriteLine($"Price: Rs.{price}");
        }
        else if (unitsConsumed > 30 && unitsConsumed <= 100)
        {
            price = unitsConsumed * 30;
            Console.WriteLine($"Units Consumed: {unitsConsumed}");
            Console.WriteLine($"Price: Rs.{price}");
        }
        else if (unitsConsumed > 100 && unitsConsumed <= 300)
        {
            price = unitsConsumed * 370;
            Console.WriteLine($"Units Consumed: {unitsConsumed}");
            Console.WriteLine($"Price: Rs.{price}");
        }
        else if (unitsConsumed > 300 && unitsConsumed <= 500)
        {
            price = unitsConsumed * 500;
            Console.WriteLine($"Units Consumed: {unitsConsumed}");
            Console.WriteLine($"Price: Rs.{price}");
        }
        else
        {
            price = 600;
            Console.WriteLine($"Units Consumed: {unitsConsumed}");
            Console.WriteLine($"Price: Rs.{price}");
        }*/

    }

    static void PayAmount(Consumer consumer)
    {
        Console.WriteLine($"You have to pay {consumer.Bill} for Consumer {consumer.ConsumerName}: ");
        Console.WriteLine("Enter Payment amount to Pay:");
        double paymentAmount = double.Parse(Console.ReadLine());

        if (paymentAmount == consumer.Bill)
        {
            consumer.Bill -= paymentAmount;
            Console.WriteLine($"Payment of {paymentAmount} successful. Remaining Bill Amount: {consumer.Bill}");

            using (var context = new DataContext())
            {
                context.Consumers.Update(consumer);
                context.SaveChanges();
            }
        }
        else
        {
            Console.WriteLine("Please enter a valid amount.");
        }
    }
  

    /* static void DeleteAccount(int userId)
     {
         using (var context = new DataContext())
         {
             var consumer = context.Consumers.Find(userId);
             if (consumer != null)
             {
                 context.Consumers.Remove(consumer);
                 context.SaveChanges();
             }
         }
     }*/


    // Helper methods for validations...
    static bool IsAlphabetic(string input)
    {
        return !string.IsNullOrEmpty(input) && input.All(char.IsLetter);
    }

    static bool IsValidMobileNumber(string input)
    {
        return !string.IsNullOrEmpty(input) && input.Length == 10 && input.All(char.IsDigit);
    }
    static void DisplayConsumerData()
    {
        using (var context = new DataContext())
        {
            var consumers = context.Consumers.ToList();

            Console.WriteLine("\nConsumer Data:");
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine("UserId | ConsumerName | City | BillId | OldReading | NewReading | BillAmount");
            Console.WriteLine("--------------------------------------------------------------------");

            foreach (var consumer in consumers)
            {
                Console.WriteLine($"{consumer.ConsumerId,-7} | {consumer.ConsumerName,-12} | {consumer.City,-10} | {consumer.BillId,-6} | {consumer.OldReading,-10} | {consumer.NewReading,-10} | Rs.{consumer.Bill}");
            }

            Console.WriteLine("--------------------------------------------------------------------");
        }
    }

    /* static void DisplayConsumersByCity(string officerCity)
     {
         using (var context = new DataContext())
         {
             var consumers = context.Consumers
                 .Where(c => c.City.Equals(officerCity, StringComparison.OrdinalIgnoreCase))
                 .ToList();

             Console.WriteLine($"\nConsumers in {officerCity}:");

             if (consumers.Count == 0)
             {
                 Console.WriteLine("No consumers found in this city.");
                 return;
             }

             Console.WriteLine("------------------------------------------------------");
             Console.WriteLine("UserId | ConsumerName | City | BillId | OldReading | NewReading");
             Console.WriteLine("------------------------------------------------------");

             foreach (var consumer in consumers)
             {
                 Console.WriteLine($"{consumer.ConsumerId,-7} | {consumer.ConsumerName,-12} | {consumer.City,-10} | {consumer.BillId,-6} | {consumer.OldReading,-10} | {consumer.NewReading}");
             }

             Console.WriteLine("------------------------------------------------------");
         }
     }*/


    static void DeleteConsumerAccount()
    {
        Console.Write("Enter User ID to delete the consumer account: ");
        int userId = int.Parse(Console.ReadLine());

        using (var context = new DataContext())
        {
            var consumer = context.Consumers.Find(userId);
            if (consumer != null)
            {
                context.Consumers.Remove(consumer);
                context.SaveChanges();
                Console.WriteLine($"Consumer account with User ID {userId} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Consumer account with User ID {userId} not found.");
            }
        }
    }
    static void DisplayOfficerData()
    {
        using (var context = new DataContext())
        {
            var officers = context.MsebOfficers.ToList();

            Console.WriteLine("\nMSEB Officer Data:");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("OfficerId | Name       | City      | Mobile Number");
            Console.WriteLine("-----------------------------------------------------");

            foreach (var officer in officers)
            {
                Console.WriteLine($"{officer.OfficerId,-9} | {officer.Name,-10} | {officer.City,-9} | {officer.MobileNumber}");
            }

            Console.WriteLine("-----------------------------------------------------");
        }
    }

    static void DeleteOfficerAccount()
    {
        Console.Write("Enter Officer ID to delete the MSEB officer account: ");
        int officerId = int.Parse(Console.ReadLine());

        using (var context = new DataContext())
        {
            var officer = context.MsebOfficers.Find(officerId);
            if (officer != null)
            {
                context.MsebOfficers.Remove(officer);
                context.SaveChanges();
                Console.WriteLine($"MSEB officer account with Officer ID {officerId} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"MSEB officer account with Officer ID {officerId} not found.");
            }
        }
    }


    static void UpdateConsumerDataByAdmin()
    {
        using (var context = new DataContext())
        {
            Console.Write("Enter Consumer ID to update the data: ");
            int consumerId = int.Parse(Console.ReadLine());

            var consumer = context.Consumers.Find(consumerId);
            if (consumer == null)
            {
                Console.WriteLine("Consumer not found.");
                return;
            }

            Console.WriteLine("Enter new Consumer Name: ");
            consumer.ConsumerName = Console.ReadLine();
            Console.WriteLine("Enter new Consumer City: ");
            consumer.City = Console.ReadLine();
            Console.WriteLine("Enter new Bill ID: ");
            consumer.BillId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter new Old Reading: ");
            consumer.OldReading = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter new New Reading: ");
            consumer.NewReading = int.Parse(Console.ReadLine());

            // Recalculate the bill amount based on the new readings
            consumer.Bill = CalculateBillAmount(consumer.OldReading, consumer.NewReading);

            context.SaveChanges();
        }

        Console.WriteLine("Consumer information updated successfully!");
    }

    static void UpdateConsumerDataByMsebOfficer()
    {
        using (var context = new DataContext())
        {
            Console.Write("Enter Consumer ID to update the data: ");
            int consumerId = int.Parse(Console.ReadLine());

            var consumer = context.Consumers.Find(consumerId);
            if (consumer == null)
            {
                Console.WriteLine("Consumer not found.");
                return;
            }

            Console.WriteLine("Enter new Old Reading: ");
            consumer.OldReading = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter new New Reading: ");
            consumer.NewReading = int.Parse(Console.ReadLine());

            // Recalculate the bill amount based on the new readings
            consumer.Bill = CalculateBillAmount(consumer.OldReading, consumer.NewReading);

            context.SaveChanges();
        }

        Console.WriteLine("Consumer information updated successfully!");
    }
    static double CalculateBillAmount(int oldReading, int newReading)
    {
        const double ratePerUnit = 5.0;
        int unitsConsumed = newReading - oldReading;
        return unitsConsumed * ratePerUnit;
    }
    static void UpdateMsebOfficerDataByAdmin()
    {
        using (var context = new DataContext())
        {
            Console.Write("Enter MSEB Officer ID to update the data: ");
            int officerId = int.Parse(Console.ReadLine());

            var msebOfficer = context.MsebOfficers.Find(officerId);
            if (msebOfficer == null)
            {
                Console.WriteLine("MSEB Officer not found.");
                return;
            }

            Console.WriteLine("Enter new MSEB Officer Name: ");
            msebOfficer.Name = Console.ReadLine();
            Console.WriteLine("Enter new MSEB Officer City: ");
            msebOfficer.City = Console.ReadLine();

            context.SaveChanges();
        }

        Console.WriteLine("MSEB Officer information updated successfully!");
    }
    
}

