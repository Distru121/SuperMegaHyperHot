using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterShooting : MonoBehaviour
{
    [SerializeField] List<GunSO> gunInventory = new List<GunSO>();
    int selectedGunIndex = 0;

    float nextTimeThatCanShoot = 0;
    const int maxProjectiles = 6;
    int projectilesRemaining = 6;
    public TMP_Text projectileText;
    public Slider projectileSlider;
    public Slider cooldownSider;

    public Transform muzzle;
    public GameObject shootParticles;
    public LineRenderer laserLine;

    private void Start()
    {
        //start by locking the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        projectileText.text = "" + projectilesRemaining;
        projectileSlider.value = ((float)projectilesRemaining / (float)maxProjectiles);
        cooldownSider.value = nextTimeThatCanShoot - Time.time;
        laserLine.SetPosition(0, Vector3.zero);
        laserLine.SetPosition(1, Vector3.zero);
        Shoot(1, 1);
    }

    public void Shoot(float damage, float shootDelay)
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextTimeThatCanShoot)
        {
            laserLine.SetPosition(0, muzzle.position);
            if (projectilesRemaining > 0)
            {
                projectilesRemaining--;
                nextTimeThatCanShoot = Time.time + shootDelay;
                Target shotTarget;
                if (CheckIfRaycastHit(out shotTarget))
                {
                    if (shotTarget != null)
                    {
                        shotTarget.Hit(damage);
                    }
                }
            }
            else
                Reload();
        }
    }
    void Reload()
    {
        projectilesRemaining = maxProjectiles;
    }
    public bool CheckIfRaycastHit(out Target target)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100))
        {
            laserLine.SetPosition(1, hit.point);

            print(hit.transform.name);
            target = hit.collider.GetComponent<Target>();
            GameObject explosion = Instantiate(shootParticles);
            explosion.transform.position = hit.point;
            Destroy(explosion, 4f);
            return true;
        }
        else
        {
            target = null;
            return false;
        }
    }
}
