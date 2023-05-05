using System;
using Microsoft.EntityFrameworkCore;
using SendIO.Application.Interfaces;
using SendIO.Persistence.Context;
using SendIO.Persistence.Repositories;

namespace SendIO.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SendIOContext Context;

        public IFileHeadRepository fileHeadRepository { get; private set; }

        public IFileContentRepository fileContentRepository { get; private set; }

        public UnitOfWork(SendIOContext context)
        {
            Context = context;
            fileHeadRepository = new FileHeadRepository(Context);
            fileContentRepository = new FileContentRepository(Context);
        }

        public int Complete()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}

