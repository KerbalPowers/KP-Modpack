using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PartModule;

namespace Torquer
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class ModuleTorqueCap : PartModule
    {
        [KSPField] public float torquePerTon = 1;

        private float totalTorque = 0;
        private float torquePercent;

        // The array list to store the found modules
        private ArrayList foundModules = new ArrayList();
        public override void OnStart(StartState state)
        {
            // Iterate through each part on the vessel
            foreach (Part part in vessel.Parts)
            {
                if (part.Modules.Contains("ModuleReactionWheel"))
                {
                    ModuleReactionWheel torque = part.FindModuleImplementing<ModuleReactionWheel>();
                    totalTorque += (torque.PitchTorque+torque.YawTorque+torque.RollTorque)/3;
                    foundModules.Add(torque);
                }
            }

            torquePercent = ((part.vessel.GetTotalMass() * torquePerTon) / totalTorque)*100;


            // Iterate through each part on the vessel
            foreach (ModuleReactionWheel torque in foundModules)
            {
                torque.authorityLimiter = Mathf.Min(100f, torquePercent);
            }
          
        }
    }
}
