using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    public string Credits;
    public string MainMenu;
 
     public void Play()
    {
        GetComponent<Animation>();
        StartCoroutine(LoadAfterAnim());
    }

    public IEnumerator LoadAfterAnim()
    {
        yield return new WaitForSeconds(05);
        SceneManager.LoadScene(0);
    }
}
