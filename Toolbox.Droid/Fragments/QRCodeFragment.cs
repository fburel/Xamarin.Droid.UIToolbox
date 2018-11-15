using System;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using ZXing;
using ZXing.Mobile;

namespace Toolbox.Droid.Fragments
{
    public abstract class QRCodeFragment : BaseFragment, IZXingScanner<View>, IScannerView
    {
        private FrameLayout frame;

        private Action<Result> scanCallback;

        private ZXingSurfaceView scanner;
        private ZxingOverlayView zxingOverlay;

        public QRCodeFragment()
        {
            UseCustomOverlayView = false;
        }

        public MobileBarcodeScanningOptions ScanningOptions { get; set; }

        public View CustomOverlayView { get; set; }
        public bool UseCustomOverlayView { get; set; }
        public string TopText { get; set; }
        public string BottomText { get; set; }

        public void Torch(bool on)
        {
            scanner?.Torch(on);
        }

        public void AutoFocus()
        {
            scanner?.AutoFocus();
        }

        public void AutoFocus(int x, int y)
        {
            scanner?.AutoFocus(x, y);
        }
        //bool scanImmediately = false;


        public void StartScanning(Action<Result> scanResultHandler, MobileBarcodeScanningOptions options = null)
        {
            ScanningOptions = options;
            scanCallback = scanResultHandler;

            if (scanner == null) return;

            scan();
        }

        public void StopScanning()
        {
            scanner?.StopScanning();
        }

        public void PauseAnalysis()
        {
            scanner?.PauseAnalysis();
        }

        public void ResumeAnalysis()
        {
            scanner?.ResumeAnalysis();
        }

        public void ToggleTorch()
        {
            scanner?.ToggleTorch();
        }

        public bool IsTorchOn => scanner?.IsTorchOn ?? false;

        public bool IsAnalyzing => scanner?.IsAnalyzing ?? false;

        public bool HasTorch => scanner?.HasTorch ?? false;

        public override View OnCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle)
        {
            frame = new FrameLayout(Activity)
            {
                LayoutParameters = new FrameLayout.LayoutParams(ViewGroup.LayoutParams.FillParent,
                    ViewGroup.LayoutParams.FillParent)
            };

            var layoutParams = getChildLayoutParams();

            try
            {
                scanner = new ZXingSurfaceView(Activity, ScanningOptions);

                frame.AddView(scanner, layoutParams);


                if (!UseCustomOverlayView)
                {
                    zxingOverlay = new ZxingOverlayView(Activity);
                    zxingOverlay.TopText = TopText ?? "";
                    zxingOverlay.BottomText = BottomText ?? "";

                    frame.AddView(zxingOverlay, layoutParams);
                }
                else if (CustomOverlayView != null)
                {
                    frame.AddView(CustomOverlayView, layoutParams);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create Surface View Failed: " + ex);
            }

            Log.Debug(MobileBarcodeScanner.TAG, "ZXingScannerFragment->OnResume exit");

            return frame;
        }

        public override void OnStart()
        {
            base.OnStart();
            // won't be 0 if OnCreateView has been called before.
            if (frame.ChildCount == 0)
            {
                var layoutParams = getChildLayoutParams();
                // reattach scanner and overlay views.
                frame.AddView(scanner, layoutParams);

                if (!UseCustomOverlayView)
                    frame.AddView(zxingOverlay, layoutParams);
                else if (CustomOverlayView != null)
                    frame.AddView(CustomOverlayView, layoutParams);
            }
        }

        public override void OnResume()
        {
            base.OnResume();
            if (scanCallback == null) StartScanning(OnScanResult, ScanningOptions);
        }

        protected abstract void OnScanResult(Result result);

        public override void OnStop()
        {
            if (scanner != null)
            {
                scanner.StopScanning();

                frame.RemoveView(scanner);
            }

            if (!UseCustomOverlayView)
                frame.RemoveView(zxingOverlay);
            else if (CustomOverlayView != null)
                frame.RemoveView(CustomOverlayView);

            base.OnStop();
        }

        private LinearLayout.LayoutParams getChildLayoutParams()
        {
            var layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.MatchParent);
            layoutParams.Weight = 1;
            return layoutParams;
        }

        private void scan()
        {
            scanner?.StartScanning(scanCallback, ScanningOptions);
        }
    }
}