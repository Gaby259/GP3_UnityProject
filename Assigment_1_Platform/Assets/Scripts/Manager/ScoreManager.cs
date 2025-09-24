using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private TMP_Text scoreText;
    private int _score;

    public void IncreaseScore(int amount) //Event to subscribe
    {
        _score += amount;
        scoreText.text = _score.ToString();
    }
}
