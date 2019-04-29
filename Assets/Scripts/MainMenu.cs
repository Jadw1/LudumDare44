using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    [SerializeField]
    private Animator creditsPanelAnimator;

    private void Start() {
        creditsPanelAnimator.enabled = false;
    }

    public void Play() {
        AudioHelper.instance.Play("click");
        SceneManager.LoadScene(1);
    }

    public void Exit() {
        AudioHelper.instance.Play("click");
    }

    public void RollCreditsIn() {
        AudioHelper.instance.Play("click");
        creditsPanelAnimator.enabled = true;
        creditsPanelAnimator.Play("SlideIn");
    }

    public void RollCreditsOut() {
        AudioHelper.instance.Play("click");
        creditsPanelAnimator.enabled = true;
        creditsPanelAnimator.Play("SlideOut");
    }
}
