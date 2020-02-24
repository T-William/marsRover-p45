using System;
using System.Collections.Generic;

namespace MarsRover.API.Library.Interfaces
{
    public interface IValidationDictionary
    {
        void AddError(string errorMessage);
        bool  IsValid { get; }
        List<string> Errors { get; set; }
        void Required(string PropertyValue, string Message = "Required");
        void DateRequired(DateTime PropertyValue, string Message ="Required");
        void MaxLength(string PropertyValue, int Max, string Message = "Max string length");
        void Range(int PropertyValue, int Min, int Max, string Message = "Max string length");
        void Reset();
    }
}