using System;
using Group3.Db;
using Group3.Reponsitory;

namespace Group3.Services
{
    public class AuthenServiceImp : IAuthenService
	{
        private readonly DatabaseContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthenServiceImp(DatabaseContext context, IHttpContextAccessor httpContextAccessor)
		{
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsUserAdmin()
        {
            string accNo = _httpContextAccessor.HttpContext.Session.GetString("accNo");
            var user = _context.User.FirstOrDefault(u => u.Email == accNo);
            return user != null && user.Role;

        }

        public bool IsUserLoggedIn()
        {
            string accNo = _httpContextAccessor.HttpContext.Session.GetString("accNo");
            return !string.IsNullOrEmpty(accNo);
           
        }
    }
}

