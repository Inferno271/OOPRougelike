using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerBase : CharacterBase, IMovable
{    public GameObject hpFX;
    [Header("Общие Характеристики Героя")]

    public float Speed;
    protected int damage;
    public int minDamage;
    public int maxDamage;
    protected Vector2 direction;
    private Animator animator;
    private Rigidbody2D rb;
    protected float timeBtwUseSpell;
    public float startTimeBtwUseSpell;
    protected Image imageCooldown;
    public Slider sl;
    protected bool isCooldown;

    protected override void Start()
    {
        base.Start(); // Вызов метода Start из CharacterBase
        rb = GetComponent<Rigidbody2D>();
        imageCooldown = GameObject.FindGameObjectWithTag("Cooldown").GetComponent<Image>();
    }

    new void Update()
    {
        TakeInput();
        Move();
        Attack();
        AdditionalActions();
        TestQ();
    }

    protected bool NeedDiagonalAdjustment()
    {
        return (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) ||
               (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) ||
               (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) ||
               (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A));
    }

    public override void TakeInput()
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) direction += Vector2.up * 0.9f;
        if (Input.GetKey(KeyCode.A)) direction += Vector2.left;
        if (Input.GetKey(KeyCode.S)) direction += Vector2.down * 0.9f;
        if (Input.GetKey(KeyCode.D)) direction += Vector2.right;
        if (NeedDiagonalAdjustment()) direction *= 0.8f;
        
    }

    public void Move()
    {
        transform.Translate(direction * Speed * Time.deltaTime);
        if (direction.x != 0 || direction.y != 0)
        {
            AnimationMove(direction);
        }
        else
        {
            anim.SetLayerWeight(1, 0);
        }
    }

    void AnimationMove(Vector2 direction)
    {
        anim.SetLayerWeight(1, 1);
        anim.SetFloat("x", direction.x);
        anim.SetFloat("y", direction.y);
    }

    protected void AddHP()
    {
        if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) && PlayerStats.numberOfBottle > 0 && Time.deltaTime != 0)
        {
            FindObjectOfType<AudioManager>().Play("Use_HP");
            PlayerStats.numberOfBottle--;
            GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<StartGame>().UpdateBottleNumber();
            Vector2 hpPosition = new Vector2(transform.position.x, transform.position.y + 0.25f);
            Destroy(Instantiate(hpFX, hpPosition, Quaternion.identity), 2f);
            PlayerStats.HP += 10;
        }
    }

    public abstract override void Attack();

    protected virtual void AdditionalActions()
    {
        // Дополнительные действия, специфичные для каждого класса
    }

    protected void StartCooldown(float cooldown)
    {
        if (isCooldown)
        {
            imageCooldown.fillAmount += 1 / cooldown * Time.deltaTime;

            if (imageCooldown.fillAmount >= 1)
            {
                imageCooldown.fillAmount = 0;
                isCooldown = false;
            }
        }
    }



    protected void TestQ()
    {
        // Дополнительные тестовые действия
    }
}
