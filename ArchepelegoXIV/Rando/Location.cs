using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchepelegoXIV.Rando
{
    public class Location(ApState apState, long i)
    {
        public string Name = apState.session.Locations.GetLocationNameFromId(i);
        public long ApId = i;

        public bool Accessible;

        internal bool stale = true;

        public bool IsAccessible()
        {
            if (stale)
            {
                stale = false;
                var allMissingLocations = apState?.session?.Locations?.AllMissingLocations;
                if (allMissingLocations == null)
                    return Accessible = false;
                if (!allMissingLocations.Contains(ApId))
                    return Accessible = false;
                if (!apState.Game.MeetsRequirements(this))
                    return Accessible = false;
                return Accessible = true;
            }
            
            return Accessible;
        }

        public bool Cleared;
    }
}
