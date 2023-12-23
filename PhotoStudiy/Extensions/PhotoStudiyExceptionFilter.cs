using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PhotoStudiy.API.Exceptions;
using PhotoStudiy.Services.Contracts.Exceptions;

namespace PhotoStudiy.API.Extensions
{
    /// <summary>
    /// Фильтр для обработки ошибок раздела администрирования
    /// </summary>
    public class PhotoStudiyExceptionFilter : IExceptionFilter
    {
        /// <inheritdoc/>
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception as PhotoStudiyException;
            if (exception == null)
            {
                return;
            }

            switch (exception)
            {
                case PhotoStudiyValidationException ex:
                    SetDataToContext(
                        new ConflictObjectResult(new ApiValidationExceptionsDetail
                        {
                            Errors = ex.Errors,
                        }),
                        context);
                    break;

                case PhotoStudiyInvalidOperationException ex:
                    SetDataToContext(
                        new BadRequestObjectResult(new ApiExceptionsDetail { Message = ex.Message, })
                        {
                            StatusCode = StatusCodes.Status406NotAcceptable,
                        },
                        context);
                    break;

                case PhotoStudiyNotFoundException ex:
                    SetDataToContext(new NotFoundObjectResult(new ApiExceptionsDetail
                    {
                        Message = ex.Message,
                    }), context);
                    break;
           
                default:
                    SetDataToContext(new BadRequestObjectResult(new ApiExceptionsDetail
                    {
                        Message = $"Ошибка записи в БД (Проверьте индексы). {exception.Message}",
                    }), context);
                    break;
            }
        }

        /// <summary>
        /// Определяет контекст ответа
        /// </summary>
        static protected void SetDataToContext(ObjectResult data, ExceptionContext context)
        {
            context.ExceptionHandled = true;
            var response = context.HttpContext.Response;
            response.StatusCode = data.StatusCode ?? StatusCodes.Status400BadRequest;
            context.Result = data;
        }
    }
}
