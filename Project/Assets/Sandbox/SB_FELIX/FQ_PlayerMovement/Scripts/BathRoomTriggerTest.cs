using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sandbox.Felix.PlayerMovement
{
    public class BathRoomTriggerTest : MonoBehaviour
    {
        //CameraManager CameraManager;

        // Start is called before the first frame update
        private void Start()
        {
            //CameraManager = GetComponentInParent<CameraManager>();
            Renderer render = GetComponent<Renderer>();
            render.material.color = Color.red;

        }

        
        void OnTriggerEnter(Collider other)
        {
            Renderer render = GetComponent<Renderer>();
            render.material.color = Color.green;

           if (gameObject.GetComponent<Renderer>().material.color == Color.green)      
            {
                //CameraManager.SetPlayerPosition("kitchen");
                Debug.Log("Shower time");
            }

        }


        private void OnTriggerExit(Collider other)
        {
            Renderer render = GetComponent<Renderer>();
            render.material.color = Color.red;
            if (other.gameObject.tag == "Player")
            {
                //CameraManager.SetPlayerPosition();
                Debug.Log("That was nice");
            }
        }

     
    }

}
