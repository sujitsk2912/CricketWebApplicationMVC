using CricketWebApplicationMVC.Dto;
using CricketWebApplicationMVC.Models;
using CricketWebApplicationMVC.Services;
using NuGet.Protocol.Core.Types;
using CricketWebApplicationMVC.Repository;

namespace CricketWebApplicationMVC.Services.ServicesImpl
{
    public class AddPlayerServiceImpl : AddPlayerServiceInterface
    {
        public int AddPlayer(AddPlayerRequestDto addPlayerRequestDto)
        {
            try
            {
                Repository.PlayerRepository playerRepository = new Repository.PlayerRepository();
                AddPlayerModel addPlayer = new AddPlayerModel();
                addPlayer.PlayerName = addPlayerRequestDto.PlayerName;
                addPlayer.Born = addPlayerRequestDto.DateOfBirth;
                addPlayer.City = addPlayerRequestDto.City;
                addPlayer.BattingStyle = addPlayerRequestDto.BattingStyle;
                addPlayer.BowlingStyle = addPlayerRequestDto.BowlingStyle;
                addPlayer.PlayingRole = addPlayerRequestDto.PlayingRole;
                addPlayer.PlayerImg = addPlayerRequestDto.PlayerImg;
                playerRepository.InsertPlayer(addPlayer);

                return 0;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
            
        }
    }
}
