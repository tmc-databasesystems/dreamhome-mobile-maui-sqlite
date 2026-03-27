using DreamHome_Mobile_SQLite.Data.Repositories;
using DreamHome_Mobile_SQLite.Models;

namespace DreamHome_Mobile_SQLite.Services
{
    /// <summary>
    /// DreamHome Service class
    /// </summary>
 
    public class DreamHomeService : IDreamHomeService
    {
        private readonly IDreamHomeRepository _dreamHomeRepository;

        public DreamHomeService(IDreamHomeRepository repository)
        {
            _dreamHomeRepository = repository;
        }


        /// <summary>
        /// Get all branches
        /// </summary>
        /// <returns>List Branch objects</returns>
        public async Task<List<Branch>> GetBranches()
        {
            return await _dreamHomeRepository.GetBranches();
        }


        /// <summary>
        /// Get properties for given branch
        /// </summary>
        /// <returns>List Property objects</returns>
        public async Task<List<PropertyForRent>> GetPropertiesForBranch(string branchNo)
        {
            return await _dreamHomeRepository.GetPropertiesForBranch(branchNo);
        }


        /// <summary>
        /// Get staff for given branch
        /// </summary>
        /// <returns>List Staff objects</returns>
        public async Task<List<Staff>> GetStaffForBranch(string branchNo)
        {
            return await _dreamHomeRepository.GetStaffForBranch(branchNo);
        }


        /// <summary>
        /// Get staff with a salary above a given threshold
        /// </summary>
        /// <param name="salary">slary threshold</param>
        /// <returns>List Staff objects</returns>
        public async Task<List<Staff>> GetHighEarners(decimal salary)
        {
            return await _dreamHomeRepository.GetHighEarners(salary);
        }


        /// <summary>
        /// Get a count of properties viewed between the given start and end dates
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>integer count</returns>
        public async Task<int> GetPropertyViewingsCount(DateTime startDate, DateTime endDate)
        {
            return await _dreamHomeRepository.GetPropertyViewingsCount(startDate, endDate);
        }


        /// <summary>
        /// Get a list of properties viewed between the given start and end dates
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>integer count</returns>
        public async Task<List<Viewing>> GetPropertyViewings(DateTime startDate, DateTime endDate)
        {
            return await _dreamHomeRepository.GetPropertyViewings(startDate, endDate);
        }


        /// <summary>
        /// Get all client viewings
        /// </summary>
        /// <returns>List ClientViewing objects</returns>
        public async Task<List<ClientViewing>> GetClientViewings()
        {
            return await _dreamHomeRepository.GetClientViewings();
        }


        /// <summary>
        /// Get count of staff for each branch
        /// </summary>
        /// <returns>List BranchStaffCount objects</returns>
        public async Task<List<BranchStaffCount>> GetBranchStaffCount()
        {
            return await _dreamHomeRepository.GetBranchStaffCount();
        }


        /// <summary>
        /// Get list of staff in branches in a given city
        /// </summary>
        /// <param name="city">city to be filtered</param>
        /// <returns>List CityStaff objects</returns>
        public async Task<List<CityStaff>> GetCityStaff(string city)
        {
            return await _dreamHomeRepository.GetCityStaff(city);
        }


        /// <summary>
        /// Add/Update a staff record
        /// </summary>
        /// <param name="staff">Staff record to be added/updated</param>
        /// <returns></returns>
        public async Task<Staff> AddOrUpdateStaffAsync(Staff staff)
        {
            return await _dreamHomeRepository.AddOrUpdateStaffAsync(staff);
        }


    }
}
