using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Ewan.UpgradeObjsTesting
{
    public class Bed : UpgradeableObject
    {
        void Awake()
        {
            
        }
        
        // Start is called before the first frame update
        void Start()
        {
            ConnectToUIManagerOnStartUp();

            currentUpgradeIndex = 0;
            maxUpgradeIndex = upgrades.Length - 2;
            CreateObjectOnStartUp();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UpgradeObject();
            }
        }

        public override void UpgradeObject()
        {
            Debug.Log("bed upgraded");
            base.UpgradeObject();
        }

        public override void ConnectToUIManagerOnStartUp()
        {
           //UIManager.UIManagerInstance.bedController = this;
        }
    }

}

