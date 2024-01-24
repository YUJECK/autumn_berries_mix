using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Internal.Codebase.Infrastructure.Services.SceneLoader
{
    public sealed class AsyncSceneLoader
    {
        public void LoadScene(string sceneName, Action onSceneLoadedCallback = null)
        {
            LoadSceneCoroutine(sceneName, onSceneLoadedCallback);
        }

        private async void LoadSceneCoroutine(string sceneName, Action onSceneLoadedCallback = null)
        {
            if (GetCurrentSceneName() == sceneName)
            {
                onSceneLoadedCallback?.Invoke();
                return;
            }

            var loadSceneOperation = SceneManager.LoadSceneAsync(sceneName);
            
            while (!loadSceneOperation.isDone)
                await UniTask.WaitForEndOfFrame();
            
            onSceneLoadedCallback?.Invoke();
        }

        public string GetCurrentSceneName() => SceneManager.GetActiveScene().name;
    }
}