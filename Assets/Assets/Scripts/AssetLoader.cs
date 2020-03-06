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
        public AssetLoader Instance;
        public static List<GroupData> Groups = new List<GroupData>();

        private async void Start()
        {
            DontDestroyOnLoad(this);
            await AssetLoader.LoadAssets();
            LoadMenu();

        }
        public static async Task LoadAssets()
        {
            AsyncOperationHandle<IList<GroupData>> objecthandle = Addressables.LoadAssetsAsync<GroupData>(new List<object> { "Placeable" }, null, Addressables.MergeMode.Intersection);
            await objecthandle.Task;
            Groups.AddRange(objecthandle.Result);
        }

        public void LoadGame()
        {
            Addressables.LoadSceneAsync("FubarLite");

        }

        public void LoadMenu()
        {
            Addressables.LoadSceneAsync("Menu");
        }

    }
}
