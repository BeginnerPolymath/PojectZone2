using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public HealthScript Health;
    public MovingScript Moving;
    public CharacterScript Character;

    public float Damage = 3;
    
    void Update()
    {
        if(Vector2.Distance(Character.transform.position, transform.position) < Moving.AttackDistance && Health.CurrentHealth != 0)
        {
            Character._Health.SetDamage(Damage*Time.deltaTime);
        }
    }
}
