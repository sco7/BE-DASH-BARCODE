using Neodynamic.SDK.Printing;
using System;
using System.Collections.Generic;

namespace FontaineVerificationProject.Labels
{
    public class V2Label : ThermalLabel  // Exact label details to be confirmed, will use the Odette label barcode to check against chassis no (see photo)
    {
        public V2Label(int customerProductNo, string description, int chassisNo) : base(UnitType.Inch, 3, 2)
        {          
            var customerProductNoValue = new TextItem(0.35, 0.7, 2.1, 0.5, customerProductNo.ToString());
            customerProductNoValue.Font.Name = Font.NativePrinterFontA;
            customerProductNoValue.Font.Size = 12;

            var descriptionValue = new TextItem(0.35, 1.3, 3.0, 0.5, description);
            descriptionValue.Font.Name = Font.NativePrinterFontA;
            descriptionValue.Font.Size = 10;

            var chassisNoValue = new TextItem(0.35, 2.1, 3.0, 0.5, chassisNo.ToString());
            chassisNoValue.Font.Name = Font.NativePrinterFontA;
            chassisNoValue.Font.Size = 10;

            var bcItem = new BarcodeItem(0.9, 2.5, 2, 1.5, BarcodeSymbology.Code128, chassisNo.ToString())
            {
                BarHeight = 0.70,
                BarWidth = 0.0104,
                Sizing = BarcodeSizing.FitProportional,
                BarcodeAlignment = BarcodeAlignment.MiddleCenter,
                QuietZone = new FrameThickness(0)
            };
      
            this.Items.Add(customerProductNoValue);
            this.Items.Add(descriptionValue);
            this.Items.Add(chassisNoValue);               
            this.Items.Add(bcItem);
        }
    }
}