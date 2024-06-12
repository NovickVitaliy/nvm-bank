using Common.Messaging.Contracts;

namespace Common.Messaging.Events.CheckAccountExistance;

public abstract class CheckAccountExistanceBase {
    public Guid AccountNumber { get; init; }

    protected CheckAccountExistanceBase(Guid accountNumber) {
        AccountNumber = accountNumber;
    }

    public abstract Task<CheckAccountExistanceResult> Accept(IAccountExistenceChecker accountExistenceChecker);
}