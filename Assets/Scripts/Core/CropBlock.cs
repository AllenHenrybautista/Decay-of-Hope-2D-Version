using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBlock : MonoBehaviour
{
    //create a system for a 2d crop block for a farming game 

    //create a system for a 2d crop block for a farming game
    //create a system for a 2d crop block for a farming game


    public SpriteRenderer landStateDefault;
    public SpriteRenderer landStateWatered;
    public SpriteRenderer landStatePlanted;
    public SpriteRenderer landStateGrowing;
    public SpriteRenderer landStateReady;
    public SpriteRenderer landStateDead;


    private void Start()
    {
        landStateDefault.enabled = true;
        landStateWatered.enabled = false;
        landStatePlanted.enabled = false;
        landStateGrowing.enabled = false;
        landStateReady.enabled = false;
        landStateDead.enabled = false;
    }

    public void SetLandState(string state)
    {
        landStateDefault.enabled = false;
        landStateWatered.enabled = false;
        landStatePlanted.enabled = false;
        landStateGrowing.enabled = false;
        landStateReady.enabled = false;
        landStateDead.enabled = false;

        switch (state)
        {
            case "default":
                landStateDefault.enabled = true;
                break;
            case "watered":
                landStateWatered.enabled = true;
                break;
            case "planted":
                landStatePlanted.enabled = true;
                break;
            case "growing":
                landStateGrowing.enabled = true;
                break;
            case "ready":
                landStateReady.enabled = true;
                break;
            case "dead":
                landStateDead.enabled = true;
                break;
        }
    }
}
