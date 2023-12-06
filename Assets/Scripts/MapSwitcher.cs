using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject playerCam;
    public bool mapIsActive = false;
    [SerializeField] private GameObject mapDot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("m"))
        {
            if(mapIsActive)
            {
                playerCam.SetActive(true);
                mapDot.SetActive(false);
                StartCoroutine(ResetTimeScale());
            }
            else if (!mapIsActive)
            {
                playerCam.SetActive(false);
                mapDot.SetActive(true);
                Time.timeScale = 0.05f;
            }
            mapIsActive = !mapIsActive;
        }
    }

    public IEnumerator ResetTimeScale()
    {
        yield return new WaitForSeconds(0.025f);
        Time.timeScale = 1f;
    }
}
