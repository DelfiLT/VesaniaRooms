using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WatchAd : MonoBehaviour
{
    public TextMeshProUGUI clueInfoText;
    [SerializeField] private string clueInfo;
    [SerializeField] private GameObject advertisementPanel;
    public Button watchAdBtn;
    
    public void PlayAdvertisement()
    {
        StartCoroutine(AwaitAdvertisement());
    }

    private IEnumerator AwaitAdvertisement()
    {
        advertisementPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        watchAdBtn.interactable = false;
        advertisementPanel.SetActive(false);
        clueInfoText.text = clueInfo;
    }
}
