using DreamHome_Mobile_SQLite.Models;
using System.Diagnostics;

namespace DreamHome_Mobile_SQLite.Data
{
    /// <summary>
    /// DbInitializer class to initialize the SQLite database
    /// </summary>
    public static class DbInitializer
    {

        public static void Initialize(DreamHomeDbContext dreamHomeDbContext)
        {
            try
            {
                dreamHomeDbContext.Database.EnsureCreated();    // create schema from models

                Seed(dreamHomeDbContext);                       // seed tables, if required
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }


        /// <summary>
        /// Seed the base tables
        /// </summary>
        /// <param name="dreamHomeDbContext">DreamHome DbContext instance</param>
        private static void Seed(DreamHomeDbContext dreamHomeDbContext)
        {
            try
            {
                if (!dreamHomeDbContext.Branches.Any())
                {
                    dreamHomeDbContext.Branches.AddRange(
                        new Branch { BranchNo = "B005", Street = "22 Deer Rd", City = "London", Postcode = "SW1 4EH" },
                        new Branch { BranchNo = "B007", Street = "16 Argyll St", City = "Aberdeen", Postcode = "AB2 3SU" },
                        new Branch { BranchNo = "B003", Street = "163 Main St", City = "Glasgow", Postcode = "G11 9QX" },
                        new Branch { BranchNo = "B004", Street = "32 Manse Rd", City = "Bristol", Postcode = "BS99 1NZ" },
                        new Branch { BranchNo = "B002", Street = "56 Clover Dr", City = "London", Postcode = "NW10 6EU" }
                    );
                }


                if (!dreamHomeDbContext.Staff.Any())
                {
                    dreamHomeDbContext.Staff.AddRange(
                        new Staff { StaffNo = "SL21", FName = "John", LName = "White", Position = "Manager", Dob = new DateTime(1975, 10, 1), Salary = 50000.00m, BranchNo = "B005" },
                        new Staff { StaffNo = "SG37", FName = "Ann", LName = "Beech", Position = "Assistant", Dob = new DateTime(1990, 11, 10), Salary = 32000.00m, BranchNo = "B003" },
                        new Staff { StaffNo = "SG14", FName = "David", LName = "Ford", Position = "Supervisor", Dob = new DateTime(1988, 3, 24), Salary = 38000.00m, BranchNo = "B003" },
                        new Staff { StaffNo = "SA9", FName = "Mary", LName = "Howe", Position = "Assistant", Dob = new DateTime(2000, 2, 19), Salary = 29000.00m, BranchNo = "B007" },
                        new Staff { StaffNo = "SG5", FName = "Susan", LName = "Brand", Position = "Manager", Dob = new DateTime(1970, 6, 3), Salary = 44000.00m, BranchNo = "B003" },
                        new Staff { StaffNo = "SL41", FName = "Julie", LName = "Lee", Position = "Assistant", Dob = new DateTime(1995, 6, 13), Salary = 29000.00m, BranchNo = "B005" }
                    );
                }

                if (!dreamHomeDbContext.PrivateOwners.Any())
                {
                    dreamHomeDbContext.PrivateOwners.AddRange(
                        new PrivateOwner { OwnerNo = "CO46", FName = "Joe", LName = "Keogh", Address = "2 Fergus Dr, Aberdeen AB2 7SX" },
                        new PrivateOwner { OwnerNo = "CO87", FName = "Carol", LName = "Farrel", Address = "6 Achray St, Glasgow G32 9DX" },
                        new PrivateOwner { OwnerNo = "CO40", FName = "Tina", LName = "Murphy", Address = "63 Well St, Glasgow G42" },
                        new PrivateOwner { OwnerNo = "CO93", FName = "Tony", LName = "Shaw", Address = "12 Park Pl, Glasgow G4 0QR" }
                    );
                }

                if (!dreamHomeDbContext.PropertiesForRent.Any())
                {
                    dreamHomeDbContext.PropertiesForRent.AddRange(
                        new PropertyForRent { PropertyNo = "PA14", Street = "16 Holhead", City = "Aberdeen", Postcode = "AB7 5SU", Type = "House", Rooms = 6, Rent = 650.0m, OwnerNo = "CO46", StaffNo = "SA9", BranchNo = "B007" },
                        new PropertyForRent { PropertyNo = "PL94", Street = "6 Argyll St", City = "London", Postcode = "NW2", Type = "Flat", Rooms = 4, Rent = 400.0m, OwnerNo = "CO87", StaffNo = "SL41", BranchNo = "B005" },
                        new PropertyForRent { PropertyNo = "PG4", Street = "6 Lawrence St", City = "Glasgow", Postcode = "G11 9QX", Type = "Flat", Rooms = 3, Rent = 350.0m, OwnerNo = "CO40", StaffNo = null, BranchNo = "B003" },
                        new PropertyForRent { PropertyNo = "PG36", Street = "2 Manor Rd", City = "Glasgow", Postcode = "G32 4QX", Type = "Flat", Rooms = 3, Rent = 375.0m, OwnerNo = "CO93", StaffNo = "SG37", BranchNo = "B003" },
                        new PropertyForRent { PropertyNo = "PG21", Street = "18 Dale Rd", City = "Glasgow", Postcode = "G12", Type = "House", Rooms = 5, Rent = 600.0m, OwnerNo = "CO87", StaffNo = "SG37", BranchNo = "B003" },
                        new PropertyForRent { PropertyNo = "PG16", Street = "5 Novar Dr", City = "Glasgow", Postcode = "G12 9AX", Type = "Flat", Rooms = 4, Rent = 450.0m, OwnerNo = "CO93", StaffNo = "SG14", BranchNo = "B003" }
                    );
                }

                if (!dreamHomeDbContext.Clients.Any())
                {
                    dreamHomeDbContext.Clients.AddRange(
                        new Client { ClientNo = "CR76", FName = "John", LName = "Kay", TelNo = "0207-774-5632", PrefType = "Flat", MaxRent = 425.0m },
                        new Client { ClientNo = "CR56", FName = "Aline", LName = "Stewart", TelNo = "0141-848-1825", PrefType = "Flat", MaxRent = 350.0m },
                        new Client { ClientNo = "CR74", FName = "Mike", LName = "Ritchie", TelNo = "01475-392178", PrefType = "House", MaxRent = 750.0m },
                        new Client { ClientNo = "CR62", FName = "Mary", LName = "Tregear", TelNo = "01224-196720", PrefType = "Flat", MaxRent = 600.0m }
                    );
                }

                if (!dreamHomeDbContext.Viewings.Any())
                {
                    dreamHomeDbContext.Viewings.AddRange(
                        new Viewing { ClientNo = "CR56", PropertyNo = "PA14", ViewDate = new DateTime(2023, 05, 24), Comments = "too small" },
                        new Viewing { ClientNo = "CR76", PropertyNo = "PG4", ViewDate = new DateTime(2023, 04, 20), Comments = "too remote" },
                        new Viewing { ClientNo = "CR56", PropertyNo = "PG4", ViewDate = new DateTime(2023, 05, 26), Comments = null },
                        new Viewing { ClientNo = "CR62", PropertyNo = "PA14", ViewDate = new DateTime(2023, 05, 14), Comments = "no dining room" },
                        new Viewing { ClientNo = "CR56", PropertyNo = "PG36", ViewDate = new DateTime(2023, 04, 28), Comments = null }
                    );
                }

                if (!dreamHomeDbContext.Registrations.Any())
                {
                    dreamHomeDbContext.Registrations.AddRange(
                        new Registration { ClientNo = "CR76", BranchNo = "B005", StaffNo = "SL41", DateJoined = new DateTime(2023, 01, 02) },
                        new Registration { ClientNo = "CR56", BranchNo = "B003", StaffNo = "SG37", DateJoined = new DateTime(2022, 04, 11) },
                        new Registration { ClientNo = "CR74", BranchNo = "B003", StaffNo = "SG37", DateJoined = new DateTime(2021, 11, 16) },
                        new Registration { ClientNo = "CR62", BranchNo = "B007", StaffNo = "SA9", DateJoined = new DateTime(2022, 03, 07) }
                    );
                }

                dreamHomeDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine( ex.ToString() );
            }
        }
    }
}
