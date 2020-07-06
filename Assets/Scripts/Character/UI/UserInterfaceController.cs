using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ObjectData.UserInterfaceData.Models;

namespace UI
{
    public class UserInterfaceController : MonoBehaviour
    {
        public KeyBindingsModel KeyBindings { get; set; }

        private Canvas InventoryCanvas { get; set; }

        private UserInterfaceModel Inventory;
        private UserInterfaceModel ToolTip;
        void Start()
        {
            Inventory = new UserInterfaceModel
            {
                Object = GameObject.Find("Inventory"),
                Canvas = GameObject.Find("Inventory").GetComponent<Canvas>(),
                DefaultPosition = GameObject.Find("Inventory").GetComponent<RectTransform>().transform.localPosition
            };
            ToolTip = new UserInterfaceModel
            {
                Object = GameObject.Find("ToolTip"),
                Canvas = GameObject.Find("ToolTip").GetComponent<Canvas>(),
                DefaultPosition = GameObject.Find("ToolTip").GetComponent<RectTransform>().transform.localPosition
            };
            Inventory.Canvas.enabled = false;
            ToolTip.Canvas.enabled = false;

            KeyBindings = new KeyBindingsModel();
            KeyBindings.Inventory = KeyCode.I;
            KeyBindings.Statistics = KeyCode.S;
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyBindings.Inventory))
            {
                Inventory.Canvas.enabled = !Inventory.Canvas.enabled;
                if(Inventory.Canvas.enabled)
                {
                    UserInterfaceLock.IsLocked = true;
                    Inventory.Object.transform.localPosition = Inventory.DefaultPosition;
                }
                else 
                {
                    UserInterfaceLock.IsLocked = false;
                }
                
            }
        }



    }
}

