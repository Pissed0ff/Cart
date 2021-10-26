using Cart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Controllers
{
    public class UserController
    {
        IActions actions = new DBActions();
        public User CurrentUser { get; set; }

        public User GetUser(string login)
        {
            var us = actions.GetOneElement<User>(login);
            return us;
        }
        public void CreateRole(Role role)
        {
            actions.Save<Role>(role);
        }

        public void CreateUser(User user)
        {
            actions.Save<User>(user);
        }
        public void SetRole(Role role)
        {
            actions.SetRole(CurrentUser, role);
        }

        public UserController(User _user)
        {
            var user = GetUser(_user.Login);
            if (user != null)
                CurrentUser = user;
            else
            {
                actions.Save<User>(_user);
                CurrentUser = _user;
            }         
        }
    }
}
