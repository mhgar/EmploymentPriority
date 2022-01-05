using BepInEx;
using HarmonyLib;
using System.Reflection;
using TimberbornAPI;
using UnityEngine;

namespace Hectare.Timberborn.EmploymentPriority
{
    [BepInPlugin("hectare.timberborn.employmentpriority", "EmploymentPriority", "1.0.2")]
    [BepInDependency("com.timberapi.timberapi")]
    [HarmonyPatch]
    public class EmploymentPriorityPlugin : BaseUnityPlugin
    {
        public void Awake()
        {
            Debug.Log($"Plugin hectare.timberborn.employmentpriority is loaded...");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
            TimberAPI.DependencyRegistry.AddConfigurator(new WorkplaceEmployNowFragmentConfigurator());
        }
    }
}