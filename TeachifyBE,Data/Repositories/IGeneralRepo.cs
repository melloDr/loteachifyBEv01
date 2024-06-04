﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachifyBE_Data.Entities;

namespace TeachifyBE_Data.Repositories
{
    public interface IGeneralRepo
    {
        public Task<bool> RegisterUser(string email, string password);
        public Task<List<TblUser>> GetListUser();


    }
}