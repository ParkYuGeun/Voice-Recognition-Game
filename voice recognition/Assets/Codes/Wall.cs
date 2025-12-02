using UnityEngine;

public class Wall : MonoBehaviour
{
    BoxCollider2D coll;
    Rigidbody2D rigid;

    public float speed = 5f;

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        int ranY = Random.Range(-7, -2);    //작은거부터 큰거
        transform.position = new Vector3(transform.position.x, ranY, transform.position.z);     //벡터 선언할땐 항상 new vector
    }
    void Start()
    {
        
    }

   

    private void FixedUpdate()
    {
        Vector2 newPos = rigid.position + Vector2.left * speed * Time.fixedDeltaTime;
        rigid.MovePosition(newPos);
    }
}
