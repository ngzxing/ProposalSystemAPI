using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProposalSystem.utils.extensions
{
    public static class ObjectExtension
    {
        public static string? GetByPropertyName(this Object obj, string propertyName){
            
            return obj.GetType().GetProperty(propertyName)?.GetValue(obj)?.ToString();
        }
    }
}