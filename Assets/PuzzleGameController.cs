using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor.EditorTools;

public class PuzzleGameController : MonoBehaviour
{
    [Header("sceneNames")]
    [Tooltip("scene utama")]
    [SerializeField]
    private string MainSceneName = "LevelSelect";
    
    [Tooltip("manual input sendiri nama scene nya")]
    [SerializeField]
    private string nextLevelSceneName = "";

    [Header("Puzzle Elements")]
    [SerializeField]
    private Transform[] simbol; // Daftar pipa yang akan diacak/dicek

    [Header("UI Pop Up Kemenangan")]
    [Tooltip("Seret (drag) Panel UI Finish/Pop-up ke sini.")]
    [SerializeField]
    private GameObject finishPanel; // Panel yang akan muncul saat menang
    [SerializeField]
    private GameObject GameOverPanel;

    [Header("Timer Settings")]
    [Tooltip("buat setiap detiknya")]
    [SerializeField]
    private float totalTime = 60f;

    [Tooltip("drag ke sini tmp nya")]
    [SerializeField]
    private TextMeshProUGUI timerText;
    private float timeLeft;
    private bool isTimeUp = false;

    public static bool youWin;

    // Toleransi rotasi untuk pengecekan Z=0
    private const float rotationTolerance = 0.1f;

    void Start()
    {
        // Ganti winText dengan finishPanel
        if (finishPanel != null)
        {
            finishPanel.SetActive(false);
        }
        if (GameOverPanel != null)
        {
            GameOverPanel.SetActive(false);
        }
       
        youWin = false;
        isTimeUp = false;
        timeLeft = totalTime;
        RandomizePuzzles();

        UpdateTimerDisplay(timeLeft);
    }

    void Update()
    {
        if (youWin || isTimeUp)
        {
            return;
        }
        CheckWinCondition();
        UpdateTimer();
    }

    void UpdateTimer()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;

            if (timerText != null)
            {
              UpdateTimerDisplay(timeLeft);
            }
        }
        else
        {
            
            timeLeft = 0;
            isTimeUp = true;

            UpdateTimerDisplay(0);
            
            
            if (GameOverPanel != null)
            {
                GameOverPanel.SetActive(true);
            }
            
            if (timerText != null)
            {
                timerText.text = "00:00"; 
            }
            
            Debug.Log("Waktu Habis! Game Over.");
        }
    }
    void RandomizePuzzles()
    {
        foreach (Transform symbolTransform in simbol)
        {
            // Acak rotasi (1 hingga 3 putaran 90 derajat, agar tidak 0)
            int randomTurns = Random.Range(1, 4);

            symbolTransform.localEulerAngles = new Vector3(
                symbolTransform.localEulerAngles.x,
                symbolTransform.localEulerAngles.y,
                randomTurns * 90f
            );

            // Catatan: Jika ada logika koneksi di PipeTile yang tergantung pada rotasi,
            // Anda harus memastikan logika itu diperbarui setelah rotasi ini.
        }
    }

    void CheckWinCondition()
    {
        if (youWin) return;

        bool allCorrect = true;

        foreach (Transform symbolTransform in simbol)
        {
            float zRotation = symbolTransform.localEulerAngles.z;

            // Periksa apakah rotasi adalah 0 derajat (atau 360 derajat)
            if (!Mathf.Approximately(zRotation, 0f) &&
                !Mathf.Approximately(zRotation, 360f))
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect)
        {
            youWin = true;
            // Tampilkan Panel Kemenangan
            if (finishPanel != null)
            {
                finishPanel.SetActive(true);
            }
            Debug.Log("Selamat! Puzzle Terpecahkan!");

            // JANGAN gunakan Invoke untuk pindah scene di sini.
            // Biarkan pemain menekan tombol di Panel.
        }
    }

    // ==========================================================
    // FUNGSI BARU UNTUK TOMBOL DI PANEL UI
    // ==========================================================

    // Dipanggil saat tombol "Next Level" di panel diklik
    public void LoadNextLevel()
    {
      if (!string.IsNullOrEmpty(nextLevelSceneName))
        {
            SceneManager.LoadScene(nextLevelSceneName);
        }
        else
        {
            Debug.LogWarning("Next Level Scene Name belum diatur atau kosong. Tidak bisa memuat scene.");
        }
    }

    // Dipanggil saat tombol "Pilihan Level" di panel diklik
    public void LoadLevelSelect()
    {
       if (!string.IsNullOrEmpty(MainSceneName))
        {
            SceneManager.LoadScene(MainSceneName);
        }
        else
        {
             Debug.LogError("Main Scene Name belum diatur atau kosong! Gagal kembali ke Menu Utama.");
        }
    }

    // Dipanggil saat tombol "Restart" di panel diklik
    public void RestartCurrentLevel()
    {
        // Muat ulang scene saat ini
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

void UpdateTimerDisplay(float timeToDisplay)
{
    if (timerText == null) return;
    
    if (timeToDisplay < 0)
    {
        timeToDisplay = 0;
    }

    
    float minutesFloat = Mathf.Floor(timeToDisplay / 60);
    float secondsFloat = timeToDisplay % 60;
    
 
    string minutes = minutesFloat.ToString("00"); 
    string seconds = secondsFloat.ToString("00"); 

    timerText.text = minutes + ":" + seconds;
}
}