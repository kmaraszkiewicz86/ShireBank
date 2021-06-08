namespace Models.Models
{
    public record ResultWithModel<TReturnType> : Result
    {
        public TReturnType ReturnType { get; init; }

        public ResultWithModel() : base(false)
        {
            ReturnType = default;
        }

        public ResultWithModel(TReturnType returnType) : base(true)
        {
            ReturnType = returnType;
        }
    }
}