using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static public CameraManager cm;
    Camera camera;
    #region Singleton
    private void Awake()
    {
        if (cm == null)
        {
            cm = this;
            camera = GetComponent<Camera>();
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }
    #endregion

    public void SetCamera(GameObject target)
    {
        Vector3 vector = new Vector3(target.transform.position.x, target.transform.position.y, this.transform.position.z);
        transform.position = vector;
    }
    
    public void SetSize(int gameState)
    {
        camera.orthographicSize = 48 * 18 / 2;
       camera.aspect = 22f / 18f;
    }
}
