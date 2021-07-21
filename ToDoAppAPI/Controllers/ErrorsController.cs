using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using Npgsql;
using ToDoAppDomainLayer.Exceptions;
using ToDoAppAPI.Models;

namespace ToDoAppAPI.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public IActionResult Error()
        {
            System.Diagnostics.Debug.WriteLine("Exception handler called");
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            switch (exception)
            {
                case NpgsqlException npgsqlException:
                    {

                        ErrorModel model = new ErrorModel()
                        {
                            Message = npgsqlException.SqlState switch
                            {
                                "23503" => "Error: Foreign key violation",
                                _ => "A database error has occured"
                            }
                        };
                        return StatusCode(500, model);
                    }
                case DatabaseInteractionException:
                    {
                        ErrorModel model = new()
                        {
                            Message = "A database error has occured",
                        };
                        return StatusCode(500, model);
                    }
                case DatabaseEntryNotFoundException databaseException:
                    {
                        ErrorModel model = new()
                        {
                            Message = databaseException.Message,
                        };
                        return NotFound(model);
                    }
                default:
                    {
                        ErrorModel model = new()
                        {
                            Message = "A database error has occured",
                        };
                        return StatusCode(500, model);
                    }
            }
        }
    }
}
