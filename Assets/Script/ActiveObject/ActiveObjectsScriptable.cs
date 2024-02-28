using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActiveObjectParamsData", menuName = "ScriptableObjects/ActiveScriptableObjects", order = 1)]
public class ActiveObjectsScriptable : ScriptableObject
{
    public enum ObjectType
    {
        ITEM,
        SKILL
    }
    [SerializeField]
    private ObjectType _default_obj_type;
    [SerializeField]
    private int[] _int_prams_Arr;
    [SerializeField]
    private float[] _float_prams_Arr;
    [SerializeField]
    private string _default_obj_name;
    public Transform[] trArr;

    public int[] INT_PARAMS_ARR
    {
        get { return _int_prams_Arr; }
    }
    public float[] FLOAT_PARAMS_ARR
    {
        get { return _float_prams_Arr; }
    }

    public string NAME
    {
        get { return _default_obj_name; }
    }

    public ObjectType TYPE
    {
        get { return _default_obj_type; }
    }
}
