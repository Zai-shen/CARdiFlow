using UnityEngine;
using UnityEngine.UIElements;

public class TutorialDisplay : VisualElement
{
    public new class UxmlFactory : UxmlFactory<TutorialDisplay, UxmlTraits> { }

    private VisualElement m_TutorialScreen;
    private ScrollView m_ScrollView;

    public TutorialDisplay()
    {
        this.RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
        m_TutorialScreen = this;
    }

    void OnGeometryChange(GeometryChangedEvent evt)
    {
        m_ScrollView = this.Q<ScrollView>();

        Animate();

        this.UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }

    public void Animate()
    {
        if (m_ScrollView == null)
            return;

        m_ScrollView.transform.position = new Vector2(+1000, 0);
        m_ScrollView.experimental.animation.Position(new Vector2(0, 0), 500);
    }

}
