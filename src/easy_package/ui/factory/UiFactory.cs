using rose.row.easy_package.ui.factory.elements;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace rose.row.easy_package.ui.factory
{
    public static class UiFactory
    {
        public static UiScreen createUiScreen(string name, int order = 2, Transform parent = null)
        {
            var gameObject = new GameObject(name);
            if (parent != null)
                gameObject.transform.SetParent(parent);

            var canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = order;

            var renderer = gameObject.AddComponent<CanvasRenderer>();
            var raycaster = gameObject.AddComponent<GraphicRaycaster>();
            var scaler = gameObject.AddComponent<CanvasScaler>();
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;

            return new UiScreen(gameObject, canvas, renderer, raycaster, scaler);
        }

        public static UiElement createGenericUiElement(string name, Transform parent)
            => createUiElement<UiElement>(name, parent);

        public static UiElement createGenericUiElement(string name, UiScreen screen)
            => createGenericUiElement(name, screen.transform);

        public static UiElement createGenericUiElement(string name, UiElement element)
            => createGenericUiElement(name, element.transform);

        public static T createUiElement<T>(string name, UiScreen screen) where T : UiElement
            => createUiElement<T>(name, screen.transform);

        public static T createUiElement<T>(string name, UiElement element) where T : UiElement
            => createUiElement<T>(name, element.transform);

        public static T createUiElement<T>(string name, Transform parent) where T : UiElement
        {
            return (T) createUiElement(typeof(T), name, parent);
        }

        public static UiElement createUiElement(Type type, string name, UiElement element)
            => createUiElement(type, name, element.transform);

        public static UiElement createUiElement(Type type, string name, UiScreen screen)
            => createUiElement(type, name, screen.transform);

        public static UiElement createUiElement(Type type, string name, Transform parent)
        {
            var gameObject = new GameObject(name);
            var rectTransform = gameObject.AddComponent<RectTransform>();
            rectTransform.SetParent(parent);
            rectTransform.anchoredPosition = Vector2.zero;
            return (UiElement) rectTransform.gameObject.AddComponent(type);
        }
    }
}