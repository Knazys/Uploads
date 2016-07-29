using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Uploads.Models
{
    public class Upload
    {
        public int Id { get; set; }
        public string Size { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        [DataType(DataType.ImageUrl)]
        public byte[] Content { get; set; }
    }


    public class UploadsHelper
    {
        public static string[] validMimeTypes = new string[]
        {
            "image/gif",
            "image/jpeg",
            "image/jpg",
            "image/png"
        };

        public static string[] validExtensions = new string[] { ".gif", ".jpg", ".jpeg", ".png", ".bmp" };

        //public static bool checkImage(string path)
        //{
        //    return new string[] { ".gif", ".jpg", ".jpeg", ".png", ".bmp" }.Contains(Path.GetExtension(path).ToLower())
        //            &&
        //            new string[] { "image/gif", "image/jpeg", "image/png", "image/bmp" }.Contains(MimeMapping.GetMimeMapping(path));
        //}
    }

    
}