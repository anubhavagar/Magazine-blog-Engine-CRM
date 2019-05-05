using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Web.Security;
using System.IO;
using AapkiMagazine.DataLayer;
using System.Text.RegularExpressions;
using System.Configuration;

namespace AapkiMagazine.Helper
{
    public static class Helper_Magazine
    {
        private static Regex isGuid =
         new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);

        public static string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

        public static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1");
            return hashedPwd;
        }

        public static UploadImageResult UploadImage(HttpPostedFileBase image, HttpServerUtilityBase server, int articleId, string article_state)
        {
            var result = new UploadImageResult { Success = false };
            string path = "/Content/images/articles/";
            int maxSize = 300000;
            string allowedExtensions = "jpg,png,bmp,gif,jpeg";
            string imagePath = string.Empty;

            if (image == null || image.ContentLength == 0)
            {
                result.Errors.Add("You didn't select a image file or the file you uploaded was invalid.");
                return result;
            }

            // Check image size
            if (image.ContentLength > maxSize)
                result.Errors.Add("Image was larger than the maximum upload size.Max image size allowed is 256KB.");

            // Check image extension
            var extension = Path.GetExtension(image.FileName).Substring(1).ToLower();

            if (!allowedExtensions.Contains(extension))
                result.Errors.Add("Only image files can be uploaded with extension. jpg,png,bmp,gif,jpeg");

            // If there are no errors save image
            if (!result.Errors.Any())
            {

                // update image to db 
                var newName = "";
                var serverPath = "";
                DataAccessLayer datalayer = null;

                //add image to repository
                newName = articleId + "." + extension;
                serverPath = server.MapPath("~" + path + newName);
                image.SaveAs(serverPath);
                result.ImagePath = "../../Content/images/articles/" + newName;

                //update image to db
                //dataAccess = new DataAccessLayer();
                imagePath = result.ImagePath;
                datalayer = new DataAccessLayer();
                if (article_state == "published") {
                    datalayer.UpdateArticlePublishedImage(articleId, imagePath);
                }
                else if (article_state == "entry") {
                    datalayer.UpdateArticleEntryImage(articleId, imagePath);
                }
                

                result.Success = true;
            }

            return result;
        }

        public static UploadImageResult DeleteImage(HttpServerUtilityBase server, int articleId, string article_state)
        {

            string sourceDir = string.Empty;
            DataAccessLayer datalayer = null;
            var result = new UploadImageResult { Success = false };
            try
            {
                sourceDir = server.MapPath("/Content/images/articles");
                string[] picList = Directory.GetFiles(sourceDir, articleId + ".*");

                foreach (string pic in picList)
                {
                    System.IO.File.Delete(pic);
                    string imagePath = string.Empty;

                    datalayer = new DataAccessLayer();
                    if (article_state == "entry")
                    {
                        datalayer.UpdateArticleEntryImage(articleId, imagePath);
                    }
                    else if (article_state == "published")
                    {
                        datalayer.UpdateArticlePublishedImage(articleId, imagePath);
                    }
                    
                }
                result.Success = true;
            }
            catch (Exception ex)
            {
                // Throw Exception
                result.Errors.Add("Error while deleting the image.");
               
            }
            return result;
        }
        
        internal static bool IsGuid(string input)
        {
            bool isValid = false;
           // output = Guid.Empty;

            if (input != null)
            {

                if (isGuid.IsMatch(input))
                {
                    //output = new Guid(input);
                    isValid = true;
                }
            }

            return isValid;
        }

        public static string ToSeoUrl(this string url)
        {
            // make the url lowercase
            string encodedUrl = (url ?? "").ToLower();

            // replace & with and
            encodedUrl = Regex.Replace(encodedUrl, @"\&+", "and");

            // remove characters
            encodedUrl = encodedUrl.Replace("'", "");

            // remove invalid characters
            encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");

            // remove duplicates
            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");

            // trim leading & trailing characters
            encodedUrl = encodedUrl.Trim('-');

            return encodedUrl;
        }

        public static string DisqusShortName
        {
            get { return ConfigurationManager.AppSettings["DisqusShortName"]; }
        }

        public static int[] ToIntArray(this string value, char separator)
        {
            return Array.ConvertAll(value.Split(separator), s => int.Parse(s));
        }
    }


    public class UploadImageResult
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string ImagePath { get; set; }
        //public int productid { get; set; }
        public UploadImageResult()
        {
            Errors = new List<string>();
        }
    }
}