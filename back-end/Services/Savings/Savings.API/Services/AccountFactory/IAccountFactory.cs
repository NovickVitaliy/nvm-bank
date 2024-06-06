using Savings.API.Models.Domain;
using Savings.API.Models.Dtos;

namespace Savings.API.Services.AccountFactory;

public interface IAccountFactory
{
    AccountType AccountType { get; }
    Models.Domain.SavingAccount CreateAccount(CreateAccountDto createAccountDto);
}