using HarmonyLib;
using Timberborn.PrioritySystem;
using Timberborn.WorkSystem;

namespace Hectare.Timberborn.EmploymentPriority
{
    /// <summary>
    /// Hook when buildings are deleted.
    /// </summary>
    [HarmonyPatch(typeof(Workplace), "OnExitFinishedState")]
    public static class Workplace_OnExitFinishedState
    {
        [HarmonyPostfix]
        public static void Postfix(Workplace __instance)
        {
            __instance.gameObject.GetComponent<Prioritizable>().Disable();
        }
    }
}