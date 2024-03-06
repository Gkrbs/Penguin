using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolInfo", menuName = "ScriptableObjects/Tools", order = 1)]
public class ToolInfo : ScriptableObject
{
    [SerializeField]
    private float[] _f_datas;
    [SerializeField]
    private string _name;
    [SerializeField]
    private string _info_menual;
    [SerializeField]
    private string _type;

    public float[] f_datas
    {
        get { return _f_datas; }
    }

    public string tool_name
    {
        get { return _name; }
    }

    public string info
    {
        get { return _info_menual; }
    }

    public string type
    {
        get { return type; }
    }

}
