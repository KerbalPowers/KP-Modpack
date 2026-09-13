using System;
using System.Collections.Generic;
using UnityEngine;
using static PartModule;

namespace Depletion
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class BatteryDrain : PartModule
    {
        [KSPField] public float percentFull = 100;

        public override void OnStart(StartState state)
        {
            if (part.Resources.Contains("ElectricCharge"))
            {
                // Find the electric charge resource in the part
                var electricChargeResource = part.Resources.Get("ElectricCharge");
                float max = (float)electricChargeResource.maxAmount;
                float current = (float)electricChargeResource.amount;

                //Calculate how much to drain to reach the amount
                float demand = (max - (max * (percentFull / 100))) - (max-current);
                //Debug.Log("[Depletion] Demand: " + demand);

                if (demand > 0)
                {
                    // Set the amount of the electric charge by subtracting what we don't want
                    Debug.Log("[Depletion] Draining to " + percentFull + "%");
                    part.RequestResource("ElectricCharge", demand, ResourceFlowMode.NO_FLOW);
                }
            }

        }
    }
}
