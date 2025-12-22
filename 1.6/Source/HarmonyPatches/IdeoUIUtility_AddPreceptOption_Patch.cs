using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace LegacyofTheRimCore;

[HarmonyPatch(typeof(IdeoUIUtility), "AddPreceptOption")]
public static class IdeoUIUtility_AddPreceptOption_Patch
{
    public static bool Prefix(Ideo ideo, PreceptDef def, IdeoEditMode editMode, List<FloatMenuOption> options, RitualPatternDef patternDef = null)
    {
        if (def == DefsOf.LOTR_HatedXenotype)
        {
            AddHatedXenotypePreceptOption(def, options, ideo, editMode);
            return false;
        }

        return true;
    }

    private static void AddHatedXenotypePreceptOption(PreceptDef pr, List<FloatMenuOption> opts, Ideo ideo, IdeoEditMode editMode)
    {
        opts.Add(new FloatMenuOption("XenotypeEditor".Translate() + "...", delegate
        {
            Find.WindowStack.Add(new Dialog_CreateXenotype(-1, delegate
            {
                CharacterCardUtility.cachedCustomXenotypes = null;
            }));
        }));

        foreach (XenotypeDef item3 in DefDatabase<XenotypeDef>.AllDefs.OrderBy((XenotypeDef x) => 0f - x.displayPriority))
        {
            XenotypeDef xenotype = item3;
            if (!ideo.PreceptsListForReading.Any((Precept y) => y is Precept_HatedXenotype precept_HatedXenotype && precept_HatedXenotype.xenotype == xenotype))
            {
                opts.Add(PostProcessOption(new FloatMenuOption(xenotype.LabelCap, delegate
                {
                    PostProcessedHatedXenotypeAction(pr, xenotype, null, ideo);
                }, xenotype.Icon, Color.white), ideo, pr, editMode));
            }
        }

        foreach (CustomXenotype item4 in CharacterCardUtility.CustomXenotypesForReading)
        {
            CustomXenotype custom = item4;
            if (!ideo.PreceptsListForReading.Any((Precept y) => y is Precept_HatedXenotype precept_HatedXenotype && precept_HatedXenotype.customXenotype == custom))
            {
                opts.Add(PostProcessOption(new FloatMenuOption(custom.name.CapitalizeFirst() + " (" + "Custom".Translate() + ")", delegate
                {
                    PostProcessedHatedXenotypeAction(pr, null, custom, ideo);
                }, custom.IconDef.Icon, Color.white), ideo, pr, editMode));
            }
        }
    }

    private static FloatMenuOption PostProcessOption(FloatMenuOption option, Ideo ideo, PreceptDef p2, IdeoEditMode editMode)
    {
        if (editMode != IdeoEditMode.Dev)
        {
            AcceptanceReport acceptance = IdeoUIUtility.CanListPrecept(ideo, p2, editMode);
            if (!acceptance && string.IsNullOrWhiteSpace(acceptance.Reason))
            {
                return option;
            }

            if (!acceptance)
            {
                option.action = null;
                option.Label = option.Label + " (" + acceptance.Reason + ")";
            }
        }
        return option;
    }

    private static void PostProcessedHatedXenotypeAction(PreceptDef pr, XenotypeDef xenotypeDef, CustomXenotype customXenotype, Ideo ideo)
    {
        Precept_HatedXenotype precept_HatedXenotype = (Precept_HatedXenotype)PreceptMaker.MakePrecept(pr);
        precept_HatedXenotype.xenotype = xenotypeDef;
        precept_HatedXenotype.customXenotype = customXenotype;
        ideo.AddPrecept(precept_HatedXenotype, init: true);
        ideo.anyPreceptEdited = true;
    }
}
