using UnityEngine;

public class GenelUI : MonoBehaviour
{
    public Tablet tablet;
    
    void Awake()
    {
        tablet.Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OpenTablet();
        } else if (Input.GetKeyUp(KeyCode.Tab))
        {
            CloseTablet();
        }
    }

    private void OpenTablet()
    {
        tablet.gameObject.SetActive(true);
        tablet.UpdateValues();
    }

    private void CloseTablet()
    {
        tablet.gameObject.SetActive(false);
    }
    
    public void ToggleTablet()
    {
        if (tablet.gameObject.activeInHierarchy)
        {
            CloseTablet();
        }
        else
        {
            OpenTablet();
        }
    }
    
}
