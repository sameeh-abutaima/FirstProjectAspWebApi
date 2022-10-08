using Autofac.Core;
using FirstprojectAspWebApi.Core.Managers.Interfaces;
using FirstprojectAspWebApi.DTOs.Users;
using FirstprojectAspWebApi.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;
using System;
using System.Linq;

namespace FirstprojectAspWebApi.Attributes
{
	public class FirstprojectAspWebApiAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
			try
			{
                var roleManager = context.HttpContext.RequestServices.GetService(typeof(IRoleManager)) as IRoleManager;
                var strId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
                _ = int.TryParse(strId, out int id);
                var user=new UserDTO { Id=id};
                if (roleManager.CheckAccess(user))
                {
                    return;
                }
                throw new Exception("UnAuthorized");
            }
            catch (RetryLimitExceededException ex)
            {
                Log.Logger.Information(ex.Message);
                throw new ServiceValidationException(ex.Message);
            }
            catch (InvalidOperationException ex)
			{
                Log.Logger.Information(ex.Message);
                throw new ServiceValidationException(ex.Message);
			}
            catch (DependencyResolutionException ex)
            {
                Log.Logger.Information(ex.Message);
                throw new ServiceValidationException(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                Log.Logger.Information(ex.Message);
                throw new ServiceValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Logger.Information(ex.Message);
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
