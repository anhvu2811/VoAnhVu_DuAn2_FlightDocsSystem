using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Entities;
using VoAnhVu_DuAn2.Models;
using VoAnhVu_DuAn2.Services;

namespace VoAnhVu_DuAn2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        private readonly IDocumentTypeService _documentTypeService;
        public DocumentTypeController(IDocumentTypeService documentTypeService)
        {
            _documentTypeService = documentTypeService;
        }
        [HttpGet]
        [Route("/api/[Controller]/get-all-document-type")]
        public IActionResult getAllDocumentType()
        {
            try
            {
                var dt = _documentTypeService.getAllDocumentType();
                if (!dt.Any())
                {
                    return BadRequest("Không có loại nào.");
                }
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("/api/[Controller]/create-document-type")]
        public IActionResult createDocumentType(DocumentTypeModel dt)
        {
            try
            {
                var kt = _documentTypeService.getAllDocumentType().Where(c => c.DocumentTypeId == dt.DocumentTypeId);
                if (kt.Any())
                {
                    return BadRequest("Id đã tồn tại ! Hãy nhập mã khác");
                }
                DocumentTypeEntity documentTypeEntity = new DocumentTypeEntity
                {
                    DocumentTypeId = dt.DocumentTypeId,
                    DocumentTypeName = dt.DocumentTypeName,
                    GroupPermissionId = dt.GroupPermission.GroupPermissionId,
                };
                _documentTypeService.createDocumentType(documentTypeEntity);
                return Ok(documentTypeEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("/api/[Controller]/update-document-type")]
        public IActionResult updateDocumentType(DocumentTypeModel dt)
        {
            try
            {
                DocumentTypeEntity documentTypeEntity = new DocumentTypeEntity
                {
                    DocumentTypeId = dt.DocumentTypeId,
                    DocumentTypeName = dt.DocumentTypeName,
                    GroupPermissionId = dt.GroupPermission.GroupPermissionId,
                };
                _documentTypeService.updateDocumentType(documentTypeEntity);
                return Ok(documentTypeEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("/api/[Controller]/delete-document-type")]
        public IActionResult deleteDocumentType(string id)
        {
            try
            {
                bool dt = _documentTypeService.deleteDocumentType(id);
                if (!dt)
                {
                    return BadRequest("Không tìm thấy loại tài liệu để xóa!");
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
