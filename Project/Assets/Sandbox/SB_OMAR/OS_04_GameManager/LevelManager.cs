using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sandbox.Omar.GameManager
{

    public class LevelManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GameManager.Instance.AssignPlayer();
            GameManager.Instance.AssignHouse();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
