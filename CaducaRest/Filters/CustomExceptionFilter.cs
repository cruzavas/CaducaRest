using CaducaRest.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public CustomExceptionFilter(IHostingEnvironment hostingEnvironment, IModelMetadataProvider modelMetadataProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }

        public override void OnException(ExceptionContext context)
        {
            string mensajeError = string.Empty;
            //Revisamos si la excepción es una excepción de SqlServer
            if (context.Exception.InnerException != null && context.Exception.InnerException.GetType() == typeof(SqlException))
            {
                //Obtenemos la acción del controlador
                string accion = context.RouteData.Values["action"].ToString();
                //Obtenemos el nombre de la tabla
                string tabla = " el/la " + context.RouteData.Values["controller"].ToString() + " ";
                //Obtenemos la excepción de SqlServer
                SqlException exSqlServer = (SqlException)context.Exception.InnerException;
                //Personalizamos nuestro mensaje de error
                CustomSQLServerException sqlServerCustomError = new CustomSQLServerException();
                mensajeError = sqlServerCustomError.MuestraErrorSqlServer(exSqlServer, tabla, GetType().Name);
                //Regresamos como mensaje de error un badrequest
                BadRequestObjectResult badRequest = new BadRequestObjectResult(new CustomError(400, mensajeError));
                context.Result = badRequest;
            }
            else
            {
                //Regresamos como mensaje de error un error del servidor
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
