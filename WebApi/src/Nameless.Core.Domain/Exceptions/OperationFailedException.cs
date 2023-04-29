using Nameless.Infra.Resources;

namespace Nameless.Core.Domain.Exceptions
{
    public abstract class OperationFailedException : DetailedException
    {
        public OperationFailedException(string detail)
            : base("OperationFailed", NamelessResource.OperationFailed, detail)
        {
        }
    }
}
