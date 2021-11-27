using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBehaviour : Behaviour
{
    //private Vector3 targetPosition;
    //private string targetPos;
    //private bool isTargetReached;

    private bool isUIListenersSet = false;

    public SeekBehaviour(PlayerController playerController) : base(playerController)
    {
        PlayerController = playerController;
    }

    public override void StartBehaviour()
    {
        Debug.Log("SeekBehaviour Start called");

        if (!isUIListenersSet)
        {
            UIManager.UIManagerInstance.SetNavigationUIListeners(this);
            isUIListenersSet = true;
        }
            
        SetUI();

        base.StartBehaviour();
    }

    public override void RunBehaviour()
    {
        if (PlayerController.HasPlayerReachedDestination())
        {
            PlayerController.IsPetSeeking(false);
            //if (Vector2.Distance(PlayerController.GetAgentPosition(), FindWaypointHelper("trophycabinet")) > 3.0f)
            //if (PlayerController.GetAgentPosition() != FindWaypointHelper("trophycabinet"))
        }
    }

    public override void EndBehaviour()
    {
        Debug.Log("SeekBehaviour End called");
        base.EndBehaviour();
    }

    public override void SeekKitchen()
    {

        //set isPetSeeking = true in playercontroller
        //isPetSeeking = false in player controller when destination is reached

        if (PlayerController.IsPetAbleToSeek())
        {
            PlayerController.IsPetSeeking(true);
            PlayerController.SetPlayerDestination(FindWaypointHelper("kitchen"));
        }
            
    }

    public override void SeekGym()
    {
        if (PlayerController.IsPetAbleToSeek())
        {
            PlayerController.IsPetSeeking(true);
            PlayerController.SetPlayerDestination(FindWaypointHelper("gym"));
        }

        //if (PlayerController.IsPetAbleToSeek())
        //    PlayerController.SetPlayerDestination(FindWaypointHelper("gym"));
    }

    public override void SeekBathroom()
    {
        if (PlayerController.IsPetAbleToSeek())
        {
            PlayerController.IsPetSeeking(true);
            PlayerController.SetPlayerDestination(FindWaypointHelper("bathroom"));
        }

        //if (PlayerController.IsPetAbleToSeek())
        //    PlayerController.SetPlayerDestination(FindWaypointHelper("bathroom"));
    }

    public override void SeekBedroom()
    {
        if (PlayerController.IsPetAbleToSeek())
        {
            PlayerController.IsPetSeeking(true);
            PlayerController.SetPlayerDestination(FindWaypointHelper("bedroom"));
        }

        //if (PlayerController.IsPetAbleToSeek())
        //    PlayerController.SetPlayerDestination(FindWaypointHelper("bedroom"));
    }

    public override void SeekTrophyCabinet()
    {
        if (PlayerController.IsPetAbleToSeek())
        {
            UIManager.UIManagerInstance.CloseAllFlyouts();
            PlayerController.IsPetSeeking(true);
            PlayerController.SetPlayerDestination(FindWaypointHelper("trophycabinet"));
        }

        //if (PlayerController.IsPetAbleToSeek()) 
        //    PlayerController.SetPlayerDestination(FindWaypointHelper("trophycabinet"));
    }

    public override void SeekLivingRoom()
    {
        if (PlayerController.IsPetAbleToSeek())
        {
            PlayerController.IsPetSeeking(true);
            PlayerController.SetPlayerDestination(FindWaypointHelper("livingroom"));
        }

        //if (PlayerController.IsPetAbleToSeek())
        //    PlayerController.SetPlayerDestination(FindWaypointHelper("livingroom"));
    }
}

