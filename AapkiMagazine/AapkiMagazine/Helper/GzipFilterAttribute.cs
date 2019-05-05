using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.IO.Compression;

namespace AapkiMagazine
{

    public sealed class CompressAttribute : ActionFilterAttribute
    {
        
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {

                var encodingsAccepted = filterContext.HttpContext.Request.Headers["Accept-Encoding"];
                if (string.IsNullOrEmpty(encodingsAccepted)) return;

                encodingsAccepted = encodingsAccepted.ToLowerInvariant();
                var response = filterContext.HttpContext.Response;
                
                if (encodingsAccepted.Contains("gzip"))
                {
                    response.AppendHeader("Content-encoding", "gzip");
                    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
                }
                else  if (encodingsAccepted.Contains("deflate"))
                {
                    response.AppendHeader("Content-encoding", "deflate");
                    response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
                }
                
            }
        }

        /// <summary>
        /// Called by MVC just before the result (typically a view) is executing.
        /// </summary>
        /// <param name="filterContext"></param>
        //public override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    var result = filterContext.Result;
        //    if (result is ViewResultBase)
        //    {
        //        MethodInfo actionMethodInfo = Common.GetActionMethodInfo(filterContext);
        //        if (GetReturnType(actionMethodInfo).ToLower() != "viewresult") return;

        //        HttpRequestBase request = filterContext.HttpContext.Request;

        //        string acceptEncoding = request.Headers["Accept-Encoding"];

        //        if (string.IsNullOrEmpty(acceptEncoding)) return;

        //        acceptEncoding = acceptEncoding.ToUpperInvariant();

        //        HttpResponseBase response = filterContext.HttpContext.Response;

        //        if (acceptEncoding.Contains("GZIP"))
        //        {
        //            response.AppendHeader("Content-encoding", "gzip");
        //            response.Filter = new WebCompressionStream(response.Filter, CompressionType.GZip);
        //        }
        //        else if (acceptEncoding.Contains("DEFLATE"))
        //        {
        //            response.AppendHeader("Content-encoding", "deflate");
        //            response.Filter = new WebCompressionStream(response.Filter, CompressionType.Deflate);
        //        }
        //    }
        //}
    
}