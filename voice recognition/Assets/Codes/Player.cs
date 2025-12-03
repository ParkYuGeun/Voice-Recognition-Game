using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool islive;
    public int pp;
    public float minHeight;

    Rigidbody2D rigid;

    public Mic2 mic;
    public GameObject fire;

    bool isCooldown = false;
    WaitForSeconds wait = new WaitForSeconds(0.5f);

     void Awake()
    {
        islive = true;
        rigid = GetComponent<Rigidbody2D>();
    }

     void OnEnable()
    {
        
        rigid = GetComponent<Rigidbody2D>();
        minHeight = transform.position.y;

    }

    void Update()
    {
        if (!islive)
            return;
        if (isCooldown) {
            pp = 0;
            return;
        }
/*        if(transform.position.y < minHeight)
            isCooldown = true;*/
        pp = mic.resultValue;
        //불 뿜기
        if (transform.position.y < minHeight)
            fire.SetActive(false);
    }

     void FixedUpdate()
    {
        if (pp > 40)
        {
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.x, 0);
            rigid.AddForce(Vector2.up * pp/8, ForceMode2D.Impulse);
            pp = 0;
            //불뿜기
            fire.SetActive(true);
            //쿨타임
            StartCoroutine(cooldown());
        }

        if (transform.position.y < minHeight)
        {
           
            transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);

            if (rigid.linearVelocity.y < 0)
            {
                rigid.linearVelocity = new Vector2(rigid.linearVelocity.x, 0);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (!coll.CompareTag("wall"))
            return;
        GameManager.instance.dead();
    }

    public void CorrectJump() {
        Vector2 direction = (Vector2.up).normalized;
        rigid.AddForce(direction * 17, ForceMode2D.Impulse);
    }
    

    IEnumerator cooldown()
    {
        isCooldown = true;
        yield return wait;
        
        isCooldown = false;
    }

}
