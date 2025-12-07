using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Progress (rare items)")]
    [SerializeField] private int targetRareItems = 3;
    private int _rareItems = 0;

    [Header("Events")]
    public UnityEvent<int,int> OnRareItemsChanged; // Number of rare items required to win
    public UnityEvent OnShouldStartLava;           //Notify the LavaManager
    public UnityEvent OnWin;
    public UnityEvent OnLose;
    
    private void Start()
    {
        PlayerHealth player = FindFirstObjectByType<PlayerHealth>();

        if (player != null)
        {
            Debug.Log("Player found");
            player.OnPlayerDeath += HandlePlayerDeath;
        }
    }
    

    public void AddRareItem()
    {
        _rareItems++;
        OnRareItemsChanged?.Invoke(_rareItems, targetRareItems);

        if (_rareItems >= targetRareItems)
        {
            Win();
        }
           
    }

    private void Win()
    {
        StartCoroutine(WinTransition());
    }

    private IEnumerator WinTransition()
    {
        OnWin?.Invoke();
        yield return new WaitForSeconds(1f);
        FadeController fade = FadeController.Instance;
        if (fade != null)
        {
            yield return fade.StartCoroutine(fade.FadeIn(fade.defaultDuration));
        }
        SceneManager.LoadScene("VictoryScene");
    }

    private void HandlePlayerDeath()
    {
        Lose();
    }
    public void Lose()
    {
       StartCoroutine(LoseTransition());
    }

    private IEnumerator LoseTransition()
    {
        OnLose?.Invoke();
        yield return new WaitForSeconds(1f);
        FadeController fade = FadeController.Instance;
        if (fade != null)
        {
            yield return fade.StartCoroutine(fade.FadeIn(fade.defaultDuration));
        }
        SceneManager.LoadScene("LoseScene");
    }
    
}
/*
General item collection flow:
1. Player picks up a collectable item → CollectableItem triggers OnItemCollected.
2. That event calls GameManager.AddRareItem().
3. GameManager increases _rareItems and fires OnRareItemsChanged(int).
4. The HUD listens to this event and updates the progress display.
*/

