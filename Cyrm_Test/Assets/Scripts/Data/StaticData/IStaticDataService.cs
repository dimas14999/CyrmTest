using Infrastructure.Services;
using Model;

namespace Data.StaticData
{
    public interface IStaticDataService : IService
    {
        LevelModel LevelModel { get; set; }
    }
}