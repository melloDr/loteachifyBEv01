using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachifyBE_Business.Utils;
using TeachifyBE_Business.Utils.TokenService;
using TeachifyBE_Data.Entities;
using TeachifyBE_Data.Models.CityModel;
using TeachifyBE_Data.Models.CourseModel;
using TeachifyBE_Data.Models.InstructorModel;
using TeachifyBE_Data.Models.ResultModel;
using TeachifyBE_Data.Models.UserModel;
using TeachifyBE_Data.Repositories;

namespace TeachifyBE_Business.Services
{
    public class GeneralService : IGeneralService
    {
        private readonly IGeneralRepo _generalRepo;
        private readonly DecodeToken _decodeToken;
        public GeneralService(IGeneralRepo generalRepo)
        {
            _decodeToken = new DecodeToken();
            _generalRepo = generalRepo;
        }

        #region user
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

        public async Task<ResultModel> ChangePassword(string oldPassword, string newPassword, string confirmPassword, string token)
        {
            Guid userId = Guid.Parse(_decodeToken.Decode(token, "userid"));
            if (confirmPassword.Trim() != newPassword.Trim())
            {
                return new ResultModel()
                {
                    Code = 400,
                    IsSuccess = false,
                    Message = "Please confirm your password again!"
                };
            }
            if (! await _generalRepo.CheckPassword(userId,oldPassword))
            {
                return new ResultModel()
                {
                    Code = 400,
                    IsSuccess = false,
                    Message = "Old password incorrect!"
                };
            }
            bool isChangedPassword = await _generalRepo.ChangePassword(userId,newPassword);
            if (!isChangedPassword)
            {
                return new ResultModel()
                {
                    Code = 400,
                    IsSuccess = false,
                    Message = "Cannot change password!"
                };
            }
            return new ResultModel()
            {
                Code = 200,
                IsSuccess = true,
                Message = "Password changed successfully!"
            };
        }

        public async Task<ResultModel> PasswordRecovery(string email)
        {
            var resultModel = new ResultModel();

            bool isRecoveredPassword = await _generalRepo.PasswordRecovery(email);
            if (!isRecoveredPassword)
            {
                return new ResultModel()
                {
                    Code = 400,
                    IsSuccess = false,
                    Message = "Recover Password Fail!"
                };
            }

            resultModel.IsSuccess = true;
            resultModel.Code = 200;
            resultModel.Data = true;
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


        #region Instructor
        public async Task<ResultModel> BecomeAnInstructor(InstructorModel model)
        {
            var resultModel = new ResultModel();
            var newInstructor = new TblInstructore()
            {
                Id = Guid.NewGuid(),
                City = model.City,
                CourseDomain = model.CourseDomain,
                Description = model.Description,
                Education = model.Education,
                Email = model.Email,
                Experience = model.Experience,
                Gender = model.Gender,
                HourlyRate = model.HourlyRate,
                ImageArray = model.ImageArray,
                Language = model.Language,
                Name = model.Name,
                Nationality = model.Nationality,
                OneLineTitle = model.OneLineTitle,
                Phone = model.Phone

            };
            bool check = await _generalRepo.BecomeAnInstructor(newInstructor);
            if (!check)
            {
                return new ResultModel()
                {
                    Code = 400,
                    IsSuccess = false,
                    Message = "Become new instructor fail!"
                };
            }
            resultModel.IsSuccess = true;
            resultModel.Code = 201;
            resultModel.Data = newInstructor;
            resultModel.Message = "Welcome to become an instructor!";
            return resultModel;
        }

        public async Task<ResultModel> GetIntructors(Guid? id, string? subject, string? gender, string? city)
        {
            var resultModel = new ResultModel();
            if (id != null)
            {
                var instructorEntity = await _generalRepo.GetIntructor((Guid)id);
                return new ResultModel()
                {
                    IsSuccess = true,
                    Code = 200,
                    Data = instructorEntity
                };
            }
            if (subject != null && gender != null && city != null )
            {
                return new ResultModel()
                {
                    Code = 200,
                    IsSuccess = true,
                    Data = await _generalRepo.SearchIntructors(subject, gender, city)
                };
            }

            var listInstructors = await _generalRepo.GetIntructors();

            resultModel.IsSuccess = true;
            resultModel.Code = 201;
            resultModel.Data = listInstructors;
            return resultModel;
        }

        #endregion
    }
}
