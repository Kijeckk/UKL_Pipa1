using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PuzzleGameController : MonoBehaviour
{
    // Ganti ini dengan nama Scene Menu Utama atau Scene Pilihan Level Anda
    private const string MainSceneName = "Main";
    // Ganti ini dengan nama scene level berikutnya (jika ada)
    private const string NextLevelSceneName = "LevelDua";

    [Header("Puzzle Elements")]
    [SerializeField]
    private Transform[] simbol; // Daftar pipa yang akan diacak/dicek

    [Header("UI Pop Up Kemenangan")]
    [Tooltip("Seret (drag) Panel UI Finish/Pop-up ke sini.")]
    [SerializeField]
    private GameObject finishPanel; // Panel yang akan muncul saat menang

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
        youWin = false;
        RandomizePuzzles();
    }

    void Update()
    {
        CheckWinCondition();
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
        // Asumsi: Level berikutnya ada
        SceneManager.LoadScene(NextLevelSceneName);
    }

    // Dipanggil saat tombol "Pilihan Level" di panel diklik
    public void LoadLevelSelect()
    {
        // Kembali ke Scene Menu Utama / Pilihan Level
        SceneManager.LoadScene(MainSceneName);
    }

    // Dipanggil saat tombol "Restart" di panel diklik
    public void RestartCurrentLevel()
    {
        // Muat ulang scene saat ini
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}