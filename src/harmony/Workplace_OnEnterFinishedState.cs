using HarmonyLib;
using Timberborn.PrioritySystem;
using Timberborn.PrioritySystemUI;
using Timberborn.WorkSystem;

namespace Hectare.Timberborn.EmploymentPriority
{
    /// <summary>
    /// Hook when buildings are completed.
    /// </summary>
    [HarmonyPatch(typeof(Workplace), "OnEnterFinishedState")]
    public static class Workplace_OnEnterFinishedState
    {
        [HarmonyPostfix]
        public static void Postfix(Workplace __instance)
        {
            // Assumes that buildings retain their prioritizable component.
            __instance.gameObject.GetComponent<PrioritizableTitle>().Title = "Employment priority:";
            __instance.gameObject.GetComponent<Prioritizable>().Enable();
        }
    }
}