using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenas1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EmpezarJuego()
    {
        SceneManager.LoadScene("Principal");
    }
    public void CerrarJuego()
    {
        Application.Quit();
    }
}
