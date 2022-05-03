namespace SkysFormsDemo.Services;

internal class RealAccountService : IRealAccountService
{
    public bool Deposit(int accountId, int belopp) 
    {
        throw new NotImplementedException();
    }

    public IRealAccountService.ErrorCode Withdraw(int accountId, int belopp)
    {
        throw new NotImplementedException();
    }
}