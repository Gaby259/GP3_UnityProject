using TMPro;
using UnityEngine;

public class AnalyticsManager : Singleton<AnalyticsManager>
{
   [SerializeField] private TMP_Text collectablesText;
   private int _collectables;

   public void IncreaseCollectables()
   {
      _collectables += 1; //is the same as ++
      collectablesText.text = _collectables.ToString();
      SoundManager.Play("RareItem");
   }
}
