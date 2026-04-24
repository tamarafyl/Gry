using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem; // Додано для роботи з новою системою

public class DemonMovement : MonoBehaviour {
    private Animator anim;
    int hIdles;
    int hAngry;
    int hAttack;
    int hGrabs;
    int hThumbsUp;
        
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator> ();
        hIdles = Animator.StringToHash("Idles");
        hAngry = Animator.StringToHash("Angry");
        hAttack = Animator.StringToHash("Attack");
        hGrabs = Animator.StringToHash("Grabs");
        hThumbsUp = Animator.StringToHash("ThumbsUp");
    }
    
    // Update is called once per frame
    void Update () {
        // Отримуємо доступ до клавіатури
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        // Заміна Input.GetKeyDown на keyboard.xxxKey.wasPressedThisFrame
        if (keyboard.wKey.wasPressedThisFrame) {
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idles")) {
                anim.SetBool(hIdles, false);
                anim.SetBool(hAngry, true);
            }
        } else if (keyboard.sKey.wasPressedThisFrame) {
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idles")) {
                anim.SetBool(hIdles, false);
                anim.SetBool(hAttack, true);
            }
        } else if (keyboard.aKey.wasPressedThisFrame) {
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idles")) {
                anim.SetBool(hIdles, false);
                anim.SetBool(hGrabs, true);
            }
        } else if (keyboard.dKey.wasPressedThisFrame) {
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idles")) {
                anim.SetBool(hIdles, false);
                anim.SetBool(hThumbsUp, true);
            }
        } else {
            // Ця частина коду спрацьовує, якщо в поточному кадрі не була НАТИСНУТА жодна з клавіш W, A, S, D
            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Idles")) {
                anim.SetBool(hIdles, true);
                anim.SetBool(hAngry, false);
                anim.SetBool(hAttack, false);
                anim.SetBool(hGrabs, false);
                anim.SetBool(hThumbsUp, false);
            }
        }
    }
}