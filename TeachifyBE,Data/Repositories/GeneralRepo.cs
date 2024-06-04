using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachifyBE_Data.Entities;

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

        public async Task<List<TblUser>> GetListUser()
        {
            return await _context.TblUsers.ToListAsync();
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
    }
}
