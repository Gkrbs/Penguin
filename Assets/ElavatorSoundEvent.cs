using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElavatorSoundEvent : MonoBehaviour
{
    public AudioSource audio;

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
    }
    public void OpenDoor()
    {
        SoundManager.instance.PlayOneShot(audio, "ElevatorDoor",4);
    }
}
