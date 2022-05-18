using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using UnityEngine.AddressableAssets;
using System.Linq;

namespace Data.Stage
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Stage", order = 1)]
    public class Stage : ScriptableObject
    {
        public int Index;
        public string BossPrefab;
        public string WorldPrefab;
    }

    public static class Instance
    {
        public static ReadOnlyDictionary<int, Stage> Container { get; private set; } = null;
        private static UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<IList<Stage>> handle;
        private static SortedDictionary<int, Stage> container = null;
        public static void Load()
        {
            if (handle.IsValid() == true)
                return;

            container = new SortedDictionary<int, Stage>();
            handle = Addressables.LoadAssetsAsync<Stage>("StageData", LoadData);
            handle.Completed += LoadComplete;
        }

        private static void LoadData(Stage stage)
        {
            container.Add(stage.Index, stage);
        }
        private static void LoadComplete(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<IList<Stage>> obj)
        {
            Container = new ReadOnlyDictionary<int, Stage>(container);
        }
    }
}
