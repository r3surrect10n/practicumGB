using UnityEngine;

public class LocationControl : MonoBehaviour
{
    [SerializeField] private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 currentPosition = GameManager.Instance.currentPlayer.currentPosition;
        if (currentPosition != Vector3.zero)
        {
            player.transform.position = currentPosition;
            player.transform.rotation = Quaternion.Euler(GameManager.Instance.currentPlayer.currentRotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SavePositionAndRotation()
    {
        GameManager.Instance.currentPlayer.currentPosition = player.transform.position;
        GameManager.Instance.currentPlayer.currentRotation = player.transform.rotation.eulerAngles;
        GameManager.Instance.currentPlayer.currentLocation = "Building";
    }

}
