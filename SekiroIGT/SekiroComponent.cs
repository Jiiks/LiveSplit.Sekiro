using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.TimeFormatters;
using LiveSplit.UI;
using LiveSplit.UI.Components;

namespace SekiroIGT {
    class SekiroComponent : IComponent {

        public string ComponentName => "Sekiro In-Game Time Component";
        public float HorizontalWidth => 0;
        public float MinimumHeight => 0;
        public float VerticalHeight => 0;
        public float MinimumWidth => 0;
        public float PaddingTop => 0;
        public float PaddingBottom => 0;
        public float PaddingLeft => 0;
        public float PaddingRight => 0;
        public IDictionary<string, Action> ContextMenuControls => null;

        private SekiroControl _control;
        private SekiroPointer _pointer;

        private InfoTimeComponent _infoTimeComponent;
        private RegularTimeFormatter _timeFormatter;

        private int _oldMillis;
        private bool _latch;

        public SekiroComponent(LiveSplitState state) {
            _control = new SekiroControl();
            _timeFormatter = new RegularTimeFormatter(TimeAccuracy.Hundredths);
            _infoTimeComponent = new InfoTimeComponent(ComponentName, TimeSpan.Zero, _timeFormatter);

            state.OnReset += delegate { _oldMillis = 0; };
            state.OnReset += delegate { _oldMillis = 0; };

            _pointer = new SekiroPointer();
        }

        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion) { }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion) {  }

        public Control GetSettingsControl(LayoutMode mode) => _control ?? (_control = new SekiroControl());

        public XmlNode GetSettings(XmlDocument document) => _control.GetSettings(document);

        public void SetSettings(XmlNode settings) => _control.SetSettings(settings);

        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) {

            int millis = _pointer.GetIgt(); //Native.GetGameTimeMilliseconds(addr, p.Id.GetHandle(), 8);
            if (millis > 100) {
                _oldMillis = millis;
                _latch = false;
            }

            if (millis == 0 && !_latch) {
                _oldMillis -= 594;
                _latch = true;
            }
            if (_oldMillis <= 0) _oldMillis = 0;

            state.SetGameTime(new TimeSpan(0, 0, 0, 0, _oldMillis <= 1 ? 1 : _oldMillis));
        }

        public void Dispose() => _infoTimeComponent.Dispose();
    }
}
