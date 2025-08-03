using Modding;
using System.Reflection;
using UnityEngine;

namespace OnlyBosses
{
    internal class OnlyBosses : Mod, ITogglableMod
    {
        internal static OnlyBosses Instance { get; private set; }

        public OnlyBosses() : base("OnlyBosses") { }

        public override string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public override void Initialize()
        {
            Log("Initializing");

            Instance = this;
            ModHooks.OnEnableEnemyHook += OnEnemyEnable;
            Log("Initialized");
        }

        // Enemy & Scene Name Values are taken from TheHuntIsOn by Korz (https://github.com/korzer420/thehuntison)
        private bool OnEnemyEnable(GameObject enemy, bool isAlreadyDead)
        {
            string enemyName = enemy.name;
            string scene = enemy.scene.name;

            if (enemyName == "Mega Moss Charger" ||
                enemyName == "Giant Fly" ||
                enemyName == "False Knight New" ||
                enemyName == "Mage Knight" ||
                enemyName == "Mage Lord Phase2" ||
                enemyName == "Head" ||
                enemyName == "Radiance" ||
                scene == "Fungus3_23_boss" ||
                scene == "Ruins2_11_boss" ||
                (enemyName.Contains("Fly") && scene == "Crossroads_04")) {
                return false;
            }

            HealthManager healthManager = enemy.GetComponent<HealthManager>();
            return healthManager.hp < 200 || isAlreadyDead; 
        }

        public void Unload()
        {
            ModHooks.OnEnableEnemyHook -= OnEnemyEnable;
        }
    }
}