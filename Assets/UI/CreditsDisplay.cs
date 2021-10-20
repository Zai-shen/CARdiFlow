using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Reflection;
using System.Linq;

public class CreditsDisplay : VisualElement
{
    public new class UxmlFactory : UxmlFactory<CreditsDisplay, UxmlTraits> { }

    private VisualElement m_CreditsScreen;
    private ScrollView m_ScrollView;

    public CreditsDisplay()
    {
        this.RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
        m_CreditsScreen = this;
    }

    void OnGeometryChange(GeometryChangedEvent evt)
    {
        m_ScrollView = this.Q<ScrollView>();

        Debug.Log(evt);

        Animate();

        this.UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }

    public void Animate()
    {
        if (m_ScrollView == null)
            return;

        m_ScrollView.transform.position = new Vector2(-1000, 0);
        m_ScrollView.experimental.animation.Position(new Vector2(0, 0), 500);
    }

}
