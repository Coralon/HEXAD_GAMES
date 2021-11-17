using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatistics : MonoBehaviour
{
    private UIManager UIManager;
    
    private int hrsSleptNightOne;
    private int hrsSleptNightTwo;
    private int hrsSleptNightThree;
    private int hrsSleptNightFour;
    private int hrsSleptNightFive;

    //set by intro scene
    private int wakeUpTime;
    private int bedTime;

    [Header("STAT: Spirit")]
    [SerializeField] private float minSpiritLevel;
    [SerializeField] private float maxSpiritLevel;
    [SerializeField] private float spiritLevel;

    [Header("STAT: Energy")]
    [SerializeField] public float energyLevel; //reduces slowly overtime
    [SerializeField] private float maxEnergyLevel;
    [SerializeField] private float minEnergyLevel;
    [SerializeField] private float energyReductionAmount;
    [SerializeField] private float secsToWaitBeforeEnergyReduction;
    [SerializeField] private int energyGainedFromFood = 5;
    [SerializeField] private int energyReducedFromBenchPress = 10;

    [Header("STAT: Fulfilment")]
    [SerializeField] private float fulfilLevel;
    [SerializeField] private float maxfulfilLevel;
    [SerializeField] private float minfulfilLevel;
    [SerializeField] private int fulfilGainedPerGame;
    [SerializeField] private int fulfilGainedFromFood = 5;
    [SerializeField] private int fulfilGainedFromJunkFood = 10;
    [SerializeField] private int fulfilGainedFromBenchPress = 10;

    [Header("SLEEP DOLLARS")]
    [SerializeField] private int sleepDollarsLevel = 0;
    [SerializeField] private int sleepDollarsIncrementPerGame = 10;

    // Start is called before the first frame update
    void Start()
    {
        UIManager = UIManager.UIManagerInstance;
        StartCoroutine(ReduceEnergyOverTime());
    }

    // Update is called once per frame
    void Update()
    {
        UIManager.StatsUpdate(
            energyLevel.ToString(), 
            fulfilLevel.ToString(), 
            CalculateSpiritLevel().ToString(),
            CalculateSpiritSliderLevel(),
            sleepDollarsLevel.ToString());
    }

    IEnumerator ReduceEnergyOverTime()
    {
        while (energyLevel > minEnergyLevel)
        {
            energyLevel -= energyReductionAmount;
            //Debug.Log("energy is now: " + energyLevel);
            yield return new WaitForSeconds(secsToWaitBeforeEnergyReduction);
        }
    }

    public void SetNewEnergyLevels()
    {
        energyLevel = maxEnergyLevel;
        StartCoroutine(ReduceEnergyOverTime());

        ResetFulfilment();
    }

    public void MinigameStatsImpact()
    {
        if (fulfilLevel + fulfilGainedPerGame < maxfulfilLevel && energyLevel > 26)
        {
            fulfilLevel += fulfilGainedPerGame;
            sleepDollarsLevel += sleepDollarsIncrementPerGame;
            energyLevel -= 25;
            Debug.Log("played minigame");
        }
        else Debug.Log("Unable to play any more games");
    }

    public void FoodEatenStatsImpact()
    {
        if (energyLevel < maxEnergyLevel - 6) energyLevel += energyGainedFromFood;
        else energyLevel = maxEnergyLevel;

        if (fulfilLevel < maxfulfilLevel - 6) fulfilLevel += fulfilGainedFromFood;
        else fulfilLevel = maxfulfilLevel;
    }

    public void JunkFoodEatenStatsImpact()
    {
        //Debug.Log(fulfilGainedFromJunkFood);
        
        if (fulfilLevel < maxfulfilLevel - 11) 
            fulfilLevel += fulfilGainedFromJunkFood;
        else fulfilLevel = maxfulfilLevel;
    }

    public void BenchPressStatsImpact()
    {
        if (energyLevel > minEnergyLevel + energyReducedFromBenchPress)
        {
            energyLevel -= energyReducedFromBenchPress;
        }
        else Debug.Log("energy level at max");

        if (fulfilLevel < maxfulfilLevel - fulfilGainedFromBenchPress)
        {
            fulfilLevel += fulfilGainedFromBenchPress;
        }
        else Debug.Log("fulfil level at max");
    }

    private void ResetFulfilment()
    {
        fulfilLevel = 0;
    }

    private float CalculateSpiritSliderLevel()
    {
        spiritLevel = (energyLevel - 10) + fulfilLevel;
        if (spiritLevel > 100) spiritLevel = 100;

        return spiritLevel / maxSpiritLevel;
    }


    private string CalculateSpiritLevel()
    {
        spiritLevel = (energyLevel - 10) + fulfilLevel;
        if (spiritLevel > 100) spiritLevel = 100;

        return spiritLevel.ToString();
    }

    public int CalculateHoursSlept(int bedTime, int wakeUpTime)
    {
        hrsSleptNightTwo = hrsSleptNightOne;
        
        hrsSleptNightOne = (24 - bedTime) + wakeUpTime; //REFACTOR
        energyLevel = maxEnergyLevel;

        Debug.Log("PlayerStats " + hrsSleptNightOne);

        //add if statements to see what new energy levels are
        SetNewEnergyLevels();

        return hrsSleptNightOne;
    }

    public Vector2Int CalculateHoursSleptNightOneTwo(int bedTime, int wakeUpTime)
    {
        //REFACTOR
        hrsSleptNightFive = hrsSleptNightFour;
        hrsSleptNightFour = hrsSleptNightThree;
        hrsSleptNightThree = hrsSleptNightTwo;        
        hrsSleptNightTwo = hrsSleptNightOne;

        hrsSleptNightOne = (24 - bedTime) + wakeUpTime; //REFACTOR
        energyLevel = maxEnergyLevel;

        //Debug.Log("PlayerStats " + hrsSleptNightOne);

        //add if statements to see what new energy levels are
        SetNewEnergyLevels();

        Vector2Int hrsSlept = new Vector2Int(hrsSleptNightOne, hrsSleptNightTwo);

        return hrsSlept;
    }

    public int GetHrsSleptNightOne()
    {
        return hrsSleptNightOne;
    }

    public int[] GetHoursSleptLastFiveNights()
    {
        int[] hrsSleptFiveNight = { hrsSleptNightOne, hrsSleptNightTwo, hrsSleptNightThree, hrsSleptNightFour, hrsSleptNightFive };
        return hrsSleptFiveNight;
    }

    public void SleepDollarsReward(int rewardAmt)
    {
        sleepDollarsLevel += rewardAmt;
    }
}
