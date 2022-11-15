using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPhase : MonoBehaviour

{
    public void OnPress()
    {
        if (CharacterSelect.HowManyEmployees == 5)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        }
    }
}
