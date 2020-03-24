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
        public static List<BasePlacementData> Placeables = new List<BasePlacementData>();

        private async void Awake()
        {
            if (Instance == null)
                Instance = this;
            DontDestroyOnLoad(this);
            await AssetLoader.LoadAssets();
            Battle.SetActive(true);
            // LoadGame();

        }

        public static async Task LoadAssets()
        {
            AsyncOperationHandle<IList<BasePlacementData>> grouphandle = Addressables.LoadAssetsAsync<BasePlacementData>(new List<object> { "Placeable" }, null, Addressables.MergeMode.Intersection);
            await grouphandle.Task;
            Placeables.AddRange(grouphandle.Result);
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
