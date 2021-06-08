namespace Models.Models
{
    public record Result
    {
        public bool OperationFinisedWithSuccess { get; init; }

        public Result(bool operationFinisedWithSuccess)
        {
            OperationFinisedWithSuccess = operationFinisedWithSuccess;
        }
    }
}