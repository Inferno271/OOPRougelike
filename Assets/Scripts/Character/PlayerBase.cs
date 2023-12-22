using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerBase : MonoBehaviour
{
    public GameObject coinText;
    public GameObject hpFX;
    [Header("Характеристики героя")]
    public float Speed;
    protected int damage;
    public int minDamage;
    public int maxDamage;
    protected Vector2 direction;
	private Animator animator;
    private Rigidbody2D rb;
    private Text txt;
    protected float timeBtwUseSpell;
    public float startTimeBtwUseSpell;
    protected Image imageCooldown;
    protected bool isCooldown;

    void Start() 
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        imageCooldown = GameObject.FindGameObjectWithTag("Cooldown").GetComponent<Image>();
        txt = GameObject.FindGameObjectWithTag("NumberCoins").GetComponent<Text>();  
        Time.timeScale = 1; 
        Boss.bossDeath = false;
        PlayerStats.armor = 1f;
    }

    protected bool NeedDiagonalAdjustment()
    {
        return (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) ||
               (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) ||
               (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) ||
               (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A));
    }

	protected virtual void TakeInput()
	{
        direction = Vector2.zero;
        if (Input.GetKey (KeyCode.W)) direction += Vector2.up*0.9f;
        if (Input.GetKey (KeyCode.A)) direction += Vector2.left;
        if (Input.GetKey (KeyCode.S)) direction += Vector2.down*0.9f;
        if (Input.GetKey (KeyCode.D)) direction += Vector2.right;
        if (NeedDiagonalAdjustment()) direction *= 0.8f;
        AddHP();
    }

    protected void Move()
	{
		transform.Translate(direction * Speed * Time.deltaTime);
        //rb.velocity = direction * Speed * Time.deltaTime;
		if (direction.x != 0 || direction.y != 0) {
			AnimationMove (direction);
		} else {
			animator.SetLayerWeight (1, 0);
		}
	}

	void AnimationMove(Vector2 direction)
	{
		animator.SetLayerWeight (1, 1);
		animator.SetFloat ("x", direction.x);
		animator.SetFloat ("y", direction.y);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
        {
            FindObjectOfType<AudioManager>().Play("Coin");
            int money = other.GetComponent<Coins>().money;
            PlayerStats.coins += money;
            coinText.GetComponent<TextMesh>().text = "+" + money.ToString();
            Instantiate(coinText, other.transform.position, Quaternion.identity);
            txt.text = PlayerStats.coins.ToString();

            Destroy(other.gameObject);            
        }
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

    protected abstract void Attack();

    protected void StartCooldown(float cooldown)
    {
        imageCooldown.fillAmount += 1 / cooldown * Time.deltaTime;

        if (imageCooldown.fillAmount >= 1)
        {
            imageCooldown.fillAmount = 0;
            isCooldown = false;
        }
    }

    protected void TestQ()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            transform.Translate(direction * 3f);
        }
    }
}