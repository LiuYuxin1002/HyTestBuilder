using HyTestRTDataService.Entities;
using System;
using System.Data;

namespace HyTestRTDataService.ConfigMode.MapEntities
{
    [Serializable]
    public class ConfigAdapterInfo
    {
        private Adapter[] adapterTable;
        private int selected;

        public Adapter[] Adapters { get => adapterTable; set => adapterTable = value; }
        public int Selected { get => selected; set => selected = value; }

        public ConfigAdapterInfo()
        {
            this.Adapters = new Adapter[0];
        }

    }
}
