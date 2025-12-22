using HarmonyLib;
using Verse;

namespace LegacyofTheRimCore
{
    public class LegacyofTheRimCoreMod : Mod
    {
        public LegacyofTheRimCoreMod(ModContentPack pack) : base(pack)
        {
            new Harmony("LegacyofTheRimCoreMod").PatchAll();
        }
    }
}