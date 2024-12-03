using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinscript : InteractableComponent
{
    public override void CloneTo(GameObject target)
    {
        coinscript clonedscript = target.AddComponent<coinscript>();
        clonedscript.particlesystem = particlesystem;
    }


    public GameObject particlesystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //do coin stuff
            //do animation collect coin
            if(gameObject.TryGetComponent<Animator>(out Animator animator))
            {
                animator.Play("collectCoin");
            }
            else
            {
                Destroy(gameObject);
            }
            GameObject instantiated_particlesystem = Instantiate(particlesystem, transform.position, Quaternion.identity);
            Destroy(gameObject, 1.4f);
        }
    }
}
