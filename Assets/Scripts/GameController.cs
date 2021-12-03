using System;
using System.Collections;
using System.Threading.Tasks;
using EventSystem;
using EventSystem.EventListeners;
using EventSystem.Events;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour, IEventListener<GameOver>, IGameStartListener
{
    public int FrameRate;

    [SerializeField] private CanvasGroup _gameoverOverlay;
    [SerializeField] private VhsEffectController _vhsEffect;

    private GameplayActions m_Actions;
    private bool m_gameOver;
    private void Awake()
    {
        if (FrameRate <= 0) return;
        Application.targetFrameRate = FrameRate;
    }

    private void Start()
    {
        GameEventSystem.current.Register<GameStarted>(this);
        GameEventSystem.current.Register<GameOver>(this);
    }

    public async void RestartGame()
    {
        if (!m_gameOver) return;
        
        _vhsEffect.SetValue(0f);
        m_gameOver = false; 
        var asyncOp = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        while (!asyncOp.isDone) await Task.Delay(100);
        
        GameEventSystem.current.Fire(new GameStarted());
    }

    public void Invoke(GameOver eventData)
    {
        _gameoverOverlay.gameObject.SetActive(true);
        _gameoverOverlay.alpha = 1f;
        m_gameOver = true;
        
        _vhsEffect.SetValue(1f);
    }

    public void Invoke(GameStarted eventData)
    {
        if (m_Actions == null)
        {
            m_Actions = new GameplayActions();
        }

        m_Actions.Enable();
        m_Actions.InlevelActions.Enable();
        m_Actions.InlevelActions.RestartReload.Enable();
        
        m_Actions.InlevelActions.RestartReload.performed += (context) =>
        {
            RestartGame();
        };
        
        _vhsEffect.SetValue(0f);
        _vhsEffect.SetNoisePartsScale(75f);
    }

    private void OnDestroy()
    {
        Dispose();
    }

    public void Dispose()
    {
        GameEventSystem.current.Unregister<GameOver>(this);
        GameEventSystem.current.Unregister<GameStarted>(this);

        m_Actions?.Dispose();
    }
}