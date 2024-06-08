using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachifyBE_Business.Utils;
using TeachifyBE_Data.Entities;
using TeachifyBE_Data.Models.CityModel;
using TeachifyBE_Data.Models.CourseModel;
using TeachifyBE_Data.Models.ResultModel;
using TeachifyBE_Data.Models.UserModel;
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

        #region
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
            if (!check)
            {
                resultModel.IsSuccess = false;
                resultModel.Code = 400;
                resultModel.Message = "Register fail!";
                return resultModel;
            }
            var tblUser = await _generalRepo.GetUser(email.Trim(), password.Trim());
            if (tblUser == null)
            {
                resultModel.IsSuccess = false;
                resultModel.Code = 400;
                resultModel.Message = "User dont found!";
                return resultModel;
            }
            string token = ClassSup.CreateToken(email, tblUser.Id, "user");

            if (token == null)
            {
                return new ResultModel()
                {
                    Code = 400,
                    IsSuccess = false,
                    Message = "Create Token fail!"
                };
            }
            DateTime now = DateTime.Now;
            var tokenModel = new TokenModel()
            {
                access_token = token,
                expires_in = 1209599,
                userName = email,
                issued = now.ToString(),
                expires = now.AddDays(1).ToString(),
                token_type = "bearer"
            };
            resultModel.Code = 200;
            resultModel.Data = tokenModel;
            resultModel.IsSuccess = true;
            return resultModel;
        }

        public async Task<ResultModel> Token(string email, string password)
        {
            var resultModel = new ResultModel();

            var tblUser = await _generalRepo.GetUser(email.Trim(), password.Trim());
            if (tblUser == null)
            {
                return new ResultModel()
                {
                    IsSuccess = false,
                    Code = 400,
                    Message = "You may have entered the wrong email or password!"
                };
            }

            string token = ClassSup.CreateToken(email, tblUser.Id, "user");

            if (token == null)
            {
                return new ResultModel()
                {
                    Code = 400,
                    IsSuccess = false,
                    Message = "Create Token fail!"
                };
            }
            DateTime now = DateTime.Now;
            var tokenModel = new TokenModel()
            {
                access_token = token,
                expires_in = 1209599,
                userName = email,
                issued = now.ToString(),
                expires = now.AddDays(1).ToString(),
                token_type = "bearer"
            };
            resultModel.Code = 200;
            resultModel.Data = tokenModel;
            resultModel.IsSuccess = true;
            return resultModel;
        }
        #endregion

        #region others
        public async Task<ResultModel> Cities()
        {
            var listCities = await _generalRepo.GetListCities();
            List<CityModel> nameCouses = new List<CityModel>();
            foreach (var item in listCities)
            {
                var city = new CityModel()
                {
                    Name = item.Name
                };
                nameCouses.Add(city);
            }

            return new ResultModel()
            {
                Code = 200,
                Data = nameCouses,
                IsSuccess = true,
            };

        }
        public async Task<ResultModel> Courses()
        {
            var listCities = await _generalRepo.GetListCourses();
            List<CourseModel> nameCouses = new List<CourseModel>();
            foreach (var item in listCities) 
            { 
                var city = new CourseModel()
                {
                    Name = item.Name
                };
                nameCouses.Add(city);
            }

            return new ResultModel()
            {
                Code = 200,
                Data = nameCouses,
                IsSuccess = true,
            };
        }
        #endregion
    }
}
