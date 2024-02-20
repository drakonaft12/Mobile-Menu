using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LoadMagaz : MonoBehaviour
{
    [SerializeField] GameObject _gameObject;
    // Start is called before the first frame update
    async void Awake()
    {
        await Task.Delay(3000);
        _gameObject.SetActive(true);
        await Task.Delay(100);
        _gameObject.SetActive(false);
    }

    
   
}
