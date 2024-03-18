using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTool : MonoBehaviour, ToolInterface
{
    [SerializeField]
    protected ToolInfo _data;
    [SerializeField]
    protected GameObject _target, _user;
    protected bool _is_trigger = false;
    public bool IS_TRIGGER
    {
        get { return _is_trigger; }
    }

    public virtual void ActiveAction()
    {
    }

    public virtual void ActiveAction(GameObject target)
    {
    }

    public virtual void Init(GameObject obj)
    {
        _target = obj;
        _user = _user != null ? _user : GameObject.Find("Player"); 
    }

    public virtual void InitObjectInfo(ToolInfo data)
    {
        _data = data;
    }

    public virtual void StopAction()
    {
    }
}
