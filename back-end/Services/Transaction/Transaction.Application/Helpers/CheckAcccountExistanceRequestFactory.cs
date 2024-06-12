using Common.Messaging.Events.CheckAccountExistance;

namespace Transaction.Application.Helpers;

public static class CheckAcccountExistanceRequestFactory {
    public static CheckAccountExistanceBase GetRequest(string type, Guid accountNumber)
        => type switch {
            "savings" => new CheckSavingAccountExistance(accountNumber),
            "checkings" => new CheckCheckingAccountExistance(accountNumber),
            _ => throw new InvalidOperationException("Unsupported account type")
        };
}