using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXManager : MonoBehaviour
{
    //craete a vfx manager script that will handle all the vfx in the game
    public static VFXManager instance;
    public VisualEffect[] vfx;
    public GameObject[] vfxObjects;

    private void Awake()
    {
        instance = this;
    }

    public void PlayVFX(string vfxName, Vector3 position)
    {
        for (int i = 0; i < vfx.Length; i++)
        {
            if (vfx[i].name == vfxName)
            {
                vfxObjects[i].transform.position = position;
                vfx[i].Play();
            }
        }
    }

    public void StopVFX(string vfxName)
    {
        for (int i = 0; i < vfx.Length; i++)
        {
            if (vfx[i].name == vfxName)
            {
                vfx[i].Stop();
            }
        }
    }

    public void StopAllVFX()
    {
        for (int i = 0; i < vfx.Length; i++)
        {
            vfx[i].Stop();
        }
    }

    public void PlayAllVFX()
    {
        for (int i = 0; i < vfx.Length; i++)
        {
            vfx[i].Play();
        }
    }
}
