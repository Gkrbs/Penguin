using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lightbug.CharacterControllerPro.Demo
{
    public class ItemCheck : MonoBehaviour
    {
        public NormalMovement normalMovement;

        public void ItemGet(Item.ITEM_TYPE itemType)
        {
            switch (itemType)
            {
                case Item.ITEM_TYPE.JET_PACK:
                    normalMovement.jetpackCount = 1;
                    break;
                case Item.ITEM_TYPE.WALL:
                    normalMovement.wallCount = 1;
                    break;
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                normalMovement.jetpackSelected = true;
                normalMovement.wallSelected = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                normalMovement.jetpackSelected = false;
                normalMovement.wallSelected = true;
            }
        }
    }
}