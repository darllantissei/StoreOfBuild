using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StoreOfBuild.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StoreOfBuild.Web.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            bool isAjaxCall = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";

            if (isAjaxCall)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var message = context.Exception is DomainException ? context.Exception.Message : $"An error ocorred. Detalhes {context.Exception.Message}";
                context.Result = new JsonResult(message);
                context.ExceptionHandled = true;
            }

            base.OnException(context);
        }
    }
}
