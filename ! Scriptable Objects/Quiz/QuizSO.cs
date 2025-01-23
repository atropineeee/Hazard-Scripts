using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quiz", menuName = "Quiz List / Create New Quiz")]
public class QuizSO : ScriptableObject
{
    public string QuizChapter;
    public string QuizLevel;

    public string Trivias;
    public AudioClip TriviaAudio;

    public QuizQuestion[] QuizQuestionList;
    public StorySO NextStory;
    public QuestSO NextQuest;
    public string NextScene;
    public LevelSelected LevelSelected;
}
