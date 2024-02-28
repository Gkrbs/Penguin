using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : ActionInterface
{
    enum PARAMS
    {
        JET_SPEED,
        PLAY_TIME
    }
    private bool _do_coroutine = false;
    private GameObject _object_self, _user;
    private ActiveObjectsScriptable _data;
    private Rigidbody _rd;
    public void ActiveAction()
    {
    }
    private async void RaiseUp()
    {
        float play_time = _data.FLOAT_PARAMS_ARR[(int)PARAMS.PLAY_TIME];
        _do_coroutine = true;
        _rd.useGravity = false;
        while (play_time > 0)
        {
            float time = Time.deltaTime;
            play_time -= time;
            _rd.velocity = _user.transform.up * time * _data.FLOAT_PARAMS_ARR[(int)PARAMS.JET_SPEED];
            //_rd.AddForce(_user.transform.up * time * _data.FLOAT_PARAMS_ARR[(int)PARAMS.JET_SPEED]);
            await System.Threading.Tasks.Task.Delay((int)(time * 1000));
        }
        _do_coroutine = false;
        _rd.useGravity = true;

        StopAction();
    }
    public void ActiveAction(GameObject target)
    {
        _user = target.transform.parent.gameObject;
        if (_rd == null) _rd = _user.GetComponent<Rigidbody>();
        if (!_do_coroutine)
        {
            RaiseUp();
        }
    }

    public void Init(GameObject objself)
    {
        _object_self = objself;
    }

    public void InitObjectInfo(ActiveObjectsScriptable data)
    {
        _data = data;
    }

    public void StopAction()
    {
        Rigidbody rd = _user.GetComponent<Rigidbody>();
        if(rd.velocity != Vector3.zero)
            rd.velocity= Vector3.zero;
        if (_do_coroutine)
            _do_coroutine = false;
        if(_object_self.transform.parent != null)
        _object_self.transform.parent = null;
        _object_self.SetActive(false);

    }
}
