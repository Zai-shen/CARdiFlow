using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSDisplay : UnitySingleton<FPSDisplay>
{
	[SerializeField]
	private bool show = false;
	private float deltaTime = 0.0f;

    protected override void Awake()
    {
		base.Awake();
		DontDestroyOnLoad(this);
    }

    void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	}

	public void Show(bool doShow) {
		show = doShow;
	}

	void OnGUI()
	{
        if (!show)
			return;

		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		GUI.Label(rect, text, style);
	}
}