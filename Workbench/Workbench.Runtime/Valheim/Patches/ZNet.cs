using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Workbench.Runtime.Valheim.Patches
{
    #region MonoBehaviour lifecycle patches
    [HarmonyPatch(typeof(ZNet), "Awake")]
    internal static class ZNetAwake
    {
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "CalledViaReflection")]
        private static bool Prefix(ref ZNet __instance)
        {
            return true;
        }
    }

    #endregion
}
