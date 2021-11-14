using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Chessman
{
    public class MoveRecorder : MonoBehaviour
    {
        [Serializable]
        public class RecordedMove
        {
            public float time;
            public Vector3 worldPos;

            public RecordedMove(float time, Vector3 worldPos)
            {
                this.time = time;
                this.worldPos = worldPos;
            }
        }

        private const string FileName = "recorded.json";
        
        private Camera _camera;
        private List<RecordedMove> _recordedMoves = new List<RecordedMove>();

        private void Awake()
        {
            _camera = Camera.main;
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                _recordedMoves.Add(new RecordedMove(Time.time, mouseWorldPos));
            }
        }

        private void OnDestroy()
        {
            var moves = JsonHelper.ToJson(_recordedMoves.ToArray());
            var path = Path.Combine(Application.streamingAssetsPath, FileName);
            File.WriteAllText(path, moves);
        }
    }
    
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}