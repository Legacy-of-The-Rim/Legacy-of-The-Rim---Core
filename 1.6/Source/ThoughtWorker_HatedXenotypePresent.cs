using System.Collections.Generic;
using Verse;
using RimWorld;

namespace LegacyofTheRimCore
{
    public class ThoughtWorker_HatedXenotypePresent : ThoughtWorker_Precept
    {
        public override ThoughtState ShouldHaveThought(Pawn p)
        {
            if (!ModsConfig.BiotechActive || !ModsConfig.IdeologyActive || p.Faction == null)
            {
                return ThoughtState.Inactive;
            }

            List<Pawn> list = p.MapHeld.mapPawns.SpawnedPawnsInFaction(p.Faction);
            int hatedXenotypesPresent = 0;

            foreach (Pawn pawn in list)
            {
                if (pawn != p && pawn.IsFreeColonist && !pawn.IsSlave && !pawn.IsPrisoner)
                {
                    if (pawn.genes != null && XenotypePreceptUtility.IsPawnHatedXenotype(pawn, p.Ideo.PreceptsListForReading))
                    {
                        hatedXenotypesPresent++;
                    }
                }
            }

            if (hatedXenotypesPresent > 0)
            {
                return ThoughtState.ActiveAtStage(0);
            }

            return ThoughtState.Inactive;
        }
    }
}
