// <copyright file="ValidationModelAttribute.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Football.Web.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Base validation model.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute" />
    public class ValidationModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// </summary>
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var result = context.ModelState.Keys.Select(k => context.ModelState[k].Errors.Select(e => new ValidationError { Key = k, Error = e.ErrorMessage }));
                context.Result = new ObjectResult(result) { StatusCode = StatusCodes.Status400BadRequest };
            }
        }
    }
}
