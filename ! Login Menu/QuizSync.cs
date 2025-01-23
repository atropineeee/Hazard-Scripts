using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuizSync
{
    #region
    public CanvasHolder CVHolder;
    public QuizSync(CanvasHolder cVHolder)
    {
        CVHolder = cVHolder;
    }
    #endregion

    public void SyncQuizFromDatabase()
    {
        CVHolder.Chp1Lvl1Quiz.QuizQuestionList = new QuizQuestion[0];

        CVHolder.FirebaseFirestore.Collection("questions").GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                QuerySnapshot allSnapshot = task.Result;
                List<QuizQuestion> tempQuiz1List = new List<QuizQuestion>();
                List<QuizQuestion> tempQuiz2List = new List<QuizQuestion>();
                List<QuizQuestion> tempQuiz3List = new List<QuizQuestion>();
                List<QuizQuestion> tempQuiz4List = new List<QuizQuestion>();
                List<QuizQuestion> tempQuiz5List = new List<QuizQuestion>();

                foreach (DocumentSnapshot document in allSnapshot.Documents)
                {
                    if (document.Exists)
                    {
                        object chapter = document.GetValue<object>("chapter");
                        object level = document.GetValue<object>("level");

                        // Chapter 1 Level 1
                        if (chapter.ToString() == "1" && level.ToString() == "1")
                        {
                            object question = document.GetValue<object>("question");
                            object[] choicesArray = document.GetValue<object[]>("choices");
                            object rightAnswer = document.GetValue<object>("answer");
                            RightAnswer rightAns = (RightAnswer)Enum.Parse(typeof(RightAnswer), rightAnswer.ToString());

                            for (int i = 0; i < choicesArray.Length; i++)
                            {
                                var choiceObject = choicesArray[i] as Dictionary<string, object>;
                            }

                            QuizQuestion nyahh = new QuizQuestion
                            {
                                Question = question.ToString(),
                                Choices = choicesArray.Select(c => ((Dictionary<string, object>)c)["choice"].ToString()).ToArray(),
                                RightAnswer = rightAns
                            };

                            tempQuiz1List.Add(nyahh);
                        }

                        if (chapter.ToString() == "1" && level.ToString() == "2")
                        {
                            object question = document.GetValue<object>("question");
                            object[] choicesArray = document.GetValue<object[]>("choices");
                            object rightAnswer = document.GetValue<object>("answer");
                            RightAnswer rightAns = (RightAnswer)Enum.Parse(typeof(RightAnswer), rightAnswer.ToString());

                            for (int i = 0; i < choicesArray.Length; i++)
                            {
                                var choiceObject = choicesArray[i] as Dictionary<string, object>;
                            }

                            QuizQuestion nyahh = new QuizQuestion
                            {
                                Question = question.ToString(),
                                Choices = choicesArray.Select(c => ((Dictionary<string, object>)c)["choice"].ToString()).ToArray(),
                                RightAnswer = rightAns
                            };

                            tempQuiz2List.Add(nyahh);
                        }

                        if (chapter.ToString() == "2" && level.ToString() == "3")
                        {
                            object question = document.GetValue<object>("question");
                            object[] choicesArray = document.GetValue<object[]>("choices");
                            object rightAnswer = document.GetValue<object>("answer");
                            RightAnswer rightAns = (RightAnswer)Enum.Parse(typeof(RightAnswer), rightAnswer.ToString());

                            for (int i = 0; i < choicesArray.Length; i++)
                            {
                                var choiceObject = choicesArray[i] as Dictionary<string, object>;
                            }

                            QuizQuestion nyahh = new QuizQuestion
                            {
                                Question = question.ToString(),
                                Choices = choicesArray.Select(c => ((Dictionary<string, object>)c)["choice"].ToString()).ToArray(),
                                RightAnswer = rightAns
                            };

                            tempQuiz3List.Add(nyahh);
                        }

                        if (chapter.ToString() == "2" && level.ToString() == "4")
                        {
                            object question = document.GetValue<object>("question");
                            object[] choicesArray = document.GetValue<object[]>("choices");
                            object rightAnswer = document.GetValue<object>("answer");
                            RightAnswer rightAns = (RightAnswer)Enum.Parse(typeof(RightAnswer), rightAnswer.ToString());

                            for (int i = 0; i < choicesArray.Length; i++)
                            {
                                var choiceObject = choicesArray[i] as Dictionary<string, object>;
                            }

                            QuizQuestion nyahh = new QuizQuestion
                            {
                                Question = question.ToString(),
                                Choices = choicesArray.Select(c => ((Dictionary<string, object>)c)["choice"].ToString()).ToArray(),
                                RightAnswer = rightAns
                            };

                            tempQuiz4List.Add(nyahh);
                        }

                        if (chapter.ToString() == "3" && level.ToString() == "5")
                        {
                            object question = document.GetValue<object>("question");
                            object[] choicesArray = document.GetValue<object[]>("choices");
                            object rightAnswer = document.GetValue<object>("answer");
                            RightAnswer rightAns = (RightAnswer)Enum.Parse(typeof(RightAnswer), rightAnswer.ToString());

                            for (int i = 0; i < choicesArray.Length; i++)
                            {
                                var choiceObject = choicesArray[i] as Dictionary<string, object>;
                            }

                            QuizQuestion nyahh = new QuizQuestion
                            {
                                Question = question.ToString(),
                                Choices = choicesArray.Select(c => ((Dictionary<string, object>)c)["choice"].ToString()).ToArray(),
                                RightAnswer = rightAns
                            };

                            tempQuiz5List.Add(nyahh);
                        }
                    }
                }

                CVHolder.Chp1Lvl1Quiz.QuizQuestionList = tempQuiz1List.ToArray();
                CVHolder.Chp1Lvl2Quiz.QuizQuestionList = tempQuiz2List.ToArray();
                CVHolder.Chp2Lvl3Quiz.QuizQuestionList = tempQuiz3List.ToArray();
                CVHolder.Chp2Lvl4Quiz.QuizQuestionList = tempQuiz4List.ToArray();
                CVHolder.Chp3Lvl5Quiz.QuizQuestionList = tempQuiz5List.ToArray();
            }
            else 
            {
                Debug.LogError(task.Exception);
            }
        });
    }
}
