using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using System;
using UnityEngine.UI;



public class CharacterScript : MonoBehaviour
{
    [Header("Health")]
    public HealthScript _Health;
    public HealthUIScript HealthUI;

    [Header("Controls")]

    public Joystick Joystick;
    public MovingScript _Moving;

    [Header("Inventory")]
    public List<SlotScript> Slots = new List<SlotScript>();

    [Header("Other")]
    public WorldScript World;

    [Header("Attack")]
    public MonsterScript MonsterTarget;

    public float DistanceAttack = 3;



    void Update()
    {
        _Moving.Moving(Joystick.Direction);
        MonsterDetection ();
        CameraFollow ();

        if(_Health.CurrentHealth == 0 && transform.localScale.y == 0)
        {
            World.GameOverPanel.SetActive(true);
        }
    }


    void CameraFollow ()
    {
        World.Camera.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, -10);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Item")
        {
            ItemScript item = other.gameObject.GetComponent<ItemScript>();
            PickUpItem(item);
        }
    }

    void MonsterDetection ()
    {
        for (int i = 0; i < World.Monsters.Count; i++)
        {
            if(!World.Monsters[i].Monster)
            {
                World.Monsters.RemoveAt(i);
                i -= 1;
            }
        }

        foreach (var monster in World.Monsters)
        {
            if(!monster.Monster)
                return;

            monster.Distance = Vector2.Distance(transform.position, monster.Monster.transform.position);

            if(monster.Distance <= DistanceAttack)
            {
                World.ButtonAttack.interactable = true;
                MonsterTarget = monster.Monster;
                return;
            }
            else
            {
                World.ButtonAttack.interactable = false;
                MonsterTarget = null;
            }
        }
    }

    public void Attack ()
    {
        foreach (var slot in Slots)
        {
            if(slot.Item && slot.Item.Name == "5.45x39")
            {
                slot.AddCount(-1);

                if(slot.Count == 0)
                {
                    slot.ClearSlot();
                }
                
                MonsterTarget.Health.SetDamage(10);
                return;
            }
        }

    }

    void PickUpItem (ItemScript item)
    {
        print("Pick up");

        foreach (var slot in Slots)
        {
            if(slot.Item != null && slot.Item.Name == item.Item.Name && slot.Count != item.Item.MaxStack)
            {
                //Высчитываем остаток
                int count = item.Item.MaxStack - slot.Count - item.Count;

                //Если число остатка положительное или равняется нулю, значит всё уместилось и как-такового остатка нету
                if(count >= 0)
                {
                    slot.SetCount(item.Item.MaxStack - count);

                    Destroy(item.gameObject);
                    break;
                }
                //Если число остатка отрицательное, то назначаем слот до максимального числа, а у самого предмета на земле отнимаем данный остаток
                //В том чилсе, даже если слотов не хватит, у нас просто останется предмет на змеле с правильным остатком
                else
                {
                    slot.SetCount(item.Item.MaxStack);

                    item.Count = count * -1;
                }

            }
            else if(slot.Count == 0)
            {
                slot.ImgItem.sprite = item.Item.Image;
                slot.ImgItem.gameObject.SetActive(true);
                //slot.ImgItem.SetNativeSize();
                slot.SetCount(item.Count);
                slot.Item = item.Item;

                if(item.Item.MaxStack > 1)
                {
                    slot.TextCount.gameObject.SetActive(true);
                    slot.TextCount.text = slot.Count.ToString();
                }

                Destroy(item.gameObject);
                break;
            }
        }
    }



    

}
