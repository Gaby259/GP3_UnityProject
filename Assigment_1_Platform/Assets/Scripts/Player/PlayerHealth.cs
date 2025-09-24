using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
   //Event declaration using Action delegates 
   public event System.Action<int, int> OnHealthChanged; //current health & max health
   public event System.Action OnPlayerDeath;
   
   private int _currentHealth;
  [SerializeField] private int maxHealth;
   
   public int CurrentHealth => _currentHealth; // => means a property / if other scripts wants to know, will reference this variable
   public int MaxHealth => maxHealth;

   private void Awake()
   {
      _currentHealth = maxHealth;                 // ya arranca lleno ANTES que la UI
   }

   private void Start()
   {
      OnHealthChanged?.Invoke(_currentHealth, maxHealth); // notifica a la UI
   }
   public void TakeDamage(int damage)
   {
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
   //when energy bar is created call this function if the player wants to heal back 
   public void Heal(int amount)
   {
      if (amount <= 0 || _currentHealth <= 0) return;

      _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
      OnHealthChanged?.Invoke(_currentHealth, maxHealth);
      Debug.Log($"Healed {amount}");
   }

   // Optional: change max health later (kept simple)
   public void SetMaxHealth(int newMax, bool fillToMax = true)
   {
      maxHealth = Mathf.Max(1, newMax);
      if (fillToMax) _currentHealth = maxHealth;
      _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
      OnHealthChanged?.Invoke(_currentHealth, maxHealth);
      Debug.Log($"SetMaxHealth {maxHealth}");
   }
}


