using Neodynamic.SDK.Printing;
using System;

namespace FontaineVerificationProject.Labels
{
    public class V1Label : ThermalLabel
    {
        public V1Label(string customerProductNo, string description, string chassisNo, DateTime dispatchDate) : base(UnitType.Inch, 4.00, 1.50)
        {          
            var customerProductNoValue = new TextItem(0.25, 0.20, 2.5, 0.5, customerProductNo.ToString());
            customerProductNoValue.Font.Name = Font.NativePrinterFontB;
            customerProductNoValue.Font.Size = 17;

            var descriptionValue = new TextItem(0.25, 0.65, 2.4, 0.5, description);
            descriptionValue.Font.Name = Font.NativePrinterFontB;
            descriptionValue.Font.Size = 10;

            var chassisNoValue = new TextItem(0.25, 1.10, 2.5, 0.5, chassisNo.ToString());
            chassisNoValue.Font.Name = Font.NativePrinterFontB;
            chassisNoValue.Font.Size = 17;

            var dispatchDateValue = new TextItem(2.53, 0.15, 2.5, 0.5, dispatchDate.ToString("dd/MM/yyy"));
            dispatchDateValue.Font.Name = Font.NativePrinterFontB;
            dispatchDateValue.Font.Size = 11;

            var bcItem = new BarcodeItem(2.77, 0.50, 2.5, 0.8, BarcodeSymbology.Code128, chassisNo.ToString())
            {
                BarHeight = 0.80,
                //BarWidth = 0.0104,
                //Sizing = BarcodeSizing.FitProportional,
                //BarcodeAlignment = BarcodeAlignment.MiddleCenter,
                QuietZone = new FrameThickness(0),
                DisplayCode = false
            };
      
            this.Items.Add(customerProductNoValue);
            this.Items.Add(descriptionValue);
            this.Items.Add(chassisNoValue);
            this.Items.Add(dispatchDateValue);               
            this.Items.Add(bcItem);
        }
    }
}