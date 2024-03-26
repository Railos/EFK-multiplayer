using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject inventoryCanvas;

    private Controls controls;

    private void Start()
    {
        controls = new Controls();
        controls.Enable();
        
        inventoryCanvas.SetActive(false);
    }

    private void Update()
    {
        if (controls.Land.OpenInventory.triggered)
        {
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
            if (inventoryCanvas.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
