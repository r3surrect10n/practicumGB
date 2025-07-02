using Unity.Cinemachine;
using UnityEngine;
using System.Text;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private UI_Control ui_control;
    [SerializeField] private CinemachineCamera[] camers;
    [SerializeField] private CameraSwitchTrigger[] triggers;
    [SerializeField] private QuestSequence[] questSequences;

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
                        //player.GetComponent<PlayerViewManager>().SetNextCamera(camera);
                        //player.GetComponent<PlayerViewManager>().Set(camera);
                    }
                    else
                    {
                        //player.GetComponent<PlayerViewManager>().SetNextCamera(camera);
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
        if (questSequences.Length != 0)
        {
            string questProgress = GameManager.Instance.currentPlayer.questProgress;
            print($"questProgress=>{questProgress}");
            int i;
            if (questProgress != "")
            {
                string[] ar = questProgress.Split('=');
                for (i = 0; i < questSequences.Length; i++)
                {
                    questSequences[i].SetQuestsStatus(ar[i], '#');
                }
            }
            else
            {
                for (i = 0; i < questSequences.Length; i++)
                {
                    questSequences[i].SetQuestsStatus("", '#');
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveQuestProgress()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < questSequences.Length; i++)
        {
            sb.Append($"{questSequences[i].ToCsvString()}=");
        }
        GameManager.Instance.currentPlayer.questProgress = sb.ToString();
    }

    public void SavePositionAndRotation()
    {
        print(player.GetComponent<PlayerViewManager>().CurrentStaticCamera.Name);

        //return;

        GameManager.Instance.currentPlayer.currentPosition = player.transform.position;
        GameManager.Instance.currentPlayer.currentRotation = player.transform.rotation.eulerAngles;
        GameManager.Instance.currentPlayer.currentLocation = "Building";
        GameManager.Instance.currentPlayer.currentCameraName = player.GetComponent<PlayerViewManager>().CurrentStaticCamera.Name;
        //ui_control.LoadMenuScene();
    }
}
