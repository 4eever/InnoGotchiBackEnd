using BusinessAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BusinessAccessLayer.Services
{
    public interface IUserService
    {
        public Task<UserDTO> SignUp(UserSignUpDTO userSignUpDTO);
        public Task<UserDTO> LogIn(UserLogInDTO userLogInDTO);

        public Task<UserDTO> UpdateUser(UserDTO userDTO);
    }
}
