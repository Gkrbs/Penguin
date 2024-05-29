using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElavatorSoundEvent : MonoBehaviour
{
    public AudioSource audio;
    public GameObject upStairDoor;
    public GameObject downStairDoor;
    public ElevatorIsUpCheck elevatorIsUpCheck;
    public void MoveElevatorStart()
    {
        SoundManager.instance.PlayOneShot(audio, "ElevatorMovingStart");

    }
    public void MoveElevatorStay()
    {
        SoundManager.instance.PlayLoop(audio, "ElevatorMovingStay");
    }

    public void MoveElevatorEnd()
    {
        SoundManager.instance.PlayOneShot(audio, "ElevatorMovingEnd");
    }
    public void CloseDoor()
    {
        SoundManager.instance.PlayOneShot(audio, "ElevatorDoor");
        StartCoroutine(CloseDoorSetFalse());
    }
    public void OpenDoor()
    {
        SoundManager.instance.PlayOneShot(audio, "ElevatorDoor",4);
        if (!elevatorIsUpCheck.isUp)
            upStairDoor.SetActive(false);
        else
            downStairDoor.SetActive(false);
    }
    IEnumerator CloseDoorSetFalse()
    {
        yield return new WaitForSeconds(3f);
        upStairDoor.SetActive(true);
        downStairDoor.SetActive(true);
    }
}
