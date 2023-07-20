using Core.CustomAttributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Sevices.AuthorizeService
{
    public class ApplicationManager : IApplicationService
    {
        public List<Menu> GetAuthorizeDefinitionEndPoints(Type type)
        {
            Assembly assembly = Assembly.GetAssembly(type);
            var controllers = assembly?.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));

            List<Menu> menus = new();
            if (controllers != null)
            {
                foreach (var controller in controllers)
                {
                    var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));
                    if (actions.Count() != 0)
                    {

                        Menu menu = new();
                        foreach (var action in actions)
                        {
                            var authorizeDefinitionAttribute = action.GetCustomAttribute(typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                            if (menu.Name==null)
                            {
                                menu.Name = authorizeDefinitionAttribute.Menu;
                            }
                            ControllerAction controllerAction = new()
                            {
                                ActionType = authorizeDefinitionAttribute.ActionType.ToString(),
                                Definition = authorizeDefinitionAttribute.Definition,
                            };
                            var httpAttribute = action.GetCustomAttribute(typeof(HttpMethodAttribute)) as HttpMethodAttribute;
                            if (httpAttribute != null)
                                controllerAction.HttpType = httpAttribute.HttpMethods.First();
                            else
                                controllerAction.HttpType = HttpMethods.Get;
                            controllerAction.Code = $"{menu.Name}.{controllerAction.HttpType}.{controllerAction.Definition.Replace(" ","")}";
                            menu.Actions.Add(controllerAction);
                        }
                        menus.Add(menu);
                    }
                }
            }
            
            return menus;
        }
    }
}
