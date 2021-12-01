using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepBehaviour : Behaviour
{
    private int dayFellAsleep = -1;

    private bool hasBeenInBedroom = false;
    private bool isRoomUISet = false;

    private bool isPlayingMinigame = false;

    public SleepBehaviour(PlayerController playerController) : base(playerController)
    {
        PlayerController = playerController;
    }

    public override void StartBehaviour()
    {
        //Debug.Log("SleepBehaviour Start called - press L to test update");

        UIManager.UIManagerInstance.CurrentBehaviour = this;
        DialogueManager.DialogueManagerInstance.CurrentBehaviour = this;


        SetUI("bedroom");

        //if (!hasBeenInBedroom) DialogueManager.DialogueManagerInstance.PetConversation("NewBedroom");
        //else SetUI("bedroom");

        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        //if (DialogueManager.DialogueManagerInstance.currentConversationComplete) hasBeenInBedroom = true;
        
        //if (!isRoomUISet && hasBeenInBedroom)
        //{
        //    SetUI("bedroom");
        //    isRoomUISet = true;
        //}

        base.RunBehaviour();
    }

    public override void EndBehaviour()
    {
        Debug.Log("SleepBehaviour End called");
        
        SetUI();
        
        base.EndBehaviour();
    }

    public override void SendToBed()
    {
        if (PlayerController.TimeController.IsTimeAfter(PlayerController.petBedTime))
        {
            SwitchCamera("bedroomSleep");

            PlayerController.IsPetSleeping(true);

            PlayerController.PlayerAnimations.GetIntoBed();

            Debug.Log("Pet sent to bed");
            if (dayFellAsleep == -1) dayFellAsleep = PlayerController.TimeController.GetGameDate();
            //PlayerController.IsPetSleeping(true);
            PlayerController.SetPlayerDestination(PlayerController.bedPos.position);

            AudioManager.AudioManagerInstance.PlaySound("Sleeping");
            AudioManager.AudioManagerInstance.StopSound("FootStep");

            UIManager.UIManagerInstance.SendToBedBtnClicked();
        }
        else
        {
            //refactor
            Debug.Log("it's not pet's bed time yet!");
            DialogueManager.DialogueManagerInstance.PetConversation("DontNeedSleep");
        }
    }

    private void SwitchCamera(string room)
    {
        PlayerController.CameraManager.SetPlayerPosition(room);
        PlayerController.CameraSwitch();
    }

    public override void WakePetUp()
    {
        PlayerController.PlayerAnimations.GetOutOfBed();
        SwitchCamera("bedroom");

        PlayerController.IsPetSleeping(false);

        Debug.Log("pet woken up");
        //PlayerController.IsPetSleeping(false);
        AudioManager.AudioManagerInstance.StopSound("Sleeping");
        UIManager.UIManagerInstance.WakeUpBtnClicked();

        if (PlayerController.TimeController.IsNextDay(dayFellAsleep) && 
            PlayerController.TimeController.IsTimeAfter(PlayerController.petWakeUpTime))
        {
            //UIManager.UIManagerInstance.WakeUpBtnClicked();
            UIManager.UIManagerInstance.WakeUpNextDayBtnClicked();
            PlayerController.IsReportDelivered(false);
            dayFellAsleep = -1;
        }
        else
        {
            PlayerController.SetPlayerDestination(FindWaypointHelper("bedroom"));
        }
    }

    public override void PlayMiniGame()
    {

        if (!isPlayingMinigame)
        {
            if (PlayerController.PlayerStatistics.energyLevel >= 30)
            {
                PlayerController.SetPlayerDestination(PlayerController.miniGamePos.position);
                SwitchCamera("bedroomGame");
                PlayerController.PlayerAnimations.PlayMinigame();
                PlayerController.PlayerStatistics.MinigameStatsImpact();
                UIManager.UIManagerInstance.MinigameClicked(true);

                isPlayingMinigame = true;
            }
            else
            {
                Debug.Log("pet too tired to play minigames");
                DialogueManager.DialogueManagerInstance.PetConversation("BedroomTooTiredForGames");
            }
        }
        else
        {
            PlayerController.SetPlayerDestination(FindWaypointHelper("bedroom"));
            UIManager.UIManagerInstance.MinigameClicked(false);
            SwitchCamera("bedroom");

            isPlayingMinigame = false;
        }


        //OLD
        /*
         if (!isPlayingMinigame)
        {
            PlayerController.SetPlayerDestination(PlayerController.miniGamePos.position);
            SwitchCamera("bedroomGame");
            PlayerController.PlayerAnimations.PlayMinigame();
            PlayerController.PlayerStatistics.MinigameStatsImpact();
            UIManager.UIManagerInstance.MinigameClicked(true);

            isPlayingMinigame = true;
        }
        else
        {
            PlayerController.SetPlayerDestination(FindWaypointHelper("bedroom"));
            UIManager.UIManagerInstance.MinigameClicked(false);
            SwitchCamera("bedroom");

            isPlayingMinigame = false;
        }
         */
    }

    public override void StartConversation()
    {
        DialogueManager.DialogueManagerInstance.PetConversation("Bedroom");
    }

    public override void StartConversationWakeUp()
    {
        DialogueManager.DialogueManagerInstance.PetConversation("BedroomWakeUp");
    }

    public override void StartConversationMinigame()
    {
        DialogueManager.DialogueManagerInstance.PetConversation("BedroomMinigame");
    }
}

