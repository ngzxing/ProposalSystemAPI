using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace AutismRehabilitationSystem.Authorization
{
    public class AuthorizeSchemePolicy : IControllerModelConvention
    {
    
        public AuthorizeSchemePolicy()
        {
        }

        public void Apply(ControllerModel controller)
        {   
            if (controller.Attributes.Any(attr => attr is ApiControllerAttribute))
            {
                // Apply the specified authorization policy
                if( (controller.ControllerName == "Account") ){
                    
                    return;
                }
                
                controller.Filters.Add(new AuthorizeFilter("ApiPolicy"));

            }
        }
    }
}