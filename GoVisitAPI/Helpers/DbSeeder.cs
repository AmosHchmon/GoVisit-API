using GoVisitAPI.DataModel;

namespace GoVisitAPI.Helpers
{
    public static class DbSeeder
    {
        public static void Seed(GoVisitContext db)
        {
            // Organization 1
            var org1 = new PublicOrganization
            {
                Name = "משרד התחבורה",
                Description = "שירותי רישוי תחבורה",
                Departments = new List<Department>()
            };

            // Organization 2
            var org2 = new PublicOrganization
            {
                Name = "משרד הפנים",
                Description = "רשות האוכלוסין וההגירה",
                Departments = new List<Department>()
            };

            // Add departments to organizations
            org1.Departments.Add(new Department
            {
                Name = "אגף רישוי נהיגה",
                Services = new List<Service>(),
                StaffMembers = new List<StaffMember>()
            });

            org1.Departments.Add(new Department
            {
                Name = "אגף מבחני נהיגה",
                Services = new List<Service>(),
                StaffMembers = new List<StaffMember>()
            });

            org2.Departments.Add(new Department
            {
                Name = "אגף מרשם אוכלוסין",
                Services = new List<Service>(),
                StaffMembers = new List<StaffMember>()
            });

            org2.Departments.Add(new Department
            {
                Name = "אגף הסדרת אשרות",
                Services = new List<Service>(),
                StaffMembers = new List<StaffMember>()
            });

            // Retrieve departments from objects
            var dep1 = org1.Departments.ElementAt(0);
            var dep2 = org1.Departments.ElementAt(1);
            var dep3 = org2.Departments.ElementAt(0);
            var dep4 = org2.Departments.ElementAt(1);

            // Add services
            dep1.Services.Add(new Service { Name = "חידוש רישיון נהיגה", DurationMinutes = 15 });
            dep1.Services.Add(new Service { Name = "הנפקת רישיון חדש", DurationMinutes = 20, RequiresDocuments = true });

            dep2.Services.Add(new Service { Name = "מבחן תיאוריה", DurationMinutes = 30 });

            dep3.Services.Add(new Service { Name = "הנפקת תעודת זהות", DurationMinutes = 25 });
            dep3.Services.Add(new Service { Name = "הנפקת דרכון ביומטרי", DurationMinutes = 20 });

            // Add staff
            dep1.StaffMembers.Add(new StaffMember { FullName = "יעל כהן", Role = "פקידת רישוי" });
            dep2.StaffMembers.Add(new StaffMember { FullName = "דני לוי", Role = "מבחן תיאוריה" });
            dep3.StaffMembers.Add(new StaffMember { FullName = "רונית פרץ", Role = "טיפול במרשם" });
            dep4.StaffMembers.Add(new StaffMember { FullName = "עדן אברג'יל", Role = "טיפול באשרות" });

            // Add organizations to DB
            db.PublicOrganizations.Add(org1);
            db.PublicOrganizations.Add(org2);

            // Invitees (standalone, no parent)
            db.Invitees.AddRange(
                new Invitee { FullName = "משה כהן", Email = "moshe@test.com", Phone = "050-1111111" },
                new Invitee { FullName = "שרה לוי", Email = "sara@test.com", Phone = "050-2222222" },
                new Invitee { FullName = "יואב ישראלי", Email = "yoav@test.com", Phone = "050-3333333" }
            );

            db.SaveChanges();
        }
    }


}
