using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lightbug.CharacterControllerPro.Demo
{
    public class ItemCheck : MonoBehaviour
    {
        public bool jetpackSelected = false;
        public bool foundationSelected = false;
        public int jetpackCount = 0;
        public int wallCount = 0;

        public void ItemGet(Item.ITEM_TYPE itemType)
        {
            switch (itemType)
            {
                case Item.ITEM_TYPE.JET_PACK:
                    jetpackCount = 1;
                    break;
                case Item.ITEM_TYPE.WALL:
                    wallCount = 1;
                    break;
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                jetpackSelected = true;
                foundationSelected = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                jetpackSelected = false;
                foundationSelected = true;
            }
        }
    }
}