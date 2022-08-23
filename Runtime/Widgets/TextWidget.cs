using Injections;
using UnityEngine.UI;

namespace Framework.Runtime.Core.Widgets
{
    public class TextWidget : Widget<Text, string>
    {
        protected override void OnViewAdded() => Refresh();

        protected override void OnAfterModelChanged() => Refresh();

        private void Refresh()
        {
            if (View != null)
            {
                if (Model != null)
                {
                    View.text = Model;
                }
                else
                {
                    View.text = "";
                }
            }
        }
    }

    public static class TextWidgetExtensions
    {
        public static TextWidget AddText(this Widget parent, Text view, string text)
        {
            var value = text;
            if (parent is IResolve resolve)
            {
                var localization = resolve.Resolve<ILocalization>();
                value = localization.Get(text);
            }

            var widget = new TextWidget();
            parent.AddWidget(widget);

            widget.SetView(view);
            widget.SetModel(value);

            return widget;
        }

        public static TextWidget AddText(this Widget parent, Text view, string format, params object[] keys)
        {
            if (parent is IResolve resolve)
            {
                var localization = resolve.Resolve<ILocalization>();
                for (var i = 0; i < keys.Length; i++)
                {
                    if (keys[i] is string)
                    {
                        keys[i] = localization.Get((string)keys[i]);
                    }
                }
            }

            var value = string.Format(format, keys);

            var widget = new TextWidget();
            parent.AddWidget(widget);

            widget.SetView(view);
            widget.SetModel(value);

            return widget;
        }
    }
}