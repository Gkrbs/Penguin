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
    private void Awake()
    {
        _action = new JetPack();
    }
    private void OnEnable()
    {
        transform.parent = _mount_pos;
        if (_action != null)
        {
            _action.Init(gameObject);
            _action.InitObjectInfo(_data);
            _action.ActiveAction(_mount_pos.gameObject);
        }
    }

    private void Update()
    {
        transform.position = _mount_pos.position;
        transform.rotation = _mount_pos.rotation;

    }
}
