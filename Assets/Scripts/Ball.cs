using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float moveSpeed;
    private TeamData teamData;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public static Ball Create(GameObject ballPrefab, Vector2 position, float speed, TeamData teamData)
    {
        GameObject ballObj = Instantiate(ballPrefab); // Убедись, что префаб называется "Ball" и находится в папке Resources
        ballObj.transform.position = position;

        Ball ballScript = ballObj.GetComponent<Ball>();

        ballScript.Setup(teamData);
        ballScript.StartMoving(speed);

        return ballScript;
    }

    public void Setup(TeamData teamData)
    {
        this.teamData = teamData;

        gameObject.layer = teamData.layer;

        spriteRenderer.material = teamData.material;
    }
    public void StartMoving(float speed)
    {
        moveSpeed = speed;

        moveDirection = Random.value < 0.5f ? Vector2.one.normalized : new Vector2(1, -1).normalized;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];

        Vector2 normal = contact.normal;

        Vector2 reflected = Vector2.Reflect(moveDirection, normal).normalized;

        moveDirection = SnapToDiagonal(reflected);
    }

    private Vector2 SnapToDiagonal(Vector2 direction)
    {
        // Diagonals: (1,1), (1,-1), (-1,1), (-1,-1)
        Vector2[] diagonals = {
            new Vector2(1, 1).normalized,
            new Vector2(1, -1).normalized,
            new Vector2(-1, 1).normalized,
            new Vector2(-1, -1).normalized
        };

        float bestDot = -1f;
        Vector2 closest = diagonals[0];

        foreach (var diag in diagonals)
        {
            float dot = Vector2.Dot(direction, diag);
            if (dot > bestDot)
            {
                bestDot = dot;
                closest = diag;
            }
        }

        return closest;
    }
}

