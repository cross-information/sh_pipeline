package com.example.zuoquan.pipemap;

import android.annotation.SuppressLint;
import android.content.DialogInterface;
import android.os.Build;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.content.Context;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.webkit.JsResult;
import android.webkit.ValueCallback;
import android.webkit.WebChromeClient;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.Button;
import android.widget.Toast;

public class MainActivity extends AppCompatActivity implements
        View.OnClickListener {

    WebView browser;
    private static String PROVIDER = LocationManager.GPS_PROVIDER;
    private LocationManager locationManager = null;

    @SuppressLint("SetJavaScriptEnabled")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        browser=(WebView)findViewById(R.id.webkit);
        // browser.setWebViewClient(new Callback());
        final MainActivity _this = this;
        browser.setWebChromeClient(new WebChromeClient() {
            public boolean onJsAlert(WebView view, String url, String message, JsResult result) {
                Log.d("PipeMap", message);
                Toast.makeText(MainActivity.this, message, Toast.LENGTH_SHORT).show();
                result.confirm();
                return true;
            }
        });

        locationManager = (LocationManager)getSystemService(Context.LOCATION_SERVICE);

        browser.getSettings().setJavaScriptEnabled(true);
        browser.loadUrl("http://116.62.209.93/shpipe/PipeMap/PipeMapMobile.html");

        // button1 = findViewById(R.id.ZoomInButton);
        // button1.setOnClickListener(this);
    }

    @Override
    public void onClick(View v) {
        String cmd = "map.zoomOut()";
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.KITKAT) {
            browser.evaluateJavascript(cmd, null);
        }
        else {
            browser.loadUrl("javascript:" + cmd);
        }
    }

    public void onZoomIn(View v) {
        String cmd = "map.zoomIn()";
        browser.evaluateJavascript(cmd, null);
    }

    public void onZoomOut(View v) {
        String cmd = "map.zoomOut()";
        browser.evaluateJavascript(cmd, null);
    }

    public void onGetZoom(View v) {
        String cmd = "map.getZoom()";
        final MainActivity _this = this;
        browser.evaluateJavascript(cmd, new ValueCallback<String>() {
            @Override
            public void onReceiveValue(String s) {
                new AlertDialog.Builder(_this)
                    .setMessage(s)
                    .setPositiveButton("确定",
                            new DialogInterface.OnClickListener() {
                                public void onClick(DialogInterface dialoginterface, int i) {
                                    //按钮事件
                                }
                            })
                    .show();
            }
        });
    }

    public void onSetZoom(View v) {
        String cmd = "map.setZoom(8)";
        browser.evaluateJavascript(cmd, null);
    }

    public void onSetCenter(View v) {
        String cmd = "map.setCenter([-5825.16, 8293.31])";
        browser.evaluateJavascript(cmd, null);
    }

    public void onGetExtent(View v) {
        String cmd = "map.getExtent()";
        final MainActivity _this = this;
        browser.evaluateJavascript(cmd, new ValueCallback<String>() {
            @Override
            public void onReceiveValue(String s) {
                new AlertDialog.Builder(_this)
                    .setMessage(s)
                    .setPositiveButton("确定",
                            new DialogInterface.OnClickListener() {
                                public void onClick(DialogInterface dialoginterface, int i) {
                                    //按钮事件
                                }
                            })
                    .show();
            }
        });
    }

    public void onGetCenterLonLat(View v) {
        String cmd = "map.getCenterLonLat()";
        final MainActivity _this = this;
        browser.evaluateJavascript(cmd, new ValueCallback<String>() {
            @Override
            public void onReceiveValue(String s) {
                new AlertDialog.Builder(_this)
                        .setMessage(s)
                        .setPositiveButton("确定",
                                new DialogInterface.OnClickListener() {
                                    public void onClick(DialogInterface dialoginterface, int i) {
                                        //按钮事件
                                    }
                                })
                        .show();
            }
        });
    }

    public void onUndo(View v) {
        String cmd = "map.undo()";
        browser.evaluateJavascript(cmd, null);
    }

    public void onRedo(View v) {
        String cmd = "map.redo()";
        browser.evaluateJavascript(cmd, null);
    }

    public void onEditWell(View v) {
        String cmd = "map.editWell(200)";
        browser.evaluateJavascript(cmd, null);
    }

    public void onGetWellUpdates(View v) {
        String cmd = "map.getWellUpdates()";
        final MainActivity _this = this;
        browser.evaluateJavascript(cmd, new ValueCallback<String>() {
            @Override
            public void onReceiveValue(String s) {
                new AlertDialog.Builder(_this)
                        .setMessage(s)
                        .setPositiveButton("确定",
                                new DialogInterface.OnClickListener() {
                                    public void onClick(DialogInterface dialoginterface, int i) {
                                        //按钮事件
                                    }
                                })
                        .show();
            }
        });
    }

    LocationListener onLocation = new LocationListener() {
        public void onLocationChanged(Location location) {
        }
        public void onProviderDisabled(String provider) {
        }
        public void onProviderEnabled(String provider) {
        }
        public void onStatusChanged(String provider, int status, Bundle extras) {
        }
    };

    @Override
    public void onResume() {
        super.onResume();
        try {
            locationManager.requestLocationUpdates(PROVIDER, 10000,
                    100.0f, onLocation);
        }
        catch (SecurityException ex) {
        }
    }

    @Override
    public void onPause() {
        super.onPause();
        locationManager.removeUpdates(onLocation);
    }

    public void onGPS(View w) {
        try {
            Location loc = locationManager.getLastKnownLocation(PROVIDER);
            if (loc != null) {
                String cmd = String.format("map.setCenterLonLat([%f, %f])", loc.getLongitude(), loc.getLatitude());
                Log.d("PipeMap", cmd);
                browser.evaluateJavascript(cmd, null);
            }
        }
        catch (SecurityException ex) {
            ex = ex;
        }
    }

    public void onSetPositionModeOn(View w) {
        String cmd = "map.setPositionMode(true)";
        browser.evaluateJavascript(cmd, null);
    }

    public void onSetPositionModeOff(View w) {
        String cmd = "map.setPositionMode(false)";
        browser.evaluateJavascript(cmd, null);
    }

    private class Callback extends WebViewClient {
        @Override
        public boolean shouldOverrideUrlLoading(WebView view, String url) {
            Log.d("PipeMap", url);
            return(true);
        }
    }
}
