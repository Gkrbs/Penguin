using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    enum INFO
    {
        ATK,
        MAX_DISTANCE
    }

    public bool is_tool_control = false;

    [SerializeField]
    ToolInfo _data;
    private ActiveTool _tool;
    private Magazine magazine;
    public float ATK
    {
        get
        {
            if (_data != null)
                return _data.f_datas[(int)INFO.ATK];
            
            return 5.0f;
        }

    }

    public float MAX_DISTANCE
    {
        get {
            if (_data != null)
                return _data.f_datas[(int)INFO.MAX_DISTANCE];
            return 15.0f;
        }
    }
    private void OnEnable()
    {

        if (_tool == null)
            _tool = GetComponent<ActiveTool>();
        if (magazine == null)
            magazine = GetComponentInParent<Magazine>();
        _tool.Init(transform.parent.gameObject);
    }
    public void DestroyBullet()
    {
        if (magazine != null)
            magazine.DestroyBulletEvent -= DestroyBullet;
        Destroy(gameObject);
    }
    public void DelayDestroyBullet(float delay)
    {
        if (magazine != null)
            magazine.DestroyBulletEvent -= DestroyBullet;
        Destroy(gameObject, delay);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (_tool != null)
                _tool.ActiveAction(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (is_tool_control) return;

    }
}
