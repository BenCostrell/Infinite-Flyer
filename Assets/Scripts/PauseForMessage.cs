using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseForMessage : Task {
    public PauseForMessage(TimeScale scale)
    {
        timeScale = scale;
    }

    private TimeScale timeScale;
    private float timeElapsed;

    protected override void Init()
    {
        Services.GameManager.messageUI.SetActive(true);
        Services.GameManager.messageText.text = timeScale.transitionMessage;
        timeElapsed = 0;
        Services.GameManager.paused = true;
    }

    internal override void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= Services.GameManager.pauseTime)
        {
            SetStatus(TaskStatus.Success);
        }
    }

    protected override void OnSuccess()
    {
        Services.GameManager.messageUI.SetActive(false);
        Services.GameManager.paused = false;
    }
}
