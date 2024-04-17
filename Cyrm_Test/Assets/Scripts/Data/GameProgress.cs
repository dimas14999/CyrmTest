using System;

namespace Data
{
    [Serializable]
    public class GameProgress
    {
        public WorldModel WorldModel;
        public GameProgress()
        {
            WorldModel = new WorldModel();
        }
    }
}