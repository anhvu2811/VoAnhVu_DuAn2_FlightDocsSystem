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
    public class GroupPermissionController : ControllerBase
    {
        private readonly IGroupPermissionRepository _groupPermissionRepository;
        public GroupPermissionController(IGroupPermissionRepository groupPermissionRepository)
        {
            _groupPermissionRepository = groupPermissionRepository;
        }
        [HttpGet]
        [Route("/api/[Controller]/get-all-group-permission")]
        public IActionResult getAllGroupPermission()
        {
            try
            {
                var gp = _groupPermissionRepository.getAllGroupPermission();
                if (!gp.Any())
                {
                    return BadRequest("Không có nhóm quyền nào.");
                }
                return Ok(gp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("/api/[Controller]/create-role")]
        public IActionResult createGroupPermission(GroupPermissionDTO gp)
        {
            try
            {
                var kt = _groupPermissionRepository.getAllGroupPermission().Where(c => c.GroupPermissionId == gp.GroupPermissionId);
                if (kt.Any())
                {
                    return BadRequest("Id đã tồn tại ! Hãy nhập mã khác");
                }
                GroupPermissionModel groupPermissionModel = new GroupPermissionModel
                {
                    GroupPermissionId = gp.GroupPermissionId,
                    GroupPermissionName = gp.GroupPermissionName,
                    CreateDate = gp.CreateDate,
                    Note = gp.Note,
                    AccessId = gp.Access.AccessId
                };
                _groupPermissionRepository.createGroupPermission(groupPermissionModel);
                return Ok(groupPermissionModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("/api/[Controller]/update-group-permission")]
        public IActionResult updateGroupPermissione(GroupPermissionDTO gp)
        {
            try
            {
                GroupPermissionModel groupPermissionModel = new GroupPermissionModel
                {
                    GroupPermissionId = gp.GroupPermissionId,
                    GroupPermissionName = gp.GroupPermissionName,
                    CreateDate = gp.CreateDate,
                    Note = gp.Note,
                    AccessId = gp.Access.AccessId
                };
                _groupPermissionRepository.updateGroupPermission(groupPermissionModel);
                return Ok(groupPermissionModel);
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
                bool role = _groupPermissionRepository.deleteGroupPermission(id);
                if (!role)
                {
                    return BadRequest("Không tìm thấy nhóm quyền để xóa!");
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
