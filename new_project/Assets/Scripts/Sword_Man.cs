using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Sword_Man : MonoBehaviour
{
    public GameObject objSwordMan;
    public int maxHp;
    public int nowHp;
    public int atkDmg;
    public float atkSpeed = 1;
    public bool attacked = false;
    public Image nowHpbar;
    bool inputRight = false;
    bool inputLeft = false;
    float moveSpeed = 2;

    Rigidbody2D rigid2D;
    Animator animator;
    BoxCollider col2D;
    

    void AttackTrue()
    {
        attacked = true;
    }

    void AttackFalse()
    {
        attacked = false;
    }

    void SetAttackSpeed(float speed)
    {
        animator.SetFloat("attackSpeed", speed);
        atkSpeed = speed;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        maxHp = 50;
        nowHp = 50;
        atkDmg = 10;

        col2D = GetComponent<BoxCollider>();
        transform.position = new Vector3(0, 0, 0);
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SetAttackSpeed(1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        nowHpbar.fillAmount = (float)nowHp / (float)maxHp;

        if(Input.GetKey(KeyCode.RightArrow))
        {
            inputRight = true;
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetBool("moving", true);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            inputLeft = true;
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("moving", true);
        }
        else animator.SetBool("moving", false);

        if(Input.GetKeyDown(KeyCode.A) &&
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                animator.SetTrigger("attack");
            }

        RaycastHit2D raycastHit = Physics2D.BoxCast(col2D.bounds.center, col2D.bounds.size, Of, Vector2.down, 0.02f, LayerMask.GetMask("Ground"));
        if (raycastHit.collider != null)
            animator.SetBool("jumping", false);
        else animator.SetBool("jumping", true);
    }

    void FixedUpdate()
    {
        if(inputRight)
        {
            inputRight = false;
            rigid2D.MovePosition(rigid2D.position + Vector2.right * moveSpeed * Time.deltaTime);
        }
        if(inputLeft)
        {
            inputLeft = false;
            rigid2D.MovePosition(rigid2D.position + Vector2.left * moveSpeed * Time.deltaTime);
        }
    }
}
