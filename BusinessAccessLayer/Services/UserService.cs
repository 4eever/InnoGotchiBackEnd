using BusinessAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Repositories;
using DataAccessLayer.Entities;
using AutoMapper;
using System.Runtime.CompilerServices;

namespace BusinessAccessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _userMapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userMapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDTO> GetUser(int userId)
        {
            User user = await _userRepository.GetUserById(userId);
            UserDTO userDTO = _userMapper.Map<User, UserDTO>(user);

            return userDTO;
        }

        public async Task<UserDTO> SignUp(UserSignUpDTO userSignUpDTO)
        {
                    
            User userEntity = _userMapper.Map<UserSignUpDTO, User>(userSignUpDTO);
            await _userRepository.AddUser(userEntity);

            User user = await _userRepository.GetUserByEmail(userEntity.UserEmail);
            UserDTO userDTOResult = _userMapper.Map<User, UserDTO>(user);

            return userDTOResult;
        }

        public async Task<UserDTO> LogIn(UserLogInDTO userLogInDTO)
        {
            User userdb = await _userRepository.GetUserByEmail(userLogInDTO.UserEmail);
            UserDTO userDTOResult = _userMapper.Map<User, UserDTO>(userdb);

            return userDTOResult;
        }

        public async Task<UserDTO> UpdateUser(UserDTO userDTO)
        {
            User userEntity = _userMapper.Map<UserDTO, User>(userDTO);
            await _userRepository.UpdateUser(userEntity);

            User user = await _userRepository.GetUserByEmail(userEntity.UserEmail);
            UserDTO userDTOResult = _userMapper.Map<User, UserDTO>(user);

            return userDTOResult;
        }

        public async Task<int?> GetUserId(string userEmail)
        {
            var user = await _userRepository.GetUserByEmail(userEmail);
            if (user != null)
            {
                return user.UserId;
            }
            else
            {
                return null;
            }
        }
    }
}
