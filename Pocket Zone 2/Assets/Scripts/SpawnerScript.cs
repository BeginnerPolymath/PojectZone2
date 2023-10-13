using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [Header("Count Spawn")]
    public int MaxCount = 3;
    public int CurrentCount;

    [Header("Spawn Settings")]
    public float TimePeriodicity = 2;
    float TimeSpawn;
    public float SpawnRadius = 10;


    [Header("Prefabs")]
    public GameObject MonsterPrefab;
    public GameObject HealthUIPrefab;

    [Header("Parents")]
    public Transform MonstersParent;

    public Transform HealthUIParent;

    [Header("Other")]
    public WorldScript World;


    void Update()
    {
        SpawnMonster ();
    }

    void SpawnMonster ()
    {
        if(CurrentCount == MaxCount)
            return;

        TimeSpawn += Time.deltaTime;

        if(TimeSpawn >= TimePeriodicity)
        {
            TimeSpawn = 0;
            CurrentCount += 1;

            SpawnSphereOnEdgeRandomly2D();
        }
        
    }

    private void SpawnSphereOnEdgeRandomly2D()
    {
        float radius = SpawnRadius;
        Vector3 randomPos = Random.insideUnitSphere * radius;
        randomPos += transform.position;
        randomPos.y = 0f;

        Vector3 direction = randomPos - transform.position;
        direction.Normalize();

        float dotProduct = Vector3.Dot(transform.forward, direction);
        float dotProductAngle = Mathf.Acos(dotProduct / transform.forward.magnitude * direction.magnitude);

        randomPos.x = Mathf.Cos(dotProductAngle) * radius + transform.position.x;
        randomPos.y = Mathf.Sin(dotProductAngle * (Random.value > 0.5f ? 1f : -1f)) * radius + transform.position.y;
        randomPos.z = transform.position.z;

        MonsterScript monster = Instantiate(MonsterPrefab, MonstersParent).GetComponent<MonsterScript>();
        monster.transform.position = new Vector2(-14, 0);
        monster.Moving.FollowTarget = World.Character.transform;
        monster.Character = World.Character;
        monster.Health.World = World;

        World.Monsters.Add(new MonsterInfo(monster));
        

        HealthUIScript healthUI = Instantiate(HealthUIPrefab, HealthUIParent).GetComponent<HealthUIScript>();
        healthUI.RectTransform.anchoredPosition = new Vector2(5000, 0);
        monster.Health.HealthUI = healthUI;

        monster.transform.position = randomPos;

        print(randomPos);
    }
}
