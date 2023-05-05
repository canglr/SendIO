using System;
using SendIO.Application.Interfaces;
using SendIO.Domain.Entities;
using SendIO.Persistence.Context;

namespace SendIO.Persistence.Repositories
{
    public class FileContentRepository : GenericRepository<FileContent>, IFileContentRepository
    {
        public FileContentRepository(SendIOContext context) : base(context)
        {
        }
    }
}

