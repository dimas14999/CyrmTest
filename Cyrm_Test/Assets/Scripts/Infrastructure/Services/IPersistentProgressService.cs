using Data;

namespace Infrastructure.Services
{
    public interface IPersistentProgressService : IService
    {
        GameProgress Progress { get; set; }
    }
}