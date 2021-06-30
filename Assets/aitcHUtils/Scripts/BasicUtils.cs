using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace aitcHUtils
{
    [System.Serializable]
    public class Range 
    {
        public float Min { get { return min; } }
        [SerializeField] float min;
        public float Max { get { return max; } }
        [SerializeField] float max;

        public Range(float min, float max) 
        {
            this.min = min;
            this.max = max;
        }

        /// <summary>
        /// Return a random number between min[inclusive] and max[inclusive]
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float GetRandom(float min, float max) 
        {
            return Random.Range(min, max);
        }

        /// <summary>
        /// Return a random number between min[inclusive] and max[exclusive]
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GetRandom(int min, int max)
        {
            return Random.Range(min, max);
        }

        public static int Clamp(int value, Range range)
        {
            if (value > range.Max)
                return (int)range.Max;
            else if (value < range.Min)
                return (int)range.Min;

            return value;
        }

        public static float Clamp(float value, Range range)
        {
            if (value > range.Max)
                return range.Max;
            else if (value < range.Min)
                return range.Min;

            return value;
        }
    }

    public static class Randoms
    {
        public static string RandomChar(bool isUpperCase = true)
        {
            string[] chars = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            string character = chars[Random.Range(0, 26)];

            if (!isUpperCase) return character.ToLower();

            return character;
        }

        public static string RandomWord(int wordLength, bool isUpperCase = true)
        {
            int i = 0;
            string x;
            string word = "";
            while (i < wordLength) 
            {
                x = Randoms.RandomChar();
                word = word + x;
                i++;
            }

            if (!isUpperCase) return word.ToLower();

            return word;
        }

        public static Vector2 RandomVector2(Vector2 minPos, Vector2 maxPos)
        {
            float x = Random.Range(minPos.x, maxPos.x);
            float y = Random.Range(minPos.y, maxPos.y);

            return new Vector2(x, y);
        }
    }

    public class MonoBehaviourExtended : MonoBehaviour
    {
        /// <summary>
        /// Toggles the active state of the object
        /// </summary>
        /// <param name="obj">The object to be toggled</param>
        public void ToggleActiveState(GameObject obj) 
        {
            if (obj.activeInHierarchy) obj.SetActive(false);
            else if (!obj.activeInHierarchy) obj.SetActive(true);
        }

        public static void TypeText(Text _textObject, string _typedText, int _frameInterval)
        {
            _textObject.text = "";
            //startc
        }



    }

    public class TextUtils
    {
        /// <summary>
        /// Types the text from first letter to the last
        /// </summary>
        /// <param name="_monoBehaviourHandler">MonoBehaviour reference. Just use "this" keyword</param>
        /// <param name="_textObject">The Text component in which the text is typed</param>
        /// <param name="_typedText">The text to be typed</param>
        /// <param name="_letterInterval">Delay between typing each letter</param>
        public static void TypeText(MonoBehaviour _monoBehaviourHandler, Text _textObject, string _typedText, out float typeTime, AudioSource source = null, float _letterInterval = 0.1f)
        {
            _textObject.text = "";
            _monoBehaviourHandler.StartCoroutine(coroutine_TypingText(_textObject, _typedText, _letterInterval, source));
            typeTime = _typedText.Length * _letterInterval;
        }

        static IEnumerator coroutine_TypingText(Text _textObject, string _typedText, float _letterInterval, AudioSource source)
        {
            for (int i = 0; i < _typedText.Length; i++)
            {
                yield return new WaitForSeconds(_letterInterval);
                _textObject.text += _typedText[i];

                if (source != null && _typedText[i].ToString() != " ") source.Play();
            }
        }
    }

    public class MiscUtils 
    {
        public static float ValueToVolume(float value, float maxVolume)
        {
            return Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * (maxVolume - (-80f)) / 4f + maxVolume;
        }

        public static Transform[] GetChildren(Transform parent) 
        {
            int childCount = parent.childCount;
            Transform[] children = new Transform[childCount];

            for (int i = 0; i < children.Length; i++)
            {
                children[i] = parent.GetChild(i);
            }

            return children;
        }

        public static List<T> ArrayToList<T>(T[] array)
        {
            List<T> list = new List<T>();

            for (int i = 0; i < array.Length; i++)
            {
                list.Add(array[i]);
            }

            return list;
        }

        public static void MoveTo(MonoBehaviour behaviour, Transform objTransform, Vector3 destination, float speed, bool isLocal = false) 
        {
            behaviour.StartCoroutine (coroutine_MoveTo(objTransform, destination, speed, isLocal));
        }

        static IEnumerator coroutine_MoveTo(Transform objTransform, Vector3 destination, float speed, bool isLocal = false) 
        {
            Vector3 refV = Vector3.zero;
            Vector3 currentPos = Vector3.zero;
            if (isLocal)
            {
                currentPos = objTransform.localPosition;
                while (Mathf.Abs((objTransform.localPosition.x - destination.x)) > 0.01f)
                {
                    objTransform.transform.localPosition = Vector3.MoveTowards(objTransform.localPosition, destination, Time.deltaTime * speed);
                    yield return null;
                }
            }
            else
            {
                currentPos = objTransform.position;
                while (Mathf.Abs((objTransform.position.x - destination.x)) > 0.01f)
                {
                    objTransform.transform.position = Vector3.SmoothDamp(currentPos, destination, ref refV, speed);
                    yield return null;
                }
            }

            
        }

        /// <summary>
        /// Runs the action code after delay. If delay = 0, code with run after 1 frame.
        /// </summary>
        /// <param name="behaviour">The behaviour on which coroutine run. Use "this" in most cases</param>
        /// <param name="code">The code to run</param>
        /// <param name="delay">The time to wait</param>
        public static Coroutine DoWithDelay(MonoBehaviour behaviour, System.Action code, float delay = 0)
        {
            return behaviour.StartCoroutine(coroutine_doWithDelay(code, delay));
        }

        static IEnumerator coroutine_doWithDelay(System.Action code, float delay)
        {
            if (delay > 0)
            {
                yield return new WaitForSeconds(delay);
                code.Invoke();
            }
            else 
            {
                yield return null;
                code.Invoke();
            }
        }

        /// <summary>
        /// Runs the action code after delay. If delay = 0, code with run after 1 frame.
        /// </summary>
        /// <param name="behaviour">The behaviour on which coroutine run. Use "this" in most cases</param>
        /// <param name="code">The code to run</param>
        /// <param name="delay">The time to wait</param>
        public static Coroutine MoveTowardsUI(MonoBehaviour behaviour, RectTransform rect, Vector2 finalAnchoredPos, float maxDistanceDelta, System.Action action = null)
        {
            return behaviour.StartCoroutine(coroutine_MoveTowardsUI(rect, finalAnchoredPos, maxDistanceDelta, action));
        }

        static IEnumerator coroutine_MoveTowardsUI(RectTransform rect, Vector2 finalAnchoredPos, float maxDistanceDelta, System.Action action)
        {
            while (rect.anchoredPosition.x != finalAnchoredPos.x || rect.anchoredPosition.y != finalAnchoredPos.y)
            {
                rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, finalAnchoredPos, maxDistanceDelta);
                yield return null;
            }
            yield return null;

            if (action != null)
            {
                action.Invoke();
            }
        }


    }
}