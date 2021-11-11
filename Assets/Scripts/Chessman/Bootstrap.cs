using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chessman
{
    public class Bootstrap : MonoBehaviour
    {
        private const string GameSceneName = "GameScene";
        
        private IEnumerator Start()
        {
            var operation = SceneManager.LoadSceneAsync(GameSceneName, LoadSceneMode.Single);
            yield return new WaitUntil(() => operation.isDone);
        }
    }
}