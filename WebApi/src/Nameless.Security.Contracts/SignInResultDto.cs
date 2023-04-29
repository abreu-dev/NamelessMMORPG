namespace Nameless.Security.Contracts
{
    public class SignInResultDto
    {
        public string Token { get; set; }
        public AccountDto Account { get; set; }
    }
}
