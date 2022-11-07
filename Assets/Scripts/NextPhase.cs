using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPhase : MonoBehaviour
{
   public void OnPress()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

}
