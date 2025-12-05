using UnityEngine;

public class Wall : MonoBehaviour
{
    BoxCollider2D coll;
    Rigidbody2D rigid;

    public float speed = 5f;
    public bool isEng;

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        int ranY = Random.Range(-7, -2);    //작은거부터 큰거
        
        if(isEng)
            transform.position = new Vector3(10, 0.57f, 0);
        else
            transform.position = new Vector3(10, ranY, 0);     //벡터 선언할땐 항상 new vector
    }
    void Start()
    {
        
    }

   

    private void FixedUpdate()
    {
/*        Vector2 newPos = rigid.position + Vector2.left * speed * Time.fixedDeltaTime;
        rigid.MovePosition(newPos);*/

        rigid.linearVelocity = new Vector2(-speed, rigid.linearVelocity.y);

        if (transform.position.x < -9) {
        gameObject.SetActive(false);
        }
        

    }

    public void goUp() {
    if (!isEng) 
            return;

    rigid.AddForce(Vector3.up * 10,ForceMode2D.Impulse);
    }

    public void delete()
    {
        if(isEng)
        gameObject.SetActive(false);
    }
}
