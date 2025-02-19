using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VialScript : MonoBehaviour
{
    public GameObject elevatorCollider;
    // Start is called before the first frame update
    void Start()
    {
        elevatorCollider.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            elevatorCollider.SetActive(true);
            Destroy(gameObject);
            SceneManager.LoadScene("YouWin");
        }
    }
}
