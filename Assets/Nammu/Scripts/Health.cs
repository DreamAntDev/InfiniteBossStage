using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{

	[SerializeField] float health = 100f;
	bool isDead = false;

	public bool IsDead()
	{
		return isDead;
	}

	public void TakeDamage(float damage)
	{
		health = Mathf.Max(health - damage, 0);

		if (health == 0f)
		{
			Death();
		}
	}

	public void Death()
	{
		if (isDead)
		{
			return;
		}
		GetComponent<Animator>().SetTrigger("Die");
		//TODO: Play death sound.
		print(gameObject.name + " Is Dead!");
		isDead = true;

		if (GetComponent<Collider>() != null)
		{
			GetComponent<Collider>().enabled = false;
		}

		if (GetComponent<NavMeshAgent>() != null)
		{
			GetComponent<NavMeshAgent>().enabled = false;
		}
	}

}

