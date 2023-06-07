using System;
using System.Text;
using Sandbox;
using Sandbox.Graphics;
using Sandbox.Graphics.GUI;
using VRage;
using VRage.Utils;
using VRageMath;

namespace ClientPlugin.GUI
{

    public class MyPluginConfigDialog : MyGuiScreenBase
    {
        private const string Caption = "GPS Marker Scaling Configuration";
        public override string GetFriendlyName() => "MyPluginConfigDialog";

        private MyLayoutTable layoutTable;

        private MyGuiControlLabel scaleLabel;
        private MyGuiControlSlider scaleSlider;

        // TODO: Add member variables for your UI controls here

        private MyGuiControlMultilineText infoText;
        private MyGuiControlButton closeButton;
        private MyGuiControlAutoScaleText scaleText;

        public MyPluginConfigDialog() : base(new Vector2(0.5f, 0.5f), MyGuiConstants.SCREEN_BACKGROUND_COLOR, new Vector2(0.5f, 0.3f), false, null, MySandboxGame.Config.UIBkOpacity, MySandboxGame.Config.UIOpacity)
        {
            EnabledBackgroundFade = true;
            m_closeOnEsc = true;
            m_drawEvenWithoutFocus = true;
            CanHideOthers = true;
            CanBeHidden = true;
            CloseButtonEnabled = true;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            RecreateControls(true);
        }

        public override void RecreateControls(bool constructor)
        {
            base.RecreateControls(constructor);

            CreateControls();
            LayoutControls();
        }

        private void CreateControls()
        {
            AddCaption(Caption);

            var config = Plugin.Instance.Config;
            CreateSlider(out scaleLabel, out scaleSlider, config.Scale, value => config.Scale = value, "Marker Scale", "Changes the marker scale.");
            // TODO: Create your UI controls here

            /*infoText = new MyGuiControlMultilineText
            {
                Name = "InfoText",
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP,
                TextAlign = MyGuiDrawAlignEnum.HORISONTAL_LEFT_AND_VERTICAL_TOP,
                TextBoxAlign = MyGuiDrawAlignEnum.HORISONTAL_LEFT_AND_VERTICAL_TOP,
                // TODO: Add 2 short lines of text here if the player needs to know something. Ask for feedback here. Etc.
                Text = new StringBuilder("\r\nTODO")
            };*/

            closeButton = new MyGuiControlButton(position: new Vector2(0f, 0.1f),originAlign: MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_CENTER, text: MyTexts.Get(MyCommonTexts.Ok), onButtonClick: OnOk);
            Controls.Add(closeButton);
        }

        private void OnOk(MyGuiControlButton _) => CloseScreen();

        private void CreateCheckbox(out MyGuiControlLabel labelControl, out MyGuiControlCheckbox checkboxControl, bool value, Action<bool> store, string label, string tooltip)
        {
            labelControl = new MyGuiControlLabel
            {
                Text = label,
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP
            };

            checkboxControl = new MyGuiControlCheckbox(toolTip: tooltip)
            {
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP,
                Enabled = true,
                IsChecked = value
            };
            checkboxControl.IsCheckedChanged += cb => store(cb.IsChecked);
        }

        private void CreateSlider(out MyGuiControlLabel labelControl, out MyGuiControlSlider checkboxControl, float value, Action<float> store, string label, string tooltip)
        {
            labelControl = new MyGuiControlLabel
            {
                Text = label,
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP
            };

            checkboxControl = new MyGuiControlSlider(minValue: 0.018f, maxValue: 2.5f, toolTip: tooltip)
            {
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP,
                Enabled = true,
                Value = value
            };
            checkboxControl.ValueChanged += cb => store(cb.Value);
        }

        private void LayoutControls()
        {
            var size = Size ?? Vector2.One;
            layoutTable = new MyLayoutTable(this, new Vector2(-0.4f, -0.3f) * size, 0.6f * size);
            layoutTable.SetColumnWidths(200f, 100f);
            // TODO: Add more row heights here as needed
            layoutTable.SetRowHeights(90f, 60f);

            var row = 0;

            layoutTable.Add(scaleLabel, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(scaleSlider, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

            // TODO: Layout your UI controls here

            //layoutTable.Add(infoText, MyAlignH.Left, MyAlignV.Top, row, 0, colSpan: 2);
            //row++;

            //layoutTable.Add(closeButton, MyAlignH.Left, MyAlignV.Center, row, 0, colSpan: 2);
            // row++;
        }

        public override bool Draw()
        {
            base.Draw();

            MyGuiManager.DrawString("White", $"Scale: {Plugin.Instance.Config.Scale}", new Vector2(0.5f), 1f, null, MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_CENTER);

            return true;
        }
    }
}