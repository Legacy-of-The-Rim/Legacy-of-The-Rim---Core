using System.Collections.Generic;
using Verse;
using RimWorld;

namespace LegacyofTheRimCore
{
    public class ThoughtWorker_HatedXenotypeSocial : ThoughtWorker_Precept_Social
    {
        public override ThoughtState ShouldHaveThought(Pawn p, Pawn otherPawn)
        {
            if (!ModsConfig.BiotechActive || !ModsConfig.IdeologyActive || otherPawn.genes == null)
            {
                return ThoughtState.Inactive;
            }

            if (XenotypePreceptUtility.IsPawnHatedXenotype(otherPawn, p.Ideo.PreceptsListForReading))
            {
                return ThoughtState.ActiveAtStage(0);
            }

            return ThoughtState.Inactive;
        }
    }
}