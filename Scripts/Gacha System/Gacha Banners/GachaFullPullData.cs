using System;
using System.Collections.Generic;

namespace GachaSystem
{
    public class GachaFullPullData
    {
        public List<GachaPullData> Pulls { get; private set; } = new List<GachaPullData>();

        public GachaFullPullData(List<GachaPullData> pulls)
        {
            Pulls.AddRange(pulls);
        }
    }
}
