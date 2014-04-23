namespace HotelWizard.Migrations
{
    using HotelWizard.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HotelWizard.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(HotelWizard.Models.ApplicationDbContext context)
        {
            this.AddUserAndRoles();
        }


        bool AddUserAndRoles()
        {
            bool success = false;

            var idManager = new IdentityManager();
            success = idManager.CreateRole("Admin");
            if (!success == true) return success;

            success = idManager.CreateRole("Reception");
            if (!success == true) return success;

            success = idManager.CreateRole("HR");
            if (!success) return success;
            
            success = idManager.CreateRole("Restaurant");
            if (!success) return success;

            success = idManager.CreateRole("EventRoom");
            if (!success) return success;

            var newUser = new ApplicationUser()
            {
                UserName = "Admin",
                FirstName = "Name",
                LastName = "LastName",
                Email = "admin@hotelwizzard.com"
            };

            // Be careful here - you  will need to use a password which will 
            // be valid under the password rules for the application, 
            // or the process will abort:
            success = idManager.CreateUser(newUser, "password");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser.Id, "Admin");
            if (!success) return success;

            //success = idManager.AddUserToRole(newUser.Id, "Reception");
            //if (!success) return success;

            //success = idManager.AddUserToRole(newUser.Id, "HR");
            //if (!success) return success;

            return success;
        }
    }
}