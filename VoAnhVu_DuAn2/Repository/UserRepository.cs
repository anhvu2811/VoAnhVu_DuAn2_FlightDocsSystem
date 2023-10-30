using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Entities;
using VoAnhVu_DuAn2.Models;

namespace VoAnhVu_DuAn2.Repository
{
    public interface IUserRepository
    {
        List<UserModel> getAllUser();
        UserModel getUserById(string id);
        void createUser(UserEntity user);
        void updateUser(UserEntity user);
        bool deleteUser(string id);
        UserEntity GetUserByUserNameAndPassword(string username, string password);
        void changePassword(string id, string oldPassword, string newPassword);
        void updateAvatar(string id, string avatarUrl);
        void deleteAvatar(string id);
        List<UserEntity> searchUser(string key);
        string getRoleNameByUserId(string id);
    }
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _context;
        public UserRepository(MyDbContext context)
        {
            _context = context;
        }
        public void createUser(UserEntity user)
        {
            try
            {
                _context.UserEntities.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool deleteUser(string id)
        {
            try
            {
                var user = _context.UserEntities.FirstOrDefault(c => c.UserId == id);
                if (user is null)
                {
                    return false;
                }
                _context.UserEntities.Remove(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserModel> getAllUser()
        {
            var users = _context.UserEntities
                .Include(user => user.Role)
                .Select(user => new UserModel
                {
                    UserId = user.UserId,
                    Avatar = user.Avatar,
                    FullName = user.FullName,
                    Gender = user.Gender,
                    Email = user.Email,
                    Phone = user.Phone,
                    Address = user.Address,
                    Password = user.Password,
                    Role = user.Role
                }).ToList();
            return users;
        }

        public UserModel getUserById(string id)
        {
            var user = _context.UserEntities.Include(u => u.Role).FirstOrDefault(c => c.UserId == id);
            if (user != null)
            {
                var userModel = new UserModel
                {
                    UserId = user.UserId,
                    Avatar = user.Avatar,
                    FullName = user.FullName,
                    Gender = user.Gender,
                    Email = user.Email,
                    Phone = user.Phone,
                    Address = user.Address,
                    Password = user.Password,
                    Role = user.Role
                };
                return userModel;
            }
            return null;
        }

        public void updateUser(UserEntity user)
        {
            try
            {
                _context.UserEntities.Update(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void changePassword(string id, string oldPassword, string newPassword)
        {
            try
            {
                var user = _context.UserEntities.FirstOrDefault(p => p.UserId == id);
                if (user != null)
                {
                    if (user.Password == oldPassword)
                    {
                        user.Password = newPassword;
                        _context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Mật khẩu cũ không đúng.");
                    }
                }
                else
                {
                    throw new Exception("Người dùng không tồn tại.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserEntity GetUserByUserNameAndPassword(string email, string password)
        {
            if (IsValidVietJetAirEmail(email))
            {
                return _context.UserEntities.FirstOrDefault(p => p.Email == email && p.Password == password);
            }
            else
            {
                return null;
            }
        }

        public bool IsValidVietJetAirEmail(string email)
        {
            // Sử dụng biểu thức chính quy để kiểm tra định dạng email
            string pattern = @"^[a-zA-Z0-9._%+-]+@vietjetair\.com$";
            return Regex.IsMatch(email, pattern);
        }

        public void updateAvatar(string id, string avatarUrl)
        {
            try
            {
                var user = _context.UserEntities.FirstOrDefault(p => p.UserId == id);
                if (user != null)
                {
                    user.Avatar = avatarUrl;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void deleteAvatar(string id)
        {
            var user = _context.UserEntities.FirstOrDefault(p => p.UserId == id);
            if (user != null)
            {
                user.Avatar = null;
                _context.SaveChanges();
            }
        }
        public List<UserEntity> searchUser(string key)
        {
            key = key.ToLower();

            return _context.UserEntities.Where(c => c.UserId.ToLower().Contains(key) ||
                                                    c.FullName.ToLower().Contains(key) ||
                                                    c.Gender.ToLower().Contains(key)).ToList();
        }
        public string getRoleNameByUserId(string id)
        {
            var roleName = _context.UserEntities.Where(u => u.UserId == id).Select(u => u.Role.RoleName).FirstOrDefault();
            return roleName;
        }
    }
}
