using System.Collections.Generic;

namespace Modification
{
    public class PlayerSpeedRateModification : Modification<float>
    {
        private const string GUID = "PlayerSpeedRateGUID";

        public PlayerSpeedRateModification()
            : base(GUID) { }

        public override List<ModificationData<float>> Data
        {
            get
            {
                return new List<ModificationData<float>>()
                {
                    new ModificationData<float>(200, 1f),
                    new ModificationData<float>(300, 1.1f),
                    new ModificationData<float>(550, 1.2f),
                    new ModificationData<float>(600, 1.3f),
                    new ModificationData<float>(1000, 1.3f),
                    new ModificationData<float>(1200, 1.4f),
                    new ModificationData<float>(1600, 1.5f)
                };
            }
        }
    }
}