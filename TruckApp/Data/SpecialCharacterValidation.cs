using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TruckApp.Data
{
    public class SpecialCharacterValidation
    {
        public string Validate(Object obj)
        {
            //Type type = Type.GetType("TruckApp.Dtos." + ClassName);
            //var Obj = Activator.CreateInstance(type);
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(obj))
            {
                if (obj.GetType().GetProperty(prop.Name).PropertyType.Name == "String" && !(Regex.IsMatch(obj.GetType().GetProperty(prop.Name).GetValue(obj).ToString(), @"^[a-zA-Z0-9\s.\?\,\'\;\:\!\-\@]+$")))
                {
                    return "Special Characters not allowed";
                }
            }
            return "Ok";
        }
    }
}

