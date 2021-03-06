using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Timberborn.Buildings;
using Timberborn.Characters;
using Timberborn.ConstructibleSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.GameDistricts;
using Timberborn.PrioritySystem;
using Timberborn.WorkSystem;
using TimberbornAPI.UIBuilderSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Hectare.Timberborn.EmploymentPriority
{
    public class WorkplaceEmployNowFragment : IEntityPanelFragment
    {
        private readonly UIBuilder _builder;

        private VisualElement _root;
        private Workplace _workplace;

        public WorkplaceEmployNowFragment(UIBuilder builder)
        {
            _builder = builder;
        }

        public VisualElement InitializeFragment()
        {
            _root = _builder.CreateComponentBuilder()
                .CreateButton()
                .SetText("Employ now")
                .SetName("employButton")
                .SetWidth(120)
                .SetJustifyContent(Justify.SpaceAround)
                .Build();

            _root.Q<Button>("employButton").clicked += OnEmployButtonClicked;

            return _root;
        }

        public void ShowFragment(GameObject entity) 
        {
            _workplace = entity.GetComponent<Workplace>();
        }

        public void ClearFragment()
        {
            _root.ToggleDisplayStyle(false);
        }

        public void UpdateFragment()
        {
            if (IsValidWorkplace())
                _root.ToggleDisplayStyle(true);
            else
                _root.ToggleDisplayStyle(false);
        }

        private bool IsValidWorkplace()
        {
            var constructionSite = _workplace?.GetComponent<Constructible>();

            return _workplace is not null &&
                _workplace.enabled &&
                _workplace.Understaffed;
        }

        private void OnEmployButtonClicked()
        {
            // Make sure we can actually fit the worker.
            if (!IsValidWorkplace())
            {
                Debug.Log("Workplace is not able to accept workers.");
                return;
            }

            // Don't do this.
            var districts = GameObject.FindObjectsOfType<DistrictCenter>();
            var workplaceDistrict = districts
                .FirstOrDefault(d => d.DistrictBuildingRegistry
                    .GetEnabledBuildings<Workplace>()
                    .Contains(_workplace));

            if (workplaceDistrict is null)
            {
                Debug.Log("Couldn't find district.");
                return;
            }

            var workers = workplaceDistrict.DistrictPopulation.GetEnabledCharacters<Worker>();

            // Try grab an unemployed beaver first.
            // Make sure the worker is within the same district.
            var unemployedWorker = workers
                .Where(worker => worker is not null && worker.enabled && !worker.Employed)
                .OrderBy(worker => worker.UnemploymentTimestamp)
                .FirstOrDefault();

            if (unemployedWorker is not null)
            {
                _workplace.AssignWorker(unemployedWorker);
            }
            else
            {
                // Find the worker with the least important job.
                var employedWorker = workers
                    .OrderBy(f => f.Workplace.GetComponent<Prioritizable>().Priority)
                    .Where(f => f.Employed)
                    .Where(f => f.Workplace != _workplace)
                    .Where(f => 
                        !(f.Workplace.GetComponent<DistrictCenter>() == workplaceDistrict &&
                        f.Workplace.NumberOfAliveAssignedWorkers <= 1))
                    .FirstOrDefault();

                if (employedWorker is not null)
                {
                    Debug.Log($"Moved worker from {employedWorker.Workplace} to {_workplace}");
                    _workplace.AssignWorker(employedWorker);
                }
                else
                {
                    Debug.Log("Can't find a suitable worker.");
                }
            }
        }
    }
}