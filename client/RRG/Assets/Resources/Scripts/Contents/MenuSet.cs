using UnityEngine;

public class MenuSet : MonoBehaviour
{
    public static bool GameIsPaused = false; // ���� ����
    public GameObject PauseMenu;

    // SoundManager instance ����
    private SoundManager soundManager;

    void Start()
    {
        // SoundManager �ʱ�ȭ
        soundManager = new SoundManager();
        soundManager.Init();
    }

    // Esc ������ Exit ��ư â, �ѹ� �� ������ �ٽ� ���� 
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
        // �ٽ� ����
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        // �ذ� �ʿ�
        soundManager.UnPauseBGM();
    }

    public void Pause()
    {
        // ����
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        // �ذ� �ʿ�
        soundManager.PauseBGM();
    }
}
