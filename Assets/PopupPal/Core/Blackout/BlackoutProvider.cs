using UnityEngine;

namespace PopupPal.Core.Blackout
{
    public static class BlackoutProvider
    {
        private const string DefaultBlackoutPath = "Prefabs/DefaultBlackout";
        
        public static Blackout LoadDefault()
        {
            return Resources.Load<Blackout>(DefaultBlackoutPath);
        }
    }
}
