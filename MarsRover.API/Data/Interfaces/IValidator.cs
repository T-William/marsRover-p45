using System.Collections.Generic;

public interface IValidator<T>
{
    IEnumerable<string> Validate(T instance);
}