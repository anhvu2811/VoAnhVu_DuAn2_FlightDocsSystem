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
using VoAnhVu_DuAn2.Repository;

namespace VoAnhVu_DuAn2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DocumentController(IDocumentRepository documentRepository, IWebHostEnvironment webHostEnvironment)
        {
            _documentRepository = documentRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        [Route("/api/[Controller]/get-all-document")]
        public IActionResult getAllDocument()
        {
            try
            {
                var dt = _documentRepository.getAllDocument();
                if (!dt.Any())
                {
                    return BadRequest("Không có tài liệu nào.");
                }
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("/api/[Controller]/create-document")]
        public IActionResult createDocument(DocumentModel dt)
        {
            try
            {
                var kt = _documentRepository.getAllDocument().Where(c => c.DocumentId == dt.DocumentId);
                if (kt.Any())
                {
                    return BadRequest("Id đã tồn tại ! Hãy nhập mã khác");
                }
                DocumentEntity documentEntity = new DocumentEntity
                {
                    DocumentId = dt.DocumentId,
                    DocumentName = dt.DocumentName,
                    FileUpLoad = dt.FileUpLoad,
                    Version = dt.Version,
                    CreateDate = dt.CreateDate,
                    Note = dt.Note,
                    DocumentTypeId = dt.DocumentType.DocumentTypeId,
                    FlightId = dt.Flight.FlightId,
                    UserId = dt.User.UserId
                };
                _documentRepository.createDocument(documentEntity);
                return Ok(documentEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("/api/[Controller]/update-document")]
        public IActionResult updateDocument(DocumentModel dt)
        {
            try
            {
                DocumentEntity documentEntity = new DocumentEntity
                {
                    DocumentId = dt.DocumentId,
                    DocumentName = dt.DocumentName,
                    FileUpLoad = dt.FileUpLoad,
                    Version = dt.Version,
                    CreateDate = dt.CreateDate,
                    Note = dt.Note,
                    DocumentTypeId = dt.DocumentType.DocumentTypeId,
                    FlightId = dt.Flight.FlightId,
                    UserId = dt.User.UserId
                };
                _documentRepository.updateDocument(documentEntity);
                return Ok(documentEntity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("/api/[Controller]/delete-document")]
        public IActionResult deleteDocumentType(string id)
        {
            try
            {
                bool dt = _documentRepository.deleteDocument(id);
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

        [HttpPost]
        [Route("/api/[Controller]/upload-file")]
        public IActionResult UploadFile(string documentId, IFormFile file)
        {
            try
            {
                var doc = _documentRepository.getDocumentById(documentId);
                if (doc == null)
                {
                    return NotFound("Tài liệu không tồn tại.");
                }

                if (file == null || file.Length == 0)
                {
                    return BadRequest("Vui lòng chọn tệp.");
                }

                // Kiểm tra nếu tệp không phải là PDF or .docx
                string fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (fileExtension != ".pdf" && fileExtension != ".docx")
                {
                    return BadRequest("Vui lòng chọn tệp PDF hoặc Word.");
                }
                else
                {
                    var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Files");
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var uniqueFileName = fileName + ".pdf";
                    var fullPath = Path.Combine(filePath, uniqueFileName);

                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    doc.FileUpLoad = "/Files/" + uniqueFileName;

                    _documentRepository.uploadFile(documentId, doc.FileUpLoad, fileName);
                    return Ok("Đường dẫn tệp đã được cập nhật.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi khi cập nhật đường dẫn tệp PDF: " + ex.Message);
            }
        }

        [HttpDelete]
        [Route("/api/[Controller]/delete-file")]
        public IActionResult DeleteFile(string documentId)
        {
            try
            {
                _documentRepository.deleteFile(documentId);
                return Ok("File đã được xóa.");
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi khi xóa file: " + ex.Message);
            }
        }
    }
}
