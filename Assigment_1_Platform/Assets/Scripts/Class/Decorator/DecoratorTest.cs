using UnityEngine;

public class DecoratorTest : MonoBehaviour
{
    private IIAttack _myAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _myAttack = new BasicAttack();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _myAttack.Execute();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Added fire decorator");
            _myAttack = new FireAttackDecorator(_myAttack);
        }
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Added Poison decorator");
            _myAttack = new PoisonAttackDecorator(_myAttack);
        }
    }
    
    
}
