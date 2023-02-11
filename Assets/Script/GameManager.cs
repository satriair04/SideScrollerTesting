using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Root yang terpasang script ini beserta semua childnya ga bakal di-destroy
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    //[Header("Scene Management")]
    //public string defaultScene;
    //private string currentScene;

    [Header("Additive Scene Load")]
    //[SerializeField] private bool useAddiviteLoad = true;
    [SerializeField] private string initialScene = "Map001";
    
    void Awake()
    {
        CreateInstance();
    }

    private void Start()
    {
        //Load additive untuk pertama kali untuk membawa GameObject penting ke scene yang lain
        SceneManager.LoadSceneAsync(initialScene, LoadSceneMode.Additive);
        //NB: Jangan lupa taro juga di build settingnya
    }

    /*
     *
    public void BackToMain()
    {
        //BELUM ADA SCENE LAIN
        //Kembali ke scene MainMenuAdditive, Hapus objek ini agar ga muncul di scene berikutnya.
        SceneManager.LoadScene("MainMenuAdditive", LoadSceneMode.Single);
        Destroy(gameObject);
    }
    */

    private void CreateInstance()
    {
        
        if(_instance != null)
        {       
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this);
    }
}
