using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

namespace FUBAR
{

    public class AssetLoader : MonoBehaviour
    {
        public static AssetLoader Instance;
        public GameObject Battle;
        private static List<BasePlacementData> Placeables;
        public List<BasePlacementData> HackList;

        public static List<BasePlacementData> GetData()
        {
            return Placeables;
        }

        private async void Awake()
        {
            Placeables = new List<BasePlacementData>(HackList);
            if (Instance == null)
                Instance = this;
            DontDestroyOnLoad(this);
            await LoadAssets();
            Battle.SetActive(true);
            // LoadGame();

        }

        public static async Task LoadAssets()
        {
            AsyncOperationHandle<IList<BasePlacementData>> grouphandle = Addressables.LoadAssetsAsync<BasePlacementData>(new List<object> { "Placeable" }, null, Addressables.MergeMode.Intersection);
            await grouphandle.Task;

            //  Placeables.AddRange(grouphandle.Result);
        }

        public void LoadGame()
        {
            Addressables.LoadSceneAsync("FubarLite");
        }

        public void LoadMenu()
        {
            Addressables.LoadSceneAsync("Menu");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

    }
}
