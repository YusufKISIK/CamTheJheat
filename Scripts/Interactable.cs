using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool interactableDuringDay = true;
    public bool interactableDuringNight;
    public bool activeDuringDay = true;
    public bool activeDuringNight;

    private bool _interactable;

    private bool _interactReady;
    private Player _player;

    public delegate void InteractAction();
    public event InteractAction OnInteract;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        gameObject.SetActive(activeDuringDay);
        _interactable = interactableDuringDay;

        var levelManager = FindObjectOfType<LevelManager>();

        levelManager.OnNightStart += () =>
        {
            gameObject.SetActive(activeDuringNight);
            _interactable = interactableDuringNight;
        };
    }

    private void Interact()
    {
        if (_interactable)
            OnInteract?.Invoke();
    }

    private void Update()
    {
        if (_interactReady && Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _player.EnableCanvas();
        _interactReady = true;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _player.DisableCanvas();
        _interactReady = false;
    }
}
