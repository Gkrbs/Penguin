using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lightbug.CharacterControllerPro.Core;
using Lightbug.Utilities;

namespace Lightbug.CharacterControllerPro.Demo
{
    public class Item : CharacterDetector
    {
        public enum ITEM_TYPE
        {
            JET_PACK,
            WALL
        }
        public ITEM_TYPE currentItemType;
        
        protected override void ProcessEnterAction(CharacterActor characterActor)
        {
            characterActor.GetComponentInBranch<ItemCheck>().ItemGet(currentItemType);
            gameObject.SetActive(false);
        }
    }
}