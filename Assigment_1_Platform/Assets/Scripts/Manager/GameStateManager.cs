using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{
    public static GameStateManager Instance;
    private IGameStates _currentState;
    public InputController Input { get; private set; }
    
    [Header("States Menus UI")]
    public GameObject pauseMenuUI;
    public GameObject controlsMenuUI;
    public GameObject confirmPopupUI;

    // States
    public ConfirmReturnState ConfirmReturnState { get; private set; }
    public PlayingState PlayingState { get; private set; }
    public PausedState PausedState { get; private set; }
    public ControlsMenuState ControlsMenuState { get; private set; }
  public IGameStates CurrentState => _currentState;
  


    private void Awake()
    {
        Instance = this;
        Input = GameObject.FindFirstObjectByType<InputController>();

        // Initialize states
        ConfirmReturnState= new ConfirmReturnState(this, confirmPopupUI);
        PlayingState = new PlayingState(this);
        PausedState = new PausedState(this, pauseMenuUI);
        ControlsMenuState = new ControlsMenuState(this,  controlsMenuUI);
    }
    

    private void Start()
    {
      ChangeState(PlayingState);
      pauseMenuUI.SetActive(false);
      confirmPopupUI.SetActive(false);
      SoundManager.PlayMusic("Gameplay");
    }

    private void Update()
    {
        _currentState?.Update();
    }

    public void ChangeState(IGameStates newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
        Debug.Log("STATE: " + newState.GetType().Name);
    }
}