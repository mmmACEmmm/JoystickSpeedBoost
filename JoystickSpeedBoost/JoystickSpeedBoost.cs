using System;
using BepInEx;
using GorillaLocomotion;
using UnityEngine;
using Valve.VR;

namespace GorillaTagSpeedBoost
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Main : BaseUnityPlugin
    {
        private float defaultJumpMultiplier = 1.2f;
        private float defaultMaxJumpSpeed = 8f;
        // Left thumbstick boost values
        private float leftBoostMaxJumpSpeed = 8f;
        private float leftBoostJumpMultiplier = 1.2f;
        // Right thumbstick boost values
        private float rightBoostMaxJumpSpeed = 6f;
        private float rightBoostJumpMultiplier = 0.8f;

        private bool showGUI = true;
        private Rect windowRect = new Rect(20, 20, 350, 300);

        private string leftMaxSpeedInput = "8.00";
        private string leftMultiplierInput = "1.20";
        private string rightMaxSpeedInput = "6.00";
        private string rightMultiplierInput = "0.80";

        private void OnGUI()
        {
            if (showGUI)
            {
                GUI.backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.9f);
                windowRect = GUI.Window(0, windowRect, DrawWindow, "<color=yellow>Speed Boost Settings</color>");
            }
        }

        private void DrawWindow(int windowID)
        {
            GUI.backgroundColor = Color.white;
            GUI.contentColor = Color.yellow;

            int y = 30;
            GUI.Label(new Rect(10, y, 200, 20), "Left Boost Max Speed:");
            leftMaxSpeedInput = GUI.TextField(new Rect(220, y, 60, 20), leftMaxSpeedInput);
            y += 20;
            leftBoostMaxJumpSpeed = GUI.HorizontalSlider(new Rect(10, y, 330, 20), leftBoostMaxJumpSpeed, 0f, 20f);
            y += 30;

            GUI.Label(new Rect(10, y, 200, 20), "Left Boost Multiplier:");
            leftMultiplierInput = GUI.TextField(new Rect(220, y, 60, 20), leftMultiplierInput);
            y += 20;
            leftBoostJumpMultiplier = GUI.HorizontalSlider(new Rect(10, y, 330, 20), leftBoostJumpMultiplier, 0f, 3f);
            y += 30;

            GUI.Label(new Rect(10, y, 200, 20), "Right Boost Max Speed:");
            rightMaxSpeedInput = GUI.TextField(new Rect(220, y, 60, 20), rightMaxSpeedInput);
            y += 20;
            rightBoostMaxJumpSpeed = GUI.HorizontalSlider(new Rect(10, y, 330, 20), rightBoostMaxJumpSpeed, 0f, 20f);
            y += 30;

            GUI.Label(new Rect(10, y, 200, 20), "Right Boost Multiplier:");
            rightMultiplierInput = GUI.TextField(new Rect(220, y, 60, 20), rightMultiplierInput);
            y += 20;
            rightBoostJumpMultiplier = GUI.HorizontalSlider(new Rect(10, y, 330, 20), rightBoostJumpMultiplier, 0f, 3f);
            y += 30;

            GUI.contentColor = Color.white;
            GUI.backgroundColor = Color.green;
            if (GUI.Button(new Rect(10, y, 160, 30), "Reset"))
            {
                ResetToDefaults();
            }
            GUI.backgroundColor = Color.red;
            if (GUI.Button(new Rect(180, y, 160, 30), "Hide (F2)"))
            {
                showGUI = false;
            }

            GUI.DragWindow();

            // Update text fields based on slider values
            leftMaxSpeedInput = leftBoostMaxJumpSpeed.ToString("F2");
            leftMultiplierInput = leftBoostJumpMultiplier.ToString("F2");
            rightMaxSpeedInput = rightBoostMaxJumpSpeed.ToString("F2");
            rightMultiplierInput = rightBoostJumpMultiplier.ToString("F2");

            // Update slider values based on text input
            UpdateValueFromInput(ref leftBoostMaxJumpSpeed, leftMaxSpeedInput, 0f, 20f);
            UpdateValueFromInput(ref leftBoostJumpMultiplier, leftMultiplierInput, 0f, 3f);
            UpdateValueFromInput(ref rightBoostMaxJumpSpeed, rightMaxSpeedInput, 0f, 20f);
            UpdateValueFromInput(ref rightBoostJumpMultiplier, rightMultiplierInput, 0f, 3f);
        }

        private void UpdateValueFromInput(ref float value, string input, float min, float max)
        {
            if (float.TryParse(input, out float parsedValue))
            {
                value = Mathf.Clamp(parsedValue, min, max);
            }
        }

        private void ResetToDefaults()
        {
            leftBoostMaxJumpSpeed = 8f;
            leftBoostJumpMultiplier = 1.2f;
            rightBoostMaxJumpSpeed = 6f;
            rightBoostJumpMultiplier = 0.8f;

            leftMaxSpeedInput = leftBoostMaxJumpSpeed.ToString("F2");
            leftMultiplierInput = leftBoostJumpMultiplier.ToString("F2");
            rightMaxSpeedInput = rightBoostMaxJumpSpeed.ToString("F2");
            rightMultiplierInput = rightBoostJumpMultiplier.ToString("F2");
        }

        private void Update()
        {
            if (Player.Instance != null)
            {
                if (SteamVR_Actions.gorillaTag_LeftJoystickClick.state)
                {
                    Player.Instance.maxJumpSpeed = leftBoostMaxJumpSpeed;
                    Player.Instance.jumpMultiplier = leftBoostJumpMultiplier;
                }
                else if (SteamVR_Actions.gorillaTag_RightJoystickClick.state)
                {
                    Player.Instance.maxJumpSpeed = rightBoostMaxJumpSpeed;
                    Player.Instance.jumpMultiplier = rightBoostJumpMultiplier;
                }
                else
                {
                    Player.Instance.maxJumpSpeed = defaultMaxJumpSpeed;
                    Player.Instance.jumpMultiplier = defaultJumpMultiplier;
                }
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                showGUI = !showGUI;
            }
        }
    }

    internal static class PluginInfo
    {
        public const string GUID = "com.ace.gorillatagspeedboost";
        public const string Name = "GorillaTagSpeedBoost";
        public const string Version = "2.0.0";
    }
}