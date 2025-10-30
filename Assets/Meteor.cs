using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private float fallSpeedMin_ = 1;
    [SerializeField] private float fallSpeedMax_ = 3;

    private Explosion explosionPrefab_;
    private BoxCollider2D groundCollider_;
    private Rigidbody2D rb_;
    private GameManager gameManager_;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb_ = GetComponent<Rigidbody2D>();
        SetupVlocity();
    }
    public void Setup(BoxCollider2D ground, GameManager gameManager, Explosion explosionPrefab) {
        gameManager_ = gameManager;
        groundCollider_ = ground;
        explosionPrefab_ = explosionPrefab;
    }

    private void SetupVlocity(){
        float left = groundCollider_.bounds.center.x - groundCollider_.bounds.size.x / 2;
        float right = groundCollider_.bounds.center.x + groundCollider_.bounds.size.x / 2;
        float top = groundCollider_.bounds.center.y + groundCollider_.bounds.size.y / 2;
        float bottom = groundCollider_.bounds.center.y - groundCollider_.bounds.size.y / 2;


        float targetX = Mathf.Lerp(left, right, Random.Range(0.0f, 1.0f));

        Vector3 target = new Vector3(targetX, top, 0);
        Vector3 direction = (target - transform.position).normalized;
        float fallSpeed = Random.Range(fallSpeedMin_, fallSpeedMax_);
        rb_.linearVelocity = direction * fallSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Explosion"))
        { Explosion(); }
        if (collision.gameObject.CompareTag("Ground"))
        { Fall(); }
    }

    private void Explosion() {
        gameManager_.AddScore(100);
        Instantiate(explosionPrefab_, transform.position,
          Quaternion.identity);
        Destroy(gameObject);
    }

    private void Fall() {
        gameManager_.Damage(1);
        Destroy(gameObject);
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
