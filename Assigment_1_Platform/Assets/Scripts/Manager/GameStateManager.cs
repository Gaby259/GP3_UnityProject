using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    private IGameStates _currentState;
    public InputController Input { get; private set; }
    
    [Header("States Menus UI")]
    public GameObject pauseMenuUI;
    public GameObject controlsMenuUI;
    public GameObject confirmPopupUI;

    // States
  //  public MainMenuState MainMenuState { get; private set; }
    public PlayingState PlayingState { get; private set; }
    public PausedState PausedState { get; private set; }
  //  public ControlsMenuState ControlsMenuState { get; private set; }
  public IGameStates CurrentState => _currentState;
  


    private void Awake()
    {
        Instance = this;
        Input = GameObject.FindFirstObjectByType<InputController>();

        // Initialize states
  //      MainMenuState = new MainMenuState(this);
        PlayingState = new PlayingState(this);
        PausedState = new PausedState(this, pauseMenuUI);
  //      ControlsMenuState = new ControlsMenuState(this);
    }
    

    private void Start()
    {
      ChangeState(PlayingState);
      pauseMenuUI.SetActive(false);
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