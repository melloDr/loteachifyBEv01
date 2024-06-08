using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachifyBE_Data.Entities;
using TeachifyBE_Data.Models.InstructorModel;

namespace TeachifyBE_Data.Repositories
{
    public interface IGeneralRepo
    {
        #region user
        public Task<bool> RegisterUser(string email, string password);
        public Task<List<TblUser>> GetListUser();
        public Task<TblUser> GetUser(string email, string password);
        public Task<bool> CheckPassword(Guid userId, string password);
        public Task<bool> ChangePassword(Guid userId, string password);
        public Task<bool> PasswordRecovery(string email);
        #endregion

        #region others
        public Task<List<TblCity>> GetListCities();
        public Task<List<TblCourse>> GetListCourses();
        #endregion

        #region Instructor
        public Task<bool> BecomeAnInstructor(TblInstructore model);
        public Task<TblInstructore> GetIntructor(Guid instructorId);
        public Task<List<TblInstructore>> GetIntructors();
        public Task<List<TblInstructore>> SearchIntructors(string? subject, string? gender, string? city);

        #endregion


    }
}
