using Lightbug.CharacterControllerPro.Core;
using Lightbug.CharacterControllerPro.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rd;
    private float _fall_time = 0.0f;
    [SerializeField]
    private CharacterActor _actor;
    [SerializeField]
    private NormalMovement _nm;
    // Start is called before the first frame update
    void Start()
    {
        _rd = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("StageGround"))
        {
            if (_fall_time >= 1.5f)
            {
                
            }
            _fall_time = 0.0f;
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            print("time : " + _fall_time);
            _fall_time = 0.0f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (_nm.verticalMovementParameters.isGrappled || _nm.DO_JETPACK)
        {
            _fall_time = 0.0f;
        }
        if (!_actor.IsGrounded && !_nm.verticalMovementParameters.isGrappled && !_nm.DO_JETPACK)
        { 
            _fall_time += Time.deltaTime;
        }

    }
}
