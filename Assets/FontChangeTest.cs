using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FontChangeTest : MonoBehaviour
{
    public TMP_FontAsset fontAsset;
    public List<TMP_Text> tmps = new();
    public void ChangeFont()
    {
        foreach (TMP_Text textMeshPro3D in tmps)
        {
            textMeshPro3D.font = fontAsset;
        }
    }
}
