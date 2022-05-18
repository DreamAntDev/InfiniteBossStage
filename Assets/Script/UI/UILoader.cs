using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UI
{
    public static class UILoader
    {
        static Dictionary<string, GameObject> uiTree = new Dictionary<string, GameObject>();
        static Dictionary<string, System.Action> loadCompleteTree = new Dictionary<string, System.Action>();

        public static void Load(string addressable, System.Action loadComplete = null)
        {
            if (uiTree.ContainsKey(addressable) == true)
            {
                return;
            }

            Addressables.InstantiateAsync(addressable).Completed += operation => OnLoadComplete(operation, addressable);
            uiTree.Add(addressable, null);
            if (loadComplete != null)
            {
                loadCompleteTree.Add(addressable, loadComplete);
            }
        }
        private static void OnLoadComplete(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj, string addressable)
        {
            if (obj.IsDone)
            {
                if (uiTree.ContainsKey(addressable) == false) // 로딩전 Unload
                {
                    GameObject.Destroy(obj.Result);// 바로제거
                }
                else
                {
                    uiTree[addressable] = obj.Result;
                }

                System.Action loadComplete;
                if (loadCompleteTree.TryGetValue(addressable, out loadComplete) == true)
                {
                    loadComplete();
                    loadCompleteTree.Remove(addressable);
                }
            }
        }
        public static void Unload(string addressable)
        {
            GameObject obj;
            if (uiTree.TryGetValue(addressable, out obj) == false)
                return;

            uiTree.Remove(addressable);
            if (obj == null) // 아직 로딩중
            {
                return;
            }
            GameObject.Destroy(obj);
        }
        public static T GetUI<T>(string addressable)
        {
            GameObject ret = null;
            if(uiTree.TryGetValue(addressable,out ret))
            {
                return ret.GetComponent<T>();
            }
            return default(T);
        }
    }
}