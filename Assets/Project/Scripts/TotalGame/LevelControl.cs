using Unity.Cinemachine;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private UI_Control ui_control;
    [SerializeField] private CinemachineCamera[] camers;
    [SerializeField] private CameraSwitchTrigger[] triggers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 currentPosition = GameManager.Instance.currentPlayer.currentPosition;
        if (currentPosition != Vector3.zero)
        {
            player.transform.position = currentPosition;
            player.transform.rotation = Quaternion.Euler(GameManager.Instance.currentPlayer.currentRotation);
            string cameraName = GameManager.Instance.currentPlayer.currentCameraName;
            foreach(CinemachineCamera camera in camers)
            {
                if (camera.Name == cameraName)
                {
                    player.GetComponent<PlayerViewManager>().SetView();
                    if (cameraName == camers[0].name)
                    {   //  камера от первого лица
                        player.GetComponent<PlayerViewManager>().SetNextCamera(camera);
                        //player.GetComponent<PlayerViewManager>().Set(camera);
                    }
                    else
                    {
                        player.GetComponent<PlayerViewManager>().SetNextCamera(camera);
                        string roomName = cameraName.Substring(0, cameraName.IndexOf("CinemachineCamera"));
                        print($"roomName <{roomName}>");
                        foreach(CameraSwitchTrigger trigger in triggers)
                        {
                            string name = trigger.gameObject.name;
                            if (name.Contains(roomName))
                            {
                                if (name.Contains("Enter")) trigger.gameObject.SetActive(false);
                                if (name.Contains("Exit")) trigger.gameObject.SetActive(true);
                            }
                        }
                    }
                    //player.GetComponent<PlayerViewManager>().SetNextCamera(camera);
                    //player.GetComponent<PlayerViewManager>().Set(camera);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavePositionAndRotation()
    {
        print(player.GetComponent<PlayerViewManager>().CurrentStaticCamera.Name);

        //return;

        GameManager.Instance.currentPlayer.currentPosition = player.transform.position;
        GameManager.Instance.currentPlayer.currentRotation = player.transform.rotation.eulerAngles;
        GameManager.Instance.currentPlayer.currentLocation = "Building";
        GameManager.Instance.currentPlayer.currentCameraName = player.GetComponent<PlayerViewManager>().CurrentStaticCamera.Name;
        ui_control.LoadMenuScene();
    }
}
