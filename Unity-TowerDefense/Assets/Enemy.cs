using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 10f;

    private Transform target;
    private int wavepointIndex = 0;
    private Quaternion rotationT;
    enum fromDirection
    {
        up,
        down,
        left,
        right
    }
    ///<summary>
    /// direction this object is comming from
    ///</summary>
    fromDirection fromDir;

    void Start()
    {
        target = Waypoints.waypoints[0];
        fromDir = fromDirection.right;
        rotationT = transform.rotation;
    }

    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {

            GetNextWaypoint();
            SetTurningRotation();

        }
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationT, Time.deltaTime * 10f);


        /* Quaternion rotation = Quaternion.LookRotation(target.position - transform.position);
         //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);
         //transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, transform.rotation.eulerAngles.z));
         transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 180f);
 */


    }
    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.waypoints.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        wavepointIndex++;
        target = Waypoints.waypoints[wavepointIndex];
    }

    void TurnLeft()
    {
        rotationT *= Quaternion.AngleAxis(90, new Vector3(0, 0, 1));
    }
    void TurnRight()
    {
        rotationT *= Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
    }

    void SetTurningRotation()
    {
        switch (fromDir)
        {
            case fromDirection.up:
                if (target.position.x > transform.position.x)
                {
                    TurnLeft();
                    fromDir = fromDirection.left;
                }
                else if (target.position.x < transform.position.x)
                {
                    TurnRight();
                    fromDir = fromDirection.right;
                }
                break;

            case fromDirection.down:
                if (target.position.x < transform.position.x)
                {
                    TurnLeft();
                    fromDir = fromDirection.right;
                }
                else if (target.position.x > transform.position.x)
                {
                    TurnRight();
                    fromDir = fromDirection.left;
                }
                break;
            case fromDirection.left:
                if (target.position.y > transform.position.y)
                {
                    TurnLeft();
                    fromDir = fromDirection.down;
                }
                else if (target.position.y < transform.position.y)
                {
                    TurnRight();
                    fromDir = fromDirection.up;
                }
                break;
            case fromDirection.right:
                if (target.position.y < transform.position.y)
                {
                    TurnLeft();
                    fromDir = fromDirection.up;
                }
                else if (target.position.y > transform.position.y)
                {
                    TurnRight();
                    fromDir = fromDirection.down;
                }
                break;
        }
        Debug.Log(fromDir);
    }
}