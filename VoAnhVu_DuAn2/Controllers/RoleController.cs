﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.DTO;
using VoAnhVu_DuAn2.Models;
using VoAnhVu_DuAn2.Repository;

namespace VoAnhVu_DuAn2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "System Admin, Nhân viên GO")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        [HttpGet]
        [Route("/api/[Controller]/get-all-roles")]
        public IActionResult getAllRole()
        {
            try
            {
                var role = _roleRepository.getAllRole();
                if (!role.Any())
                {
                    return BadRequest("Không có vai trò nào.");
                }
                return Ok(role);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("/api/[Controller]/create-role")]
        public IActionResult createRole(RoleDTO role)
        {
            try
            {
                var kt = _roleRepository.getAllRole().Where(c => c.RoleId == role.RoleId);
                if (kt.Any())
                {
                    return BadRequest("Id đã tồn tại ! Hãy nhập mã khác");
                }
                RoleModel roleModel = new RoleModel
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName,
                    Description = role.Description,
                };
                _roleRepository.createRole(roleModel);
                return Ok(roleModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("/api/[Controller]/update-role")]
        public IActionResult updateRole(RoleDTO role)
        {
            try
            {
                RoleModel roleModel = new RoleModel
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName,
                    Description = role.Description,
                };
                _roleRepository.updateRole(roleModel);
                return Ok(roleModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("/api/[Controller]/delete-role")]
        public IActionResult deleteRole(string id)
        {
            try
            {
                bool role = _roleRepository.deleteRole(id);
                if (!role)
                {
                    return BadRequest("Không tìm thấy vai trò để xóa!");
                }
                return Ok("Xóa thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
