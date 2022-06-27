using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AddressableAssets;

namespace Static
{
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager instance;
        public static SoundManager Instance
        {
            get
            {
                if (instance == null)
                {
                    var obj = new GameObject();
                    instance = obj.AddComponent<SoundManager>();
                    instance.soundContainer = new Dictionary<SoundType, List<AudioSource>>();
                    instance.soundContainer.Add(SoundType.BGM, new List<AudioSource>());
                    instance.soundContainer.Add(SoundType.Effect, new List<AudioSource>());
                    //Debug.LogError("SoundManager가 Scene에 없습니다.");
                }
                return instance;
            }
        }

        private Dictionary<SoundType, List<AudioSource>> soundContainer;

        public enum SoundType
        {
            BGM,
            Effect
        }

        private void Awake()
        {
            if (SoundManager.instance != null)
            {
                Destroy(this);
                return;
            }

            SoundManager.instance = this;
        }

        private void LateUpdate()
        {
            soundContainer[SoundType.Effect].RemoveAll(o => o.isPlaying == false);
            //foreach (var soundObj in soundContainer[SoundType.Effect])
            //{
            //    if(soundObj.isPlaying == false)
            //    {
            //        GameObject.Destroy(soundObj.gameObject);
            //    }
            //}
        }

        public void ClearSound()
        {
            foreach(var list in soundContainer)
            {
                foreach(var soundObj in list.Value)
                {
                    soundObj.Stop();
                    GameObject.Destroy(soundObj.gameObject); // 버그날것 같은데
                }
                list.Value.Clear();
            }
        }

        public void PlaySound(string soundPath, SoundType soundType)
        {
            var handle = LoadSound(soundPath);
            if (soundType == SoundType.BGM)
            {
                handle.Completed += PlayBGM;
            }
            else
            {
                handle.Completed += PlayEffect;
            }
        }

        public void PlaySound(AudioClip clip, SoundType soundType)
        {
            var go = new GameObject();
            var audio = go.AddComponent<AudioSource>();
            
            this.soundContainer[soundType].Add(audio);

            audio.clip = clip;

            if (soundType == SoundType.BGM)
            {
                audio.loop = true;
            }
            else
            {
                audio.loop = false;
            }
            audio.Play();
        }

        public UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<AudioClip> LoadSound(string soundPath)
        {
            var retValue = Addressables.LoadAssetAsync<AudioClip>(soundPath);

            return retValue;
        }

        private void PlayBGM(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<AudioClip> obj)
        {
            PlaySound(obj.Result, SoundType.BGM);
        }

        private void PlayEffect(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<AudioClip> obj)
        {
            PlaySound(obj.Result, SoundType.Effect);
        }
    }
}