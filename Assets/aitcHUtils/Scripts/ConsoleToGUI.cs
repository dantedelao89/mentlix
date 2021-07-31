using UnityEngine;
using System.Collections.Generic;
using System;

namespace DebugStuff
{
    public class ConsoleToGUI : MonoBehaviour
    {
        //#if !UNITY_EDITOR
        static string myLog = "";
        List<LogData> allLogs = new List<LogData>();
        List<LogData> logsToShow = new List<LogData>();

        private bool showConsole = true;
        private bool allowConsole = true;

        private int fontSize = 12;

        private bool allowStackTrace = false;
        private bool allowLogs = true;
        private bool allowWarnings = true;
        private bool allowErrors = true;
        private bool isCollapsed = false;
        private bool showLatest = false;

        private int clickCount = 0;
        private float clickElapsed = 0;

        private Vector2 scrollPosition = Vector2.zero;


        void Awake()
        {
            //callback += Log;
            Application.logMessageReceived += Log;
        }

        private void OnDestroy()
        {
            Application.logMessageReceived -= Log;
        }

        //void OnDisable()
        //{
        //    Application.logMessageReceived -= Log;
        //    //Application.logMessageReceivedThreaded -= Log;
        //    //callback -= Log;
        //}

        public void Log(string logString, string stackTrace, LogType type)
        {
            int logCount = 1;

            for (int i = 0; i < allLogs.Count; i++)
            {
                if (allLogs[i].Output == logString) 
                {
                    logCount++;
                    allLogs[i].isLast = false;
                }
            }

            allLogs.Add(new LogData(logString, stackTrace, type, logCount));

            UpdateLogs();

            //myLog = type + ": " + output + "\nStack: " + stack + "\n" + myLog;
            if (myLog.Length > 5000)
            {
                myLog = myLog.Substring(0, 4000);
            }
        }

        private void UpdateLogs()
        {
            myLog = "";
            logsToShow.Clear();
            foreach (var log in allLogs)
            {
                if (log.GetLogType() == LogType.Log && !allowLogs)
                    continue;

                if (log.GetLogType() == LogType.Warning && !allowWarnings)
                    continue;

                if ((log.GetLogType() == LogType.Error || log.GetLogType() == LogType.Assert || log.GetLogType() == LogType.Exception) && !allowErrors)
                    continue;

                if (isCollapsed)
                {
                    if (!log.isLast)
                        continue;
                }

                myLog += log.GetDiplayMessage(allowStackTrace, isCollapsed) + "\n";
            }
        }

