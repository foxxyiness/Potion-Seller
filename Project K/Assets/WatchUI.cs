using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchUI : MonoBehaviour
{
    private GameObject rightHand;
    private PlayerWallet playerWallet;
    
    private void Update()
    {
        //Check for Z rotation being over 90 degrees
        playerWallet.balanceText.gameObject.SetActive(rightHand.transform.rotation.z > 90.0);
    }
}
