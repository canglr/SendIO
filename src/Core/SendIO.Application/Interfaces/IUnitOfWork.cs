using System;
namespace SendIO.Application.Interfaces
{
	public interface IUnitOfWork : IDisposable
    {
		IFileHeadRepository fileHeadRepository { get; }
        IFileContentRepository fileContentRepository { get; }
        int Complete();
    }
}

