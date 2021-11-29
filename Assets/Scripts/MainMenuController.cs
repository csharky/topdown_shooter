using System.Threading.Tasks;
using EventSystem;
using EventSystem.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public async void LoadLastScene()
    {
        var id = PlayerPrefs.GetInt("_LAST_FINISHED_SCENE", 1);
        
        var asyncOp = SceneManager.LoadSceneAsync(id);
        while (!asyncOp.isDone)
        {
            await Task.Delay(100);
        }
        
        GameEventSystem.current.Fire(new GameStarted());
    }
}