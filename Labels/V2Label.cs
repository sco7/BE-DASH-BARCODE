using Neodynamic.SDK.Printing;

namespace FontaineVerificationProject.Labels
{
    public class V2Label : ThermalLabel
    {
        public V2Label(string stockCode, string customerProductNo) : base(UnitType.Inch, 4.00, 1.50)
        {                  
            var stockCodeLabel = new TextItem(0.65, 1.22, 2.5, 0.5, stockCode.ToString());
            stockCodeLabel.Font.Name = Font.NativePrinterFontB;
            stockCodeLabel.Font.Size = 10;

            var customerProductNoLabel = new TextItem(2.60, 1.22, 2.5, 0.5, customerProductNo.ToString());
            customerProductNoLabel.Font.Name = Font.NativePrinterFontB;
            customerProductNoLabel.Font.Size = 10;

            var bcItemStockCode = new BarcodeItem(0.55, 0.25, 2.5, 0.9, BarcodeSymbology.Code128, stockCode.ToString())
            {
                BarHeight = 0.80,
                //BarWidth = 0.0104,
                //Sizing = BarcodeSizing.FitProportional,
                //BarcodeAlignment = BarcodeAlignment.MiddleCenter,
                QuietZone = new FrameThickness(0),
                DisplayCode = false
            };
        
            var bcCustomerProductNo = new BarcodeItem(2.40, 0.25, 2.5, 0.9, BarcodeSymbology.Code128, customerProductNo.ToString())
            {
                BarHeight = 0.80,
                //BarWidth = 0.0104,
                //Sizing = BarcodeSizing.FitProportional,
                //BarcodeAlignment = BarcodeAlignment.MiddleCenter,
                QuietZone = new FrameThickness(0),
                DisplayCode = false
            };

            //bcItemStockCode.Font.Size = 7;
            //bcCustomerProductNo.Font.Size = 7;

            this.Items.Add(stockCodeLabel);
            this.Items.Add(customerProductNoLabel);
            this.Items.Add(bcItemStockCode);
            this.Items.Add(bcCustomerProductNo);
        }
    }
}