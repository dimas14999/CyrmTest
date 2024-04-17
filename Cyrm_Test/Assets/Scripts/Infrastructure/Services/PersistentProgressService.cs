using Data;

namespace Infrastructure.Services
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public GameProgress Progress { get; set; }
    }
}