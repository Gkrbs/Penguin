using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ToolInterface
{
    public void Init(GameObject obj);
    public void ActiveAction();
    public void ActiveAction(GameObject target);
    public void StopAction();
    public void InitObjectInfo(ToolInfo data);
}
