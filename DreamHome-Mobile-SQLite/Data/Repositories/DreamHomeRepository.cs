using DreamHome_Mobile_SQLite.Models;
using Microsoft.EntityFrameworkCore;

namespace DreamHome_Mobile_SQLite.Data.Repositories
{
    /// <summary>
    /// DreamHome Repository class
    /// </summary>
    public class DreamHomeRepository : IDreamHomeRepository
    {
        private readonly IDbContextFactory<DreamHomeDbContext> _factory;

        public DreamHomeRepository(IDbContextFactory<DreamHomeDbContext> factory)
        {
            _factory = factory;
        }


        /// <summary>
        /// Get all branches
        /// </summary>
        /// <returns>List of Branch objects</returns>
        public async Task<List<Branch>> GetBranches()
        {
            await using var db = await _factory.CreateDbContextAsync();
            var branches = await db.Branches.AsNoTracking().ToListAsync();

            return branches;
        }


        /// <summary>
        /// Get properties for given branch
        /// </summary>
        /// <returns>List Property objects</returns>
        public async Task<List<PropertyForRent>> GetPropertiesForBranch(string branchNo)
        {
            await using var db = await _factory.CreateDbContextAsync();
            var properties = await db.PropertiesForRent
                                    .AsNoTracking()
                                    .Where(p => p.BranchNo == branchNo)
                                    .ToListAsync();
            return properties;
        }


        /// <summary>
        /// Get staff for given branch
        /// </summary>
        /// <returns>List Staff objects</returns>
        public async Task<List<Staff>> GetStaffForBranch(string branchNo)
        {
            await using var db = await _factory.CreateDbContextAsync();
            var staff = await db.Staff
                                    .AsNoTracking()
                                    .Where(s => s.BranchNo == branchNo)
                                    .ToListAsync();
            return staff;
        }


        /// <summary>
        /// Get all earners above a gven threshold
        /// </summary>
        /// <param name="salary">Salary threshold</param>
        /// <returns>List Staff objects</returns>

        public async Task<List<Staff>> GetHighEarners(decimal salary)
        {
            await using var db = await _factory.CreateDbContextAsync();

            var highEarners = await db.Staff
                                    .AsNoTracking()
                                    .Where(s => s.Salary > salary)
                                    .ToListAsync();

            return highEarners;
        }


        /// <summary>
        /// Get a count properties viewed between the given start and end dates
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>integer count</returns>
        public async Task<int> GetPropertyViewingsCount(DateTime startDate, DateTime endDate)
        {
            await using var db = await _factory.CreateDbContextAsync();

            var viewingCount = await db.Viewings
                            .AsNoTracking()
                            .Where(v => v.ViewDate >= startDate &&
                                        v.ViewDate <= endDate)
                            .Select(v => v.PropertyNo)
                            .Distinct()
                            .CountAsync();

            return viewingCount;
        }


        /// <summary>
        /// Get a list of properties viewed between the given start and end dates
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>integer count</returns>
        public async Task<List<Viewing>> GetPropertyViewings(DateTime startDate, DateTime endDate)
        {
            await using var db = await _factory.CreateDbContextAsync();

            var viewings = await db.Viewings
                            .AsNoTracking()
                            .Where(v => v.ViewDate >= startDate &&
                                        v.ViewDate <= endDate)
                            .OrderByDescending(v => v.ViewDate)
                            .ToListAsync();

            return viewings;
        }

        /// <summary>
        /// Get all client viewings
        /// </summary>
        /// <returns>List ClientViewing objects</returns>
        public async Task<List<ClientViewing>> GetClientViewings()
        {
            await using var db = await _factory.CreateDbContextAsync();

            var clientViewings = await db.Clients
                                    .AsNoTracking()
                                    .Join(db.Viewings,
                                          c => c.ClientNo,
                                          v => v.ClientNo,
                                          (c, v) => new ClientViewing
                                          {
                                              ClientNo = c.ClientNo,
                                              FName = c.FName,
                                              LName = c.LName,
                                              PropertyNo = v.PropertyNo,
                                              Comment = v.Comments
                                          })
                                    .ToListAsync();
            return clientViewings;
        }


        /// <summary>
        /// Get count of staff for each branch
        /// </summary>
        /// <returns>List BranchStaffCount objects</returns>
        public async Task<List<BranchStaffCount>> GetBranchStaffCount()
        {
            await using var db = await _factory.CreateDbContextAsync();

            var branchStaffCount = await db.PropertiesForRent
                                        .AsNoTracking()
                                        .Join(db.Staff,
                                              p => p.StaffNo,
                                              s => s.StaffNo,
                                              (p, s) => new { s.BranchNo, s.StaffNo })
                                        .GroupBy(x => new { x.BranchNo, x.StaffNo })
                                        .Select(g => new BranchStaffCount
                                        {
                                            BranchNo = g.Key.BranchNo,
                                            StaffNo = g.Key.StaffNo,
                                            MyCount = g.Count()
                                        })
                                        .OrderBy(x => x.BranchNo)
                                        .ThenBy(x => x.StaffNo)
                                        .ToListAsync();
            return branchStaffCount;
        }


        /// <summary>
        /// Get list of staff in branches in a given city
        /// </summary>
        /// <param name="city">city to be filtered</param>
        /// <returns>List CityStaff objects</returns>
        public async Task<List<CityStaff>> GetCityStaff(string  city)
        {
            await using var db = await _factory.CreateDbContextAsync();

            var cityStaff = await db.Staff
                                .AsNoTracking()
                                .Where(s => EF.Functions.Like(s.Branch.City, city))
                                .Select(s => new CityStaff
                                {
                                    StaffNo = s.StaffNo,
                                    FName = s.FName,
                                    LName = s.LName,
                                    Position = s.Position
                                })
                                .ToListAsync();

            return cityStaff;
        }


        /// <summary>
        /// Add/Update a staff record
        /// </summary>
        /// <param name="staff">Staff record to be added/updated</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Staff> AddOrUpdateStaffAsync(Staff staff)
        {
            if (staff is null) throw new ArgumentNullException(nameof(staff));
            if (string.IsNullOrWhiteSpace(staff.StaffNo))
                throw new ArgumentException("StaffNo is required.", nameof(staff));
            if (string.IsNullOrWhiteSpace(staff.BranchNo))
                throw new ArgumentException("BranchNo is required.", nameof(staff));

            await using var db = await _factory.CreateDbContextAsync();

            // 1) Validate FK: Branch must exist
            var branchExists = await db.Branches
                .AsNoTracking()
                .AnyAsync(b => b.BranchNo == staff.BranchNo);

            if (!branchExists)
                throw new ArgumentException($"Branch number '{staff.BranchNo}' does not exist.", nameof(staff.BranchNo));

            // 2) Try to find existing Staff by PK
            var existing = await db.Staff
                .FirstOrDefaultAsync(s => s.StaffNo == staff.StaffNo);

            if (existing is null)
            {
                // 3a) Insert
                db.Staff.Add(staff);
            }
            else
            {
                existing.FName = staff.FName;
                existing.LName = staff.LName;
                existing.Position = staff.Position;
                existing.Dob = staff.Dob;
                existing.Salary = staff.Salary;
                existing.BranchNo = staff.BranchNo;
            }
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Failed to persist Staff changes.", ex);
            }

            return existing ?? staff;
        }

    }
}
