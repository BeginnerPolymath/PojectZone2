using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


[Serializable]
public class MonsterInfo
{
    public MonsterScript Monster;
    public float Distance;

    public MonsterInfo (MonsterScript monster)
    {
        Monster = monster;
    }
}

public class WorldScript : MonoBehaviour
{
    public int MaxFrameRate;

    public List<GameObject> ItemPrefabs = new List<GameObject>();

    public CharacterScript Character;

    public List<MonsterInfo> Monsters = new List<MonsterInfo>();

    public GameObject GameOverPanel;

    public Camera Camera;

    public Button ButtonAttack;


    

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = MaxFrameRate;
    }


    public void Restart ()
    {
        Character._Health.CurrentHealth = 100;
        Character.transform.localScale = Vector3.one;
        Character.transform.position = Vector2.zero;

        foreach (var sprite in Character._Health.Sprites)
        {
            sprite.color = Color.white;
        }

        foreach (var monster in Monsters)
        {
            monster.Monster.Moving.Aggr = false;
        }

        foreach (var slot in Character.Slots)
        {
            slot.ClearSlot();
        }

        GameOverPanel.SetActive(false);
    }

    public static void SwitchCanvasGroup (CanvasGroup canvasGroup)
    {
        if(canvasGroup.blocksRaycasts)
        {
            canvasGroup.alpha = 0;
        }
        else
        {
            canvasGroup.alpha = 1;
        }

        canvasGroup.blocksRaycasts = !canvasGroup.blocksRaycasts;
        canvasGroup.interactable = !canvasGroup.interactable;
    }
}
