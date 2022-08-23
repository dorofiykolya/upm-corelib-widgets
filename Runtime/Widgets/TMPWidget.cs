using System.Text;
using Injections;
using TMPro;

namespace Framework.Runtime.Core.Widgets
{
    public class TMPWidget : Widget<TMP_Text, string>
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

    public static class TMPWidgetExtensions
    {
        public static TMPWidget AddText(this Widget parent, TMP_Text view, string text)
        {
            var value = text;
            if (parent is IResolve resolve)
            {
                var localization = resolve.Resolve<ILocalization>();
                value = localization.Get(text);
            }

            if (string.IsNullOrEmpty(value))
                value = text;

            var widget = new TMPWidget();
            parent.AddWidget(widget);

            widget.SetView(view);
            widget.SetModel(value);

            return widget;
        }

        public static TMPWidget AddText(this Widget parent, TMP_Text view, string format, params object[] keys)
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

            var widget = new TMPWidget();
            parent.AddWidget(widget);

            widget.SetView(view);
            widget.SetModel(value);

            return widget;
        }

        public static void UpdateText(this Widget parent, TMPWidget textWidget, string format, params object[] keys)
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

            textWidget.SetModel(value);
        }

        public static void AddText(this Widget parent, TMPWidget textWidget, string text)
        {
            var value = text;
            if (parent is IResolve resolve)
            {
                var localization = resolve.Resolve<ILocalization>();
                value = localization.Get(text);
            }

            if (string.IsNullOrEmpty(value))
                value = text;

            textWidget.SetModel(value);
        }
    }
}