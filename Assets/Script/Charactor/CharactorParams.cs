using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharactorParamsData", menuName = "ScriptableObjects/CharactorScriptableObjects", order = 1)]
public class CharactorParams : ScriptableObject
{
    [SerializeField]
    private int _default_save_count;
    [SerializeField]
    private float _default_speed;
    [SerializeField]
    private float _default_jump_force;
    [SerializeField]
    private float _default_rotate_speed;
    [SerializeField]
    private string _default_charactor_name;

    public int SAVE_COUNT
    {
        get { return _default_save_count; }
    }

    public float SPEED
    {
        get { return _default_rotate_speed; }
    }

    public float JUMP_FORCE
    {
        get { return _default_jump_force; }
    }

    public float ROTATE_SPEED
    {
        get { return _default_rotate_speed; }
    }

    public string NAME
    {
        get { return _default_charactor_name; }
    }
}
