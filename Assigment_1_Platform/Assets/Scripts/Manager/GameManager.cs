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
        StartCoroutine(WinRoutine());
    }

    private IEnumerator WinRoutine()
    {
        yield return new WaitForSeconds(2f);
        FadeController fade = FindObjectOfType<FadeController>();
        fade.FadeIn(1f);
        SceneManager.LoadScene("VictoryScreen");
    }

    private void HandlePlayerDeath()
    {
        Lose();
    }
    public void Lose()
    {
        OnLose?.Invoke();
        SceneManager.LoadScene("LoseScene");
        //UI for loosing
    }
    
}
/*
General item collection flow:
1. Player picks up a collectable item → CollectableItem triggers OnItemCollected.
2. That event calls GameManager.AddRareItem().
3. GameManager increases _rareItems and fires OnRareItemsChanged(int).
4. The HUD listens to this event and updates the progress display.
*/

