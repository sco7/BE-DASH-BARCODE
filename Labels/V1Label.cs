using Neodynamic.SDK.Printing;
using System;
using System.Collections.Generic;

namespace FontaineVerificationProject.Labels
{
    public class V1Label : ThermalLabel
    {
        public V1Label(int customerProductNo, string description, int chassisNo) : base(UnitType.Inch, 3.90, 1.34)
        {          
            var customerProductNoValue = new TextItem(0.20, 0.15, 2.5, 0.5, customerProductNo.ToString());
            customerProductNoValue.Font.Name = Font.NativePrinterFontB;
            customerProductNoValue.Font.Size = 14;

            var descriptionValue = new TextItem(0.20, 0.50, 2.5, 0.5, description);
            descriptionValue.Font.Name = Font.NativePrinterFontB;
            descriptionValue.Font.Size = 10;

            var chassisNoValue = new TextItem(0.20, 0.80, 2.5, 0.5, chassisNo.ToString());
            chassisNoValue.Font.Name = Font.NativePrinterFontB;
            chassisNoValue.Font.Size = 14;

            var bcItem = new BarcodeItem(1.5, 0.25, 2.0, 0.7, BarcodeSymbology.Code128, chassisNo.ToString())
            {
                BarHeight = 0.70,
                //BarWidth = 0.0104,
                //Sizing = BarcodeSizing.FitProportional,
                BarcodeAlignment = BarcodeAlignment.MiddleCenter,
                QuietZone = new FrameThickness(0),
                DisplayCode = false
            };
      
            this.Items.Add(customerProductNoValue);
            this.Items.Add(descriptionValue);
            this.Items.Add(chassisNoValue);               
            this.Items.Add(bcItem);
        }
    }
}