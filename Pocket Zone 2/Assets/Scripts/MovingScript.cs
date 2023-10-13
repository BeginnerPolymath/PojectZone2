using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingScript : MonoBehaviour
{
    public Rigidbody2D Rigidbody;
    public Vector2 Direction;

    public HealthScript Health;

    [Header("Simple AI")]
    public Transform FollowTarget;
    public float FollowDistance;
    public float DetectionDistance;

    public float Speed = 5;

    public float AttackDistance;

    public bool Aggr;


 
    void Start()
    {
        //Для того, чтобы монстры меньше сливались друг в друге.
        AttackDistance = Random.Range(1, 3);
    }

    void Update()
    {
        FollowUpdate ();
        DirectionMove ();
    }

    public void Moving (Vector2 direction)
    {
        Direction = direction;
        Rigidbody.MovePosition(Rigidbody.position + (Direction * Speed * Time.deltaTime));
    }

    void FollowUpdate ()
    {
        if(!FollowTarget)
            return;

        FollowDistance = Vector2.Distance(transform.position, FollowTarget.transform.position);

        if(FollowDistance < DetectionDistance && !Aggr)
        {
            Aggr = true;
        }
        else if(FollowDistance > DetectionDistance && !Aggr)
        {
            return;
        }

        if(FollowDistance > AttackDistance)   
        {
            Direction = FollowTarget.transform.position - transform.position;

            Vector2 pos = Vector2.Lerp((Vector2)transform.position, (Vector2)transform.position + (Direction * Speed), Time.deltaTime);
            Rigidbody.MovePosition(pos);
        }
    }

    void DirectionMove ()
    {
        if(Direction.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if(Direction.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }


}
