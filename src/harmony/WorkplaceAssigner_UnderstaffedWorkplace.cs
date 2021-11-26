using HarmonyLib;
using System.Linq;
using System.Reflection;
using Timberborn.GameDistricts;
using Timberborn.PrioritySystem;
using Timberborn.WorkSystem;

namespace Hectare.Timberborn.EmploymentPriority
{
    /// <summary>
    /// Hook to change how unemployed workers 
    /// </summary>
    [HarmonyPatch]
    public static class WorkplaceAssigner_UnderstaffedWorkplace
    {
        static MethodInfo TargetMethod()
        {
            // Get method from internal class.
            return Assembly.GetAssembly(typeof(Workplace))
                .GetType("Timberborn.WorkSystem.WorkplaceAssigner")
                .GetMethod("UnderstaffedWorkplace", BindingFlags.NonPublic | BindingFlags.Static);
        }

        [HarmonyPostfix]
        public static void Postfix(ref Workplace __result, DistrictCenter districtCenter)
        {
            __result = districtCenter.DistrictBuildingRegistry
                .GetEnabledBuildings<Workplace>()
                .OrderByDescending(w => w.GetComponent<Prioritizable>().Priority)
                .FirstOrDefault(workplace => workplace.Understaffed);
        }
    }
}