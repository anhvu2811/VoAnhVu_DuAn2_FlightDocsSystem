using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoAnhVu_DuAn2.Data;
using VoAnhVu_DuAn2.DTO;
using VoAnhVu_DuAn2.Models;

namespace VoAnhVu_DuAn2.Repository
{
    public interface IDocumentTypeRepository
    {
        List<DocumentTypeDTO> getAllDocumentType();
        void createDocumentType(DocumentTypeModel dt);
        void updateDocumentType(DocumentTypeModel dt);
        bool deleteDocumentType(string id);
    }
    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        private readonly MyDbContext _context;
        public DocumentTypeRepository(MyDbContext context)
        {
            _context = context;
        }

        public void createDocumentType(DocumentTypeModel dt)
        {
            try
            {
                _context.DocumentTypeModels.Add(dt);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool deleteDocumentType(string id)
        {
            try
            {
                var dt = _context.DocumentTypeModels.FirstOrDefault(c => c.DocumentTypeId == id);
                if (dt is null)
                {
                    return false;
                }
                _context.DocumentTypeModels.Remove(dt);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentTypeDTO> getAllDocumentType()
        {
            var types = _context.DocumentTypeModels
                .Include(type => type.GroupPermission)
                .Select(type => new DocumentTypeDTO
                {
                    DocumentTypeId = type.DocumentTypeId,
                    DocumentTypeName = type.DocumentTypeName,
                    GroupPermission = type.GroupPermission
                }).ToList();
            return types;
        }

        public void updateDocumentType(DocumentTypeModel dt)
        {
            try
            {
                _context.DocumentTypeModels.Update(dt);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
