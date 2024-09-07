using System.Collections.Generic;

namespace Modification
{
    public class PlayerInteractSpeedRate : Modification<float>
    {
        private const string GUID = "PlayerCookingSpeedRateGUID";

        public PlayerInteractSpeedRate()
            : base(GUID) { }

        public override List<ModificationData<float>> Data
        {
            get
            {
                return new List<ModificationData<float>>()
                {
                    new ModificationData<float>(100, 0.2f),
                    new ModificationData<float>(250, 0.6f),
                    new ModificationData<float>(450, 1.2f),
                    new ModificationData<float>(600, 2.1f),
                    new ModificationData<float>(750, 2.5f),
                    new ModificationData<float>(950, 3f),
                    new ModificationData<float>(1200, 3.5f)
                };
            }
        }
    }
}