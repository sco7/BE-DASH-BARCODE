using Neodynamic.SDK.Printing;
using System;
using System.Collections.Generic;

namespace FontaineVerificationProject.Labels
{
    public class V2Label : ThermalLabel
    {
        public V2Label(int serialNo, int chassisNo) : base(UnitType.Inch, 3.90, 1.34)
        {                  
            var serialNoLabel = new TextItem(0.50, 1.00, 2.5, 0.5, "Serial Number");
            serialNoLabel.Font.Name = Font.NativePrinterFontB;
            serialNoLabel.Font.Size = 10;

            var chassisNoLabel = new TextItem(2.20, 1.00, 2.5, 0.5, "Chassis Number");
            chassisNoLabel.Font.Name = Font.NativePrinterFontB;
            chassisNoLabel.Font.Size = 10;

            var bcItemSerialNo = new BarcodeItem(0.40, 0.22, 2.0, 0.7, BarcodeSymbology.Code128, serialNo.ToString())
            {
                BarHeight = 0.50,
                //BarWidth = 0.0104,
                //Sizing = BarcodeSizing.FitProportional,
                //BarcodeAlignment = BarcodeAlignment.MiddleCenter,
                QuietZone = new FrameThickness(0)          
            };

            var bcItemChassisItem = new BarcodeItem(2.30, 0.22, 2.0, 0.7, BarcodeSymbology.Code128, chassisNo.ToString())
            {
                BarHeight = 0.50,
                //BarWidth = 0.0104,
                //Sizing = BarcodeSizing.FitProportional,
                //BarcodeAlignment = BarcodeAlignment.MiddleCenter,
                QuietZone = new FrameThickness(0)          
            };

            this.Items.Add(serialNoLabel);
            this.Items.Add(chassisNoLabel);
            this.Items.Add(bcItemSerialNo);
            this.Items.Add(bcItemChassisItem);
        }
    }
}