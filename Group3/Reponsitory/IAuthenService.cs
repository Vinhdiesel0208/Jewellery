using System;
namespace Group3.Reponsitory
{
	public interface IAuthenService
	{

        bool IsUserLoggedIn();

        bool IsUserAdmin();
    }
}

