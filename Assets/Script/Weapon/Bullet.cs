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

    public void CollisionObject()
    {
        RaycastHit hit;
        int layer = LayerMask.GetMask("Wall");

        if (Physics.SphereCast(transform.position, 0.5f, GetComponent<Rigidbody>().velocity, out hit, 0.5f, layer))
        {
             if (_tool != null)
                _tool.ActiveAction(hit.collider.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (is_tool_control) return;
        CollisionObject();

    }
}
