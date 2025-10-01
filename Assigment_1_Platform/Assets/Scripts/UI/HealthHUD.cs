using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private PlayerHealth playerHealth;   // auto-find if null
    [SerializeField] private Transform heartsParent;      // panel con HorizontalLayoutGroup
    [SerializeField] private Image heartPrefab;           // prefab azul de Assets (Image)

    [Header("Sprites")]
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    [Header("Layout")]
    [SerializeField] private bool rightToLeft = true;

    private readonly List<Image> _hearts = new List<Image>();

    private void Awake()
    {
        if (playerHealth == null) playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void OnEnable()
    {
        if (!HasAllRefs()) return;

        RebuildHearts(playerHealth.MaxHealth);

        playerHealth.OnHealthChanged += UpdateHeartsDisplay;
        playerHealth.OnPlayerDeath  += HandlePlayerDeath;

        UpdateHeartsDisplay(playerHealth.CurrentHealth, playerHealth.MaxHealth);
    }

    private void OnDisable()
    {
        if (playerHealth == null) return;
        playerHealth.OnHealthChanged -= UpdateHeartsDisplay;
        playerHealth.OnPlayerDeath  -= HandlePlayerDeath;
    }

    private bool HasAllRefs()
    {
        if (playerHealth == null || heartsParent == null || heartPrefab == null || fullHeart == null || emptyHeart == null)
        {
            Debug.LogWarning("[HealthHUD] Missing refs (PlayerHealth/HeartsParent/HeartPrefab/Sprites).");
            return false;
        }
        return true;
    }

    private void RebuildHearts(int max)
    {
        // 1) Deletes every previous heart it was existing before 
        for (int i = heartsParent.childCount - 1; i >= 0; i--)
            Destroy(heartsParent.GetChild(i).gameObject);

        _hearts.Clear();
        if (max <= 0) return;

        // 2) Creates X number of hearts 
        for (int i = 0; i < max; i++)
        {
            //It creates the heart by instiate and parent the RectTransform 
            var image = Instantiate(heartPrefab);
            image.rectTransform.SetParent(heartsParent, false);

            // Normalize the size of the heart if ots going off set 
            var rectTransform = image.rectTransform;
            rectTransform.anchorMin = rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.localScale = Vector3.one;

            image.sprite = fullHeart;
            image.preserveAspect = true;

            _hearts.Add(image);
        }
    }

    private void UpdateHeartsDisplay(int current, int max)
    {
        if (_hearts.Count != max) RebuildHearts(max);

        for (int i = 0; i < _hearts.Count; i++)
        {
            int logicalIndex = rightToLeft ? (max - 1 - i) : i; // 0 = corazón más a la derecha si rightToLeft
            bool filled = logicalIndex < current;

            var updateImage = _hearts[i];
            if (updateImage != null) updateImage.sprite = filled ? fullHeart : emptyHeart;
        }
    }

    private void HandlePlayerDeath()
    {
        //Can be called by Lose and show an animation or sound effect
    }
}
