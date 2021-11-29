using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyController : MonoBehaviour
{
    [SerializeField] private Transform petTrophyPos; //DELETE
    private PlayerController PlayerController;
    
    [System.Serializable]
    public class Trophy
    {
        [SerializeField] public string name;
        [SerializeField] public string trophyConditions;
        [SerializeField] public bool isTrophyCreated;
        [SerializeField] public bool isConditionMet;
        [SerializeField] public GameObject trophyPrefab;
        [SerializeField] public Transform trophyPosition;
        [SerializeField] public int sleepDollarReward;
    }

    [SerializeField] Trophy[] trophies;

    // Start is called before the first frame update
    void Start()
    {
        //get data from player prefs

        PlayerController = GameManager.Instance.player.GetComponent<PlayerController>();
        PlayerController.trophyCabinetPosition = petTrophyPos; //DELETE
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TrophyConditionCheck()
    {
        //replace with actual trophy conditions
        if (!trophies[0].isConditionMet && IsTrophyOneConditionMet())
            trophies[0].isConditionMet = true;

        if (!trophies[1].isConditionMet && IsTrophyTwoConditionMet())
            trophies[1].isConditionMet = true;

        if (!trophies[2].isConditionMet && IsTrophyThreeConditionMet())
            trophies[2].isConditionMet = true;

        if (!trophies[3].isConditionMet && Input.GetKeyDown(KeyCode.Alpha4))
            trophies[3].isConditionMet = true;

        if (!trophies[4].isConditionMet && Input.GetKeyDown(KeyCode.Alpha5))
            trophies[4].isConditionMet = true;
    }

    public string[] NEWTrophyConditionCheck()
    {
        string[] unlockedTrophyTitles = { "null", "null" };

        if (!trophies[0].isConditionMet && IsTrophyOneConditionMet())
        {
            trophies[0].isConditionMet = true;

            unlockedTrophyTitles[0] = trophies[0].name;
            unlockedTrophyTitles[1] = trophies[0].trophyConditions;

            return unlockedTrophyTitles;
        }
            

        if (!trophies[1].isConditionMet && IsTrophyTwoConditionMet())
        {
            trophies[1].isConditionMet = true;
            unlockedTrophyTitles[0] = trophies[1].name;
            unlockedTrophyTitles[1] = trophies[1].trophyConditions;

            return unlockedTrophyTitles;
        }
            

        if (!trophies[2].isConditionMet && IsTrophyThreeConditionMet())
        {
            trophies[2].isConditionMet = true;
            unlockedTrophyTitles[0] = trophies[2].name;
            unlockedTrophyTitles[1] = trophies[2].trophyConditions;

            return unlockedTrophyTitles;
        }
            

        if (!trophies[3].isConditionMet && Input.GetKeyDown(KeyCode.Alpha4))
        {
            trophies[3].isConditionMet = true;
            unlockedTrophyTitles[0] = trophies[3].name;
            unlockedTrophyTitles[1] = trophies[3].trophyConditions;

            return unlockedTrophyTitles;
        }
            

        if (!trophies[4].isConditionMet && Input.GetKeyDown(KeyCode.Alpha5))
        {
            trophies[4].isConditionMet = true;
            unlockedTrophyTitles[0] = trophies[4].name;
            unlockedTrophyTitles[1] = trophies[4].trophyConditions;

            return unlockedTrophyTitles;
        }

        return unlockedTrophyTitles;
    }


    public void InstantiateTrophy()
    {
        foreach (Trophy trophy in trophies)
        {
            if (trophy.isConditionMet && !trophy.isTrophyCreated)
            {
                Instantiate(trophy.trophyPrefab, trophy.trophyPosition);
                trophy.isTrophyCreated = true;
                PlayerController.PlayerStatistics.SleepTrophyGoals++;
                PlayerController.PlayerStatistics.SleepDollarsReward(trophy.sleepDollarReward);
            }
        }
        //store data in player prefs
    }

    private bool IsTrophyOneConditionMet()
    {
        if (PlayerController.PlayerStatistics.GetHrsSleptNightOne() > 7) return true;

        return false;
    }

    private bool IsTrophyTwoConditionMet()
    {
        int[] hoursSleptPastThreeNights = PlayerController.PlayerStatistics.GetHoursSleptLastFiveNights();

        for (int i = 0; i < 3; i++)
        {
            if (hoursSleptPastThreeNights[i] < 7) return false;
        }

        return true;
    }

    private bool IsTrophyThreeConditionMet()
    {
        int[] hoursSleptPastFiveNights = PlayerController.PlayerStatistics.GetHoursSleptLastFiveNights();

        for (int i = 0; i < 5; i++)
        {
            if (hoursSleptPastFiveNights[i] < 7) return false;
        }

        return true;
    }

    //trophy conditions 4 - 9 required

    public string[] GetGoalsText()
    {
        string[] goalsText = { trophies[0].trophyConditions,
            trophies[1].trophyConditions,
            trophies[2].trophyConditions,
            trophies[3].trophyConditions,
            trophies[4].trophyConditions, 
            trophies[5].trophyConditions, 
            trophies[6].trophyConditions, 
            trophies[7].trophyConditions,
            trophies[8].trophyConditions };

        return goalsText;
    }
}