        void OnGUI()
        {
            if (allowConsole)
            {
                if (GUI.Button(new Rect(5, 5, Screen.width * 0.1f, Screen.width * 0.05f), showConsole ? "▲" : "▼"))
                {
                    showConsole = !showConsole;
                }

                if (showConsole) //Do not display in editor ( or you can use the UNITY_EDITOR macro to also disable the rest)
                {
                    GUIStyle logColor = new GUIStyle(GUI.skin.button);
                    GUIStyle warningColor = new GUIStyle(GUI.skin.button);
                    GUIStyle errorColor = new GUIStyle(GUI.skin.button);
                    GUIStyle disableColor = new GUIStyle(GUI.skin.button);

                    logColor.normal.textColor = Color.white;
                    warningColor.normal.textColor = Color.yellow;
                    errorColor.normal.textColor = Color.red;
                    disableColor.normal.textColor = Color.black;

                    GUI.skin.button.normal.textColor = Color.white;

                    float width = Screen.width - 20;
                    float height = Screen.height - ((Screen.height / 1.5f) - 10);

                    //GUISkin skin = new GUISkin();
                    GUI.skin.textArea.fontSize = fontSize;
                    GUI.skin.button.fontSize = (int)(Screen.width * 0.03f);
                    GUI.skin.textField.fontSize = (int)(Screen.width * 0.03f);
                    GUI.skin.textField.alignment = TextAnchor.MiddleCenter;

                    #region ControlBtns
                    if (GUI.Button(new Rect(
                                            //Screen.width - Screen.width * 0.1f - 10f,
                                            Screen.width - (1f * (0.05f * (Screen.width)) + 10f + (5f * (0))),
                                            Screen.height - height - Screen.width * 0.055f ,
                                            Screen.width * 0.05f,
                                            Screen.width * 0.05f), "X")) // Clear Logs
                    {
                        myLog = "";
                        allLogs.Clear();
                    }

                    if (GUI.Button(new Rect(Screen.width - (2f * (0.05f * (Screen.width)) + 10f + (5f * (1))),
                                            Screen.height - height - Screen.width * 0.055f,
                                            Screen.width * 0.05f,
                                            Screen.width * 0.05f), "S", allowStackTrace ? GUI.skin.button : disableColor)) // Allow Stack
                    {
                        allowStackTrace = !allowStackTrace;
                        UpdateLogs();
                    }

                    try
                    {
                        fontSize = int.Parse(GUI.TextField(new Rect
                                            (Screen.width - (3f * (0.05f * (Screen.width)) + 10f + (5f * (2))),
                                            Screen.height - height - Screen.width * 0.055f,
                                            Screen.width * 0.05f,
                                            Screen.width * 0.05f), fontSize > 0 ? fontSize.ToString() : ""));
                    }
                    catch (Exception)
                    {
                        fontSize = 0;
                    }

                    if (GUI.Button(new Rect(Screen.width - (4f * (0.05f * (Screen.width)) + 10f + (5f * (3))),
                        Screen.height - height - Screen.width * 0.055f,
                        Screen.width * 0.05f,
                        Screen.width * 0.05f), "C", isCollapsed ? GUI.skin.button : disableColor)) // Collapse
                    {
                        isCollapsed = !isCollapsed;
                        UpdateLogs();
                    }


                    if (GUI.Button(new Rect(Screen.width - (5f * (0.05f * (Screen.width)) + 10f + (5f * (4))),
                        Screen.height - height - Screen.width * 0.055f,
                        Screen.width * 0.05f,
                        Screen.width * 0.05f), "N", showLatest ? GUI.skin.button : disableColor)) // Latest
                    {
                        showLatest = !showLatest;
                        UpdateLogs();
                    }

                    if (GUI.Button(new Rect(10,
                                            Screen.height - height - Screen.width * 0.055f,
                                            Screen.width * 0.05f,
                                            Screen.width * 0.05f), "L", allowLogs ? logColor : disableColor)) // Log Btn
                    {
                        allowLogs = !allowLogs;
                        UpdateLogs();
                    }

                    if (GUI.Button(new Rect(15 + Screen.width * 0.05f,
                                            Screen.height - height - Screen.width * 0.055f,
                                            Screen.width * 0.05f,
                                            Screen.width * 0.05f), "W", allowWarnings ? warningColor : disableColor)) // Warning Btn
                    {
                        allowWarnings = !allowWarnings;
                        UpdateLogs();
                    }

                    if (GUI.Button(new Rect(20 + Screen.width * 0.05f + Screen.width * 0.05f,
                                            Screen.height - height - Screen.width * 0.055f,
                                            Screen.width * 0.05f,
                                            Screen.width * 0.05f), "E", allowErrors ? errorColor : disableColor)) // Error Btn
                    {
                        allowErrors = !allowErrors;
                        UpdateLogs();
                    }
                    #endregion

                    float textAreaHeight = GUI.skin.textArea.CalcHeight(new GUIContent(myLog), width);

                    if(showLatest)
                        scrollPosition = new Vector2(0, textAreaHeight);

                    scrollPosition = GUI.BeginScrollView(new Rect(10, Screen.height - height, width, height), scrollPosition, new Rect(10, Screen.height - height, width, textAreaHeight));
                    myLog = GUI.TextArea(new Rect(10, Screen.height - height, width, textAreaHeight), myLog);
                    GUI.EndScrollView();
                }
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Space");
            }

            if (Input.GetMouseButtonDown(0)) 
            {
                clickCount++;
                clickElapsed = 0;

                if (clickCount >= 3)
                {
                    allowConsole = !allowConsole;
                    clickCount = 0;
                }
            }

            if (clickCount > 0) 
            {
                clickElapsed += Time.deltaTime;

                if (clickElapsed > 0.2f) 
                {
                    clickCount = 0;
                    clickElapsed = 0;
                }
            }
        }
    }
}

[System.Serializable]
public class LogData 
{
    private string output;
    public string Output { get { return output; } }
    private string stack;
    private LogType logType;
    private int logCount;
    public int Count { get { return logCount; } }
    public string fullMessage;
    public bool isLast = true;

    public LogData(string logString, string stackTrace, LogType type, int count) 
    {
        this.output = logString;
        this.stack = stackTrace;
        this.logType = type;
        this.logCount = count;
        this.isLast = true;

        fullMessage = logType + ": " + output + "\n" + "Stack: " + stack;
    }

    public string GetDiplayMessage(bool allowStack, bool isCollapsed) 
    {
        string finalMessage = fullMessage;

        if (!allowStack)
            finalMessage = finalMessage.Replace("Stack: " + stack, "");

        if (isCollapsed)
            finalMessage = "(" + Count + ") " + finalMessage;

        return finalMessage;
    }

    public LogType GetLogType() 
    {
        return logType;
    }
}