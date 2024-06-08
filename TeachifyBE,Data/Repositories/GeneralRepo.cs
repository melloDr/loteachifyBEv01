using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachifyBE_Data.Entities;
using TeachifyBE_Data.Models.InstructorModel;

namespace TeachifyBE_Data.Repositories
{
    public class GeneralRepo : IGeneralRepo
    {
        private readonly LoTeachify01DbContext _context;
        public GeneralRepo(LoTeachify01DbContext context) 
        {
            //_mapper = mapper;
            _context = context;
        }

        #region user
        public async Task<List<TblUser>> GetListUser()
        {
            return await _context.TblUsers.ToListAsync();
        }

        public async Task<TblUser> GetUser(string email, string password)
        {
            return await _context.TblUsers.Where(x => x.Email.Trim().Equals(email) 
                && x.Password.Trim().Equals(password)).FirstOrDefaultAsync();
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            var userEntity = new TblUser()
            {
                Id = Guid.NewGuid(),
                Email = email,
                Password = password
            };
            var userResult = await _context.TblUsers.AddAsync(userEntity);
            await _context.SaveChangesAsync();
            return userResult != null;
        }
        
        public async Task<bool> CheckPassword(Guid userId, string password)
        {
            var userEntity = await _context.TblUsers.Where(x => x.Id.Equals(userId)).FirstOrDefaultAsync();
            if (userEntity.Password.Trim() != password.Trim())
                return false;
            return true;
        }

        public async Task<bool> ChangePassword(Guid userId, string password)
        {
            var userEntity = await _context.TblUsers.Where(x => x.Id.Equals(userId)).FirstOrDefaultAsync();
            if (userEntity == null)
                return false;
            userEntity.Password = password;
            _context.Update(userEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PasswordRecovery(string email)
        {
            var userEntity = await _context.TblUsers.Where(x => x.Email.Equals(email)).FirstOrDefaultAsync();
            userEntity.Password = "1";
            _context.Update(userEntity);    
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion

        #region others
        public async Task<List<TblCity>> GetListCities()
        {
            return await _context.TblCities.ToListAsync();
        }

        public async Task<List<TblCourse>> GetListCourses()
        {
            return await _context.TblCourses.ToListAsync();
        }
        #endregion

        #region Instructor
        public async Task<bool> BecomeAnInstructor(TblInstructore entity)
        {
            var instructorResult = await _context.TblInstructores.AddAsync(entity);            
            await _context.SaveChangesAsync();
            return instructorResult != null;
        }

        public async Task<TblInstructore> GetIntructor(Guid instructorId)
        {
            return await _context.TblInstructores.Where(x => x.Id.Equals(instructorId)).FirstOrDefaultAsync();
        }

        public async Task<List<TblInstructore>> GetIntructors()
        {
            return await _context.TblInstructores.ToListAsync();
        }

        public async Task<List<TblInstructore>> SearchIntructors(string? subject, string? gender, string? city)
        {
            return await _context.TblInstructores.Where(x => 
            x.CourseDomain.Equals(subject)
            && x.Gender.Equals(gender)
            && x.City.Equals(city)).ToListAsync();
        }



        #endregion
    }
}
