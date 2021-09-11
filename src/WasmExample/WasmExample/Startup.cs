using System;
using WebAssembly;

namespace WasmExample
{
    public class Startup
    {
        private static void Run()
        {
            using (var document = (JSObject)Runtime.GetGlobalObject("document"))
            using (var body = (JSObject)document.GetObjectProperty("body"))
            using (var button = (JSObject)document.Invoke("createElement", "button"))
            using (var div=(JSObject)document.Invoke("createElement","div"))
            {
                button.SetObjectProperty("innerHTML", "Click me!");
                button.SetObjectProperty(
                    "onclick",
                    new Action<JSObject>(_ =>
                    {
                        using (var window = (JSObject)Runtime.GetGlobalObject())
                        {
                            window.Invoke("alert", "What!!!, You click me!!!");
                            using (var doc=(JSObject)Runtime.GetGlobalObject("document"))
                            using(var divNoice=(JSObject)doc.Invoke("getElementById", "divNotice"))
                            {
                                divNoice.SetObjectProperty("innerHTML", $"Ok, you win at: {DateTime.Now.ToLongTimeString()}");
                            }
                            
                        }
                    }));
                div.SetObjectProperty("id", "divNotice");
                div.SetObjectProperty("innerHTML", "I am watching you...");
                body.Invoke("appendChild", button);
                body.Invoke("appendChild", div);
            }
        }
    }
}
