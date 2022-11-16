using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance;
    public JsonSerializer Serializer;
    public TMP_InputField NumberInput;
    public TMP_InputField TextInput;
    public TMP_InputField Day;
    public TMP_InputField Phase;
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

 
}
