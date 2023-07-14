using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using PaintIn3D;
//using CW.Common;
//using Volpi.ObjectyPool;
public class HammerDirty : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    GameManager gameManager;
    //[SerializeField] private List<P3dChangeCounter> counters;

    private int decimalPlaces;

    Animator anim;
    bool isClean = false;
    bool isCleanAnimStarted = false;
    bool isKnifePlaced = false;

    private void Start()
    {
        gameManager = GameObject.Find("/GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
    }

    private void OnParticleCollision(GameObject collision)
    {
        if (isClean == false)
        {
            if (collision.CompareTag("Water"))
            {  /*
                if (CleaningRate() > 23)
                {
                    isClean = true;
                }
                */
            }
        }
    }

    private void Update()
    {
        if (isClean == true)
        {
            if (isCleanAnimStarted == false)
            {
                //ObjectyManager.Instance.ObjectyPools["ObjectyPool"].Spawn("CleanHammer", transform.position, new Vector3(0, -90, -90)).transform.SetParent(transform.parent); // When we clear the weapon, it spawns a new weapon onto the platform.
                anim.SetTrigger("clean");
                isCleanAnimStarted = true;
            }
            Invoke("GoToHand", 1.5f);

            if (transform.position == gameManager.targetPosition.position)
            {
                PlaceWeapon();
            }
        }
    }

    void PlaceWeapon()
    { /*
        if (isKnifePlaced == false)
        {
            GameObject spawnedWeapon;
            spawnedWeapon = ObjectyManager.Instance.ObjectyPools["ObjectyPool"].Spawn("CleanHammer", gameManager.weaponPositions[gameManager.currentWeaponIndex].position);
            spawnedWeapon.transform.SetParent(gameManager.targetPosition.parent);
            spawnedWeapon.transform.eulerAngles = new Vector3(180, 0, 180);
            spawnedWeapon.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            spawnedWeapon.GetComponent<HammerRunner>().enabled = true;
            gameManager.currentWeaponIndex++;
            isKnifePlaced = true;
            Destroy(this.gameObject);
        }
        */
            }
    void GoToHand()
    {
        if (isKnifePlaced == false)
        {
            anim.enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, gameManager.targetPosition.transform.position, Time.deltaTime * 5);
            transform.rotation = Quaternion.Slerp(transform.rotation, gameManager.targetPosition.rotation, Time.deltaTime * 5);
        }
    }
    /*
   float CleaningRate()
   {
        var finalCounters = counters.Count > 0 ? counters : null;
        var total = P3dChangeCounter.GetTotal(finalCounters);
        var count = P3dChangeCounter.GetCount(finalCounters);
        count = total - count;
        var percent = P3dCommon.RatioToPercentage(CwHelper.Divide(count, total), decimalPlaces);   //Knife 54 Max.   
        return percent;
   }*/
    }
