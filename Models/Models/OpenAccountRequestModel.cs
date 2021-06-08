namespace Models.Models
{
    public record OpenAccountRequestModel(string FirstName, string LastName, float DebtLimit);

    public record WithdrawRequestModel(uint Account, float Asmount);

    public record DepositRequestModel(uint Account, float Amount);

    public record GetHistoryRequestModel(uint Account);

    public record CloseAccountRequestModel(uint Account);
}
