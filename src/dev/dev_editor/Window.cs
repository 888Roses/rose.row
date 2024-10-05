using rose.row.easy_package.ui.factory;
using rose.row.easy_package.ui.factory.elements;
using System;
using System.Collections.Generic;

namespace rose.row.dev.dev_editor
{
    public abstract class Window : UiElement
    {
        protected List<AbstractWindowModule> _modules = new List<AbstractWindowModule>();

        public new abstract string name { get; }
        public abstract bool isGameOnly { get; }

        public void registerModule(AbstractWindowModule module)
        {
            module.parent = this;
            _modules.Add(module);
        }

        protected virtual void updateModules()
        {
            foreach (var module in _modules)
                module.update();
        }

        protected virtual void Update()
        {
            updateModules();
        }

        public Button button(string name, UiElement container, string text, bool highlighted = false, Action onClick = null)
        {
            var btn = UiFactory.createUiElement<Button>(name, container);
            btn.setText(text);
            btn.onClick = onClick;
            btn.setBackgroundColor(highlighted ? "#3B83BD" : "#0A0A0A");
            btn.setHeight(28f);

            return btn;
        }

        public virtual bool canBuild()
        {
            return isGameOnly ? GameManager.IsIngame() : true;
        }
    }
}
