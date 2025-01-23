using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuizQuestion
{
    public string Question;
    public string[] Choices;
    public RightAnswer RightAnswer;
}

public enum RightAnswer { A, B, C, D}
