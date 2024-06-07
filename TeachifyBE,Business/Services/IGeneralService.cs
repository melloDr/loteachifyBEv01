using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachifyBE_Data.Models.ResultModel;

namespace TeachifyBE_Business.Services
{
    public interface IGeneralService
    {
        #region userService
        Task<ResultModel> RegisterUser(string email, string password, string confirmPassword);
        Task<ResultModel> Token(string email, string password);
        Task<ResultModel> GetListUserAll();
        #endregion

        #region others
        // city
        Task<ResultModel> Cities();
        Task<ResultModel> Courses();

        #endregion

    }
}
