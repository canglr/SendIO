using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;

namespace SendIO.WebUI.Model
{
	public class FileUpload
	{
        public string? title { get; set; }

        public string? description { get; set; }
    }
}

