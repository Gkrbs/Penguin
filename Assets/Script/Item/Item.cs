using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lightbug.CharacterControllerPro.Core;
using Lightbug.Utilities;

namespace Lightbug.CharacterControllerPro.Demo
{
    public class Item : CharacterDetector
    {
        [SerializeField]
        private bool itemActiveState = true;
        [SerializeField]
        private float respawnTime = 60f;
        [SerializeField]
        private float elapsedTime = 0f;
        public enum ITEM_TYPE
        {
            JET_PACK,
            WALL
        }
        public ITEM_TYPE currentItemType;
        
        protected override void ProcessEnterAction(CharacterActor characterActor)
        {
            characterActor.GetComponentInBranch<ItemCheck>().ItemGet(currentItemType);
            ItemActive(false);
        }
        private void Update()
        {
            if (!itemActiveState)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= respawnTime)
                {
                    ItemActive(true);
                    elapsedTime = 0;
                }
            }
        }

        void ItemActive(bool set)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = set;
            gameObject.GetComponent<BoxCollider>().enabled = set;
            itemActiveState = set;
        }
    }
}