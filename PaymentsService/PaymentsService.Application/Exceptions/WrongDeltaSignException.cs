namespace PaymentsService.Application.Exceptions;

public class WrongDeltaSignException(decimal delta, char expectedSign): 
    ApplicationException($"Wrong delta sign: {delta}; Expected: {expectedSign}")
{
    public decimal Delta { get; } = delta;
    public char ExpectedSign { get; } = expectedSign;
}
