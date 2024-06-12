using Common.Messaging.Events.CheckAccountExistance;

namespace Common.Messaging.Contracts;

public interface IAccountExistenceChecker {
    Task<CheckAccountExistanceResult> Visit(CheckCheckingAccountExistance checkingAccountExistance);
    Task<CheckAccountExistanceResult> Visit(CheckSavingAccountExistance checkSavingAccountExistance);
}