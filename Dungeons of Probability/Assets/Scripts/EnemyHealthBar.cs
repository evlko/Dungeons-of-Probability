using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public GameObject healthLevel;
    public GameObject healthBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangePosition(float pos){
        Transform healthBarTransform = healthBar.GetComponent<Transform>();    
        healthBarTransform.transform.localPosition = new Vector3 (0, pos, 0); 
    }

    public void ResetHealthBar(){
        StopAllCoroutines();
        Transform healthLevelTransform = healthLevel.GetComponent<Transform>();    
        healthLevelTransform.transform.localScale = new Vector3 (33, 1, 1);
    }

    public IEnumerator ScaleObject(float scale, float duration)
    {    
        Transform healthLevelTransform = healthLevel.GetComponent<Transform>();             
        Vector3 actualScale = healthLevelTransform.transform.localScale;    
        Vector3 targetScale = new Vector3 (scale, 1, 1);   
        
        for(float t = 0; t < 1; t += Time.deltaTime / duration)
        {
            healthLevelTransform.transform.localScale = Vector3.Lerp(actualScale ,targetScale ,t);
            yield return null;
        }
    }
}
