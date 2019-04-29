using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageDisplay : GenericSingleton<MessageDisplay> {
    private GameObject panel;
    private TextMeshProUGUI text;
    private Image avatar;

    private Queue<Message> queue = new Queue<Message>();
    private Message current = null;
    private int currentPage = -1;

    [SerializeField]
    private float slideDuration = 3.0f;

    private float timer = 0.0f;

    private bool shown = false;

    public class Message {
        public string[] text;
        public Delegate callback;

        public Message(string[] text, Action callback) {
            this.text = text;
            this.callback = callback;
        }
    }

    private void Show(bool doShow) {
        if (doShow) {
            panel.SetActive(true);
            avatar.enabled = true;
            currentPage = -1;
        }
        else {
            panel.SetActive(false);
            avatar.enabled = false;
            AudioHelper.instance.Play("book_close");
        }

        shown = doShow;
    }

    private void Start() {
        panel = transform.GetChild(0).gameObject;
        avatar = transform.GetChild(1).GetComponent<Image>();
        text = panel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        panel.SetActive(false);
        avatar.enabled = false;
        shown = false;
    }

    private void Refresh() {
        timer = 0.0f;

        if (current == null && queue.Count != 0) {
            current = queue.Dequeue();
        }

        if (current != null) {
            if (!shown) Show(true);
            currentPage++;

            if (currentPage == current.text.Length) {
                if (current.callback != null) {
                    current.callback.DynamicInvoke();
                }

                current = null;
            }
            else {
                text.text = current.text[currentPage];
                
                if (currentPage == 0) {
                    AudioHelper.instance.Play("book_open");
                } else {
                    if (currentPage % 2 == 0) {
                        AudioHelper.instance.Play("book_flip1");
                    }
                    else {
                        AudioHelper.instance.Play("book_flip2");
                    }
                }
            }
        }

        if (current == null && shown) {
            Show(false);
        }
    }

    private void Update() {

        if (timer == 0.0f) {
            Refresh();
        }

        timer += Time.deltaTime;

        if (timer >= slideDuration) {
            timer = 0.0f;
        }
    }

    public void ShowMessage(Message msg) {
        queue.Enqueue(msg);
        
        if (current == null) {
            //Refresh();
        }
    }
}