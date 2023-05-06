using System;
namespace SendIO.Application.Services
{
	public interface IMinIO
	{
        Task<string> Add(string folder,Stream file, string filename, string contenttype);

		Task<string> ShareLink(string filename, int minute);
	}
}

