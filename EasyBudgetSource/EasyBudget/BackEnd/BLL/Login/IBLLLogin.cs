using BackEnd.Model;
using System;
using System.Collections.Generic;

namespace BackEnd.BLL.Login
{
	public interface IBLLLogin : IBLLGenerico<LOGIN>
    {
        String ByteArrayToHexString(byte[] ba);
        String GenerateHash(String input);
        LOGIN LoginUser(LOGIN login);
        String CreateUser(LOGIN login);
        String ChangePassword(LOGIN login);
        List<LOGIN> FindByUsername(LOGIN login);
        int? GetLastID();

        string[] getRolesForUser(string userName);
        LOGIN getUser(string userName, string password);
        bool isUserInRole(string userName, string roleName);
        List<LOGIN> getUsuariosRole(string roleName);

        bool isLoginInRole(string userName, string roleName);
    }

}
