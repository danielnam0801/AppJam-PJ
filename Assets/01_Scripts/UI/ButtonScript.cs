using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    static private bool isPause = false;
    private bool isOnSounds = true;

    
    [SerializeField] private GameObject pause;
    [SerializeField] private Text sounds;

    private void Start()
    {
        //pause = GameObject.Find("Canvas/PauseUI");
        //sounds = GameObject.Find("Canvas/PauseUI/PausePane/Sound Button/Sounds Text").GetComponent<Text>();
    }

    private void Update()
    {
        if(isPause)
        {
            if(Input.GetKeyDown(KeyCode.Space))
                Resume();
        }
    }

    public void Pause()
    {
        UIManager.Instance.PauseUI(true);
        PlayerManager.Instance.Player.SetClick(false);
        isPause = true;
        if (isPause)
        {
            pause.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    public void Resume()
    {
        PlayerManager.Instance.Player.SetClick(true);
        UIManager.Instance.PauseUI(false);
        isPause = false;
        pause.SetActive(false);
        Time.timeScale = 1.0f;
        
    }

    public void Sounds()
    {
        isOnSounds = !isOnSounds;
        if (isOnSounds)
            sounds.text = "Sounds : On";
        else
            sounds.text = "Sounds : Off";
    }

    public void MainMenu()
    {
        Debug.Log("메인메뉴");
    }
}
