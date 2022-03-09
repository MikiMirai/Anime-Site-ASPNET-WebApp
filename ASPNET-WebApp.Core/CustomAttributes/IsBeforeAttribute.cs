using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET_WebApp.Core.CustomAttributes
{
    public class IsBeforeAttribute : ValidationAttribute
    {
        private readonly DateTime dateToCompare;

        public IsBeforeAttribute(DateTime _dateToCompare, string errorMessage = "")
        {
            dateToCompare = _dateToCompare;
            this.ErrorMessage = errorMessage;
        }

        public override bool IsValid(object? value)
        {
            if (value == null) return false;

            return (DateTime)value < dateToCompare;
        }
    }
}
