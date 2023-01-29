using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    float initPos, pos;
    int score;

    private void Start()
    {
        initPos = 1;
        pos = initPos;
        score = (int)(pos - initPos);

        if (text != null)
            text.text = "Score : " + score.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameplayManager.manager.playerTransform == null || text == null)
            return;

        pos = GameplayManager.manager.playerTransform.position.z;
        score = (int) (pos - initPos);

        GameplayManager.manager.score = score;
        text.text = "Score : " + score.ToString();
    }
}
