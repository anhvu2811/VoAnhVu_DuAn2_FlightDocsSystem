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
    public class DocumentTypeController : ControllerBase
    {
        private readonly IDocumentTypeRepository _documentTypeRepository;
        public DocumentTypeController(IDocumentTypeRepository documentTypeRepository)
        {
            _documentTypeRepository = documentTypeRepository;
        }
        [HttpGet]
        [Route("/api/[Controller]/get-all-document-type")]
        public IActionResult getAllDocumentType()
        {
            try
            {
                var dt = _documentTypeRepository.getAllDocumentType();
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
        public IActionResult createDocumentType(DocumentTypeDTO dt)
        {
            try
            {
                var kt = _documentTypeRepository.getAllDocumentType().Where(c => c.DocumentTypeId == dt.DocumentTypeId);
                if (kt.Any())
                {
                    return BadRequest("Id đã tồn tại ! Hãy nhập mã khác");
                }
                DocumentTypeModel documentTypeModel = new DocumentTypeModel
                {
                    DocumentTypeId = dt.DocumentTypeId,
                    DocumentTypeName = dt.DocumentTypeName,
                    GroupPermissionId = dt.GroupPermission.GroupPermissionId,
                };
                _documentTypeRepository.createDocumentType(documentTypeModel);
                return Ok(documentTypeModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("/api/[Controller]/update-document-type")]
        public IActionResult updateDocumentType(DocumentTypeDTO dt)
        {
            try
            {
                DocumentTypeModel documentTypeModel = new DocumentTypeModel
                {
                    DocumentTypeId = dt.DocumentTypeId,
                    DocumentTypeName = dt.DocumentTypeName,
                    GroupPermissionId = dt.GroupPermission.GroupPermissionId,
                };
                _documentTypeRepository.updateDocumentType(documentTypeModel);
                return Ok(documentTypeModel);
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
                bool dt = _documentTypeRepository.deleteDocumentType(id);
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
