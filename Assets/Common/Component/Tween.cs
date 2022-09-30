using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace Common.Component
{
    public class Tween : MonoBehaviour
    {
        public enum Type
        {
            Position,
            Alpha,
            Rotate,
        }

        [Serializable]
        public class Context
        {
            public enum PlayType
            {
                Default,
                PingPong,
            }
            public AnimationCurve animationCurve;
            public int loopCount = 0;
            public PlayType playType = PlayType.Default;
            public float GetAnimTime()
            {
                if (this.animationCurve == null)
                    return 0;

                var last = this.animationCurve.keys.Last();
                
                return last.time;
            }
            public float GetEvaluateValue(float curTime)
            {
                return this.animationCurve.Evaluate(curTime);
            }
        }

        [Serializable]
        public class PostionContext : Context
        {
            public Vector3 Begin;
            public Vector3 End;
        }

        [Serializable]
        public class AlphaContext : Context
        {
            public float Begin;
            public float End;
        }

        [Serializable]
        public class RotateContext : Context
        {
            public Vector3 Begin;
            public Vector3 End;
        }

        public PostionContext positionContext;
        public AlphaContext alphaContext;
        public RotateContext rotateContext;

        public void StartTween()
        {
            StartCoroutine(Alpha_Co());
        }

        IEnumerator Alpha_Co()
        {
            if (this.alphaContext != null)
            {
                float animTime = this.alphaContext.GetAnimTime();
                float curTime = 0.0f;
                var canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
                if(canvasGroup == null)
                {
                    canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
                }
                int playCount = 0;
                bool reverse = false;
                while (this.alphaContext != null)
                {
                    if (this.alphaContext.playType == Context.PlayType.Default)
                    {
                        if (curTime > animTime)
                        {
                            curTime = 0;
                            playCount++;
                        }
                    }
                    else if(this.alphaContext.playType == Context.PlayType.PingPong)
                    {
                        if (curTime > animTime)
                        {
                            curTime = animTime;
                            reverse = true;
                        }
                        
                        if(curTime <= 0 && reverse == true)
                        {
                            reverse = false;
                            curTime = 0;
                            playCount++;
                        }
                    }

                    if (this.alphaContext.loopCount > 0)
                    {
                        if (this.alphaContext.loopCount <= playCount)
                            break;
                    }

                    var value = this.alphaContext.GetEvaluateValue(curTime);
                    canvasGroup.alpha = value;
                    if (reverse == false)
                        curTime += Time.deltaTime;
                    else
                        curTime -= Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
            }
            Destroy(this);
            yield return null;
        }
    }
}