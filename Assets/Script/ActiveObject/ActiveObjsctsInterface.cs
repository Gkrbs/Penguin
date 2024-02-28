using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ActiveObjsctsInterface
{
    public void Init(GameObject objectSelf);

    public void ReInit();

    public void DoState<T>(T state);

    public void DeInit();

    public void InitObjectInfo(ActiveObjectsScriptable data);
}
