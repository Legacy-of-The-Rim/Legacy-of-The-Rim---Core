using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace LegacyofTheRimCore;

[HarmonyPatch(typeof(IdeoUIUtility), nameof(IdeoUIUtility.DoPrecepts))]
public static class IdeoUIUtility_DoPrecepts_Patch
{
    public static void Postfix(ref float curY, float width, Ideo ideo, IdeoEditMode editMode)
    {
        if (ModsConfig.BiotechActive)
        {
            IdeoUIUtility.DoPreceptsInt("LOTR_HatedXenotypes".Translate(), "Xenotype".Translate().ToString().UncapitalizeFirst(), mainPrecepts: false, ideo, editMode, ref curY, width, (PreceptDef p) => p == DefsOf.LOTR_HatedXenotype);
        }
    }
}
