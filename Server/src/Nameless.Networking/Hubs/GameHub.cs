using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Nameless.Infra.Common.Interfaces;

namespace Nameless.Networking.Hubs
{
    [Authorize]
    public class GameHub : Hub
    {
        private readonly IPlayerRepository _playerRepository;

        public GameHub(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async void GetPlayersForSelection()
        {
            var accountIdClaim = Context.User.Claims.SingleOrDefault(x => x.Type == "Id")?.Value ?? string.Empty;
            var accountId = Guid.Parse(accountIdClaim);

            var playerList = _playerRepository.GetPlayersByAccount(accountId);

            var dtos = playerList.Select(x => new PlayerForSelectionDto(x.Id, x.Name)).ToList();
            await Clients.Caller.SendAsync("PlayerSelection", new PlayerSelectionMessage(dtos));
        }

        public class PlayerSelectionMessage
        {
            public List<PlayerForSelectionDto> Players { get; }

            public PlayerSelectionMessage(List<PlayerForSelectionDto> players)
            {
                Players = players;
            }
        }

        public class PlayerForSelectionDto
        {
            public Guid PlayerId { get; set; }
            public string PlayerName { get; }

            public PlayerForSelectionDto(Guid playerId, string playerName)
            {
                PlayerId = playerId;
                PlayerName = playerName;
            }
        }
    }
}
