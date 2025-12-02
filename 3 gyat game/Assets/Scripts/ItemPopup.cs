using UnityEngine;
using TMPro;

public class ItemPopup : MonoBehaviour
{
    [Header("Popup Settings")]
    public string itemName = "Mystery Item";
    public float showDistance = 3f;
    public GameObject popupPrefab;

    public Transform player;
    public GameObject popupInstance;
    public TMP_Text popupText;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Create popup and hide it initially
        if (popupPrefab != null)
        {
            popupInstance = Instantiate(popupPrefab, transform);
            popupText = popupInstance.GetComponentInChildren<TMP_Text>();
            popupText.text = itemName;
            popupInstance.SetActive(false);
        }
    }

    void Update()
    {
        if (player == null || popupInstance == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        bool shouldShow = distance <= showDistance;

        popupInstance.SetActive(shouldShow);

        if (shouldShow)
        {
            // Billboard toward player
            popupInstance.transform.LookAt(player);
            popupInstance.transform.Rotate(0, 180, 0); // flip to face camera correctly
        }
    }
}
