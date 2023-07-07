using BepInEx;
using UnboundLib;
using UnityEngine;

namespace LoadIntoSandbox
{
    // Tells BepIn what process the mod is using
    [BepInProcess("Rounds.exe")]
    // Registers the mod to BepIn
    [BepInPlugin(ModId, ModName, Version)]
    public class Main : BaseUnityPlugin
    {
        private const string ModId = "dev.scyye.rounds.directToSandbox";
        private const string ModName = "Direct Into Sandbox";
        private const string Version = "1.0.0";

        private float nextCheck = Time.time + 1f;
        private bool inSandbox = false;

        void Awake()
        {
            Unbound.RegisterClientSideMod(ModId);
        }

        void Update()
        {
            // if we're already in sandbox, or we shouldn't be checking, do nothing
            if (inSandbox ||
                Time.time < nextCheck) 
                return;

            // Set the check time.
            nextCheck = Time.time + 1f;

            // Paths to the game object buttons
            const string localBtnPath = "Game/UI/UI_MainMenu/Canvas/ListSelector/Main/Group/LOCAL";
            const string startSandboxBtnPath = "Game/UI/UI_MainMenu/Canvas/ListSelector/LOCAL/Group/Grid/Scroll View/Viewport/Content/Test";

            GameObject menuLocalBtn = GameObject.Find(localBtnPath);
            GameObject menuSandboxBtn = GameObject.Find(startSandboxBtnPath);

            // if either of the buttons are null, do nothing
            if (menuLocalBtn == null || menuSandboxBtn == null) 
                return;

            // if the menu button is active
            if (menuLocalBtn.activeInHierarchy)
            {
                // Click it, and set our next check to .5 seconds from now.
                menuLocalBtn.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
                nextCheck = Time.time + 0.5f;
            }
            // if the sandbox button is active
            else if (menuSandboxBtn.activeInHierarchy)
            {
                // Click it and make sure the game knows we're in sandbox
                menuSandboxBtn.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
                inSandbox = true;
            }

            //PlayerAssigner.instance.StartCoroutine(PlayerAssigner.instance.CreatePlayer(null, isAI: true));
        }
    }
}
