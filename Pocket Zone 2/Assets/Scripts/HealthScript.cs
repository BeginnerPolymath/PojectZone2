using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthScript : MonoBehaviour
{
    public List<SpriteRenderer> Sprites = new List<SpriteRenderer>();

    public float CurrentHealth = 100;
    public float MaxHealth = 100;

    public float Height = 250;

    public WorldScript World;

    public HealthUIScript HealthUI;

    void FixedUpdate()
    {
        HealthUIPositionUpdate ();
    }

    void Update()
    {
        
        DeathProcess ();
    }

    public void HealthUIPositionUpdate ()
    {
        Vector2 pos = World.Camera.WorldToScreenPoint(transform.position);
        HealthUI.RectTransform.anchoredPosition = pos + new Vector2(0, Height);

        HealthUI.Fill.fillAmount = CurrentHealth/100;
    }

    public void SetDamage (float count)
    {
        CurrentHealth -= count;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        DeathEvent ();
    }

    public void SetHealth (float count)
    {
        CurrentHealth = count;
    }

    void DeathEvent ()
    {
        if(CurrentHealth == 0)
        {
            foreach (var sprite in Sprites)
            {
                sprite.color = Color.red;
            }
        }
    }

    void DeathProcess ()
    {
        if(CurrentHealth == 0)
        {
            transform.localScale -= new Vector3(0, 3 * Time.deltaTime, 0);
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Clamp(transform.localScale.y, 0, 10), transform.localScale.z);

            if(gameObject.tag == "Player" && transform.localScale.y == 0)
            {
                return;
            }

            if(transform.localScale.y <= 0)
            {
                ItemScript item = Instantiate(World.ItemPrefabs[0], transform.position, Quaternion.identity).GetComponent<ItemScript>();

                item.Count = 20;

                Destroy(HealthUI.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
