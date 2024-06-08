using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachifyBE_Data.Models.InstructorModel;
using TeachifyBE_Data.Models.ResultModel;

namespace TeachifyBE_Business.Services
{
    public interface IGeneralService
    {
        #region userService
        Task<ResultModel> RegisterUser(string email, string password, string confirmPassword);
        Task<ResultModel> Token(string email, string password);
        Task<ResultModel> GetListUserAll();
        Task<ResultModel> ChangePassword(string oldPassword, string newPassword, string confirmPassword, string token);
        Task<ResultModel> PasswordRecovery(string email);
        #endregion

        #region others
        // city
        Task<ResultModel> Cities();
        Task<ResultModel> Courses();
        #endregion
        #region Instructor

        Task<ResultModel> BecomeAnInstructor(InstructorModel instructor);
        Task<ResultModel> GetIntructors(Guid? id, string? subject, string? gender, string? city);
        #endregion

    }
}
