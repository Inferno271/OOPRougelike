using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterBase : MonoBehaviour, IDamageable
{
    [Header("Основные характеристики")]
    public float HP;
    public float Speed;
    public int minDamage;
    public int maxDamage;
    protected Vector2 direction;
    protected Animator anim;
    private Shake shake;
    protected AudioSource audioSource;
    public Slider HPBar;
    public GameObject hpEffect;

    // Инициализация базовых компонентов и настроек
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        shake = GameObject.FindGameObjectWithTag("ShakeManager").GetComponent<Shake>();
        audioSource = GetComponent<AudioSource>();
        if (HPBar != null)
        {
            HPBar.maxValue = HP;
            HPBar.value = HP;
        }
    }

    // Обновление каждый кадр
    protected virtual void Update()
    {
        TakeInput();
        Move();
        Attack();
    }

    // Обработка ввода (переопределена в подклассах)
    public abstract void TakeInput();

    // Движение персонажа
    protected void Move()
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

    // Анимация движения
    void AnimationMove(Vector2 dir)
    {
        anim.SetLayerWeight(1, 1);
        anim.SetFloat("x", dir.x);
        anim.SetFloat("y", dir.y);
    }

    // Получение урона (реализация может варьироваться в подклассах)
    public virtual void TakeDamage(int damage)
    {
        HP -= damage;
        if (HPBar != null)
        {
            HPBar.value = HP;
        }
        ShowHPEffect();
        CheckDeath();
    }

    // Проверка на смерть персонажа
    public virtual void CheckDeath()
    {
        if (HP <= 0)
        {
            OnDeath();
        }
    }

    // Логика смерти персонажа
    protected virtual void OnDeath()
    {
        if (audioSource != null)
        {
            audioSource.Play(); // Звук смерти
        }
        if (shake != null)
        {
            shake.CamShake();
        }
        Destroy(gameObject);
    }

    // Отображение эффекта получения урона
    protected void ShowHPEffect()
    {
        if (hpEffect != null)
        {
            Vector2 hpPosition = new Vector2(transform.position.x, transform.position.y + 0.25f);
            Destroy(Instantiate(hpEffect, hpPosition, Quaternion.identity), 2f);
        }
    }

    // Атака (реализация в подклассах)
    public abstract void Attack();
}
