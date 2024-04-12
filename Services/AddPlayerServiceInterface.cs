using CricketWebApplicationMVC.Dto;

namespace CricketWebApplicationMVC.Services
{
    public interface AddPlayerServiceInterface
    {
        public int AddPlayer(AddPlayerRequestDto addPlayerRequestDto);
    }
}
