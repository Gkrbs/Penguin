using Lightbug.CharacterControllerPro.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackScript : MonoBehaviour
{
    private ActionInterface _action = null;
    [SerializeField]
    private ActiveObjectsScriptable _data = null;
    [SerializeField]
    private Transform _mount_pos;
    CharacterActor characterActor = null;
    private void Awake()
    {
        _action = new JetPack();
    }
    private void OnEnable()
    {
        transform.parent = _mount_pos;
        if (characterActor == null)
            characterActor = _mount_pos.parent.gameObject.GetComponent<CharacterActor>();

        characterActor.alwaysNotGrounded = true;
        characterActor.stableLayerMask = 0;

        if (_action != null)
        {
            _action.Init(gameObject);
            _action.InitObjectInfo(_data);
            _action.ActiveAction(_mount_pos.gameObject);
        }
    }
    private void OnDisable()
    {
        characterActor.alwaysNotGrounded = false;
        characterActor.stableLayerMask = 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Wall");
    }
    private void Update()
    {
        transform.position = _mount_pos.position;
        transform.rotation = _mount_pos.rotation;

    }
}
