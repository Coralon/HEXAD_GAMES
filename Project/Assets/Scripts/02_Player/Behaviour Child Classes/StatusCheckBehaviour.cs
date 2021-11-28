using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusCheckBehaviour : Behaviour
{
    private bool isReportUIActivated = false;
    private bool isMorningReportDelivered = false;

    private UIMorningReport UIMorningReport;
    
    public StatusCheckBehaviour(PlayerController playerController) : base(playerController)
    {
        PlayerController = playerController;
    }

    public override void StartBehaviour()
    {
        Debug.Log("StatusCheckBehaviour Start called");
        PlayerController.SetPlayerDestination(FindWaypointHelper("trophycabinet"));

        UIMorningReport = UIManager.UIManagerInstance.UIMorningReportObj.GetComponent<UIMorningReport>();

        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        if (!isReportUIActivated && Vector2.Distance(PlayerController.GetAgentPosition(), FindWaypointHelper("trophycabinet")) < 2.0f)
        {
            SetUI("report");
            isReportUIActivated = true;
        }

        if (UIMorningReport.hasTimesBeenSubmitted && !isMorningReportDelivered) //&& hasAlreadyBeenCalled ?
        {
            //get and set hours slept
            int bedTime = UIMorningReport.GetBedtime();
            int wakeUpTime = UIMorningReport.GetWakeUpTime();

            //Debug.Log("Bedtime " + bedTime + "WakeUpTime " + wakeUpTime);

            Vector2Int hrsSlept = PlayerController.PlayerStatistics.CalculateHoursSleptNightOneTwo(bedTime, wakeUpTime);

            //if achievement unlcoked set trophy titles
            string[] trophyReceivedTitles = PlayerController.TrophyController.NEWTrophyConditionCheck();

            if (trophyReceivedTitles[0] != "null")
                UIMorningReport.IsAchieveUnlocked(true, trophyReceivedTitles);

            UIMorningReport.DeliverMorningReport(hrsSlept);

            //set goals text
            UIMorningReport.SetGoalsText(PlayerController.TrophyController.GetGoalsText());

            isMorningReportDelivered = true;
        }

        if(UIMorningReport.IsMorningReportClosed)
        {
            PlayerController.TrophyController.InstantiateTrophy();

            isReportUIActivated = false;
            isMorningReportDelivered = false;

            UIMorningReport.IsMorningReportClosed = false;

            PlayerController.IsReportDelivered(true); //leads to end behaviour
        }

        #region OLD REPORT UI

        //if (PlayerController.HasPlayerReachedDestination())
        //{
        //    SetUI("report");
        //    UIManager.UIManagerInstance.SetGoalsText(
        //        PlayerController.TrophyController.GetGoalsText());
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SetUI("report");
        //    UIManager.UIManagerInstance.SetGoalsText(
        //        PlayerController.TrophyController.GetGoalsText());
        //}

        //if (UIManager.UIManagerInstance.bedTime != -1 && UIManager.UIManagerInstance.wakeUpTime != -1)
        //{
        //    int bedTime = UIManager.UIManagerInstance.GetBedtime();
        //    int wakeUpTime = UIManager.UIManagerInstance.GetWakeUpTime();

        //    Vector2Int hrsSlept = PlayerController.PlayerStatistics.CalculateHoursSleptNightOneTwo(bedTime, wakeUpTime);

        //    //int hrsSlept = PlayerController.PlayerStatistics.CalculateHoursSlept(bedTime, wakeUpTime);

        //    //UIManager.UIManagerInstance.SetHoursSleptText(hrsSlept);
        //    UIManager.UIManagerInstance.SetHoursSleptTextNightOneTwo(hrsSlept);

        //    //REFACTOR BELOW
        //    PlayerController.TrophyController.TrophyConditionCheck();
        //    PlayerController.TrophyController.InstantiateTrophy();

        //    PlayerController.IsReportDelivered(true);
        //}

        #endregion

        base.RunBehaviour();
    }

    public override void EndBehaviour()
    {
        Debug.Log("StatusCheckBehaviour End called");
        SetUI();
        base.EndBehaviour();
    }

    /*
    
    new Update

    if (UIMorningReport.hasTimesBeenSubmitted) && hasAlreadyBeenCalled?
    {
        //get and set hours slept
        int bedTime = UIMorningReport.GetBedtime();
        int wakeUpTime = UIMorningReport.GetWakeUpTime();
        Vector2Int hrsSlept = PlayerController.PlayerStatistics.CalculateHoursSleptNightOneTwo(bedTime, wakeUpTime);
        UIMorningReport.SetHoursSleptTextNightOneTwo(hrsSlept);    
        
        //if achievement unlcoked set trophy titles
        string[] trophyReceivedTitles = PlayerController.TrophyController.NEWTrophyConditionCheck();
        if (trophiesReceive > 0) UIMorningReport.ActivateUnlockAchievementButton(true, trophyReceivedTitles);
        
        //set goals text
        UIMorningReport.SetGoalsText(PlayerController.TrophyController.GetGoalsText())
    }


    call later (close report button?)

            PlayerController.TrophyController.InstantiateTrophy();

     */

}
