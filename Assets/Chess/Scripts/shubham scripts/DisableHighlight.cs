using UnityEngine;

public class DisableHighlight : MonoBehaviour
{
    
    //Disable highlight 
    public void OnTriggerEnter2D(Collider2D collision)
    {        
        //print(collision.name + " with by:" + gameObject.name + " count:" + count);       
        gameObject.SetActive(false);

    }
}
