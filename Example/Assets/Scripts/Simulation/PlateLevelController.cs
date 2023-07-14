using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using PaintIn3D;
//using Volpi.ObjectyPool;
public class PlateLevelController : MonoBehaviour
{
    //[SerializeField] P3dPaintSphere painterParticle;
    public GameObject particles, tool1,tool2,tool3,clearLv;
    public static bool isRunnerStarted = false;

    GameManager gameManager;
    private void Awake()
    {
        isRunnerStarted = false;
    }
    private void Start()
    {
        gameManager = GameObject.Find("/GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plate"))
        {
            if (other.GetComponent<WeaponPlate>().WeaponLevel() > SaveManager.Instance.ClearLevel)
            {
                //painterParticle.Opacity = 0.01f;
            }
        }
        if (other.CompareTag("RunnerStart"))
        {
            if (gameManager.currentWeaponIndex == 0)  // If couldn't collect anything, gives a knife.
            {
                SpawnKnife();  
            }
            StartRunner();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Plate"))
        {
            //painterParticle.Opacity = 1;
        }
    }
    void SpawnKnife()
    { /*
        GameObject spawnedWeapon;
        spawnedWeapon = ObjectyManager.Instance.ObjectyPools["ObjectyPool"].Spawn("CleanKnife", gameManager.weaponPositions[gameManager.currentWeaponIndex].position);
        spawnedWeapon.transform.SetParent(gameManager.targetPosition.parent);
        spawnedWeapon.transform.eulerAngles = new Vector3(150.346f, -19.711f, -266.125f);
        spawnedWeapon.GetComponent<KnifeRunner>().enabled = true;
        gameManager.currentWeaponIndex++;*/
    }
    void StartRunner()
    {
        isRunnerStarted = true;
        clearLv.SetActive(false);
        tool1.SetActive(false);
        tool2.SetActive(false);
        tool3.SetActive(false);
        particles.SetActive(false);
        transform.parent.transform.position = new Vector3(transform.position.x, 0.7f, transform.position.z);
    }
}
