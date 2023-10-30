using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Entities;
using VoAnhVu_DuAn2.Models;
using VoAnhVu_DuAn2.Repository;

namespace VoAnhVu_DuAn2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IAccessRepository _accessRepository;
        public AccessController(IAccessRepository accessRepository)
        {
            _accessRepository = accessRepository;
        }
        [HttpGet]
        [Route("/api/[Controller]/get-all-Access")]
        public IActionResult getAllAccess()
        {
            try
            {
                var access = _accessRepository.getAllAccess();
                if (!access.Any())
                {
                    return BadRequest("Không có truy cập nào.");
                }
                return Ok(access);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("/api/[Controller]/create-access")]
        public IActionResult createAccess(AccessModel access)
        {
            try
            {
                var kt = _accessRepository.getAllAccess().Where(c => c.AccessId == access.AccessId);
                if (kt.Any())
                {
                    return BadRequest("Id đã tồn tại ! Hãy nhập mã khác");
                }
                AccessEntity accessEntity = new AccessEntity
                {
                    AccessId = access.AccessId,
                    AccessName = access.AccessName
                };
                _accessRepository.createAccess(accessEntity);
                return Ok(accessEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("/api/[Controller]/update-access")]
        public IActionResult updateAccess(AccessModel access)
        {
            try
            {
                AccessEntity accessEntity = new AccessEntity
                {
                    AccessId = access.AccessId,
                    AccessName = access.AccessName
                };
                _accessRepository.updateAccess(accessEntity);
                return Ok(accessEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("/api/[Controller]/delete-access")]
        public IActionResult deleteAccess(string id)
        {
            try
            {
                bool access = _accessRepository.deleteAccess(id);
                if (!access)
                {
                    return BadRequest("Không tìm thấy quyền truy cập nào để xóa!");
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
