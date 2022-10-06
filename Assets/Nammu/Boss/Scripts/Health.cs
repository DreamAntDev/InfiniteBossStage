using IBS.Monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{

	[SerializeField] float health = 100f;
    float maxHealth;

	public float HealthValue
    {
		set => health *= (float)(1-(value/100));
		get => health;
    }

	bool isDead = false;
	Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        maxHealth = health;
		Debug.Log("Boss :" + maxHealth);
        UI.MainInterface.MainInterface.Instance.bossHpSlider.SetValue((float)this.health / this.maxHealth);
    }

    public bool IsDead()
	{
		return isDead;
	}

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
			TakeDamage(10);
        }
    }

    public void TakeDamage(float damage)
	{
		health = Mathf.Max(health - damage, 0);

		animator.SetTrigger("GetHit");
        UI.MainInterface.MainInterface.Instance.bossHpSlider.SetValue((float)this.health / this.maxHealth); // MaxHP정보가 없어서 임시로 100
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
		animator.SetTrigger("Die");
		//TODO: Play death sound.
		print(gameObject.name + " Is Dead!");
		isDead = true;

		GetComponent<Mover>().Stop();


		if (GetComponent<Collider>() != null)
		{
			GetComponent<Collider>().enabled = false;
		}

		if (GetComponent<NavMeshAgent>() != null)
		{
			GetComponent<NavMeshAgent>().enabled = false;
		}

		SendMessage("OnDropRelic");
	}

}

