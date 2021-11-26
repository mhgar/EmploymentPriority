using Bindito.Core;
using Timberborn.EntityPanelSystem;

namespace Hectare.Timberborn.EmploymentPriority
{
    public class WorkplaceEmployNowFragmentConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition)
        {
            containerDefinition.Bind<WorkplaceEmployNowFragment>().AsSingleton();
            containerDefinition.MultiBind<EntityPanelModule>().ToProvider<EntityPanelModuleProvider>().AsSingleton();
        }

        private class EntityPanelModuleProvider : IProvider<EntityPanelModule>
        {
            private readonly WorkplaceEmployNowFragment _fragment;

            public EntityPanelModuleProvider(WorkplaceEmployNowFragment fragmentExample)
            {
                _fragment = fragmentExample;
            }

            public EntityPanelModule Get()
            {
                EntityPanelModule.Builder builder = new EntityPanelModule.Builder();
                builder.AddMiddleFragment(_fragment);
                return builder.Build();
            }
        }
    }
}