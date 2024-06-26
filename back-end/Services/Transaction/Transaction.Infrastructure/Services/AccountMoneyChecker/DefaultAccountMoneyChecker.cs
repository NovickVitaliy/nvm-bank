using Common.Messaging.Contracts;
using Common.Messaging.Events.CheckAccountMoney;
using MassTransit;

namespace Transaction.Infrastructure.Services.AccountMoneyChecker;

public class DefaultAccountMoneyChecker : IAccountMoneyChecker {
    private readonly IRequestClient<CheckCheckingAccountMoney> _checkingRequestClient;
    private readonly IRequestClient<CheckSavingsAccountMoney> _savingRequestClient;

    public DefaultAccountMoneyChecker(IRequestClient<CheckCheckingAccountMoney> checkingRequestClient,
        IRequestClient<CheckSavingsAccountMoney> savingRequestClient) {
        _checkingRequestClient = checkingRequestClient;
        _savingRequestClient = savingRequestClient;
    }

    public async Task<CheckAccountMoneyResult> Visit(CheckCheckingAccountMoney checkCheckingAccountMoney) {
        return (await _checkingRequestClient.GetResponse<CheckAccountMoneyResult>(checkCheckingAccountMoney)).Message;
    }

    public async Task<CheckAccountMoneyResult> Visit(CheckSavingsAccountMoney checkSavingsAccountMoney) {
        return (await _savingRequestClient.GetResponse<CheckAccountMoneyResult>(checkSavingsAccountMoney)).Message;
    }
}