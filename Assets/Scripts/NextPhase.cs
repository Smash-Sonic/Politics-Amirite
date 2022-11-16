using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPhase : MonoBehaviour
{
    public AudioClip GameStart;
    public void OnPress()
    {
        if (CharacterSelect.HowManyEmployees == 5)
        {
            StartCoroutine(SoundEffect());
        }
    }

    IEnumerator SoundEffect()
    {
        Vector3 camPos = Camera.main.transform.position;
        AudioSource.PlayClipAtPoint(GameStart, camPos);
        yield return new WaitForSeconds(0.75f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
}
