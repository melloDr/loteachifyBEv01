using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachifyBE_Data.Entities;

namespace TeachifyBE_Data.Repositories
{
    public interface IGeneralRepo
    {
        #region user
        public Task<bool> RegisterUser(string email, string password);
        public Task<List<TblUser>> GetListUser();
        public Task<TblUser> GetUser(string email, string password);
        #endregion

        #region others
        public Task<List<TblCity>> GetListCities();
        public Task<List<TblCourse>> GetListCourses();
        #endregion


    }
}
