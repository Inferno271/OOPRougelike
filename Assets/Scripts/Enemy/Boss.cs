using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBase
{
	public GameObject projectile;
	public float radius;
    public float projectileSpeed;
	private int numberOfProjectiles;
	public static bool bossDeath = false;

	protected override void Start() 
	{
		base.Start();
		Attack();
	}

    void Update() 
    {
        Following();
        CheckDeath();
    }

	IEnumerator SpawnProjectiles()
	{
		while(true)
		{
			numberOfProjectiles = Random.Range(5, 12);
			float angleStep = 360f / numberOfProjectiles;
			float angle = 0f;

			for (int i = 0; i <= numberOfProjectiles - 1; i++) 
			{
				float projectileDirXposition = transform.position.x + Mathf.Sin ((angle * Mathf.PI) / 180) * radius;
				float projectileDirYposition = transform.position.y + Mathf.Cos ((angle * Mathf.PI) / 180) * radius;

				Vector2 projectileVector = new Vector2 (projectileDirXposition, projectileDirYposition);
				Vector2 tp = new Vector2 (transform.position.x, transform.position.y);
				Vector2 projectileMoveDirection = (projectileVector - tp).normalized * projectileSpeed;

				var proj = Instantiate (projectile, transform.position, Quaternion.identity);
				projectile.GetComponent<ProjectileBoss>().damage = damage * PlayerStats.armor;
				proj.GetComponent<Rigidbody2D> ().velocity = new Vector2 (projectileMoveDirection.x, projectileMoveDirection.y);

				angle += angleStep;
			}

			yield return new WaitForSeconds(3f);
		}
	}

    protected override void Attack()
    {
        StartCoroutine(SpawnProjectiles ());
		//другие корутины
    }

    protected override void Following()
    {
        //движение босса
    }

	private void OnDestroy() 
	{
		bossDeath = true;
	}
}