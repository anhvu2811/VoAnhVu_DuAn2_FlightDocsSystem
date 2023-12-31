﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.DTO;
using VoAnhVu_DuAn2.Models;
using VoAnhVu_DuAn2.Repository;
using VoAnhVu_DuAn2.Services;

namespace VoAnhVu_DuAn2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthenticationService _authenticationService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UserController(IUserRepository userRepository, AuthenticationService authenticationService, IWebHostEnvironment webHostEnvironment)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost]
        [Route("/api/[Controller]/login")]
        public IActionResult Login([FromBody] LoginDTO model)
        {
            var user = _userRepository.GetUserByUserNameAndPassword(model.Email, model.Password);
            if (user == null)
            {
                return BadRequest("Invalid username/password");
            }
            var userId = user.UserId;
            var roleName = _userRepository.getRoleNameByUserId(userId);
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Authenticate success",
                Token = _authenticationService.GenerateToken(user, roleName)
            });
        }
        [Authorize]
        [HttpGet]
        [Route("/api/[Controller]/get-all-users")]
        public IActionResult getAllUsers()
        {
            try
            {
                var user = _userRepository.getAllUser();
                if (!user.Any())
                {
                    return BadRequest("Không có người dùng nào.");
                }
                return Ok(user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet]
        [Route("/api/[Controller]/get-user-by-id")]
        public IActionResult getUserById(string id)
        {
            try
            {
                var user = _userRepository.getUserById(id);
                if (user is null)
                {
                    return BadRequest("Không tìm thấy người dùng.");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet]
        [Route("/api/[Controller]/search-user")]
        public IActionResult searchUser(string key)
        {
            try
            {
                var user = _userRepository.searchUser(key).ToList();
                if (!user.Any())
                {
                    return BadRequest("Không tìm thấy người dùng.");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        [Route("/api/[Controller]/create-user")]
        public IActionResult createUser(UserDTO user)
        {
            try
            {
                var kt = _userRepository.getAllUser().Where(c => c.UserId == user.UserId);
                if (kt.Any())
                {
                    return BadRequest("Id này đã tồn tại ! Hãy nhập mã khác.");
                }
                UserModel userModel = new UserModel
                {
                    UserId = user.UserId,
                    Avatar = user.Avatar,
                    FullName = user.FullName,
                    Gender = user.Gender,
                    Email = user.Email,
                    Phone = user.Phone,
                    Address = user.Address,
                    Password = user.Password,
                    RoleId = user.Role.RoleId,
                };
                _userRepository.createUser(userModel);
                return Ok(userModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPut]
        [Route("/api/[Controller]/update-user")]
        public IActionResult updateUser(UserDTO user)
        {
            try
            {
                UserModel userModel = new UserModel
                {
                    UserId = user.UserId,
                    Avatar = user.Avatar,
                    FullName = user.FullName,
                    Gender = user.Gender,
                    Email = user.Email,
                    Phone = user.Phone,
                    Address = user.Address,
                    Password = user.Password,
                    RoleId = user.Role.RoleId
                };
                _userRepository.updateUser(userModel);
                return Ok(userModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("/api/[Controller]/delete-user")]
        public IActionResult deleteUser(string id)
        {
            try
            {
                var user = _userRepository.deleteUser(id);
                if (!user)
                {
                    return BadRequest("Không tìm thấy người dùng để xóa.");
                }
                return Ok("Xóa thành công.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        [Route("/api/[Controller]/change-password")]
        public IActionResult ChangePassword(string userId, string oldPassword, string newPassword)
        {
            try
            {
                _userRepository.changePassword(userId, oldPassword, newPassword);
                return Ok("Mật khẩu đã được thay đổi thành công.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        [Route("/api/[Controller]/update-avatar")]
        public IActionResult UpdateAvatar(string userId, IFormFile avatarFile)
        {
            try
            {
                var user = _userRepository.getUserById(userId);
                if (user == null)
                {
                    return NotFound("Người dùng không tồn tại.");
                }

                if (avatarFile == null || avatarFile.Length == 0)
                {
                    return BadRequest("Vui lòng chọn tệp ảnh.");
                }
                var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images");
                var uniqueFileName = avatarFile.FileName;
                var fullPath = Path.Combine(imagePath, uniqueFileName);

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    avatarFile.CopyTo(fileStream);
                }
                user.Avatar = "/Images/" + uniqueFileName;

                _userRepository.updateAvatar(userId, user.Avatar);
                return Ok("Đường dẫn ảnh đại diện đã được cập nhật.");
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi khi cập nhật đường dẫn ảnh đại diện: " + ex.Message);
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("/api/[Controller]/update-avatar")]
        public IActionResult DeleteAvatar(string userId)
        {
            try
            {
                _userRepository.deleteAvatar(userId);
                return Ok("Avatar đã được xóa.");
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi khi xóa avatar: " + ex.Message);
            }
        }
    }
}
