using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Threading;

public class AsyncSceneLoader
{
	string sceneName;
	LoadSceneMode loadSceneMode;
    AsyncOperation asyncLoad;

    public bool LoadDone { get; private set; }
    private bool allowSceneActivation;

    public AsyncSceneLoader(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
	{
		this.sceneName = sceneName;
		this.loadSceneMode = loadSceneMode;
        LoadDone = false;
        allowSceneActivation = false;
    }

	public IEnumerator Load()
	{
		asyncLoad = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                yield return new WaitUntil(() => allowSceneActivation);
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
        LoadDone = asyncLoad.isDone;
    }

    public void AllowSceneActivation()
    {
        allowSceneActivation = true;
    }
}

