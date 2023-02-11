using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//1. Lakukan pengecekkan OnTriggerEnter jika suatu objek bertabrakan dgn collider ini. Simpan refernsinya.
//2. Cek dahulu string sceneTarget apakah sudah ada pada build setting atau belum.
//3. Jika kondisi 1 & 2 terpenuhi maka loadScene sesuai dengan scene tujuan
//4. Pada scene tujuan, atur titik spawn objek sesuai dengan spawnPoint pada objek SceneTeleporter yang lain yang ada pada scene terbaru.

[RequireComponent(typeof(BoxCollider2D))]
public class SceneTeleporter : MonoBehaviour
{
    [SerializeField] public string sceneTarget;
    [SerializeField] public TeleporterGate portalGate;
    [SerializeField] public Transform spawnPoint;

    private Transform objectToTeleport;

    private void Awake()
    {
        //Paksa BoxCollider2D untuk secara otomatis menjadi trigger
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ambil referensi objek yang bakal berpindah scene untuk diubah transform-nya nanti
        objectToTeleport = collision.GetComponent<Transform>();
        //Mulai teleport dan ubah spawnPoint si objek sesuai dgn tempat tujuan
        //Tempat tujuan : spawnPoint dari SceneTeleporter pada scene tujuan dengan kedua enum "TeleporterGate"-nya yang sama dengan SceneTeleporter sebelumnya
        StartCoroutine(SwitchScene());
    }

    IEnumerator SwitchScene()
    {
        Debug.Log("1. Object yang bakal teleport: " + objectToTeleport.name);
        //1. Untuk sementara jangan destroy dulu hingga eksekusi selesai.
        DontDestroyOnLoad(gameObject);

        //2. Lalu pindahkan scene-nya
        yield return SceneManager.LoadSceneAsync(sceneTarget, LoadSceneMode.Single);

        //3. Cari semua SceneTeleporter pada scene itu lalu ubah datanya dari Array<T> ke List<T>
        var arrayTeleporter = FindObjectsOfType<SceneTeleporter>();
        List<SceneTeleporter> listTeleporter = new List<SceneTeleporter>(arrayTeleporter);
        Debug.Log("SceneTeleporter yang ditemukan: " + listTeleporter.Count);
        //3. Dari List tsb, cari SceneTeleporter yang sesuai pencarian berikut
        //Pencarian 1 : Referensi portalnya bukan yang "this" (sama dengan reference portal awal), DAN
        //Pencarian 2 : Enum Gate tujuan akhir portalnya sama dengan yang sebelumnya
        SceneTeleporter targetGate = listTeleporter.Find(target => target != gameObject && target.portalGate == this.portalGate);

        //4. Atur posisi transform objek yang lewat sesuai dengan spawnPoint pada targetGate tujuan akhir
        objectToTeleport.position = targetGate.spawnPoint.transform.position;

        //5. Hancurkan game object setelah semua baris logika sebelumnya telah dijalankan
        //Debug.Log("4. EKSEKUSI SELESAI! " + this.gameObject.name + " akan hancur");
        Destroy(gameObject);
    }

   
    //Script referensi ke suatu objek


}

public enum TeleporterGate
{
    //Tambahkan sendiri enum-nya jika dirasa kurang. GATE_A = 0 dan terus incremen satu angka kebawah
    GATE_A,
    GATE_B,
    GATE_C,
    GATE_D,
    GATE_E,
    GATE_F
}
