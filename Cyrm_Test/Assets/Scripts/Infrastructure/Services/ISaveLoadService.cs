using Data;

namespace Infrastructure.Services
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        GameProgress LoadProgress();
        void DeleteProgress();
    }
}