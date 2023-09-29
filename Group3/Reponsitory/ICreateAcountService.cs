using System;
using Lib;

namespace Group3.Reponsitory
{
	public interface ICreateAcountService
	{
        Task<IEnumerable<Users>> GetUser();

        Task<Users> GetUserById(int id);
        Task<Users> GetUserByAccNo(string accNo);
        Task<bool> addUser(Users newUser);
        Task<bool> edit(Users editUser);
    }
}

