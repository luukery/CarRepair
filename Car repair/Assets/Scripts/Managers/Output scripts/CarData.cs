using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarData : CarManager<CarData>
{
    [System.Serializable]
    public struct Carparts
    {
        public string name;
        public GameObject actualObject;
        public Quaternion startRotation;
        public Quaternion openRotation;
    }

   [SerializeField] public Carparts[] carParts;



    
    public IEnumerator Open(Quaternion rotation, float time)
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }
}
