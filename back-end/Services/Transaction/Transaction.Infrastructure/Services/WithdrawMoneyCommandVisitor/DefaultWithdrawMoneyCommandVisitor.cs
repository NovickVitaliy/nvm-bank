using Common.Messaging.Commands.WithdrawMoney;
using Common.Messaging.Contracts;
using MassTransit;

namespace Transaction.Infrastructure.Services.WithdrawMoneyCommandVisitor;

public class DefaultWithdrawMoneyCommandVisitor : IWithdrawMoneyCommandVisitor {
    private readonly IRequestClient<WithdrawMoneyFromCheckingAccount> _withdrawMoneyFromCheckingAccountClient;
    private readonly IRequestClient<WithdrawMoneyFromSavingAccount> _withdrawMoneyFromSavingAccountClient;

    public DefaultWithdrawMoneyCommandVisitor(IRequestClient<WithdrawMoneyFromCheckingAccount> withdrawMoneyFromCheckingAccountClient, IRequestClient<WithdrawMoneyFromSavingAccount> withdrawMoneyFromSavingAccountClient) {
        _withdrawMoneyFromCheckingAccountClient = withdrawMoneyFromCheckingAccountClient;
        _withdrawMoneyFromSavingAccountClient = withdrawMoneyFromSavingAccountClient;
    }

    public async Task<WithdrawMoneyResult> Visit(WithdrawMoneyFromCheckingAccount command) {
        return (await _withdrawMoneyFromCheckingAccountClient.GetResponse<WithdrawMoneyResult>(command)).Message;
    }

    public async Task<WithdrawMoneyResult> Visit(WithdrawMoneyFromSavingAccount command) {
        return (await _withdrawMoneyFromSavingAccountClient.GetResponse<WithdrawMoneyResult>(command)).Message;
    }
}