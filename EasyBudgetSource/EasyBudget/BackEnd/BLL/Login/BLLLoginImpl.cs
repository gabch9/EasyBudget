using BackEnd.DAL;
using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace BackEnd.BLL.Login
{
	public class BLLLoginImpl : BLLGenericoImpl<LOGIN>, IBLLLogin
    {
        UnidadDeTrabajo<LOGIN> unit;
        EasyBudgetContext context;

        public string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }

            return hex.ToString();
        }

        public string CreateUser(LOGIN login)
        {
            try
            {
                login.LoginUsername = GenerateHash(login.LoginUsername);
                login.LoginPassword = GenerateHash(login.LoginPassword);

                using (unit = new UnidadDeTrabajo<LOGIN>(new EasyBudgetContext()))
                {
                    unit.genericDAL.Add(login);
                    unit.Complete();
                }
                return "Usuario insertado satisfactoriamente";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public List<LOGIN> FindByUsername(LOGIN login)
        {
            try
            {
                List<LOGIN> resultado;
                using (unit = new UnidadDeTrabajo<LOGIN>(new EasyBudgetContext()))
                {
                    Expression<Func<LOGIN, bool>> consulta = (l => l.LoginUsername.Equals(login.LoginUsername));
                    resultado = unit.genericDAL.Find(consulta).ToList();
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string GenerateHash(string input)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input);
            System.Security.Cryptography.SHA256Managed sha256hash = new SHA256Managed();
            byte[] hash = sha256hash.ComputeHash(bytes);
            return ByteArrayToHexString(hash);
        }

        public LOGIN LoginUser(LOGIN login)
        {
            try
            {
                login.LoginUsername = GenerateHash(login.LoginUsername);
                login.LoginPassword = GenerateHash(login.LoginPassword);
                LOGIN resultadoConsulta;
                
                using (unit = new UnidadDeTrabajo<LOGIN>(new EasyBudgetContext()))
                {
                    Expression<Func<LOGIN, bool>> consulta = (l => l.LoginUsername.Equals(login.LoginUsername) & l.LoginPassword.Equals(login.LoginPassword));
                    resultadoConsulta = unit.genericDAL.Find(consulta).ToList().FirstOrDefault();
                }
                
                return resultadoConsulta;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public String ChangePassword(LOGIN login)
        {
            try
            {
                login.LoginPassword = GenerateHash(login.LoginPassword);
                using (unit = new UnidadDeTrabajo<LOGIN>(new EasyBudgetContext()))
                {
                    unit.genericDAL.Update(login);
                }
                return "Contraseña actualizada exitosamente";
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Metodos para las sesiones//
        public int? GetLastID()
        {
            try
            {
                int? resultado;
                resultado = int.Parse(GetAll().Max(x => x.LoginId).ToString());

                return resultado;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<LOGIN> getUsuariosRole(string roleName)
        {
            List<LOGIN> result = new List<LOGIN>();
            List<string> lista;

            using (context = new EasyBudgetContext())
            {
                lista = context.sp_getLoginRole(roleName).ToList();
                LOGIN user;
                foreach (var item in lista)
                {
                    user = this.getUser(item);
                    result.Add(user);
                }
            }

            return result;
        }

        public bool isUserInRole(string userName, string roleName)
        {
            bool result = false;

            using (context = new EasyBudgetContext())
            {
                result = context.sp_isLoginInRole(userName, roleName) != 0;
            }

            return result;
        }

        public LOGIN getUser(string userName, string password)
        {
            try
            {
                if (password == null || userName == null)
                {
                    return null;
                }
                userName = GenerateHash(userName);
                password = GenerateHash(password);

                LOGIN resultado;
                using (unit = new UnidadDeTrabajo<LOGIN>(new EasyBudgetContext()))
                {
                    Expression<Func<LOGIN, bool>> consulta = (u => u.LoginUsername.Equals(userName) && u.LoginPassword.Equals(password));
                    resultado = unit.genericDAL.Find(consulta).ToList().FirstOrDefault();
                }

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public LOGIN getUser(string userName)
        {
            try
            {
                LOGIN resultado;
                userName = GenerateHash(userName);
                using (unit = new UnidadDeTrabajo<LOGIN>(new EasyBudgetContext()))
                {
                    Expression<Func<LOGIN, bool>> consulta = (u => u.LoginUsername.Equals(userName));
                    resultado = unit.genericDAL.Find(consulta).ToList().FirstOrDefault();
                }

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string[] getRolesForUser(string userName)
        {
            string[] result;
            using (context = new EasyBudgetContext())
            {
                result = context.sp_getRolesForLogin(userName).ToArray();
            }

            return result;
        }

        public bool isLoginInRole(string userName, string roleName)
        {
            bool result = false;
            using (context = new EasyBudgetContext())
            {
                result = context.sp_isLoginInRole(userName, roleName) != 0;
            }

            return result;
        }

    }

}
