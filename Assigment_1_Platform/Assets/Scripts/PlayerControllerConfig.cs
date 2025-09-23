using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerControllerConfig", menuName = "Scriptable Objects/PlayerControllerConfig")]
public class PlayerControllerConfig : ScriptableObject
{
    [Header("Movement")] //good usage of headers; makes it a lot clearer
    [field:SerializeField]public float MovementSpeed { get; private set; }= 6f; //better practice for variables
    public float groundAcceleration = 10f; //max velocity when a player is on the floor 
    public float groundDeceleration = 10f;
    public float accelerationMultiplier = 2f;
   
    [Header("Jump")]
    public float jumpHeight = 10f;   
    public float gravity = 9.81f;    
    public int maxAirJumps = 1;    

    [Header("Dash")]
    public float dashSpeed = 18f;      
    public float dashDuration = 0.18f;  
    public float dashCooldown = 0.6f;   
    public bool allowAirDash = true;   
    [field:SerializeField]public float AirAcceleration { get; private set; } = 5f; //max velocity when a player is in the air 

    [Header("Interact")] 
    public float interactDistance = 1000f;
    

}