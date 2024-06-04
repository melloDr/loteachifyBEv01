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
        Task<ResultModel> RegisterUser(string email, string password, string confirmPassword);
        Task<ResultModel> GetListUserAll();
    }
}
