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
