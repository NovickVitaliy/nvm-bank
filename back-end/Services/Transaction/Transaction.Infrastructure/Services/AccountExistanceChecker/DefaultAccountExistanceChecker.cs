using Common.Messaging.Contracts;
using Common.Messaging.Events.CheckAccountExistance;
using MassTransit;

namespace Transaction.Infrastructure.Services.AccountExistanceChecker;

public class DefaultAccountExistanceChecker : IAccountExistenceChecker {
    private readonly IRequestClient<CheckCheckingAccountExistance> _checkingExistanceRequestClient;
    private readonly IRequestClient<CheckSavingAccountExistance> _savingExistanceRequestClient;

    public DefaultAccountExistanceChecker(IRequestClient<CheckCheckingAccountExistance> checkingExistanceRequestClient, IRequestClient<CheckSavingAccountExistance> savingExistanceRequestClient) {
        _checkingExistanceRequestClient = checkingExistanceRequestClient;
        _savingExistanceRequestClient = savingExistanceRequestClient;
    }

    public async Task<CheckAccountExistanceResult> Visit(CheckCheckingAccountExistance checkingAccountExistance) {
        return (await _checkingExistanceRequestClient.GetResponse<CheckAccountExistanceResult>(checkingAccountExistance)).Message;
    }

    public async Task<CheckAccountExistanceResult> Visit(CheckSavingAccountExistance checkSavingAccountExistance) {
        return (await _checkingExistanceRequestClient.GetResponse<CheckAccountExistanceResult>(checkSavingAccountExistance)).Message;
    }
}