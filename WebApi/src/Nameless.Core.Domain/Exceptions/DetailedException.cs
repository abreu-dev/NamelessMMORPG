namespace Nameless.Core.Domain.Exceptions
{
    public abstract class DetailedException : Exception
    {
        public string Type { get; set; }
        public string Error { get; set; }
        public string Detail { get; set; }

        public DetailedException(string type,
                                    string error,
                                    string detail)
            : base(GetMessage(type, error, detail))
        {
            Type = type;
            Error = error;
            Detail = detail;
        }

        public static string GetMessage(string type,
                                        string error,
                                        string detail)
            => $"{type} | {error} | {detail}";
    }
}
