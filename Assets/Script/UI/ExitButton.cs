using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : ButtonControl
{
    public override void OnClickedEvent()
    {
        base.OnClickedEvent();
        QuitApplication();
    }

    async private void QuitApplication()
    {
        await System.Threading.Tasks.Task.Delay(1000);
        Application.Quit();
    }
}
