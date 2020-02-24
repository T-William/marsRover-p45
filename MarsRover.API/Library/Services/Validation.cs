using System;
using System.Collections.Generic;
using MarsRover.API.Library.Interfaces;

namespace MarsRover.API.Library.Services
{
    public class Validation : IValidationDictionary
    {
        public List<string> Errors { get; set; }
        public Validation()
        {
            Errors = new List<string>();
        }

        public virtual bool IsValid
        {
            get
            {
                return Errors.Count == 0;
            }
        }

        public void AddError(string errorMessage)
        {
            Errors.Add(errorMessage);
        }

        public void MaxLength(string PropertyValue, int Max, string Message = "Max string length")
        {
            if (PropertyValue != null)
                if (PropertyValue.Trim().Length > Max)
                    AddError(Message);
        }

        public void Required(string PropertyValue, string Message = "Required")
        {
            if (PropertyValue == null)
            {
                AddError(Message);
            }
            else
            {
                if (PropertyValue.Trim().Length == 0)
                    AddError(Message);
            }

        }
        public void DateRequired(DateTime PropertyValue,string Message = "Required"){
            var nulldate = Convert.ToDateTime(null);
            if(PropertyValue == nulldate){
                AddError(Message);
            }
        }

        public void Range(int PropertyValue, int Min, int Max, string Message)
        {
            if (PropertyValue < Min || PropertyValue > Max)
                AddError(Message);
        }

        public void Reset()
        {
           Errors = new List<string>();
        }
    }
}