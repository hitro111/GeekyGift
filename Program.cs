using System;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using GeekyGift.Resources;
using NBitcoin;
using QRCoder;

namespace GeekyGift
{
    class Program
    {
        static readonly Mnemonic Mnemo = new Mnemonic(Wordlist.English, WordCount.TwentyFour);
        static readonly ExtKey HdRoot = Mnemo.DeriveExtKey();
        static readonly KeyPath KeyPath = new KeyPath("m/84'/0'/0'/0/0"); 
        static readonly ExtKey Key = HdRoot.Derive(KeyPath);

        private static string _addressWithLock = string.Empty;
        private static string _redeemScript = string.Empty;

        static readonly Font MainFont = new Font("Arial", 10);
        static readonly Font SmallFont = new Font("Arial", 8);

        private const int WordsForPersonCount = 12; // We use 24 words as memo => 12 words for each

        private const int RightCornerMarginX = 20;
        private const int MarginX = 40;
        private const int MarginY = 40;
        private const int WordsMarginX = 60;
        private const int WordsMarginY = 60;
        private const int IntervalY = 20;
        private const int SmallIntervalY = 14;
        private const int RectangleWidth = 400;
        private const int RectangleHeight = 80;


        private static int _pageCounter = 1;

        async static Task Main(string[] args)
        {
            if (args == null || string.IsNullOrEmpty(args[0]))
            {
                Console.WriteLine("Printer name was not set. Exiting.");
                return;
            }
            var printerName = args[0];
            Console.WriteLine($"Printer: {printerName}");

            Console.WriteLine("Public key:");
            Console.WriteLine(Key.PrivateKey.PubKey);
            Console.WriteLine("Address:");
            Console.WriteLine(Key.PrivateKey.PubKey.GetAddress(ScriptPubKeyType.Legacy, Network.Main));

            Console.WriteLine("Enter address:");
            _addressWithLock = Console.ReadLine();

            Console.WriteLine("Enter redeem script:");
            _redeemScript = Console.ReadLine();

            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = printerName; //e.g "Canon G3010 series";
            pd.PrintPage += PdOnPrintPage;
            pd.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);

            pd.Print();

            Console.WriteLine("Success!. Press any key to exit.");
            Console.ReadKey();
        }

        private static void PdOnPrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.TranslateTransform(e.PageSettings.PaperSize.Width, e.PageSettings.PaperSize.Height);
            e.Graphics.RotateTransform(180);

            e.Graphics.DrawString(_pageCounter.ToString(), MainFont, Brushes.Black,
                e.PageSettings.PaperSize.Width - RightCornerMarginX, MarginY, new StringFormat());

            switch (_pageCounter)
            {
                case 1:
                    PrintFirstPage(e);
                    break;
                case 2:
                    PrintInstructionsPage(e);
                    break;
                case 3:
                    PrintWordsPage(e, Mnemo.Words.Take(WordsForPersonCount).ToArray());
                    break;
                case 4:
                    PrintProtectionPage(e);
                    break;
                case 5:
                    PrintInstructionsPage(e);
                    break;
                case 6:
                    PrintWordsPage(e, Mnemo.Words.Skip(WordsForPersonCount).ToArray());
                    break;
                case 7:
                    PrintProtectionPage(e);
                    break;
            }

            _pageCounter++;
            e.HasMorePages = true;

            if (_pageCounter > 7)
                e.HasMorePages = false;
        }

        //Intro page
        private static void PrintFirstPage(PrintPageEventArgs e)
        {
            var marginY = MarginY;
            var text = string.Format(Texts.MainPageText, _addressWithLock);

            foreach (var line in text.Split(Environment.NewLine))
            {
                e.Graphics.DrawString(line, MainFont, Brushes.Black,
                    MarginX, marginY, new StringFormat());
                marginY += IntervalY;
            }

            QRCodeGenerator _qrCode = new QRCodeGenerator();
            QRCodeData _qrCodeData = _qrCode.CreateQrCode(_addressWithLock, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(_qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(4);

            Point loc = new Point(MarginX, marginY + IntervalY);
            e.Graphics.DrawImage(qrCodeImage, loc);
        }

        //Black rectangle covering print area
        private static void PrintProtectionPage(PrintPageEventArgs e)
        {
            using SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            e.Graphics.FillRectangle(brush, new Rectangle(MarginX, MarginY, RectangleWidth, RectangleHeight));
        }

        //Mnemonic words page
        private static void PrintWordsPage(PrintPageEventArgs e, string[] mnemonicWords)
        {
            var marginY = WordsMarginY;
            var firstLine = string.Join(' ', mnemonicWords.Take(mnemonicWords.Length / 2).ToArray());
            var secondLine = string.Join(' ', mnemonicWords.Skip(mnemonicWords.Length / 2).ToArray());

            e.Graphics.DrawString(firstLine, MainFont, Brushes.Black,
                WordsMarginX, marginY, new StringFormat());
            marginY += IntervalY;

            e.Graphics.DrawString(secondLine, MainFont, Brushes.Black,
                WordsMarginX, marginY, new StringFormat());
            marginY += IntervalY;

            e.Graphics.DrawString($"Redeem script: {_redeemScript}", MainFont, Brushes.Black,
                WordsMarginX, marginY, new StringFormat());
            marginY += IntervalY;

            QRCodeGenerator _qrCode = new QRCodeGenerator();
            QRCodeData _qrCodeData = _qrCode.CreateQrCode(_redeemScript, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(_qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(4);

            Point loc = new Point(WordsMarginX, marginY);
            e.Graphics.DrawImage(qrCodeImage, loc);
        }

        private static void PrintInstructionsPage(PrintPageEventArgs e)
        {
            PrintProtectionPage(e);

            var marginY = MarginY + RectangleHeight + SmallIntervalY;

            foreach (var line in Texts.InstructionsText.Split(Environment.NewLine))
            {
                e.Graphics.DrawString(line, SmallFont, Brushes.Black,
                    MarginX, marginY, new StringFormat());
                marginY += SmallIntervalY;
            }
        }
    }
}
