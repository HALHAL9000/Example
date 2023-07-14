using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GameData")]
public class GameData : ScriptableObject
{
    [SerializeField] float dragSpeed;
    [SerializeField] float forwardSpeed;
    [SerializeField] float runnerForwardSpeed;
    [SerializeField] int maxClearLevel;
    [SerializeField] int maxIncomeLevel;
    [SerializeField] int maxDamageLevel;
    [SerializeField] int maxFireRateLevel;
    public float DragSpeed => dragSpeed;
    public float ForwardSpeed => forwardSpeed;
    public float RunnerForwardSpeed => runnerForwardSpeed;
    public int MaxClearLevel => maxClearLevel;
    public int MaxIncomeLevel => maxIncomeLevel;
    public int MaxDamageLevel => maxDamageLevel;
    public int MaxFireRateLevel => maxFireRateLevel;


}
