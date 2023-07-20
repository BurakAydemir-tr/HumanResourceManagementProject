using Business.Abstract;
using Core.CustomAttributes;
using Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace WebAPI.ActionFilters
{
    public class RolePermissionFilter : IAsyncActionFilter
    {
        readonly IUserService _userService;
        readonly IEndpointService _endpointService;

        public RolePermissionFilter(IUserService userService, IEndpointService endpointService)
        {
            _userService = userService;
            _endpointService = endpointService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //var admin= context.HttpContext.User.ClaimRoles().Contains("Admin");
            //var email = context.HttpContext.User.Identity?.Name;
            //if (!string.IsNullOrEmpty(email) && !admin)
            //{
            //    /*Actionlar hakkında bilgi almak için kullanıyoruz.as operatörü ile ControllerActionDescriptor  dönüştürerek daha fazla bilgi alabiliyoruz.*/
            //    var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

            //    /*Action'nın üzerindeki AuthorizeDefinitionAttribute ile tanımlanmış attribute na ulaşıyoruz. As AuthorizeDefinitionAttribute ile de AuthorizeDefinitionAttribute dönüştürüyoruz.*/
            //    var attribute = descriptor.MethodInfo.GetCustomAttribute(
            //        typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;

            //    if (attribute!=null)
            //    {
            //        /*Action'nın üzerindeki HttpMethodAttribute ile tanımlanmış attribute na ulaşıyoruz.
            //     *Yani HttpGet mi Post ya As HttpMethodAttribute ile de HttpMethodAttribute dönüştürüyoruz.*/
            //        var httpAttribute = descriptor.MethodInfo.GetCustomAttribute(typeof(HttpMethodAttribute))
            //            as HttpMethodAttribute;

            //        var code = $"{attribute.Menu}.{(httpAttribute != null ? httpAttribute.HttpMethods.First() : HttpMethods.Get)}.{attribute.Definition.Replace(" ", "")}";

            //        var hasOperationClaims = _userService.HasOperationClaimPermissionToEndpoint(email, code);
            //        if (!hasOperationClaims.Success)
            //            context.Result = new UnauthorizedResult();
            //        else
            //            await next();
            //    }
            //    else
            //        await next();


            //}
            //else
            //    await next();


            var roles = context.HttpContext.User.ClaimRoles();

            if (roles.Contains("Admin"))
            {
                await next();
            }
            else
            {
                var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

                var attribute = descriptor.MethodInfo.GetCustomAttribute(
                        typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;

                if (attribute != null)
                {
                    var httpAttribute = descriptor.MethodInfo.GetCustomAttribute(typeof(HttpMethodAttribute))
                            as HttpMethodAttribute;

                    var code = $"{attribute.Menu}.{(httpAttribute != null ? httpAttribute.HttpMethods.First() : HttpMethods.Get)}.{attribute.Definition.Replace(" ", "")}";

                    var endpoint = _endpointService.GetByCode(code);
                    var endpointOperationClaim = _endpointService.GetClaims(endpoint.Data);

                    foreach (var item in endpointOperationClaim.Data)
                    {
                        if (roles.Contains(item.Name))
                        {
                            await next();
                        }
                        
                    }
                    context.Result = new UnauthorizedResult();
                }
                else
                    await next();

            }


        }
    }
}
