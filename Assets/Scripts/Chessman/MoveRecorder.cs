using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Chessman
{
    public class MoveRecorder : MonoBehaviour
    {
        [Serializable]
        public class RecordedMove
        {
            public float time;
            public float diff;
            public Vector3 worldPos;

            public RecordedMove(float time, Vector3 worldPos, float diff)
            {
                this.time = time;
                this.worldPos = worldPos;
                this.diff = diff;
            }
        }

        private const string FileName = "recorded.json";
        private const string PromotionFileName = "promotion_recorded.json";

        public bool recordMoves;
        
        private Camera _camera;
        private List<RecordedMove> _recordedMoves = new List<RecordedMove>();
        private List<RecordedMove> _loadedMoves = new List<RecordedMove>();
        private int _loadedMoveIndex = 0;
        private float _lastLoadedTime = 0;

        private void Awake()
        {
            _camera = Camera.main;
            var loadedMovesText = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, PromotionFileName));
            _loadedMoves = JsonHelper.FromJson<RecordedMove>(loadedMovesText).ToList();
        }

        public RecordedMove GetMove()
        {
            if (_loadedMoves.Count - 1 >= _loadedMoveIndex)
            {
                var next = _loadedMoves[_loadedMoveIndex];
                if (next.diff * 0.9f <= Time.time - _lastLoadedTime)
                {
                    _loadedMoveIndex++;
                    _lastLoadedTime = Time.time;
                    return next;
                }
            }

            return null;
        }
        
        private void Update()
        {
            if (!recordMoves)
            {
                return;
            }
            if (Input.GetMouseButtonDown(0))
            {
                var mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                var count = _recordedMoves.Count;
                var diff = count > 0 ? (Time.time - _recordedMoves[count - 1].time) : 0;
                _recordedMoves.Add(new RecordedMove(Time.time, mouseWorldPos, diff));
            }
        }
        
        private string GetRecordedMovePath() => Path.Combine(Application.streamingAssetsPath, FileName);

        private void OnDestroy()
        {
            if (recordMoves)
            {
                var moves = JsonHelper.ToJson(_recordedMoves.ToArray());
                var path = GetRecordedMovePath();
                File.WriteAllText(path, moves);
            }
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