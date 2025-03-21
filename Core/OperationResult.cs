namespace FoodOnDelivery.Core.OperationResult;

public class OperationResult<T>
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public T Data { get; set; }

    public static OperationResult<T> CreateSuccess(T data)
    {
        return new OperationResult<T> { Success = true, Data = data };
    }

    public static OperationResult<T> CreateFailure(string errorMessage)
    {
        return new OperationResult<T> { Success = false, ErrorMessage = errorMessage };
    }
}
