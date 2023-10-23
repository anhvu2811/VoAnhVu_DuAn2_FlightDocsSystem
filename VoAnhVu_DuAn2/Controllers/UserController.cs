using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Entities;
using VoAnhVu_DuAn2.Models;
using VoAnhVu_DuAn2.Services;

namespace VoAnhVu_DuAn2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AuthenticationService _authenticationService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UserController(IUserService userService, AuthenticationService authenticationService, IWebHostEnvironment webHostEnvironment)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost]
        [Route("/api/[Controller]/login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = _userService.GetUserByUserNameAndPassword(model.Email, model.Password);
            if (user == null)
            {
                //return Ok(new ApiResponse
                //{
                //    Success = false,
                //    Message = "Invalid username/password"
                //});
                return BadRequest("Invalid username/password");
            }
            var userId = user.UserId;
            var roleName = _userService.getRoleNameByUserId(userId);
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Authenticate success",
                Data = _authenticationService.GenerateToken(user, roleName)
            });
        }
        [HttpGet]
        [Route("/api/[Controller]/get-all-users")]
        public IActionResult getAllUsers()
        {
            try
            {
                var user = _userService.getAllUser();
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
        [HttpGet]
        [Route("/api/[Controller]/get-user-by-id")]
        public IActionResult getUserById(string id)
        {
            try
            {
                var user = _userService.getUserById(id);
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
        [HttpGet]
        [Route("/api/[Controller]/search-user")]
        public IActionResult searchUser(string key)
        {
            try
            {
                var user = _userService.searchUser(key).ToList();
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
        [HttpPost]
        [Route("/api/[Controller]/create-user")]
        public IActionResult createUser(UserModel user)
        {
            try
            {
                var kt = _userService.getAllUser().Where(c => c.UserId == user.UserId);
                if (kt.Any())
                {
                    return BadRequest("Id này đã tồn tại ! Hãy nhập mã khác.");
                }
                UserEntity userEntity = new UserEntity
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
                _userService.createUser(userEntity);
                return Ok(userEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("/api/[Controller]/update-user")]
        public IActionResult updateUser(UserModel user)
        {
            try
            {
                UserEntity userEntity = new UserEntity
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
                _userService.updateUser(userEntity);
                return Ok(userEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("/api/[Controller]/delete-user")]
        public IActionResult deleteUser(string id)
        {
            try
            {
                var user = _userService.deleteUser(id);
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
        [HttpPost]
        [Route("/api/[Controller]/change-password")]
        public IActionResult ChangePassword(string userId, string oldPassword, string newPassword)
        {
            try
            {
                _userService.changePassword(userId, oldPassword, newPassword);
                return Ok("Mật khẩu đã được thay đổi thành công.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("/api/[Controller]/update-avatar")]
        public IActionResult UpdateAvatar(string userId, IFormFile avatarFile)
        {
            try
            {
                var user = _userService.getUserById(userId);
                if (user == null)
                {
                    return NotFound("Người dùng không tồn tại.");
                }

                if (avatarFile == null || avatarFile.Length == 0)
                {
                    return BadRequest("Vui lòng chọn tệp ảnh.");
                }
                var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_avatar.jpg";
                var fullPath = Path.Combine(imagePath, uniqueFileName);

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    avatarFile.CopyTo(fileStream);
                }
                user.Avatar = "/Images/" + uniqueFileName;

                _userService.updateAvatar(userId, user.Avatar);
                return Ok("Đường dẫn ảnh đại diện đã được cập nhật.");
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi khi cập nhật đường dẫn ảnh đại diện: " + ex.Message);
            }
        }
        [HttpDelete]
        [Route("/api/[Controller]/update-avatar")]
        public IActionResult DeleteAvatar(string userId)
        {
            try
            {
                _userService.deleteAvatar(userId);
                return Ok("Avatar đã được xóa.");
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi khi xóa avatar: " + ex.Message);
            }
        }
    }
}
