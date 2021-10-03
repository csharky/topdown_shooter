using System.Threading.Tasks;
using EventSystem;
using EventSystem.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private VhsEffectController _vhsEffect;
    [SerializeField] private Animator _animator;
    public void Awake()
    {
        _vhsEffect.SetValue(0.0f);
        _vhsEffect.SetNoisePartsScale(150f);
    }
    
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