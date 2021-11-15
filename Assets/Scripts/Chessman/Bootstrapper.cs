using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chessman
{
    public class Bootstrapper : MonoBehaviour
    {
        private const string GameSceneName = "GameScene";
        
        private IEnumerator Start()
        {
            var operation = SceneManager.LoadSceneAsync(GameSceneName, LoadSceneMode.Additive);
            while (!operation.isDone)
            {
                yield return null;
            }
        }
    }
}