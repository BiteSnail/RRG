using UnityEngine;

public class MenuSet : MonoBehaviour
{
    public static bool GameIsPaused = false; // 게임 정지
    public GameObject PauseMenu;

    // SoundManager instance 참조
    private SoundManager soundManager;

    void Start()
    {
        // SoundManager 초기화
        soundManager = new SoundManager();
        soundManager.Init();
    }

    // Esc 누르면 Exit 버튼 창, 한번 더 누르면 다시 시작 
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        // 다시 시작
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        // 해결 필요
        soundManager.UnPauseBGM();
    }

    public void Pause()
    {
        // 정지
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        // 해결 필요
        soundManager.PauseBGM();
    }
}
