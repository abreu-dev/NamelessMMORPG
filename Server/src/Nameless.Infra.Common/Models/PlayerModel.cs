namespace Nameless.Infra.Common.Models
{
    public class PlayerModel
    {
        public Guid Id { get; set; }

        public virtual AccountModel Account { get; set; }
        public Guid AccountId { get; set; }

        public string Name { get; set; }
    }
}
