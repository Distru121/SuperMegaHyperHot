using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetEnemy : Target
{
    [SerializeField] float health = 0;
    float currentHealth;

    public Slider healthBar;
    private void Start()
    {
        currentHealth = health;
        UpdateHealthBar();
    }
    public override void Hit(float damage)
    {
        base.Hit(damage);
        currentHealth -= damage;
        UpdateHealthBar();
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void UpdateHealthBar()
    {
        healthBar.value = 1 / health * currentHealth;
    }
}
