using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
   //Event declaration using Action delegates
   [SerializeField] private int maxHealth;
   public event System.Action<int, int> OnHealthChanged; //current health & max health
   public event System.Action OnPlayerDeath;
   
   private int _currentHealth;
   
   public int CurrentHealth => _currentHealth; // => means a property / if other scripts wants to know, will reference this variable
   public int MaxHealth => maxHealth;
   public bool isInvulnerable = false;
   public void SetInvulnerable(bool value) => isInvulnerable = value;

   private void Awake()
   {
      _currentHealth = maxHealth;               
   }

   private void Start()
   {
      OnHealthChanged?.Invoke(_currentHealth, maxHealth); // notifies the UI
   }
   public void TakeDamage(int damage)
   {
      if (isInvulnerable) return;
      _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth); //Mathf.Max Dont go over
      Debug.Log("Take damage= " + damage +"current Health = " +  _currentHealth);
      //Notify all observer of health damage
      //? can be not --> null check
      OnHealthChanged?.Invoke(_currentHealth, maxHealth); // Invoke = notifies the other method that calls this event... then something happens

      if (_currentHealth <= 0)
      {
         OnPlayerDeath?.Invoke();
      }
   }
   
   public void Heal(int amount)
   {
      if (amount <= 0 || _currentHealth <= 0) return;

      _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
      OnHealthChanged?.Invoke(_currentHealth, maxHealth);
      Debug.Log($"Healed {amount}");
   }
   

   // Optional: change max health late
   public void SetMaxHealth(int newMax, bool fillToMax = true)
   {
      maxHealth = Mathf.Max(1, newMax);
      if (fillToMax) _currentHealth = maxHealth;
      _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
      OnHealthChanged?.Invoke(_currentHealth, maxHealth);
      Debug.Log($"SetMaxHealth {maxHealth}");
   }
}


