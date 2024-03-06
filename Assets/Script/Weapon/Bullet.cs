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
    private bool _initialize_complete = false;

    [SerializeField]
    ToolInfo _data;
    private ActiveTool _tool;
    private Magazine magazine;

    private void OnEnable()
    {

        if (_tool == null)
            _tool = GetComponent<ActiveTool>();
        if(magazine == null)
            magazine = GetComponentInParent<Magazine>();
        _tool.Init(transform.parent.gameObject);
        if (!_initialize_complete) return;
        transform.parent = null;
    }
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (_tool != null)
                _tool.ActiveAction(other.gameObject);
        }
    }
    private void Start()
    {
        _initialize_complete = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (is_tool_control) return;

    }
}
