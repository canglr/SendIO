using System;
using SendIO.Application.Interfaces;
using SendIO.Domain.Entities;
using SendIO.Persistence.Context;

namespace SendIO.Persistence.Repositories
{
    public class FileHeadRepository : GenericRepository<FileHead>, IFileHeadRepository
    {
        public FileHeadRepository(SendIOContext context) : base(context)
        {
        }
    }
}

