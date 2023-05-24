using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PEI_ETL.Services.DTO
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class Numeric : ValidationAttribute
    {
        public Numeric(string errorMessage) : base(errorMessage)
        {
        }

        /// <summary>
        /// Check if given value is numeric
        /// </summary>
        /// <param name="value">The input value</param>
        /// <returns>True if value is numeric</returns>
        public override bool IsValid(object value)
        {
            return decimal.TryParse(value?.ToString(), out _);
        }
    }
}
