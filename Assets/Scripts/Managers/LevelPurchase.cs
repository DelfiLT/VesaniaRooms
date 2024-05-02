using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;

public class LevelPurchase : MonoBehaviour
{
    [SerializeField] private Button levelToUnlock;
    [SerializeField] private Button buyButton;

    public void PurchaseCompleted(Product product)
    {
        buyButton.gameObject.SetActive(false);
        levelToUnlock.gameObject.SetActive(true);
        Debug.Log(product + "purchased");
    }

    public void PurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        Debug.Log(product + "Failed for" + failureDescription);
    }

    public void ProductsFetch(Product product)
    {
        Debug.Log("Fetching products: " + product);
    }
}
