using System;
using BepInEx;
using GorillaLocomotion;
using UnityEngine;

namespace JoystickSpeedBoost
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Main : BaseUnityPlugin
    {
        private void Update()
        {
            ApplySpeedBoost();
        }

        private void ApplySpeedBoost()
        {
            // Always set the speed boost values
            Player.Instance.maxJumpSpeed = 80;
            Player.Instance.jumpMultiplier = 20;
        }
    }

    internal static class PluginInfo
    {
        public const string GUID = "ACE.COOL";
        public const string Name = "JoystickSpeedBoost";
        public const string Version = "1.0.0";
    }
}
