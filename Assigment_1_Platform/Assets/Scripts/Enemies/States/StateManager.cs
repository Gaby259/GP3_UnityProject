using UnityEngine;

public class StateManager : MonoBehaviour
{
    // States 
       private PatrolState _patrolState;
       private AlertState _alertState;
       private ChaseState _chaseState;
       private AttackState _attackState;
       
       //State Properties 
       public PatrolState PatrolState => _patrolState;
       public AlertState AlertState => _alertState;
       public ChaseState ChaseState => _chaseState;
       public AttackState Attack => _attackState;

       [Header("Behavior on sight")]
       [SerializeField] private bool chaseOnSight = false; //Shooting enemy = false; chaser enemy = true\
       public bool ChaseOnSight => chaseOnSight;

       //Reference to components
       [SerializeField] private PlayerChecker playerChecker;
       public PlayerChecker PlayerChecker => playerChecker;
       [SerializeField] private CharacterMover characterMover;
       public CharacterMover CharacterMover => characterMover;
    
       
       //Keep track of the current state 
       private IState _currentState;
   
       private void Awake()
       {
           //Initialize states and pass the StateManager reference 
           _patrolState = new PatrolState(this);
           _alertState = new AlertState(this);
           _chaseState = new ChaseState(this);
           _attackState = new AttackState(this);
       }
       
       //Change State 
       public void ChangeState(IState newState)
       {
           // if there is an existing state, exit it 
           _currentState?.OnExit(); // ? is null conditional operator
           
           // set the new state 
           _currentState = newState;
           
           //enter the new state 
           _currentState.OnEnter();
       }
   
       void Start()
       {
           ChangeState(PatrolState);
       }
       void Update()
       {
           //if there is a current state, update it 
           _currentState?.UpdateState();
       }
   
       private void FixedUpdate()
       {
           //if there is a current state, fixed update it
           _currentState?.FixedUpdateState();
       }
}
