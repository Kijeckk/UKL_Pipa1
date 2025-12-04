//using UnityEngine;

//public class PipeTile : MonoBehaviour
//{
//    // Enum untuk mendefinisikan tipe pipa
//    public enum PipeType { Empty, Straight, Elbow, T, Cross, Start, End }

//    [Header("Pipe Configuration")]
//    public PipeType type;
//    public bool isRotatable = true;

//    // Koneksi logis. [HideInInspector] agar tidak memenuhi Inspector.
//    [HideInInspector] public bool up, down, left, right;

//    private PipePathChecker _checker;

//    void Awake()
//    {
//        // Mendapatkan referensi checker hanya sekali
//        _checker = FindObjectOfType<PipePathChecker>();
//        if (_checker == null)
//        {
//            Debug.LogError("PipePathChecker TIDAK DITEMUKAN di scene. Pengecekan jalur tidak akan berfungsi.");
//        }
//    }

//    void Start()
//    {
//        // Pastikan rotasi awal disetel ke 90 derajat terdekat
//        SnapRotationTo90();

//        // set koneksi awal sesuai tipe dan rotasi
//        SetConnectionsFromType();
//    }

//    // Dipanggil saat pemain klik pipa
//    void OnMouseDown()
//    {
//        if (!isRotatable) return;

//        Rotate90();
//        // Setelah rotasi, update koneksi logis
//        SetConnectionsFromType();

//        // Cek Path
//        if (_checker != null)
//        {
//            bool pathFound = _checker.CheckPath();

//            if (pathFound)
//            {
//                Debug.Log("LEVEL SELESAI! 🎉");

//                // Panggil LevelComplete (Pastikan LevelComplete ada dan berfungsi)
//                LevelComplete lc = FindObjectOfType<LevelComplete>();
//                if (lc != null)
//                {
//                    lc.ShowFinish();
//                }
//            }
//        }
//    }

//    // Rotasi visual + men-snap ke 90 derajat
//    void Rotate90()
//    {
//        // Rotasi -90 derajat (Clockwise)
//        transform.Rotate(0f, 0f, -90f);
//        SnapRotationTo90();
//    }

//    // PipeTile.cs

//    void SnapRotationTo90()
//    {
//        float z = transform.eulerAngles.z;

//        // 1. Pembulatan yang ketat ke kelipatan 90 terdekat
//        z = Mathf.Round(z / 90f) * 90f;

//        // 2. Normalisasi, pastikan 360 menjadi 0
//        if (z >= 360f)
//        {
//            z -= 360f;
//        }

//        // 3. Set rotasi yang sudah bersih
//        transform.eulerAngles = new Vector3(0f, 0f, z);
//    }

//    // Set koneksi boolean (up/down/left/right) berdasarkan tipe + rotation
//    public void SetConnectionsFromType()
//    {
//        // 1. Reset koneksi
//        up = down = left = right = false;

//        // 2. Hitung orientasi rotasi (0, 90, 180, 270)
//        int angle = Mathf.RoundToInt(transform.eulerAngles.z);
//        // Normalisasi angle (memastikan nilai antara 0 sampai 359)
//        angle = ((angle % 360) + 360) % 360;

//        // 3. Terapkan koneksi berdasarkan tipe dan rotasi
//        switch (type)
//        {
//            case PipeType.Straight:
//                // 0/180 (Vertikal): up, down. 90/270 (Horizontal): left, right
//                if (angle == 0 || angle == 180) { up = true; down = true; }
//                else { left = true; right = true; }
//                break;

//            case PipeType.Elbow:
//                // 0: up+right | 90: right+down | 180: down+left | 270: left+up
//                if (angle == 0) { up = true; right = true; }
//                else if (angle == 90) { right = true; down = true; }
//                else if (angle == 180) { down = true; left = true; }
//                else /*270*/ { left = true; up = true; }
//                break;

//            case PipeType.T:
//                // 0: up+left+right | 90: up+right+down | 180: right+down+left | 270: up+left+down
//                if (angle == 0) { up = true; left = true; right = true; }
//                else if (angle == 90) { up = true; right = true; down = true; }
//                else if (angle == 180) { right = true; down = true; left = true; }
//                else /*270*/ { up = true; left = true; down = true; }
//                break;

//            case PipeType.Cross:
//                // Semua sisi terbuka
//                up = down = left = right = true;
//                break;

//            // Start dan End hanya memiliki satu sisi yang terbuka
//            case PipeType.Start:
//            case PipeType.End:
//                if (angle == 0) up = true;
//                else if (angle == 90) right = true;
//                else if (angle == 180) down = true;
//                else left = true;
//                break;

//            case PipeType.Empty:
//                // Tidak ada koneksi
//                break;
//        }
//    }
//}