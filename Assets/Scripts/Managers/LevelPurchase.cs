using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class LevelPurchase : MonoBehaviour
{
    [SerializeField] private Button levelToUlock;

    public void PurchaseCompleted()
    {
        this.gameObject.SetActive(false);
        levelToUlock.gameObject.SetActive(true);
    }
}
