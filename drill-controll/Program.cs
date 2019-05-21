using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System;
using VRage.Collections;
using VRage.Game.Components;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game;
using VRageMath;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {
        IMyPistonBase horizontal1;
        IMyPistonBase horizontal2;
        IMyPistonBase horizontal3;
        IMyPistonBase vertical1;
        IMyPistonBase vertical2;
        IMyPistonBase vertical3;
        IMyBlockGroup pistons;

        List<IMyPistonBase> cranePistons = new List<IMyPistonBase>();
        IMyMotorBase rotor;

        public Program()
        {
            rotor = (IMyMotorBase)GridTerminalSystem.GetBlockWithName("Advanced Rotor");
            
            pistons = GridTerminalSystem.GetBlockGroupWithName("Crane Pisonts");
            pistons.GetBlocksOfType<IMyPistonBase>(cranePistons);
            horizontal1 = (IMyPistonBase)GridTerminalSystem.GetBlockWithName("Piston Crane Horizontal 1");
            horizontal2 = (IMyPistonBase)GridTerminalSystem.GetBlockWithName("Piston Crane Horizontal 2");
            horizontal3 = (IMyPistonBase)GridTerminalSystem.GetBlockWithName("Piston Crane Horizontal 3");

            vertical1 = (IMyPistonBase)GridTerminalSystem.GetBlockWithName("Piston Crane Vertical 1");
            vertical2 = (IMyPistonBase)GridTerminalSystem.GetBlockWithName("Piston Crane Vertical 2");
            vertical3 = (IMyPistonBase)GridTerminalSystem.GetBlockWithName("Piston Crane Vertical 3");

            Echo("Status");
            Echo("-------------------------------------------");
            Echo("Rotor: " + (rotor == null ? "Not Available" : "Ok"));
            Echo("Crane Pistons: " + (pistons == null ? "Not Available" : "Ok") + " / " + (cranePistons == null ? "Not Available" : "Ok"));
            Echo(" - Horizontal 1: " + (horizontal1 == null ? "Not Available" : "Ok"));
            Echo(" - Horizontal 2: " + (horizontal2 == null ? "Not Available" : "Ok"));
            Echo(" - Horizontal 3: " + (horizontal3 == null ? "Not Available" : "Ok"));
            Echo(" - Vertical 1: " + (vertical1 == null ? "Not Available" : "Ok"));
            Echo(" - Vertical 2: " + (vertical2 == null ? "Not Available" : "Ok"));
            Echo(" - Vertical 3: " + (vertical3 == null ? "Not Available" : "Ok"));
        }

        public void Save()
        {
            // Called when the program needs to save its state. Use
            // this method to save your state to the Storage field
            // or some other means. 
            // 
            // This method is optional and can be removed if not
            // needed.
        }

        public void Main(string argument, UpdateType updateSource)
        {
            float offset = 0;
            // Rotor
            if (argument.Equals("start"))
            {
                rotor.SetValueFloat("Velocity", 0.4f);
                rotor.SetValueFloat("UpperLimit", 45 + offset);
                rotor.SetValueFloat("LowerLimit", -45 + offset);
                rotor.SetValueBool("RotorLock", false);
                rotor.SetValueBool("ShareInertiaTensor", false);

                cranePistons.ForEach(piston => ((IMyPistonBase)piston).SetValueFloat("Velocity", -0.3f));
                horizontal1.SetValueFloat("UpperLimit", 1);

            }
            else if (argument.Equals("piston-half"))
            {
                vertical1.SetValueFloat("UpperLimit", 1);
            }
            else if (argument.Equals("piston-half-low"))
            {
                vertical1.SetValueFloat("LowerLimit", 1);
            }
            else if (argument.Equals("piston-full"))
            {
                vertical1.SetValueFloat("UpperLimit", 2);
            }
            else if (argument.Equals("piston-full-low"))
            {
                vertical1.SetValueFloat("LowerLimit", 2);
            }
            else if (argument.Equals("phase-two"))
            {
                rotor.SetValueFloat("Velocity", -0.5f);
                rotor.SetValueFloat("LowerLimit", -90 + offset);
            }
            else if (argument.Equals("rotor-phase-two-finish"))
            {
                rotor.SetValueFloat("UpperLimit", 90 + offset);

            }
            else if (argument.Equals("rotor-phase-three"))
            {
                rotor.SetValueFloat("Velocity", -0.5f);
                rotor.SetValueFloat("LowerLimit", -45 + offset);
            }
            else if (argument.Equals("rotor-phase-three-finish"))
            {
                rotor.SetValueFloat("UpperLimit", 45 + offset);

            }
            else if (argument.Equals("reset"))
            {
                if (rotor.GetValueFloat("Velocity") < 0)
                {
                    rotor.SetValueFloat("UpperLimit", 0 + offset);
                    rotor.SetValueFloat("Velocity", 1f);
                } else
                {
                    rotor.SetValueFloat("LowerLimit", 0 + offset);
                    rotor.SetValueFloat("Velocity", -1f);
                }
                horizontal1.SetValueFloat("LowerLimit", 0.2063f);
            }
            else if (argument.Equals("rotor-end"))
            {
                rotor.SetValueFloat("Velocity", 0);
                rotor.SetValueBool("RotorLock", true);
                rotor.SetValueBool("ShareInertiaTensor", true);

                // Pistons
            }
            else if (argument.Equals("piston-start"))
            {
            }
        }
    }
}