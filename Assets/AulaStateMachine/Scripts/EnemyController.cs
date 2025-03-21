using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState
    {
        Patrol,
        Attack,
        Search
    }
    public EnemyState currentState = EnemyState.Patrol;

    public Transform[] waypoints;
    public float speed = 2f;
    public float stopTime = 1f;
    public float detectionRange = 5f;
    public float searchDuration = 3f;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;
    public int rayCount = 5;
    public float coneAngle = 45f;

    private Rigidbody2D rb;
    private int currentWaypointIndex = 0;
    private bool isWaiting = false;
    private Transform player;
    private Vector2 lastSeenPosition;
    private float searchTimer = 0f;
    private Vector2 lastMovementDirection = Vector2.right;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(waypoints.Length > 0 )
        {
            transform.position = waypoints[0].position;
        }
    }

    private void FixedUpdate()
    {
        DetectPlayer();

        switch(currentState)
        {
            case EnemyState.Patrol:

                if(!isWaiting && waypoints.Length > 0)
                {
                    MoveToWaypoint();
                }
                break;

            case EnemyState.Attack:
                if(player != null)
                {
                    MoveToPlayer();
                }
                break;

            case EnemyState.Search:
                SearchForPlayer();
                break;
        }
    }

    void MoveToWaypoint()
    {
        Vector2 targetPosition = waypoints[currentWaypointIndex].position;
        Vector2 direction = (targetPosition - rb.position).normalized;

        lastMovementDirection = direction;
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        if(Vector2.Distance(rb.position, targetPosition) < 0.1f)
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(stopTime);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        isWaiting = false;
    }

    void DetectPlayer()
    {
        bool playerDetected = false;
        float angleStep = coneAngle / (rayCount - 1);
        float startAngle = -coneAngle / 2;

        for(int i = 0; i < rayCount; i++)
        {
            float angle = startAngle + (angleStep * i);
            Vector2 rayDirection = Quaternion.Euler(0, 0, angle) * lastMovementDirection;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, detectionRange, playerLayer | obstacleLayer);
            Debug.DrawRay(transform.position, rayDirection * detectionRange, Color.red);

            if(hit.collider != null)
            {
                if(hit.collider.CompareTag("Player"))
                {
                    Debug.Log("hit: " + hit.collider.name);
                    player = hit.collider.transform;
                    lastSeenPosition = player.position;
                    currentState = EnemyState.Attack;
                    searchTimer = 0f;
                    playerDetected = true;
                    break;
                }
                else if(((1 << hit.collider.gameObject.layer) & obstacleLayer) != 0)
                {
                    continue;
                }
            }
        }

        if(!playerDetected && currentState == EnemyState.Attack)
        {
            player = null;
            currentState = EnemyState.Search;
            searchTimer = searchDuration;
        }
    }

    void MoveToPlayer()
    {
        if (player == null) return;
        Vector2 targetPosition = player.position;
        Vector2 direction = (targetPosition - rb.position).normalized;
        lastMovementDirection = direction;
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }

    void SearchForPlayer()
    {
        Vector2 newPosition = Vector2.MoveTowards(rb.position, lastSeenPosition, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        if(Vector2.Distance(rb.position, lastSeenPosition) < 0.1f)
        {
            searchTimer -= Time.fixedDeltaTime;
            if(searchTimer <= 0)
            {
                currentState = EnemyState.Patrol;
            }
        }
    }
}
