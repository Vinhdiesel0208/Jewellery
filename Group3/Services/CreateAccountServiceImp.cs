using System;
using Microsoft.EntityFrameworkCore;
using Lib;
using Group3.Db;
using Group3.Reponsitory;

namespace Group3.Services
{
	public class CreateAccountServiceImp : ICreateAcountService
    {
        private DatabaseContext db;
        public CreateAccountServiceImp(DatabaseContext db)
		
            { this.db = db; }

        public async Task<bool> addUser(Users newUser)
        {
            bool userExists = await db.User.AnyAsync(u => u.Email == newUser.Email );
            if (userExists)
            {
                return false; // Người dùng đã tồn tại
            }

            await db.User.AddAsync(newUser);
            await db.SaveChangesAsync();
            return true; // Thêm người dùng thành công
        }


        public async Task<bool> edit(Users editUser)
        {
            var user = await db.User.FirstOrDefaultAsync(u => u.Id == editUser.Id);
            if (user != null) 
            {
                user.Password = editUser.Password;
                user.Email = editUser.Email;
                user.PhoneNumber = editUser.PhoneNumber;
                user.Address = editUser.Address;
                user.Role = editUser.Role;
                await db.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }

        public async Task<IEnumerable<Users>> GetUser()
        {
            return await db.User.ToListAsync();
        }

        public async Task<Users> GetUserById(int id)
        {
            return await db.User.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Users> GetUserByAccNo(string accNo)
        {
            return await db.User.FirstOrDefaultAsync(u => u.Email.Equals(accNo));
        }
    }
}

