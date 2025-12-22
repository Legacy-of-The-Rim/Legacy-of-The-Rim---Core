using System.Collections.Generic;
using Verse;
using RimWorld;

namespace LegacyofTheRimCore
{
    public static class XenotypePreceptUtility
    {
        public static bool IsPawnHatedXenotype(Pawn pawn, Precept precept)
        {
            if (pawn.genes == null)
            {
                return false;
            }
            if (precept is Precept_HatedXenotype hatedXenotype)
            {
                if (hatedXenotype.xenotype != null && pawn.genes.Xenotype == hatedXenotype.xenotype)
                {
                    return true;
                }
                if (hatedXenotype.customXenotype != null && GeneUtility.PawnIsCustomXenotype(pawn, hatedXenotype.customXenotype))
                {
                    return true;
                }
            }
            if (precept.def.comps != null)
            {
                foreach (PreceptComp comp in precept.def.comps)
                {
                    if (comp is PreceptComp_SituationalThought_HatedXenotype hatedComp && hatedComp.hatedXenotypes != null)
                    {
                        foreach (XenotypeDef xenotypeDef in hatedComp.hatedXenotypes)
                        {
                            if (pawn.genes.Xenotype == xenotypeDef)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static bool IsPawnHatedXenotype(Pawn pawn, IEnumerable<Precept> precepts)
        {
            foreach (Precept precept in precepts)
            {
                if (IsPawnHatedXenotype(pawn, precept))
                {
                    return true;
                }
            }
            return false;
        }

    }
}