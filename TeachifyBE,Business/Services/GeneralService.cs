using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachifyBE_Data.Models.ResultModel;
using TeachifyBE_Data.Repositories;

namespace TeachifyBE_Business.Services
{
    public class GeneralService : IGeneralService
    {
        private readonly IGeneralRepo _generalRepo;
        public GeneralService(IGeneralRepo generalRepo)
        {
            _generalRepo = generalRepo;
        }

        public async Task<ResultModel> GetListUserAll()
        {
            var result = await _generalRepo.GetListUser();
            var resultModel = new ResultModel()
            {
                Code = 200,
                Data = result,
                IsSuccess = true
            };
            return resultModel;
            
        }

        public async Task<ResultModel> RegisterUser(string email, string password, string confirmPassword)
        {
            var resultModel = new ResultModel();

            if (password != confirmPassword)
            {
                resultModel.IsSuccess = false;
                resultModel.Code = 400;
                resultModel.Message = "Confirm password fail!";
                return resultModel;
            }

            bool check = await _generalRepo.RegisterUser(email, password);
            if (check)
            {
                resultModel.IsSuccess = true;
                resultModel.Code = 201;
                resultModel.Message = "Register sucessfully!";
            }
            else
            {
                resultModel.IsSuccess = false;
                resultModel.Code = 400;
                resultModel.Message = "Register fail!";
            }
            return resultModel;
        }
    }
}
