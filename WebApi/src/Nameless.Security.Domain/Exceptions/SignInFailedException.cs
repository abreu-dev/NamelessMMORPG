using Nameless.Core.Domain.Exceptions;
using Nameless.Infra.Resources;

namespace Nameless.Security.Domain.Exceptions
{
    public class SignInFailedException : OperationFailedException
    {
        public SignInFailedException()
            : base(NamelessResource.SignInFailed)
        {
        }
    }
}
