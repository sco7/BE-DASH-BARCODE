using Neodynamic.SDK.Printing;
using System;
using System.Collections.Generic;

namespace FontaineVerificationProject.Labels
{
    public class V2Label : ThermalLabel
    {
        public V2Label(int stockCode, int chassisNo) : base(UnitType.Inch, 4.00, 1.50)
        {                  
            var stockCodeLabel = new TextItem(0.62, 1.22, 2.5, 0.5, " Stock Code");
            stockCodeLabel.Font.Name = Font.NativePrinterFontB;
            stockCodeLabel.Font.Size = 10;

            var chassisNoLabel = new TextItem(2.40, 1.22, 2.5, 0.5, "Chassis Number");
            chassisNoLabel.Font.Name = Font.NativePrinterFontB;
            chassisNoLabel.Font.Size = 10;

            var bcItemSerialNo = new BarcodeItem(0.55, 0.25, 2.5, 0.9, BarcodeSymbology.Code128, stockCode.ToString())
            {
                BarHeight = 0.80,
                //BarWidth = 0.0104,
                //Sizing = BarcodeSizing.FitProportional,
                //BarcodeAlignment = BarcodeAlignment.MiddleCenter,
                QuietZone = new FrameThickness(0),
            };
        
            var bcItemChassisItem = new BarcodeItem(2.40, 0.25, 2.5, 0.9, BarcodeSymbology.Code128, chassisNo.ToString())
            {
                BarHeight = 0.80,
                //BarWidth = 0.0104,
                //Sizing = BarcodeSizing.FitProportional,
                //BarcodeAlignment = BarcodeAlignment.MiddleCenter,
                QuietZone = new FrameThickness(0)              
            };

            bcItemSerialNo.Font.Size = 6;
            bcItemChassisItem.Font.Size = 6;

            this.Items.Add(stockCodeLabel);
            this.Items.Add(chassisNoLabel);
            this.Items.Add(bcItemSerialNo);
            this.Items.Add(bcItemChassisItem);
        }
    }
}