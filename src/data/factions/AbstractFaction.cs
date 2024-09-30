using rose.row.easy_package.audio;
using UnityEngine;

namespace rose.row.data.factions
{
    public abstract class AbstractFaction
    {
        public abstract Texture2D factionIcon { get; }
        public abstract ImageHolder[] factionRanks { get; }
        public abstract AudioHolder captureJingle { get; }

        public abstract string factionName { get; }
        public abstract string factionISO2 { get; }
    }

    public class GmFaction : AbstractFaction
    {
        public override Texture2D factionIcon => ImageRegistry.germanyFactionImage.get();
        public override ImageHolder[] factionRanks => ImageRegistry.ranksGM;
        public override AudioHolder captureJingle => AudioRegistry.capturePointGM;

        public override string factionName => "Germany";
        public override string factionISO2 => "de";
    }

    public class UsFaction : AbstractFaction
    {
        public override Texture2D factionIcon => ImageRegistry.usFactionImage.get();
        public override ImageHolder[] factionRanks => ImageRegistry.ranksUS;
        public override AudioHolder captureJingle => AudioRegistry.capturePointUS;

        public override string factionName => "United States";
        public override string factionISO2 => "us";
    }

    public class RuFaction : AbstractFaction
    {
        public override Texture2D factionIcon => ImageRegistry.ruFactionImage.get();
        public override ImageHolder[] factionRanks => ImageRegistry.ranksRU;
        public override AudioHolder captureJingle => AudioRegistry.capturePointRU;

        public override string factionName => "URSS";
        public override string factionISO2 => "ru";
    }
}