using Common.Messaging.Contracts;

namespace Common.Messaging.Events.CheckAccountExistance;

public class CheckSavingAccountExistance : CheckAccountExistanceBase {
    public CheckSavingAccountExistance(Guid accountNumber) : base(accountNumber) {
    }

    public override Task<CheckAccountExistanceResult> Accept(IAccountExistenceChecker accountExistenceChecker) {
        return accountExistenceChecker.Visit(this);
    }
}