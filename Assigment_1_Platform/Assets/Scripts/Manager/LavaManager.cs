using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LavaManager : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private BossAcender lava;
    [SerializeField] private LavaPhasesConfig config;
    
    [Header("Events")]
    public UnityEvent OnLavaCycleStarted; // Full lava cycle begins
    public UnityEvent OnLavaCycleEnded;  // Full lava cycle ends
    public UnityEvent<int> OnPhaseStarted;  // Triggered when entering a phase 
    public UnityEvent<int> OnPhaseEnded;   // Triggered when leaving a phase

    private bool _isRunning = false; 
    private Coroutine _routine;
    
    private void OnDestroy()
    {
        if (lava != null)
        {
            lava.OnPlayerCaught.RemoveListener(HandlePlayerCaught);
        }
    }

    [ContextMenu("Start Phases (Editor)")]
    public void StartPhases()
    {
        if (_isRunning) return;
        if (!CanStart()) return;
        
        StopPhases();
        _isRunning = true;
        _routine = StartCoroutine(PhasesRoutine());
       SoundManager.PlaySFX("Lava");

    }

    [ContextMenu("Stop Phases (Editor)")]
    public void StopPhases()
    {
        if (_routine != null)
        {
            StopCoroutine(_routine);
            _routine = null;
        }
        lava.Stop();
        
    }

    private bool CanStart() //Null checks
    {
        if (lava == null)
        {
            Debug.LogError("LavaManager has not been setup yet.");
            return false;
        }
        if (config == null || config.phases == null || config.phases.Length == 0)
        {
            Debug.LogError("LavaConfigure has not been setup yet.");
            return false;
        }
        return true;
    }

    private IEnumerator PhasesRoutine()
    {
        OnLavaCycleStarted?.Invoke();
        lava.Begin();

        do
        {
            for (int i = 0; i < config.phases.Length; i++)
            {
                var phase = config.phases[i]; 

                OnPhaseStarted?.Invoke(i);

                // raise lava 
                lava.SetSpeed(phase.riseSpeed);
                yield return new WaitForSeconds(phase.riseDuration);

                // pause lava 
                lava.SetSpeed(0f);
                OnPhaseEnded?.Invoke(i);

                if (phase.pauseDuration > 0f)
                    yield return new WaitForSeconds(phase.pauseDuration);
            }
        }
        while (config.loopPhases);

        lava.Stop();
        OnLavaCycleEnded?.Invoke();
        _routine = null;
    }

    private void HandlePlayerCaught()
    {
        StopPhases();               
    }
}
