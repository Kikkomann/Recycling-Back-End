using System;
using System.Linq;
using Recycling.Model.Entities;

namespace Recycling.Data
{
    public class RecyclingDbInitializer
    {
        private static RecyclingContext context;
        public static void Initialize(IServiceProvider serviceProvider)
        {
            context = (RecyclingContext)serviceProvider.GetService(typeof(RecyclingContext));

            InitializeSchedules();
        }

        private static void InitializeSchedules()
        {
            WasteManagement wasteMgmt01 = new WasteManagement { Name = "AffaldPlus" };

            if (!context.WasteManagements.Any()) { context.WasteManagements.Add(wasteMgmt01); }

            User user01 = new User { FirstName = "Chris", LastName = "Sakellarios" };
            User user02 = new User { FirstName = "Charlene", LastName = "Campbell" };
            User user03 = new User { FirstName = "Mattie", LastName = "Lyons" };
            User user04 = new User { FirstName = "Kelly", LastName = "Alvarez" };
            User user05 = new User { FirstName = "Charlie", LastName = "Cox" };
            User user06 = new User { FirstName = "Megan", LastName = "Fox" };

            if (!context.Users.Any())
            {
                context.Users.Add(user01); context.Users.Add(user02);
                context.Users.Add(user03); context.Users.Add(user04);
                context.Users.Add(user05); context.Users.Add(user06);
            }

            Hub hub01 = new Hub { Name = "Canadien Hubs", Location = "G4", WasteManagementId = wasteMgmt01.Id };
            Hub hub02 = new Hub { Name = "Hubsa", Location = "J4", WasteManagementId = wasteMgmt01.Id };
            Hub hub03 = new Hub { Name = "Downtown", Location = "C3", WasteManagementId = wasteMgmt01.Id };
            Hub hub04 = new Hub { Name = "Seje Hub", Location = "D2", WasteManagementId = wasteMgmt01.Id };
            Hub hub05 = new Hub { Name = "Wazzup Hub", Location = "A1", WasteManagementId = wasteMgmt01.Id };

            if (!context.Hubs.Any())
            {
                context.Hubs.Add(hub01); context.Hubs.Add(hub02);
                context.Hubs.Add(hub03); context.Hubs.Add(hub04);
                context.Hubs.Add(hub05);
            }

            Fraction fraction01 = new Fraction { HubId = hub01.Id, UserId = user01.Id, IsClean = true, Weight = 0.73 };
            Fraction fraction02 = new Fraction { HubId = hub02.Id, UserId = user02.Id, IsClean = true, Weight = 0.5 };
            Fraction fraction03 = new Fraction { HubId = hub03.Id, UserId = user03.Id, IsClean = true, Weight = 1.6 };
            Fraction fraction04 = new Fraction { HubId = hub04.Id, UserId = user01.Id, IsClean = true, Weight = 0.73 };
            Fraction fraction05 = new Fraction { HubId = hub05.Id, UserId = user04.Id, IsClean = false, Weight = 0.73 };
            Fraction fraction06 = new Fraction { HubId = hub01.Id, UserId = user05.Id, IsClean = true, Weight = 0.73 };
            Fraction fraction07 = new Fraction { HubId = hub01.Id, UserId = user05.Id, IsClean = true, Weight = 0.73 };
            Fraction fraction08 = new Fraction { HubId = hub01.Id, UserId = user01.Id, IsClean = false, Weight = 0.73 };
            Fraction fraction09 = new Fraction { HubId = hub02.Id, UserId = user02.Id, IsClean = true, Weight = 0.73 };
            Fraction fraction10 = new Fraction { HubId = hub03.Id, UserId = user03.Id, IsClean = true, Weight = 0.73 };
            Fraction fraction11 = new Fraction { HubId = hub04.Id, UserId = user01.Id, IsClean = true, Weight = 0.73 };

            if (!context.Fractions.Any())
            {
                context.Fractions.Add(fraction01); context.Fractions.Add(fraction02);
                context.Fractions.Add(fraction03); context.Fractions.Add(fraction04);
                context.Fractions.Add(fraction05); context.Fractions.Add(fraction06);
                context.Fractions.Add(fraction07); context.Fractions.Add(fraction08);
                context.Fractions.Add(fraction09); context.Fractions.Add(fraction10);
                context.Fractions.Add(fraction11); 
            }

            UserHub userHub01 = new UserHub { HubId = hub01.Id, UserId = user01.Id };
            UserHub userHub02 = new UserHub { HubId = hub02.Id, UserId = user02.Id };
            UserHub userHub03 = new UserHub { HubId = hub03.Id, UserId = user03.Id };
            UserHub userHub04 = new UserHub { HubId = hub04.Id, UserId = user01.Id };
            UserHub userHub05 = new UserHub { HubId = hub05.Id, UserId = user04.Id };
            UserHub userHub06 = new UserHub { HubId = hub01.Id, UserId = user05.Id };

            if (!context.UserHubs.Any())
            {
                context.Add(userHub01); context.Add(userHub02);
                context.Add(userHub03); context.Add(userHub04);
                context.Add(userHub05); context.Add(userHub06);
            }

            context.SaveChanges();
        }
    }
}
