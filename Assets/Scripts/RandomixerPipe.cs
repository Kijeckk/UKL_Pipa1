//using UnityEngine;

//public class PipeRandomizer : MonoBehaviour
//{
//    void Start()
//    {
//        PipeTile[] tiles = FindObjectsOfType<PipeTile>();

//        foreach (PipeTile tile in tiles)
//        {
//            // Pengecualian: Start & End biasanya tidak diacak
//            if (tile.isRotatable)
//            {
//                // 1. Acak rotasi dalam kelipatan 90 derajat (0, 90, 180, 270)
//                int randomRot = 90 * Random.Range(0, 4);
//                tile.transform.rotation = Quaternion.Euler(0, 0, randomRot);

//                // 2. PENTING: Panggil fungsi SetConnectionsFromType() dari PipeTile
//                //    Ini memperbarui properti tile.up/down/left/right sesuai rotasi visual baru.
//                tile.SetConnectionsFromType();
//            }
//        }

//        // PENTING: Setelah semua pipa diacak, langsung lakukan pengecekan jalur
//        // Ini memastikan level yang baru di-randomized langsung dicek apakah secara kebetulan sudah terhubung.
//        // Jika tidak, ini memastikan PipePathChecker terinisialisasi dan siap.
//        PipePathChecker checker = FindObjectOfType<PipePathChecker>();
//        if (checker != null)
//        {
//            // Cek Path bisa dipanggil di sini jika Anda ingin memastikan level awal tidak selesai.
//            // Atau, Anda bisa membiarkannya dipanggil saat pemain klik pipa pertama kali.
//            // Jika Anda TIDAK ingin level yang baru diacak langsung terhubung, panggil checker.CheckPath()
//            // dan ulangi pengacakan jika hasilnya TRUE.
//        }
//    }
//}