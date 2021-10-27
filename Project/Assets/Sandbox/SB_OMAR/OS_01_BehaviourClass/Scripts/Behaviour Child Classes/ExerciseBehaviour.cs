using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.Behaviour
{
    public class ExerciseBehaviour : Behaviour
    {
        public ExerciseBehaviour(PlayerController playerController) : base(playerController)
        {

        }

        public override void StartBehaviour()
        {
            Debug.Log("ExerciseBehaviour Start called - press E to test update");
            base.StartBehaviour();
        }

        public override void RunBehaviour()
        {
            //Debug.Log("ExerciseBehaviour Update called");

            if (Input.GetKeyDown(KeyCode.E))
                Debug.Log("key E has been pressed");

            base.RunBehaviour();
        }

        public override void EndBehaviour()
        {
            Debug.Log("ExerciseBehaviour End called");
            base.EndBehaviour();
        }
    }
}