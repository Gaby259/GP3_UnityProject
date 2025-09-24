using UnityEngine;

[CreateAssetMenu(fileName = "LavaPhasesConfig", menuName = "Scriptable Objects/LavaPhasesConfig")]
public class LavaPhasesConfig : ScriptableObject
{
    [System.Serializable]
    public class LavaPhase // variables for just one lava phase 
    {
        [Header("Speed")] public float riseSpeed = 5;

        [Header("Time Acceleration")] public float riseDuration = .1f;

        [Header("Pausing Time")] public float pauseDuration = .1f;
    }

    public LavaPhase[] phases;
    public bool loopPhases = true;
}
