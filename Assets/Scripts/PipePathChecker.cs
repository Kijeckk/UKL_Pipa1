//using System.Collections.Generic;
//using UnityEngine;

//public class PipePathChecker : MonoBehaviour
//{
//    public PipeTile startPipe;
//    public PipeTile endPipe;

//    private Dictionary<Vector2Int, PipeTile> pipeMap = new Dictionary<Vector2Int, PipeTile>();

//    void BuildPipeMap()
//    {
//        pipeMap.Clear();

//        PipeTile[] pipes = FindObjectsOfType<PipeTile>();

//        foreach (PipeTile tile in pipes)
//        {
//            // PENTING: Gunakan posisi Transform.position yang dibulatkan.
//            // PASTIKAN SEMUA PIPA ANDA BERADA DI POSISI INTEGER yang bersih (X.0, Y.0)
//            Vector2Int pos = new Vector2Int(
//                Mathf.RoundToInt(tile.transform.position.x),
//                Mathf.RoundToInt(tile.transform.position.y)
//            );

//            if (!pipeMap.ContainsKey(pos))
//                pipeMap.Add(pos, tile);
//            else
//            {
//                // JIKA INI MUNCUL, ADA DUA PIPA BERTUMPUK DI POSISI YANG SAMA
//                Debug.LogError($"Dua PipeTile ditemukan di posisi yang sama: {pos}");
//            }
//        }
//    }

//    public bool CheckPath()
//    {
//        BuildPipeMap();

//        // Pastikan startPipe dan endPipe tidak null
//        if (startPipe == null || endPipe == null)
//        {
//            Debug.LogError("startPipe atau endPipe belum di set di Inspector!");
//            return false;
//        }

//        HashSet<PipeTile> visited = new HashSet<PipeTile>();

//        bool result = DFS(startPipe, visited);
//        Debug.Log("--- HASIL CHECK PATH = " + (result ? "TRUE (JALUR TERHUBUNG)" : "FALSE (JALUR PUTUS)"));

//        return result;
//    }

//    bool DFS(PipeTile pipe, HashSet<PipeTile> visited)
//    {
//        if (pipe == endPipe)
//            return true;

//        visited.Add(pipe);

//        Vector2Int pos = new Vector2Int(
//            Mathf.RoundToInt(pipe.transform.position.x),
//            Mathf.RoundToInt(pipe.transform.position.y)
//        );

//        // TryMove akan memanggil DFS secara rekursif jika koneksi berhasil

//        // UP
//        if (TryMove(pipe, pos + Vector2Int.up, pipe.up, visited)) return true;
//        // DOWN
//        if (TryMove(pipe, pos + Vector2Int.down, pipe.down, visited)) return true;
//        // LEFT
//        if (TryMove(pipe, pos + Vector2Int.left, pipe.left, visited)) return true;
//        // RIGHT
//        if (TryMove(pipe, pos + Vector2Int.right, pipe.right, visited)) return true;

//        // Jika tidak ada jalur yang berhasil dari pipa ini
//        return false;
//    }

//    // Fungsi TryMove diubah menjadi tipe bool untuk kode yang lebih bersih
//    bool TryMove(PipeTile fromPipe, Vector2Int nextPos, bool fromPipeOpen, HashSet<PipeTile> visited)
//    {
//        // 1. Cek: Apakah pipa *asal* terbuka ke arah ini?
//        if (!fromPipeOpen) return false;

//        // 2. Cek: Apakah ada pipa di posisi tetangga?
//        if (!pipeMap.ContainsKey(nextPos))
//        {
//            // Debug.Log($"[FAIL] No pipe found at position {nextPos}"); // Matikan jika terlalu banyak log
//            return false;
//        }

//        PipeTile nextPipe = pipeMap[nextPos];
//        if (visited.Contains(nextPipe)) return false;

//        // 3. Cocokkan Arah Koneksi (Memastikan Pipa Tujuan Terbuka di Sisi yang Benar)

//        // Bergerak KANAN (fromPipe.x < nextPipe.x). nextPipe harus terbuka KIRI
//        if (fromPipe.transform.position.x < nextPipe.transform.position.x && !nextPipe.left)
//        {
//            Debug.Log($"[FAIL] {fromPipe.name} -> {nextPipe.name}. {nextPipe.name} is not open on the LEFT.");
//            return false;
//        }

//        // Bergerak KIRI (fromPipe.x > nextPipe.x). nextPipe harus terbuka KANAN
//        if (fromPipe.transform.position.x > nextPipe.transform.position.x && !nextPipe.right)
//        {
//            Debug.Log($"[FAIL] {fromPipe.name} -> {nextPipe.name}. {nextPipe.name} is not open on the RIGHT.");
//            return false;
//        }

//        // Bergerak ATAS (fromPipe.y < nextPipe.y). nextPipe harus terbuka BAWAH
//        if (fromPipe.transform.position.y < nextPipe.transform.position.y && !nextPipe.down)
//        {
//            Debug.Log($"[FAIL] {fromPipe.name} -> {nextPipe.name}. {nextPipe.name} is not open on the DOWN.");
//            return false;
//        }

//        // Bergerak BAWAH (fromPipe.y > nextPipe.y). nextPipe harus terbuka ATAS
//        if (fromPipe.transform.position.y > nextPipe.transform.position.y && !nextPipe.up)
//        {
//            Debug.Log($"[FAIL] {fromPipe.name} -> {nextPipe.name}. {nextPipe.name} is not open on the UP.");
//            return false;
//        }

//        // Jika koneksi logis benar, panggil DFS
//        Debug.Log($"[SUCCESS] DFS moving from {fromPipe.name} to {nextPipe.name} at {nextPos}");
//        return DFS(nextPipe, visited);
//    }
//}