using System.Collections.Generic;
using System.Linq;
using InternAPI.Data;
using InternAPI.Models;
using InternAPI.Repository.IRepository;

namespace InternAPI.Repository
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly ApplicationDbContext _db;
        public ManagerRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Create manager in database.
        /// </summary>
        /// <param name="manager">Manager object</param>
        /// <returns>Boolean</returns>
        public bool CreateManager(Manager manager)
        {
            _db.Managers.Add(manager);
            return Save();
        }

        /// <summary>
        /// Get manager with specified ID.
        /// </summary>
        /// <param name="id">Manager ID</param>
        /// <returns>Manager</returns>
        public Manager GetManager(int id)
        {
            return _db.Managers.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Get manager with specified mail address.
        /// </summary>
        /// <param name="mail">Manager mail address</param>
        /// <returns>List of managers</returns>
        public Manager GetManager(string mail)
        {
            return _db.Managers.FirstOrDefault(x => x.Mail == mail);
        }

        /// <summary>
        /// Returns all managers in database.
        /// </summary>
        /// <returns>List of managers</returns>
        public ICollection<Manager> GetManagers()
        {
            return _db.Managers.OrderBy(x => x.Id).ToList();
        }

        /// <summary>
        /// Checks if manager does exists with given ID in database.
        /// </summary>
        /// <param name="id">Manager ID</param>
        /// <returns>Boolean</returns>
        public bool ManagerExists(int id)
        {
            return _db.Managers.Any(x => x.Id == id);
        }

       /// <summary>
       /// Checks if manager does exists with given mail in database.
       /// </summary>
       /// <param name="mail">Manager mail address</param>
       /// <returns>Boolean</returns>
        public bool ManagerExists(string mail)
        {
            return _db.Managers.Any(x => x.Mail == mail);
        }

        /// <summary>
        /// Removes manager in database
        /// </summary>
        /// <param name="manager">Manager object</param>
        /// <returns>Boolean</returns>
        public bool RemoveManager(Manager manager)
        {
            _db.Managers.Remove(manager);
            return Save();
        }

        /// <summary>
        /// Saves changes to database.
        /// </summary>
        /// <returns>boolean</returns>
        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        /// <summary>
        /// Updates manager in database.
        /// </summary>
        /// <param name="manager">Manager object</param>
        /// <returns>Boolean</returns>
        public bool UpdateManager(Manager manager)
        {
            _db.Managers.Update(manager);
            return Save();
        }
    }
}